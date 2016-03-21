using System;
using System.Collections.Generic;
using System.Linq;
using SolvencyII.Data.Repository;
using SolvencyII.Data.SQLite;
using SolvencyII.Domain.Configuration;

namespace SolvencyII.Data.Shared.Repository
{
    /// <summary>
    /// Base repository that is SQLite specific. It is utilised by GenericRepository.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseRepository<TEntity> : IGenericRepository<TEntity>, IDisposable
        where TEntity : class, new()
    {
        #region Constructors and Private Vars

        private SQLiteConnection _conn;
        private bool _doNotCloseConnection;

        public BaseRepository() : this(StaticSettings.ConnectionString) {}

        public BaseRepository(string connectionString)
        {
            _conn = RepositorySingletonConnection.Instance(connectionString);
            //_conn = new SQLiteConnection(connectionString);
        }

        public BaseRepository(SQLiteConnection connection)
        {
            _conn = connection;
            _doNotCloseConnection = true;
        }

        #endregion

        #region CRUD


        public bool Create(TEntity entity)
        {
            if (entity == null) throw new ArgumentException("Cannot add a null entity");
            try
            {
                _conn.Insert(entity);
                return true;
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
            return false;
        }

        public TEntity Read(Func<TEntity, bool> predicate)
        {
            return _conn.Table<TEntity>().Where(predicate).FirstOrDefault();
        }

        public TEntity Read(long uniqueId)
        {
            return _conn.Get<TEntity>(uniqueId);
        }

        public bool Update(TEntity entity)
        {
            try
            {
                _conn.Update(entity);
                // _conn.InsertOrReplace(entity);
                return true;
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
                return false;
            }
        }

        public bool Delete(Func<TEntity, bool> predicate)
        {
            try
            {
                foreach (TEntity entity in Select().Where(predicate))
                {
                    _conn.Delete(entity);
                }
                return true;
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
                return false;
            }

            //IEnumerable<TEntity> result = Where(predicate);
            //var enumerable = result as IList<TEntity> ?? result.ToList();
            //if (enumerable.Count() == 1)
            //{
            //    _conn.Delete(enumerable[0]);
            //    return true;
            //}
            //return false;
        }

        public bool Delete(long uniqueId)
        {
            try
            {
                _conn.Delete<TEntity>(uniqueId);
                return true;
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
                return false;
            }
        }

        


        #endregion

        #region Transactions

        public void BeginTransaction()
        {
            _conn.BeginTransaction();
        }

        public void Commit()
        {
            _conn.Commit();
        }

        public void RollBack()
        {
            _conn.Rollback();
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            private set {
                _errorMessage = value;
                if (_errorMessage == "Constraint")
                    _errorMessage = "Key column values must be unique. Please correct.";
                    //_errorMessage = LanguageLabels.GetLabel(112, "Key column values must be unique. Please correct.");
            }
        }

        #endregion


        public IQueryable<TEntity> Select()
        {
            return _conn.Table<TEntity>().AsQueryable();
        }

        public IEnumerable<TEntity> Where(Func<TEntity, bool> predicate)
        {
            return Select().Where(predicate);
        }

        public IEnumerable<TEntity> Select(string sqlQuery)
        {
            return _conn.Query<TEntity>(sqlQuery);
        }

        #region IDisposable implementation

        private bool disposedValue;
        private string _errorMessage;

        public void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    if (!_doNotCloseConnection)
                    {
                        _conn.Dispose();
                        RepositorySingletonConnection.Dispose();
                    }
                    // dispose managed state here if required            
                } // dispose unmanaged objects and set large fields to null        
            }
            this.disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
