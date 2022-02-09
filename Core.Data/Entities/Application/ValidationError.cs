using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Data.Entities.Application
{
   public class ValidationError
    {
       
            [Column("ID")]
            public int ID { get; set; }
            public string RecordTypeID { get; set; }
            public string FieldID { get; set; }
            public decimal ValidationID { get; set; }
            //public string UserName { get; set; }
           // [ForeignKey("Header")]
          //  public int ApplicationID { get; set; }
            //[JsonIgnore]
           // public Application.Header Header { get; set; }
            //public int? ImportFieldID { get; set; }
            public string CurrentValue { get; set; }
            public int? PKValue { get; set; }
            public string SectionName { get; set; }
            public string Severity { get; set; }
            public string Label { get; set; }
            public string ErrorMessage { get; set; }
            public string GroupID { get; set; }
            public string Identifier { get; set; }
            public string AssociatedTabID { get; set; }
            public bool FocusableInd { get; set; }
            public string HighlightControlID { get; set; }
            public string FocusControlID { get; set; }
            public string ControlType { get; set; }

            public bool RepeatableInd { get; set; }

        }

        public static class ControlType
        {
            public static string Text = "text box";
            public static string RadioButton = "radio btn";
            public static string CheckBox = "check box";
            public static string SelectBox = "Select box";
        } 
}
