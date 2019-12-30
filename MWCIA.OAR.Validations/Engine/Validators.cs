using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Web;

using System.Xml.Xsl;
using System.Xml.XPath;
using System.Text.RegularExpressions;
using Models = Core.Data.Entities.Administration;
using Core.Utilities.Extensions;
 
using Wmhelp.XPath2;
using Core.Data.Entities.Administration;
using Microsoft.Data.SqlClient;

namespace Core.Validations.Engine
{
    /// <summary>
    /// Contains the Implementation of Each Validator used for MPS Validation Engine
    /// </summary>
    public class Validators
    {

      //  static BO.OARFunctionStore functionStore = new BO.OARFunctionStore();
        /// <summary>
        /// Triggers the appropriate Validator based on Validation Type
        /// </summary>
        /// <param name="value"></param>
        /// <param name="recordTypeCode"></param>
        /// <param name="validation"></param>
        /// <param name="rootXML"></param>
        /// <param name="xPathNavigator"></param>
        /// <param name="outputValue"></param>
        /// <returns></returns>
        public static bool Validate(string value, string recordTypeCode, Validation validation, System.Xml.XmlElement rootXML, System.Xml.XPath.XPathNavigator xPathNavigator, ref string outputValue)
        {
            outputValue = value;
            switch ((EnumModel.ValidationType)validation.Type)
            {
                case EnumModel.ValidationType.Required:
                    return (!string.IsNullOrWhiteSpace(value));
                case EnumModel.ValidationType.BlankOrZero:
                    return string.IsNullOrWhiteSpace(value) || value == "0";
                case EnumModel.ValidationType.BlankOrOne:
                    return string.IsNullOrWhiteSpace(value) || value == "1";
                case EnumModel.ValidationType.Range:
                    return ValidateRanage(value, validation.RangeFromValue, validation.RangeToValue);
                case EnumModel.ValidationType.Compare:
                    break;
                case EnumModel.ValidationType.RegularExpression:
                    return ValidateRegularExpression(value, validation.RegularExpression);
                case EnumModel.ValidationType.Code:
                    return ValidateCustomCode(value, validation, rootXML, recordTypeCode, ref outputValue);

                case EnumModel.ValidationType.Javascript:
                    break;
                case EnumModel.ValidationType.Lookup:
                    return ValidateLookupValue( value, validation.LookupID);
                case EnumModel.ValidationType.Date:
                    return value.IsDateOrEmpty();
                case EnumModel.ValidationType.RequiredDate:
                    return value.IsValidDate();
                case EnumModel.ValidationType.XPathExpression:
                    return ValidateXPathExpression(rootXML, validation.XPathExpression, xPathNavigator, value);
                case EnumModel.ValidationType.RecordType:
                    return ValidateRecordType(rootXML, validation, xPathNavigator, value);
                case EnumModel.ValidationType.Extension:
                    return ValidateExtension(rootXML, validation, xPathNavigator, validation.XPathExpression, value);
                case EnumModel.ValidationType.Procedure:
                    return ValidateProcedure(rootXML, validation, xPathNavigator, ref outputValue);

            }
            return true;
        }

        /// <summary>
        /// Not Implemented yet
        /// </summary>
        /// <param name="rootXML"></param>
        /// <param name="validation"></param>
        /// <param name="xPathNavigator"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static bool ValidateRecordType(System.Xml.XmlElement rootXML,Validation validation, System.Xml.XPath.XPathNavigator xPathNavigator, string value)
        {

            return true;
        }

        /// <summary>
        /// Validates against type = 16 
        /// </summary>
        /// <param name="rootXML"></param>
        /// <param name="xPathExpression"></param>
        /// <param name="xPathNavigator"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static bool ValidateExtension(System.Xml.XmlElement rootXML, Validation validation, System.Xml.XPath.XPathNavigator nav, string xPathExpression, string value)
        {
            return (bool)XPath.Utilities.EvaluateWithExtension(validation, rootXML, nav, xPathExpression, value);

        }
        /// <summary>
        /// Validates against type = 17 
        /// </summary>
        /// <param name="rootXML"></param>
        /// <param name="xPathExpression"></param>
        /// <param name="xPathNavigator"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static bool ValidateProcedure(System.Xml.XmlElement rootXML, Validation validation, System.Xml.XPath.XPathNavigator nav, ref string value)
        {

            string procedureName = validation.FunctionName;
            if (procedureName.IsEmpty())
                procedureName = string.Format("ValidationEngine.Edit{0}",validation.SpectrumEditID);

            //Create argument list and add the parameters.
            XsltArgumentList varList = new XsltArgumentList();
            var result = new  SqlParameter("@Result", System.Data.SqlDbType.Bit) { Direction = System.Data.ParameterDirection.Output };
             SqlParameter[] args = new  SqlParameter[validation.ValidationParameters.Count + 3];
            var valueParam = new  SqlParameter("@Value", value);
            valueParam.Direction = System.Data.ParameterDirection.InputOutput;
            valueParam.Value = value;
            valueParam.Size = 100;
            args[0] = valueParam;

            int counter = 1;
            args[args.Length - 2] = new  SqlParameter("@ValidationID", validation.ID) { };
            args[args.Length - 1] = result;
            foreach ( ValidationParameter parameter in validation.ValidationParameters)
            {
                object pvalue = null;
                if (parameter.XPath.ToUpper().StartsWith("DEFAULT_"))
                    pvalue = parameter.XPath.Replace("DEFAULT_", string.Empty);
                else
                    pvalue = XPath.Utilities.ExtractParameterValue(parameter, rootXML, string.Empty);

                args[counter] = new  SqlParameter(string.Format("@{0}", parameter.Name), pvalue);
                counter++;

            }

            //  return functionStore.GetValidationResult(procedureName, args);


            return false;


        }

