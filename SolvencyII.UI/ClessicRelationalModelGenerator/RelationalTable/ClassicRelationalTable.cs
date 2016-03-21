using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace T4U.CRT.Generation.Model
{
    public class ClassicRelationalTable
    {
        public ClassicRelationalTable(long TableVerionID)
        {
            this.TableVersionId = TableVerionID;
        }

        public long TableVersionId;
        public HashSet<IRelationalColumn> Columns { get; set; }

        private string _name;
        public string Name {
            get {return _name.Replace('.', '_').Replace(' ','_').Trim();}
            set {_name = value; }}

        public override string ToString()
        {
            return Name;
        }

        public IEnumerable<IRelationalColumn> getUniqueColumns()
        {
            var res = Columns.Where(x => x != null && x.isInTable(this) && (x.isKeyColumn || x.isPageColumn));
            return res;
        }

        int pageColNumber = -1;
        public int pageColumnsNumber
        {
            get
            {
                if(pageColNumber == -1)                
                    pageColNumber = Columns.Where(x => x is PageColumn).Select(x=>x.getColumnName()).Distinct().Count();
                
                return pageColNumber;
            }
        }
    }
}
