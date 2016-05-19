using System.Text;

namespace ucGenerator.Classes
{
    /// <summary>
    /// Used within this solution to carry information needed to generate a single control
    /// </summary>
    public class ControlParameter
    {
        public StringBuilder SbInstantiate;
        public StringBuilder SbProperties;
        public StringBuilder SbThisControl;
        public StringBuilder SbDeclare;
        public StringBuilders stringBuilder;
        public int ControlCount;
        public int LocationX;
        public int LocationY;
        public int Dim1;
        public int Dim2;
        public bool GreyBox;
        public int CtrlWidth;
        public int CtrlHeight;
        public string ColName;
        public long AxisID;
        public string TableNames;
        public string DimensionText;
        public int OrdinateID;
        public int HierarchyID;
        public string ControlName; // Used only when specifically needed.
        public long StartOrder;
        public long NextOrder;
        public bool IsRowKey;
        public bool IsTyped;

        public ControlParameter(StringBuilder instantiate, StringBuilder properties, StringBuilder thisControl, StringBuilder declare)
        {
            SbInstantiate = instantiate;
            SbProperties = properties;
            SbThisControl = thisControl;
            SbDeclare = declare;
        }


        public ControlParameter(StringBuilders stringBuilders)
        {
            stringBuilder = stringBuilders;
            SbInstantiate = stringBuilder.sbInstantiate;
            SbProperties = stringBuilder.sbProperties;
            SbThisControl = stringBuilder.sbThisControl;
            SbDeclare = stringBuilder.sbDeclare;
        }



    }
}
