using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Validations.Engine
{
    public class EnumModel
    {
        /// <summary>
        /// Validation Type Enum
        /// </summary>
        public enum ValidationType
        {
            Required = 1,
            Range = 2,
            Compare = 3,
            RegularExpression = 4,
            Lookup = 5,
            Date = 6,
            RequiredDate = 7,
            XPathExpression = 10,
            Code = 11,
            Javascript = 12,
            RecordType = 13,
            BlankOrZero = 14,
            BlankOrOne = 15,
            Extension = 16,
            Procedure = 17
        }

    }
}
