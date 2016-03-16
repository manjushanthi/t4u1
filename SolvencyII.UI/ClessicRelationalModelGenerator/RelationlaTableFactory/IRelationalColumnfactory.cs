using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using DpmDB;
using ClassicRelationalModelGenerator.RelationalTable;

namespace ClassicRelationalModelGenerator.RelationlaTableFactory
{
    public interface IRelationalColumnFactory
    {
        IRelationalColumn getColumn();
    }
}
