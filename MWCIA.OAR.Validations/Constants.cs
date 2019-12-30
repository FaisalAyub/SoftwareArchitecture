using System;
using System.Collections.Generic;
using System.Text; 

namespace Core.Validations
{
    public class Constants
    {
        public const string USERNAME = "System";

        public class Severity
        {
            public const string ERROR = "E";
            public const string WARNING = "W";
            
        }

        public class Records
        {
            public const string APPLICATION = "99";
            public const string PAGE="98";
        }

        public class RecordCategory
        {
            public const string PAGE = "P";
            public const string RECORD = "R";
            public const string APPLICATION = "A";
        }
    }
}
