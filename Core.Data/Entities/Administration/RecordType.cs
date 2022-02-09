using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Data.Entities.Administration
{
   public class RecordType
    {
        [Key]
        public string ID { get; set; }

        /// <summary>
        /// Page/Screen of the Validation e.g. Application,Signup 
        /// </summary>
        public string PageID { get; set; }

        /// <summary>
        /// Section ID - e.g. S1 for Application > Section 1
        /// </summary>
        public string SectionID { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 1=Active, 0 = Inactive
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Relative Path of the Record in XML
        /// </summary>
        public string XPathRoot { get; set; }

        /// <summary>
        /// XPath of the PK Column. 
        /// </summary>
        public string PKColumn { get; set; }

        /// <summary>
        /// Holds a XPath expression to get key columns of the record while displaying the errors. e.g. for Section 4 > Class = /ClassCode 
        /// </summary>
        public string IdentifierExpression { get; set; }

        /// <summary>
        /// Indicates if this record type can be repeated e.g. Addresses.
        /// </summary>
        public bool RepeatableInd { get; set; }

        /// <summary>
        /// Type of Validation i.e. R,P or A
        /// </summary>
        public string Category { get; set; }
    }

}
