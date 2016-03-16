using System;

namespace SolvencyII.Data.Shared.Entities
{
    public class FactInformation
    {
        public int CellID { get; set; }
        public int DataPointVID { get; set; }
        public int DataPointID { get; set; }
        public string TableCellSignature { get; set; }
        public bool IsShaded { get; set; }
        public string DataPointKey { get; set; }
        public string Unit { get; set; }
        public double? NumericValue { get; set; }
        public DateTime? DateTimeValue { get; set; }
        public bool BoolValue { get; set; }
        public string TextValue { get; set; }
        public long InstanceID { get; set; }
        public int XordinateID { get; set; }
        public int YordinateID { get; set; }
    }
}