using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using DpmDB.BusinessData;


namespace DpmDB
{
    public class SQLiteConnector : ICellShadeChecker
    {
        readonly string connString;
        SQLiteConnection conn;
        SQLiteCommand comm;
        SQLiteTransaction trans;

        public SQLiteConnector(string filePath)
        {
            //connString = new DpmDbHelper().GetDpmDbConnectionString(filePath);
            connString = "Data Source=" + filePath + ";Version=3";
            conn = new SQLiteConnection(connString);
        }

        public void openConnection()
        {
            this.conn.Open();
            comm = new SQLiteCommand(conn);
        }

        public void beginTransaction()
        {
            trans = conn.BeginTransaction();
            comm = new SQLiteCommand(conn);
        }

        public void commitTransaction()
        {
            if(trans != null && trans.Connection != null)
                trans.Commit();
        }

        public void closeConnection()
        {
            this.commitTransaction();
            this.conn.Close();            
        }

        public void executeNonQuery(string nonQuery)
        {          
            bool close = true;
            if (conn.State == ConnectionState.Open)
            {
                close = false;
                comm.CommandText = nonQuery;
            }
            else
            {
                conn.Open();
                comm = new SQLiteCommand(nonQuery, conn);
            }

            comm.ExecuteNonQuery();

            if(close)
                conn.Close();
        }

        public DataTable executeQuery(string query)
        {
            comm = new SQLiteCommand(query, conn);

            bool close = true;
            if (conn.State == ConnectionState.Open)
                close = false;
            else
                conn.Open();

            SQLiteDataReader rdr = comm.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(rdr);

            if (close)
                conn.Close();

            return dt;
        }

       

        private HashSet<Tuple<int, int>> greyedOutOrdinatesPairs = null;
        public HashSet<Tuple<int, int>> GreyedOutOrdinatesPairs
        {
            get
            {
                if (greyedOutOrdinatesPairs == null)
                    getGreyedOutOrdinatesParis();

                return greyedOutOrdinatesPairs;
            }
        }

        /// <summary>
        /// Checks wether exists greyed out cell connected with two given ordinates
        /// </summary>
        /// <param name="xOrdinateId"></param>
        /// <param name="yOrdinateId"></param>
        /// <returns></returns>
        bool ICellShadeChecker.IsGreyedOut(long xOrdinateId, long yOrdinateId)
        {
            if (GreyedOutOrdinatesPairs.Contains(new Tuple<int, int>(Convert.ToInt16(xOrdinateId), Convert.ToInt16(yOrdinateId))))
                return true;

            return false;
        }

        private void getGreyedOutOrdinatesParis()
        {
            string query = @"SELECT tc.CellID, tc.IsShaded, ao2.OrdinateID xOrd, ao1.OrdinateID yOrd
                            FROM mTableCell tc INNER JOIN mCellPosition cp1 on cp1.CellID = tc.CellID
                            inner join mAxisOrdinate ao1 on ao1.OrdinateID = cp1.OrdinateID                     
                            inner join mCellPosition cp2 on cp2.CellID = tc.CellID 
                            inner join mAxisOrdinate ao2 on ao2.OrdinateID = cp2.OrdinateID                            
                            where tc.IsShaded = 1 and ao2.OrdinateID <> ao1.OrdinateID";

            DataTable dt = this.executeQuery(query);
            greyedOutOrdinatesPairs = new HashSet<Tuple<int, int>>();

            foreach (DataRow dr in dt.Rows)
            {
                greyedOutOrdinatesPairs.Add(new Tuple<int, int>(int.Parse(dr["xOrd"].ToString()), int.Parse(dr["yOrd"].ToString())));
            }
        }

        #region ForExcelTemplate Generation

        public object getColumnType(string scalerQuery)
        {



            bool close = true;
            if (conn.State == ConnectionState.Open)
            {
                close = false;
                comm.CommandText = scalerQuery;
            }
            else
            {
                conn.Open();
                comm = new SQLiteCommand(scalerQuery, conn);
            }
            object val = comm.ExecuteScalar();
            if (close)
                conn.Close();

            return val;

           
        }

        public int executeScalar(string scalerQuery)
        {
            bool close = true;
            if (conn.State == ConnectionState.Open)
            {
                close = false;
                comm.CommandText = scalerQuery;
            }
            else
            {
                conn.Open();
                comm = new SQLiteCommand(scalerQuery, conn);
            }

           int val= Convert.ToInt32(comm.ExecuteScalar());
            if (close)
                conn.Close();

            return (val);

           
        }

        #endregion


        public void rollBackTransaction()
        {
            if (trans != null && trans.Connection != null)
                trans.Rollback();

            trans = null;
        }
    }
}
