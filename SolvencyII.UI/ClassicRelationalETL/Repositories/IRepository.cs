using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.Data.CRT.ETL.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> All {get;}
        void Add(IEnumerable<T> objectsToAdd);
    }
}
