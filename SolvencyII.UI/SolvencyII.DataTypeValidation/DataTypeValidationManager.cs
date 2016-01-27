using SolvencyII.Data.Shared;
using SolvencyII.Data.SQLite;
using SolvencyII.DataTypeValidation.DialogUI;
using SolvencyII.Domain;
using SolvencyII.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolvencyII.DataTypeValidation
{
    

    public class DataTypeValidationManager
    {
       
        private ProgressReporter progressReporter = new ProgressReporter();
        BackgroundWorker validateDBThread { get; set; }
        ISolvencyData dpmConn = null;
        GetSQLData sqlData = null;
        DataValidationProgress statusdlg = null;
        

        public event UserControlClickHandler UpdateResult;
        //public delegate void UserControlClickHandler(object sender, EventArgs e);
        public delegate void UserControlClickHandler(DataTypeValidationInput result,bool isFault);


        public void ValidateDB(DataTypeValidationInput dataTypeValidationInput)
        {
            //Initialize the validate active container progress dialog & BackgroundWorker processor 
            using (statusdlg = new DataValidationProgress())
            {                

                validateDBThread = new BackgroundWorker();
                validateDBThread.WorkerSupportsCancellation = true;
                validateDBThread.WorkerReportsProgress = true;
                validateDBThread.DoWork += new DoWorkEventHandler(ValidateActiveContainer_DoWork);
                validateDBThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ValidateActiveContainer_Completed);               
                statusdlg.Canceled += new EventHandler<EventArgs>(buttonCancel_Click);
                showprogress("Calculating.........", 0);
                validateDBThread.RunWorkerAsync(dataTypeValidationInput);
                
                statusdlg.ShowDialog();
               
            } 
        }


        public void ValidateActiveContainer_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            DataTypeValidationInput dataTypeValidationInput = null;
            bool isFaultOperation=false;
            if (e.Cancelled == true || e.Error != null)
            {
                if (dpmConn != null)
                    dpmConn.Close();

                if (sqlData != null)
                    sqlData.Dispose(); 

                isFaultOperation=true;
            }

            if (this.UpdateResult != null)
            {            

              if (!isFaultOperation)
              {
                  if (e.Result is DataTypeValidationInput)
                  {
                      dataTypeValidationInput = (DataTypeValidationInput)e.Result;
                  }
              }else
              {
                  dataTypeValidationInput = null;
              }

                this.UpdateResult(dataTypeValidationInput, isFaultOperation);
            }

            statusdlg.Close();
            validateDBThread.Dispose();


        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (validateDBThread.WorkerSupportsCancellation == true)                          
                validateDBThread.CancelAsync(); 
            
        }

        void ValidateActiveContainer_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
          

            DataTypeValidationInput dataTypeValidationInput = null;
            if (e.Argument is DataTypeValidationInput)             
                dataTypeValidationInput = (DataTypeValidationInput)e.Argument;

            //Initialize the Validation result set
            if(dataTypeValidationInput!=null)
            dataTypeValidationInput.resultSet = new List<DataTypeValidationResult>();

            //Initialize the Connection String & InstanceID 
            string connectionString = dataTypeValidationInput.ConnectionString;
            long instanceID = dataTypeValidationInput.InstanceID;
                       

            try
            {
               
                //Open the database connection
                dpmConn = new SQLiteConnection(connectionString);

                dInstance instance;

                //Get the instance from dpm
                sqlData = new GetSQLData(connectionString);
                instance = sqlData.GetInstanceDetails(instanceID);

                mModule module = sqlData.GetModuleDetails(instance.ModuleID);

                if (module == null)
                    throw new DataTypeValidationException("An error occured. The process could not found the active module", null);

                //Get all the details respective to the selected instance from the view
                string query = string.Format("SELECT distinct i.InstanceID, td.* FROM vwGetTreeData td left outer join dInstance i on (i.ModuleID = td.ModuleID) where i.instanceid = {0}  ORDER BY BusinessOrder, TemplateOrder, TemplateOrder2 ", instanceID);
                //string query = string.Format("select count(*) from {0} where instance = {1} ", tableName, instance.InstanceID);

                List<TreeViewData> response = dpmConn.Query<TreeViewData>(query);

                string[] tableCodes = (from t in response
                                       where t.ModuleID == instance.ModuleID
                                       select t.TableCode).ToArray<string>();

                IList<string> emptyTables = new List<string>();
                IList<string> nonEmptyTable = new List<string>();

                foreach (string s in tableCodes)
                {
                    //Get the table information from database
                    int count = DataTypeValidationSqlHelper.GetTotalTableRows(dpmConn, instance, s);

                    //find the empty & non empty tables 
                    if (count > 0)
                        nonEmptyTable.Add(s);
                    else
                        emptyTables.Add(s);

                }

                //Initialize all the connection managers by Initialize DataTypeTableValidation
                DataTypeTableValidation dataTypeTableValidation = new DataTypeTableValidation(connectionString, dpmConn);
                int totalNonEmptyTable = nonEmptyTable.Count;

                //Validate all the non empty tables 
                for (int i = 0; i < totalNonEmptyTable; i++)
                {
                    //If the cancel button clicked, then check for the background worker is in CancellationPending status
                    if (worker.CancellationPending == true)
                    {
                        //showprogressCancel("Canceling.........");
                        e.Cancel = true;
                        if (dpmConn != null)
                        {
                            dpmConn.Close();
                            dpmConn.Dispose();
                            dpmConn = null;
                        }

                        if (sqlData != null)
                        {

                            sqlData.Dispose();
                            sqlData = null;
                        }                    

                        break;
                    }
                     if (worker.CancellationPending )
                        return;

                    string tableCode = nonEmptyTable[i];

                    //Validate the non empty table
                    List<DataTypeValidationResult> dataTypeValidationResults = dataTypeTableValidation.ValidateTable(instance, tableCode);

                    if (worker.CancellationPending)
                    {                       
                        e.Cancel = true;
                        return;
                    }                     

                    //Report the validation progress
                    int percentage = Convert.ToInt32(Math.Round((i + 1) * (100.0 / totalNonEmptyTable), 0));
                    progressReporter.ReportProgress(() =>
                    {
                        string processMessage = "Datatype Validation-" + tableCode;                       
                        showprogress(processMessage, percentage);
                    }
                    );

                    //Add the current table's validated result in the list
                    if (dataTypeValidationResults.Count > 0)
                    {
                        foreach (DataTypeValidationResult dataTypeValidationResult in dataTypeValidationResults)
                        {
                            dataTypeValidationInput.resultSet.Add(dataTypeValidationResult);
                        }
                    }

                }

            }
            catch (DataTypeValidationException te)
            {
                MessageBox.Show(te.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //logger
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

                if (dpmConn != null)
                    dpmConn.Close();

                if (sqlData != null)
                    sqlData.Dispose();

                e.Result = dataTypeValidationInput;

            }           
            
        }

        public void showprogress(string step, int percentage)
        {
            statusdlg.ProcessingStep = step;
            statusdlg.ProgressValue = percentage;
            statusdlg.Percent = percentage.ToString()+"%";

        }
       
      
    }
}
