using System;
using System.Collections.Generic;
using System.Linq;

namespace SolvencyII.Data.Shared.Repository
{
    /// <summary>
    /// Interface used for repositories
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        bool Create(TEntity entity);
        TEntity Read(Func<TEntity, bool> predicate);
        TEntity Read(long uniqueId);
        Boolean Update(TEntity entity);
        bool Delete(Func<TEntity, bool> predicate);
        bool Delete(long uniqueId);

        IQueryable<TEntity> Select();
        IEnumerable<TEntity> Where(Func<TEntity, bool> predicate);
        IEnumerable<TEntity> Select(string sqlQuery);

        void BeginTransaction();
        void Commit();
        void RollBack();

        string ErrorMessage { get; }

        void Dispose(bool disposing);
        void Dispose();
    }
}
