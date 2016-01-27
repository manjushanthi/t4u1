using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using T4U.CRT.Generation.Model;

namespace T4U.CRT.Generation.RelationlaTableFactory
{
    interface IRcColumnFactory : IRelationalColumnFactory
    {
        new RcColumn getColumn();
    }
}
