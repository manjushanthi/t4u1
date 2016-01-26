﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using DpmDB;
using T4U.CRT.Generation.Model;

namespace T4U.CRT.Generation.RelationlaTableFactory
{
    public interface IRelationalColumnFactory
    {
        IRelationalColumn getColumn();
    }
}