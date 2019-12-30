using Core.Utilities.Extensions;
using System;
using System.Linq;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
namespace Core.Validations.Engine
{
    class CustomContext : System.Xml.Xsl.XsltContext
    {
        private const string ExtensionsNamespaceUri = "http://mwcia.org/oar";
        // XsltArgumentList to store names and values of user-defined variables.
        private XsltArgumentList argList;

        public CustomContext() : base(new System.Xml.NameTable())
        {
            AddNamespace("oar", ExtensionsNamespaceUri);
            argList = new XsltArgumentList();
        }

        public CustomContext(NameTable nt, XsltArgumentList args)
            : base(nt)
        {
            argList = args;
        }
        public void AddParam(string name, string uri, object value)
        {
            argList.AddParam(name, uri, value);
        }
        public void AddParams(XsltArgumentList list)
        {
            argList = list;
        }
        // Function to resolve references to user-defined XPath extension 
        // functions in XPath query expressions evaluated by using an 
        // instance of this class as the XsltContext. 
        public override System.Xml.Xsl.IXsltContextFunction ResolveFunction(
                                    string prefix, string name,
                                    System.Xml.XPath.XPathResultType[] argTypes)
        {
            // Verify namespace of function.
            if (this.LookupNamespace(prefix) == ExtensionsNamespaceUri)
            {
                string strCase = name;

                switch (strCase.ToLower())
                {
                    case "evaluate":
                        return new XPathExtensionFunctions(1, 1, XPathResultType.Any,
                                                                     argTypes, "Evaluate");
                    case "duplicate":
                        return new XPathExtensionFunctions(2, 2, XPathResultType.String,
                                                                    argTypes, "Duplicate");
                    case "contains":
                        return new XPathExtensionFunctions(2, 2, XPathResultType.Boolean,
                                                                    argTypes, "Contains");
                    case "comparedate":
                        return new XPathExtensionFunctions(3, 4, XPathResultType.String,
                                                                    argTypes, "CompareDate");
                    case "iif":
                        return new XPathExtensionFunctions(3, 3, XPathResultType.String,
                                                                    argTypes, "IIF");
                    case "iifnull":
                        return new XPathExtensionFunctions(2, 2, XPathResultType.String,
                                                                    argTypes, "IIFNULL");
                    case "isemptyornull":
                        return new XPathExtensionFunctions(1, 1, XPathResultType.String,
                                                                    argTypes, "isemptyornull");
                    case "isnotemptyornull":
                        return new XPathExtensionFunctions(1, 1, XPathResultType.String,
                                                                    argTypes, "isnotemptyornull");
                    case "countchar":
                        return new XPathExtensionFunctions(2, 2, XPathResultType.Number,
                                                                    argTypes, "CountChar");

                    case "abs": // This function is implemented but not called.
                        return new XPathExtensionFunctions(2, 2, XPathResultType.String,
                                                                    argTypes, "abs");

                    case "right": // This function is implemented but not called.
                        return new XPathExtensionFunctions(2, 2, XPathResultType.String,
                                                                        argTypes, "Right");

                    case "left": // This function is implemented but not called.
                        return new XPathExtensionFunctions(2, 2, XPathResultType.String,
                                                                         argTypes, "Left");
                }
            }
            // Return null if none of the functions match name.
            return null;
        }

        // Function to resolve references to user-defined XPath 
        // extension variables in XPath query.
        public override System.Xml.Xsl.IXsltContextVariable ResolveVariable(
                                                         string prefix, string name)
        {
            if (this.LookupNamespace(prefix) == ExtensionsNamespaceUri || !prefix.Equals(string.Empty))
            {
                throw new XPathException(string.Format("Variable '{0}:{1}' is not defined.", prefix, name));
            }

            // Verify name of function is defined.
            //if (name.Equals("text") || name.Equals("charToCount") ||
            //    name.Equals("right") || name.Equals("left"))
            //{
            // Create an instance of an XPathExtensionVariable 
            // (custom IXsltContextVariable implementation) object 
            //  by supplying the name of the user-defined variable to resolve.
            XPathExtensionVariable var;
            var = new XPathExtensionVariable(prefix, name);

            // The Evaluate method of the returned object will be used at run time
            // to resolve the user-defined variable that is referenced in the XPath
            // query expression. 
            return var;
            //}
            // return null;
        }

