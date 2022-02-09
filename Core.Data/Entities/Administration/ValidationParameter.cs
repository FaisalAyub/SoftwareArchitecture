using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Data.Entities.Administration
{
   public class ValidationParameter
    {
        [ForeignKey("Validation")]
        /// <summary>
        /// Validation ID
        /// </summary>
        [Key]
        public decimal ValidationID { get; set; }

        /// <summary>
        /// Ordinal position of the parameter
        /// </summary>
        public byte SequenceNo { get; set; }

        /// <summary>
        /// XPath to get the value
        /// </summary>
        public string XPath { get; set; }

        /// <summary>
        /// Name of the paramter as passed to the Function/Procedure
        /// </summary>
        public string Name { get; set; }
    }
}
