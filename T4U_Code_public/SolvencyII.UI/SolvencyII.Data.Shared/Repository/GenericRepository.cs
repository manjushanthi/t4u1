using System;
using System.Collections.Generic;
using System.Linq;
using SolvencyII.Data.SQLite;
using SolvencyII.Domain.Configuration;

namespace SolvencyII.Data.Shared.Repository
{
    /// <summary>
    /// Used in closed templates to access data.
    /// Currently only used for SQLite. It will need modification for SQL Server usage.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public sealed class GenericRepository<TEntity> : IGenericRepository<TEntity>, IDisposable
        where TEntity : class, new()
        //where TEntity : class, new()
    {
        #region Constructors and Private Vars

        private IGenericRepository<TEntity> _baseRepository;

        public GenericRepository() : this(StaticSettings.ConnectionString) {}

        public GenericRepository(string connectionString)
        {
            _baseRepository = new BaseRepository<TEntity>(connectionString);
        }

        public GenericRepository(SQLiteConnection connection)
        {
            _baseRepository = new BaseRepository<TEntity>(connection);
        }

        public string ErrorMessage { get { return _baseRepository.ErrorMessage; }}

        #endregion

        #region CRUD


        public bool Create(TEntity entity)
        {
            if (entity == null) throw new ArgumentException("Cannot add a null entity");
            return _baseRepository.Create(entity);
        }

        public TEntity Read(Func<TEntity, bool> predicate)
        {
            return _baseRepository.Read(predicate);
        }

        public TEntity Read(long uniqueId)
        {
            return _baseRepository.Read(uniqueId);
        }

        public bool Update(TEntity entity)
        {
            return _baseRepository.Update(entity);
        }

        public bool Delete(Func<TEntity, bool> predicate)
        {
            return _baseRepository.Delete(predicate);
        }

        public bool Delete(long uniqueId)
        {
            return _baseRepository.Delete(uniqueId);
        }

        #endregion

        public IQueryable<TEntity> Select()
        {
            return _baseRepository.Select();
        }

        public IEnumerable<TEntity> Where(Func<TEntity, bool> predicate)
        {
            return _baseRepository.Where(predicate);
        }

        public IEnumerable<TEntity> Select(string sqlQuery)
        {
            return _baseRepository.Select(sqlQuery);
        }

        #region Transactions

        public void BeginTransaction()
        {
            _baseRepository.BeginTransaction();
        }

        public void Commit()
        {
            _baseRepository.Commit();
        }

        public void RollBack()
        {
            _baseRepository.RollBack();
        }

        #endregion


        #region IDisposable implementation

        private bool disposedValue;

        public void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _baseRepository.Dispose(); 
                }    
            }
            disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