        /// <summary>
        /// Validates against type = 10 
        /// </summary>
        /// <param name="rootXML"></param>
        /// <param name="xPathExpression"></param>
        /// <param name="xPathNavigator"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static bool ValidateXPathExpression(System.Xml.XmlElement rootXML, string xPathExpression, System.Xml.XPath.XPathNavigator xPathNavigator, string value)
        {
            if (xPathExpression.IsNotEmpty())
            {
                if (!value.IsEmpty())
                    xPathExpression = string.Format(xPathExpression, value);

                return (bool)XPath.Utilities.EvaluateWithExtension(null, rootXML, xPathNavigator, xPathExpression, value); //(bool)xPathNavigator.Evaluate(xPathExpression);
            }
            return true;
        }


        /// <summary>
        /// Validates against type = 11 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validation"></param>
        /// <param name="rootXML"></param>
        /// <param name="recordTypeCode"></param>
        /// <param name="outPutValue"></param>
        /// <returns></returns>
        private static bool ValidateCustomCode(string value, Validation validation, System.Xml.XmlElement rootXML, string recordTypeCode, ref string outPutValue)
        {
            Assembly assembly = null;

            if (validation.AssemblyName.IsNotEmpty())
            {
                var path = System.Reflection.Assembly.GetEntryAssembly().Location;
                assembly = Assembly.LoadFrom($"{path}\\{validation.AssemblyName}");
            }
            else
                assembly = Assembly.GetExecutingAssembly();
            string function = "", method, typename;

            int index = validation.FunctionName.LastIndexOf('.');
            if (index == -1)
            {
                function = string.Format("MWCIA.OAR.Validations.Record{0}.{1}", validation.RecordTypeID, validation.FunctionName);
                index = function.LastIndexOf('.');
            }
            else
            {
                function = validation.FunctionName;
            }
            method = function.Substring(index + 1);
            typename = function.Substring(0, index);



            Type type = assembly.GetType(typename);



            object[] args = new object[validation.ValidationParameters.Count + 1];
            //ParameterModifier[] p = new ParameterModifier[validation.ValidationParameters.Count+1];
            //p[0]
            //object[] args = new object[validation.ValidationParameters.Count+1];
            args[0] = value;
            int counter = 1;

            foreach ( ValidationParameter parameter in validation.ValidationParameters)
            {
                if (parameter.XPath.ToUpper() == "--XML--")
                    args[counter] = rootXML;
                else if (parameter.XPath.ToUpper().StartsWith("DEFAULT_"))
                    args[counter] = parameter.XPath.Replace("DEFAULT_", string.Empty);
                else
                    args[counter] = XPath.Utilities.ExtractParameterValue(parameter, rootXML, string.Empty);

                counter++;
            }
            object result = null;

            //System.Reflection.MethodInfo method = type.GetMethod(function,args,
            try
            {
                result = type.InvokeMember(method, BindingFlags.InvokeMethod, null, null, args);
            }
            catch
            {
                result = false;
            }
            outPutValue = args[0].ToString();
            return Convert.ToBoolean(result);
        }

        /// <summary>
        /// Validates against type = 5
        /// </summary>
        /// <param name="value"></param>
        /// <param name="lookupID"></param>
        /// <returns></returns>
        private static bool ValidateLookupValue( string value, string lookupID)
        {

            throw new NotImplementedException();
           // return Utilities.Lookup.Manager.ValidateLookupValue( lookupID, value);
        }

        /// <summary>
        /// Validates against type = 4
        /// </summary>
        /// <param name="value"></param>
        /// <param name="regularExpression"></param>
        /// <returns></returns>
        private static bool ValidateRegularExpression(string value, string regularExpression)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                if (regularExpression.StartsWith("not"))
                    return !System.Text.RegularExpressions.Regex.IsMatch(value.Trim(), regularExpression.Substring(3), System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled);
                else
                    return System.Text.RegularExpressions.Regex.IsMatch(value.Trim(), regularExpression, System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled);
            }
            return true;
        }

        /// <summary>
        /// Validates against type = 2
        /// </summary>
        /// <param name="value"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        private static bool ValidateRanage(string value, decimal? from, decimal? to)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                decimal? outValue = value.ToDecimal();

                if (outValue.HasValue)
                {
                    if (from.HasValue && to.HasValue)
                        return (outValue >= from.Value && outValue <= to.Value);
                    else if (from.HasValue)
                        return outValue >= from.Value;
                    else if (to.HasValue)
                        return outValue <= to.Value;

                }
            }
            return true;
        }


    }
}
