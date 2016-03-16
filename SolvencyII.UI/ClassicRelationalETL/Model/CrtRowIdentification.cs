using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections.Specialized;
using System.Collections;

namespace SolvencyII.Data.CRT.ETL.Model
{
    /// <summary>
    /// Identification of CRT row based on context column
    /// </summary>
    public class CrtRowIdentification
    {
        public string TABLE_NAME;
        public int INSTANCE = -1;

        private OrderedDictionary _contextColumnsValues;
        /// <summary>
        /// Gets the context columns values.
        /// </summary>
        /// <value>
        /// The context columns values.
        /// </value>
        public OrderedDictionary contextColumnsValues
        {
            get
            {
                return _contextColumnsValues;
            }
        }

        private string contColValCode = "";
        private int thisHashCode;
        /// <summary>
        /// The p k_ identifier
        /// </summary>
        public int PK_ID;

        /// <summary>
        /// Initializes a new instance of the <see cref="CrtRowIdentification"/> class.
        /// </summary>
        /// <param name="TABLE_NAME">Name of the tabl e_.</param>
        /// <param name="INSTANCE">The instance.</param>
        /// <param name="contextColumnsValues">The context columns values.</param>
        public CrtRowIdentification (string TABLE_NAME, int INSTANCE, OrderedDictionary contextColumnsValues)
        {
            this.TABLE_NAME = TABLE_NAME;
            this.INSTANCE = INSTANCE;
            this._contextColumnsValues = contextColumnsValues;

            foreach (DictionaryEntry kvp in _contextColumnsValues)            
                contColValCode = contColValCode + kvp.Key + kvp.Value;            

            thisHashCode = this.myHashCode();
        }

        private int myHashCode()
        {
            string code = "";
            if (!string.IsNullOrEmpty(TABLE_NAME))
                code = TABLE_NAME;
            if (INSTANCE != -1)
                code = code + INSTANCE;

            code = code + contColValCode;

            //foreach (var kvp in contextColumnsValues.OrderBy(x=>x.Key))
            //{
            //    code = code + kvp.Key + kvp.Value;
            //}

            return code.GetHashCode();
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            //string code = "";
            //if (!string.IsNullOrWhiteSpace(TABLE_NAME))
            //    code = TABLE_NAME;
            //if (INSTANCE != -1)
            //    code = code + INSTANCE;

            //code = code + contColValCode;

            ////foreach (var kvp in contextColumnsValues.OrderBy(x=>x.Key))
            ////{
            ////    code = code + kvp.Key + kvp.Value;
            ////}

            //return code.GetHashCode();

            return this.thisHashCode;
        }
        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if(obj == this)
                return true;
            if(!(obj is CrtRowIdentification))
                return false;
            CrtRowIdentification objRI = obj as CrtRowIdentification;

            //if (!TABLE_NAME.Equals(objRI.TABLE_NAME))
            //    return false;
            //if (!INSTANCE.Equals(objRI.INSTANCE))
            //    return false;
            //if (this.contextColumnsValues.Count != objRI.contextColumnsValues.Count)
            //    return false;

            //foreach (KeyValuePair<string, string> kvp in contextColumnsValues)
            //{
            //    if (!objRI.contextColumnsValues.ContainsKey(kvp.Key)
            //        || objRI.contextColumnsValues[kvp.Key] != kvp.Value)
            //        return false;
            //}

            //return thisHashCode.GetHashCode().Equals(objRI.GetHashCode());
            return false;
        }
    }
}
