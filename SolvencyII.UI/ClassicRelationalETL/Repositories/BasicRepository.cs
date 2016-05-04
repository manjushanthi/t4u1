using SolvencyII.Data.CRT.ETL.DataConnectors;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Data;
using System.Linq;
using System.Text;

namespace SolvencyII.Data.CRT.ETL.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class, new() 
    {
        string getAllQuery;
        IDataConnector connector;
        List<T> all = null;

        public BaseRepository(string getAllQuery, IDataConnector connector)
        {
            this.getAllQuery = getAllQuery;
            this.connector = connector;
        }

        public BaseRepository(IDataConnector connector)
        {            
            this.connector = connector;
        }

        public IEnumerable<T> All
        {
            get
            {
                if (all == null)
                    populateAll();

                return all.AsReadOnly();
            }
        }

        private void populateAll()
        {
            DataTable dt = getAllData();
            Dictionary<string, DataColumn> colNameToTableColumn = readColumns(dt);
            all = new List<T>(mapTs(dt, colNameToTableColumn));
        }

        private IEnumerable<T> mapTs(DataTable dt, Dictionary<string, DataColumn> colNameToTableColumn)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            DataColumn column;
            foreach (DataRow dr in dt.Rows)
            {                
                T newT = new T();
                foreach (PropertyInfo property in properties)
                    column = setPropertyValue(colNameToTableColumn, dr, newT, property);                    

                yield return newT;
            }
        }

        private static DataColumn setPropertyValue(Dictionary<string, DataColumn> colNameToTableColumn, DataRow dr, T newT, PropertyInfo property)
        {
            DataColumn column;
            if (colNameToTableColumn.TryGetValue(property.Name, out column))
                property.SetValue(newT, dr[column], null);
            return column;
        }

        private Dictionary<string, DataColumn> readColumns(DataTable dt)
        {
            Dictionary<string, DataColumn> result = new Dictionary<string, DataColumn>();
            foreach (DataColumn dc in dt.Columns)           
                result.Add(dc.ColumnName, dc);
            return result;
        }

        private DataTable getAllData()
        {
            IDbCommand comm = connector.createCommand();
            comm.CommandText = getAllQuery;
            DataTable dt = connector.executeQuery(comm);
            return dt;
        }

        
        public void Add(IEnumerable<T> objectsToAdd)
        {
            throw new NotImplementedException();
        }
    }
}
