using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace AT2DPM.DAL.Model
{
    public partial class DPMdb : DbContext
    {
        public DPMdb(string connectionString)
            : base(connectionString)
        {
        }
    }
}
