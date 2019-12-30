using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Xsl; 

using System.Xml.XPath; 
using Wmhelp.XPath2;
using Core.Data.Entities.Administration;
using Core.Utilities.Extensions;

namespace Core.Validations.Engine.XPath
{
    /// <summary>
    /// XPath Helper Class
    /// </summary>
    public class Utilities
    {
        #region Extract Values


        internal static object EvaluateWithExtension( Validation validation, XmlElement currentNode, System.Xml.XPath.XPathNavigator nav, string xPathExpression, string value = null)
        {
            if (xPathExpression.IsNotEmpty())
            {

                //Create argument list and add the parameters.
                XsltArgumentList varList = new XsltArgumentList();
                //object[] args = new object[validation.ValidationParameters.Count + 1];
                CustomContext context = new CustomContext();
                Func<string, object> extractConstValue = (variable) =>
                {
                    Regex r = new Regex(@"^\$\$\{(.+)\}");
                    return r.Replace(variable, "$1");
                };
                if (value != null)
                    varList.AddParam("value", string.Empty, value);
                if (validation != null)
                {
                    int counter = 0;
                    foreach (ValidationParameter parameter in validation.ValidationParameters)
                    {
                        counter++;
                        string paramName = parameter.Name;
                        if (parameter.Name.IsEmpty())
                            paramName = string.Format("param{0}", counter);
                        object pvalue = null;
                        if (parameter.XPath.ToUpper() == "--XML--")
                            pvalue = currentNode;
                        else if (parameter.XPath.ToUpper().StartsWith("DEFAULT_"))
                            pvalue = parameter.XPath.Replace("DEFAULT_", string.Empty);
                        else if (parameter.XPath.StartsWith("$$"))
                            pvalue = extractConstValue(parameter.XPath);
                        else
                            pvalue = ExtractParameterValue(parameter, currentNode, string.Empty);

                        varList.AddParam(paramName, string.Empty, pvalue);

                    }
                }
                context.AddParams(varList);

                XPath2Expression xpath = XPath2Expression.Compile(xPathExpression);
                
                
                return nav.XPath2Evaluate(xpath);

            }
            return null;
        }

        /// <summary>
        /// Retrives Value based on the type and XML Path 
        /// </summary>
        /// <param name="wcpolField"></param>
        /// <returns></returns>
        internal static string ExtractValue( Validation validation, XmlElement currentNode, string xmlRecordRootPath)
        {
            string xPath = string.Empty;
            string value = string.Empty;

            if (!string.IsNullOrWhiteSpace(validation.XPath))
            {

                xPath = validation.XPath;

                if (!xPath.StartsWith(@"//"))
                    xPath = xmlRecordRootPath + xPath;
            }

            if (xPath.Length > 0 && currentNode != null)
            {

                XmlNode element = currentNode.SelectSingleNode(xPath);
                if (element != null)
                    value = element.InnerText;
            }



            return value.Trim();

        }

        /// <summary>
        /// Retrives Value based on the type and XML Path 
        /// </summary>
        /// <param name="wcpolField"></param>
        /// <returns></returns>
        internal static string ExtractValue(string xPath, XmlElement currentNode, string xmlRecordRootPath)
        {

            string value = string.Empty;

            if (!string.IsNullOrWhiteSpace(xPath))
            {
                if (!xPath.StartsWith(@"//"))
                    xPath = xmlRecordRootPath + xPath;
            }

            if (xPath.Length > 0 && currentNode != null)
            {
                XmlNode element = currentNode.SelectSingleNode(xPath);
                if (element != null)
                    value = element.InnerText;
            }



            return value.Trim();

        }
        /// <summary>
        /// Retrives Value based on the type and XML Path 
        /// </summary>
        /// <param name="wcpolField"></param>
        /// <returns></returns>
        internal static object ExtractParameterValue( ValidationParameter validationParameter, XmlElement currentNode, string xmlRecordRootPath)
        {
            string xPath = string.Empty;
            string value = string.Empty;

            if (!string.IsNullOrWhiteSpace(validationParameter.XPath))
            {

                xPath = validationParameter.XPath;

                if (!xPath.StartsWith(@"//"))
                    xPath = xmlRecordRootPath + xPath;
            }

            if (xPath.Length > 0 && currentNode != null)
            {



                try
                {
                    var list = currentNode.SelectNodes(xPath);
                    var element = list.Count > 0 ? list.Item(0) : null;
                    if (element != null && list.Count == 1)
                        value = element.InnerText.Trim();
                    else
                        return list;
                }
                catch (Exception)
                {
                    return currentNode.CreateNavigator().Evaluate(xPath);

                }
            }



            return value;

        }
        #endregion
    }


}
