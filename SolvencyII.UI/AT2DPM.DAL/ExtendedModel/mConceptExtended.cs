using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AT2DPM.DAL.Model
{
    [MetadataType(typeof(mConceptMetadata))]
    public partial class mConcept
    {
        public class mConceptMetadata
        {
            [Column(TypeName = "Date")]
            public object CreationDate { get; set; }

            [Column(TypeName = "Date")]
            public object ModificationDate { get; set; }

            [Column(TypeName = "Date")]
            public object FromDate { get; set; }

            [Column(TypeName = "Date")]
            public object ToDate { get; set; }
        }
    }
}
