using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.Domain.Interfaces
{
    public interface IControlContainsRepository
    {
        IClosedRowRepository CtrlRepository { get; set; }
    }
}