        // Empty implementation, returns false.
        public override bool PreserveWhitespace(System.Xml.XPath.XPathNavigator node)
        {
            return false;
        }

        // empty implementation, returns 0.
        public override int CompareDocument(string baseUri, string nextbaseUri)
        {
            return 0;
        }

        public override bool Whitespace
        {
            get
            {
                return true;
            }
        }

        // The XsltArgumentList property is accessed by the Evaluate method of the 
        // XPathExtensionVariable object that the ResolveVariable method returns. It is used 
        // to resolve references to user-defined variables in XPath query expressions. 
        public XsltArgumentList ArgList
        {
            get
            {
                return argList;
            }
        }
    }

    // The interface that resolves and executes a specified user-defined function. 
    public class XPathExtensionFunctions : System.Xml.Xsl.IXsltContextFunction
    {
        // The data types of the arguments passed to XPath extension function.
        private System.Xml.XPath.XPathResultType[] argTypes;
        // The minimum number of arguments that can be passed to function.
        private int minArgs;
        // The maximum number of arguments that can be passed to function.
        private int maxArgs;
        // The data type returned by extension function.
        private System.Xml.XPath.XPathResultType returnType;
        // The name of the extension function.
        private string FunctionName;

        // Constructor used in the ResolveFunction method of the custom XsltContext 
        // class to return an instance of IXsltContextFunction at run time.
        public XPathExtensionFunctions(int minArgs, int maxArgs,
            XPathResultType returnType, XPathResultType[] argTypes, string functionName)
        {
            this.minArgs = minArgs;
            this.maxArgs = maxArgs;
            this.returnType = returnType;
            this.argTypes = argTypes;
            this.FunctionName = functionName;
        }

        // Readonly property methods to access private fields.
        public System.Xml.XPath.XPathResultType[] ArgTypes
        {
            get
            {
                return argTypes;
            }
        }
        public int Maxargs
        {
            get
            {
                return maxArgs;
            }
        }

        public int Minargs
        {
            get
            {
                return maxArgs;
            }
        }

        public System.Xml.XPath.XPathResultType ReturnType
        {
            get
            {
                return returnType;
            }
        }


        // XPath extension functions.

        private bool CompareDate(XPathNodeIterator node, string optr, DateTime? date1, DateTime? date2)
        {
            var element = node.Cast<XPathNavigator>().FirstOrDefault();
            if (element == null)
                return true;
            switch (optr)
            {
                case "eq":
                    return element.Value.ToDate() == date1;
                case "lt":
                    return element.Value.ToDate() < date1;
                case "gt":
                    return element.Value.ToDate() > date1;
                case "lte":
                    return element.Value.ToDate() <= date1;
                case "gte":
                    return element.Value.ToDate() >= date1;
                case "!eq":
                    return element.Value.ToDate() != date1;
                case "btw":
                    return element.Value.ToDate() >= date1 && element.Value.ToDate() <= date2;
                default:
                    return element.Value.ToDate() == date1;
            }

        }

        private bool Contains(XPathNodeIterator node, string list)
        {
            var element = node.Cast<XPathNavigator>().FirstOrDefault();
            if (element == null)
                return true;
            return list.Contains(element.Value);
        }

        private bool Duplicate(XsltContext context, XPathNavigator nav, XPathNodeIterator iter, string value)
        {
            do
            {
                return iter.Current.Value == value;
            } while (iter.MoveNext());
        }
        private int CountChar(XPathNodeIterator node, char charToCount)
        {
            int charCount = 0;
            for (int charIdx = 0; charIdx < node.Current.Value.Length; charIdx++)
            {
                if (node.Current.Value[charIdx] == charToCount)
                {
                    charCount++;
                }
            }
            return charCount;
        }


        private decimal Abs(decimal value)
        {
            return Math.Abs(value);
        }

