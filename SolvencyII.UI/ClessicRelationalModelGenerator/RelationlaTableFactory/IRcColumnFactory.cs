using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ClassicRelationalModelGenerator.RelationalTable;

namespace ClassicRelationalModelGenerator.RelationlaTableFactory
{
    interface IRcColumnFactory : IRelationalColumnFactory
    {
        new RcColumn getColumn();
    }
}
