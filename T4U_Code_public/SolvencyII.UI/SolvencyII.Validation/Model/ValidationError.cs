using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.Validation.Model
{
    public class ValidationError
    {
        public int SNO { get; set; }
        public long InstanceId { get; set; }
        public long PK_ID { get; set; }
        public long TableId { get; set; }
        public string TableCode { get; set; }
        public string Formula { get; set; }
        public string Cells { get; set; }
        public long ValidationId { get; set; }
        public string ValidationCode { get; set; }
        public long ExpressionId { get; set; }
        public string Expression { get; set; }
        public string ErrorMessage { get; set; }

        public string Context { get; set; }
        public Page[] Pages { get; set; }

        public string Scope { get; set; }

        public string SerializedContext 
        { 
            get 
            {
                if (Pages != null)
                {
                    StringBuilder sb = new StringBuilder();

                    //string[] str = Pages.Select(p => p.Dimension.DimensionLabel + ": " + p.Member != null ? p.Member.MemberLabel : p.MemberXBRLCode).ToArray();
                    //string serializedStr = string.Join(", ", str);

                    foreach (Page p in Pages)
                    {
                        if (p.Dimension != null)
                        {
                            sb.Append(p.Dimension.DimensionLabel).Append(": ");

                            if (p.Member != null)
                                sb.Append(p.Member.MemberLabel);
                            else
                                sb.Append(p.MemberText);

                            sb.Append(", ");
                        }
                    }

                    //Trim the excess ',' and space


                    return sb.ToString().TrimEnd(new char[] { ',', ' ' });
                }

                return null;
            } 
        }
    }
}