        private string Left(string str, int length)
        {
            return str.Substring(0, length);
        }

        private string Right(string str, int length)
        {
            return str.Substring((str.Length - length), length);
        }

        // Function to execute a specified user-defined XPath extension 
        // function at run time.
        public object Invoke(System.Xml.Xsl.XsltContext xsltContext,
                       object[] args, System.Xml.XPath.XPathNavigator docContext)
        {
            string str = FunctionName.ToLower();
            switch (str)
            {
                case "evaluate":
                    return args[0];
                case "contains":
                    return (Object)Contains((XPathNodeIterator)args[0],
                                               Convert.ToString(args[1]));
                case "iif":
                    return (bool)args[0] ? args[1] : args[2];
                case "ifnull":
                    if (args[0] is string)
                        return string.IsNullOrWhiteSpace(Convert.ToString(args[0])) ? args[1] : args[0];
                    return args[0] ?? args[1];
                case "ifemptyornull":
                    {
                        var iter = (XPathNodeIterator)args[0];
                        var element = iter.Cast<XPathNavigator>().FirstOrDefault();
                        if (element != null)
                            return element.Value.IsEmpty();
                        else
                            return true;
                    }
                case "isnotemptyornull":
                    {
                        var iter = (XPathNodeIterator)args[0];
                        var element = iter.Cast<XPathNavigator>().FirstOrDefault();
                        if (element != null)
                            return element.Value.IsNotEmpty();
                        else
                            return false;
                    }

                case "comparedate":
                    var date1 = Convert.ToString(args[2]).ToDate();
                    var date2 = args.Length == 4 ? Convert.ToString(args[3]).ToDate() : null;
                    return (Object)CompareDate((XPathNodeIterator)args[0],
                                                    Convert.ToString(args[1]),
                                                    date1,
                                                    date2);

                case "countchar":
                    return (Object)CountChar((XPathNodeIterator)args[0],
                                                Convert.ToChar(args[1]));
                case "left":
                    return (Object)Left(Convert.ToString(args[0]),
                                                Convert.ToInt16(args[1]));
                case "right":
                    return (Object)Right(Convert.ToString(args[0]),
                                                 Convert.ToInt16(args[1]));
                case "duplicate":
                    return (Object)Duplicate(xsltContext,
                                                 docContext,
                                                 (XPathNodeIterator)args[0],
                                                 Convert.ToString(args[1]));
                case "abs":
                    return (Object)Abs(
                                                Convert.ToString(args[0]??"").ToDecimal().GetValueOrDefault(0));
                default:
                    return args[0];

            }

            
        }
    }

    // The interface used to resolve references to user-defined variables
    // in XPath query expressions at run time. An instance of this class 
    // is returned by the overridden ResolveVariable function of the 
    // custom XsltContext class.
    public class XPathExtensionVariable : IXsltContextVariable
    {
        // Namespace of user-defined variable.
        private string prefix;
        // The name of the user-defined variable.
        private string varName;

        // Constructor used in the overridden ResolveVariable function of custom XsltContext.
        public XPathExtensionVariable(string prefix, string varName)
        {
            this.prefix = prefix;
            this.varName = varName;
        }

        // Function to return the value of the specified user-defined variable.
        // The GetParam method of the XsltArgumentList property of the active
        // XsltContext object returns value assigned to the specified variable.
        public object Evaluate(System.Xml.Xsl.XsltContext xsltContext)
        {
            XsltArgumentList vars = ((CustomContext)xsltContext).ArgList;
            return vars.GetParam(varName, prefix);
        }

        // Determines whether this variable is a local XSLT variable.
        // Needed only when using a style sheet.
        public bool IsLocal
        {
            get
            {
                return false;
            }
        }

        // Determines whether this parameter is an XSLT parameter.
        // Needed only when using a style sheet.
        public bool IsParam
        {
            get
            {
                return false;
            }
        }

        public System.Xml.XPath.XPathResultType VariableType
        {
            get
            {
                return XPathResultType.Any;
            }
        }
    }

}
