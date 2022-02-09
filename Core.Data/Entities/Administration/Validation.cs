using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Data.Entities.Administration
{
   public class Validation
    {
        [Key]
        public decimal ID { get; set; }

        /// <summary>
        /// Spectrum Edit ID
        /// </summary>
        public string SpectrumEditID { get; set; }

        /// <summary>
        /// Represents Section Code. 
        /// </summary>
        public string GroupID { get; set; }


        /// <summary>
        /// Sequence No is used to skip the further validations on the same fields if is it already evaluated as invalid.  
        /// e.g. if an input is not numeric, next validation what verifies the value domain will not run.
        /// </summary>
        public byte SequenceNo { get; set; }


        /// <summary>
        /// R-Record Type e.g. Section 1 > Address, 
        /// A-Application, 
        ///R-Record Level
        ///RT- Record Type but run only when whole transaction is validated
        /// </summary>
        public string Category { get; set; }


        /// <summary>
        /// Record Type of the validation
        /// </summary>
        public string RecordTypeID { get; set; }

        /// <summary>
        /// Internal Name/Code for the Field.
        /// </summary>
        public string FieldID { get; set; }

        /// <summary>
        /// Display Name of the associated field while showing the error message on the screen. 
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Error Message
        /// </summary>
        public string ErrorMessage { get; set; }


        /// <summary>
        /// Evaluates to boolean to check if validation passes pre-conditions
        /// </summary>
        public string Condition { get; set; }


        /// <summary>
        /// 1=Active, 0 = Inactive
        /// </summary>
        public bool Status { get; set; }


        /// <summary>
        /// Type of the validation. Please see Validation Engine documentation for more details. 
        /// </summary>
        public byte Type { get; set; }

        /// <summary>
        /// E = Error, W = Warning
        /// </summary>
        public string Severity { get; set; }

        /// <summary>
        /// XPath of the field to be validated
        /// </summary>
        public string XPath { get; set; }

        /// <summary>
        /// Used if Type = 4
        /// </summary>
        public string RegularExpression { get; set; }

        /// <summary>
        /// Used if Type = 5 
        /// </summary>
        public string LookupID { get; set; }

        /// <summary>
        /// Used if Type = 3 
        /// </summary>
        public string ComparisonFieldXPath { get; set; }

        /// <summary>
        /// Used if Type = 2 
        /// </summary>
        public decimal? RangeFromValue { get; set; }

        /// <summary>
        /// Used if Type = 2 
        /// </summary>
        public decimal? RangeToValue { get; set; }

        /// <summary>
        /// If type = 11,16 or 17 then fully qualified function name of .net code, expression or Procedure
        /// </summary>
        public string FunctionName { get; set; }

        /// <summary>
        /// If type = 11 then Assembly Name of function to exceute.
        /// </summary>
        public string AssemblyName { get; set; }

        /// <summary>
        /// Section Name to show on the screen.
        /// </summary>
        public string SectionName { get; set; }

        /// <summary>
        /// Used if Type = 10
        /// </summary>
        public string XPathExpression { get; set; }

        /// <summary>
        /// Tab on the screen the validation assoicated with. 
        /// </summary>
        public string AssociatedTabID { get; set; }

        /// <summary>
        /// Control to highlight in case of error
        /// </summary>
        public string HighlightControlID { get; set; }

        /// <summary>
        /// Name of the control to set the focus on in case of error
        /// </summary>
        public string FocusControlID { get; set; }

        /// <summary>
        /// Indicates if the validation is for the focusable control. e.g. Textbox
        /// </summary>
        public bool FocusableInd { get; set; }

        /// <summary>
        /// If value is present, it will be considerd as replacement of blank value during the validation. 
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// Type of Control to be validated. It helps to highlight control when validation error occurs. 
        /// </summary>
        public string ControlType { get; set; }


        public List<ValidationParameter> ValidationParameters { get; set; }
    }
}
