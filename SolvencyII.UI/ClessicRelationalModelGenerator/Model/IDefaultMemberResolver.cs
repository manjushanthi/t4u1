using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace T4U.CRT.Generation.Model
{
    public interface IDefaultMemberResolver
    {
        bool isDefault(string mappingCode);
    }
}
