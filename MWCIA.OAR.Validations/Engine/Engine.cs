using Core.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Xml; 
using Core.Application;
using Core.Data.Entities.Administration;
using Core.Utilities.Extensions;
using Core.Data.Entities.Application;
using GlobalConstants = Core.Data.Constants;
namespace Core.Validations.Engine
{

    /// <summary>
    /// Validation Engine
    /// </summary>
    public class Processor
    {

        #region Private Declaration

       private static IRepositoryWrapper repositoryWrapper; 


       
        static object lockObject = new object();


        #endregion

        #region Constructors
        /// <summary>
        /// Initialize objects of RecordType 
        /// </summary>
        public Processor(IRepositoryWrapper _repositoryWrapper)
        {
            repositoryWrapper = _repositoryWrapper;
            //var recordTypeStore = new BO.Administration.RecordType();
            //recordTypes = recordTypeStore.All().ToList() ;
        }
        #endregion

        #region Public Methods


        /// <summary>
        /// Validates Header from XML
        /// </summary>
        /// <param name="rootXML"></param>

        /// <param name="errors"></param>
        /// <returns></returns>
        public  string Validate(XmlElement rootXML, List< ValidationError> errors,string pageID,string sectionID)
        {
            var recordTypesToValidate = repositoryWrapper.RecordType.GetAll().Where(t => t.PageID == pageID && (sectionID.IsEmpty() || sectionID.ToUpper() == t.SectionID.ToUpper()));

            foreach (var record in recordTypesToValidate)
            {
                Validate(rootXML, record.ID, record.Category, errors);
               
            }

            
           
            return ValidateCurrentStatus(errors);
        }

        
        /// <summary>
        /// Validates single category validations. Mostly used for "R" types validation from Web. 
        /// </summary>
        /// <param name="rootXML"></param>
        /// <param name="validateLinkData"></param>
        /// <param name="recordTypeCode"></param>
        /// <param name="category"></param>
        /// <param name="errors"></param>
        /// <param name="PKValue"></param>
        /// <param name="removeCurrent"></param>
        /// <returns></returns>
        private static string Validate(System.Xml.XmlElement rootXML, string recordTypeCode, string category, ICollection<ValidationError> errors, long PKValue = 0, bool removeCurrent = true)
        {
            RecordType recordType = GetRecordType(recordTypeCode);
            if (removeCurrent)
                RemoveCurrentErrors(errors, recordTypeCode, PKValue);

            List<Validation> currentValidations = null;
            
                 currentValidations = GetValidations(recordTypeCode, category);
            var recordTypePath = recordType.XPathRoot.TrimEnd("/".ToCharArray());
            if (recordTypePath.IsEmpty())
                recordTypePath = "/";
            XmlNodeList recordTypeXMLDocumentList = rootXML.SelectNodes(recordTypePath);
            string identifier = string.Empty;
            foreach (XmlNode recordTypeXMLDocument in recordTypeXMLDocumentList)
            {
                if (recordTypeXMLDocument.InnerXml.Length > 0)
                {
                    bool conditionResult = true;

                    System.Xml.XPath.XPathNavigator xPathNavigator = recordTypeXMLDocument.CreateNavigator();
                    if (!recordType.IdentifierExpression.IsEmpty())
                    {
                        var result = xPathNavigator.Evaluate(recordType.IdentifierExpression);
                        if (result is System.Xml.XPath.XPathNodeIterator)
                        {
                            var element = (result as System.Xml.XPath.XPathNodeIterator).Cast<System.Xml.XPath.XPathNavigator>().FirstOrDefault();
                            if (element != null)
                                identifier = element.Value;
                        }
                        else
                        {
                            identifier = result.ToString();
                        }
                    }
                    else
                        identifier = string.Empty;

                    int? pkValue = null;

                    if (!string.IsNullOrWhiteSpace(recordType.PKColumn))
                        pkValue = XPath.Utilities.ExtractValue(recordType.PKColumn, (recordTypeXMLDocument as XmlElement), string.Empty).ToNumber();
                    else
                        pkValue = null;

                   
                    string value = string.Empty;
                    string outputValue = string.Empty;
                    
                    foreach (Validation validation in currentValidations)
                    {

                        value = XPath.Utilities.ExtractValue(validation, (recordTypeXMLDocument as XmlElement), string.Empty);
                        if (!string.IsNullOrWhiteSpace(validation.Condition))
                        {
                            conditionResult = (bool)XPath.Utilities.EvaluateWithExtension(validation, (recordTypeXMLDocument as XmlElement), xPathNavigator, validation.Condition, value);
                        }
                        else
                        {
                            conditionResult = true;
                        }
                        if (conditionResult && CheckFieldValidationStatus(errors, validation, pkValue))
                        {

                            if (!Validators.Validate(value, recordTypeCode, validation, (recordTypeXMLDocument as XmlElement), xPathNavigator, ref outputValue))
                            {
                                if (string.IsNullOrWhiteSpace(outputValue))
                                    outputValue = value;

                                AddErrorRecord(errors, outputValue, validation, recordType, pkValue, identifier);

                            }
                        }
                        outputValue = string.Empty;
                        
                    }
                }
            }

            return ValidateCurrentStatus(errors);
        }

        
        #endregion


        #region Private Functions

        /// <summary>
        /// Add Error Record
        /// </summary>
        /// <param name="errors"></param>
        /// <param name="value"></param>
        /// <param name="validation"></param>
        /// <param name="recordTypeCode"></param>
        /// <param name="pkValue"></param>
        internal static  ValidationError AddErrorRecord(ICollection<ValidationError> errors, string value, Validation validation, RecordType recordType, int? pkValue, string identifier = "", string linkData = "")
        {
             ValidationError error = new ValidationError();
            error.CurrentValue = value;
            if (error.CurrentValue.IsEmpty() && validation.DefaultValue.IsNotEmpty())
                error.CurrentValue = validation.DefaultValue;
            error.ValidationID = validation.ID;
            if (validation.Category == Constants.RecordCategory.RECORD)
                error.PKValue = pkValue;
            error.ValidationID = validation.ID;
            error.FieldID = validation.FieldID.ToString();
            error.RecordTypeID = recordType.ID;
            error.GroupID = validation.GroupID;
          
            error.Severity = validation.Severity;
            error.ErrorMessage = validation.ErrorMessage;
            error.SectionName = validation.SectionName;
            error.Label = validation.Label;
            
            if (identifier.IsNotEmpty())
                error.Identifier = identifier.Trim("-".ToCharArray()).Trim("/".ToCharArray()) + " - ";

            error.AssociatedTabID = validation.AssociatedTabID;
            error.FocusControlID = validation.FocusControlID;
            error.HighlightControlID = validation.HighlightControlID;
            error.ControlType = validation.ControlType;
            error.RepeatableInd = recordType.RepeatableInd;
            error.FocusableInd = validation.FocusableInd;

            
            errors.Add(error);
            return error;


        }
        #endregion
        #region Linq based Data Functions

        /// <summary>
        /// Check if the validation of a fild is already failed or not. 
        /// </summary>
        /// <param name="errors"></param>
        /// <param name="validation"></param>
        /// <param name="pakValue"></param>
        /// <returns></returns>
        internal static bool CheckFieldValidationStatus(ICollection<ValidationError> errors, Validation validation, int? pakValue)
        {
            if (validation.FieldID == "0")
                return true;
            if (pakValue.HasValue)
                return !errors.Any(t => t.RecordTypeID == validation.RecordTypeID && t.FieldID == validation.FieldID && t.PKValue == pakValue.Value);
            else
                return !errors.Any(t => t.RecordTypeID == validation.RecordTypeID && t.FieldID == validation.FieldID && t.PKValue == null);
        }

        /// <summary>
        /// Remove errors from collection based on Recordtype being validated. 
        /// </summary>
        /// <param name="errors"></param>
        /// <param name="recordTypeCode"></param>
        public static void RemoveCurrentErrors(ICollection<ValidationError> errors, string recordTypeCode, long PKValue = 0)
        {

            if (PKValue == 0)
                foreach (var item in errors.Where(t => t.RecordTypeID == recordTypeCode).ToList())
                    errors.Remove(item);
            else
                foreach (var item in errors.Where(t => t.RecordTypeID == recordTypeCode && t.PKValue == PKValue).ToList())
                    errors.Remove(item);
        }

        /// <summary>
        /// Returns true if contains only warnings or valid otherwise false;
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        internal static string ValidateCurrentStatus(ICollection<ValidationError> errors)
        {
            string status;
            int wc = errors.Count(t => t.Severity == Constants.Severity.WARNING),
              ec = errors.Count(t => t.Severity == Constants.Severity.ERROR);
            if (wc > 0 && ec > 0)
            {
                status = GlobalConstants.ValidationStatus.ERRORS;
            }
            else if (wc > 0)
            {
                status = GlobalConstants.ValidationStatus.WARNINGS;
            }
            else if (ec > 0)
            {
                status = GlobalConstants.ValidationStatus.ERRORS;
            }
            else
            {
                status = GlobalConstants.ValidationStatus.VALID;
            }
            
            return status;
        }

        /// <summary>
        /// Get RecordType Object by Code
        /// </summary>
        /// <param name="recordTypeID"></param>
        /// <returns></returns>
        internal static RecordType GetRecordType(string recordTypeID)
        {
            return repositoryWrapper.RecordType.GetAll().Where(t => t.ID == recordTypeID).SingleOrDefault();
        }

        /// <summary>
        /// Get Validations by RecordTypeCode and Category
        /// </summary>
        /// <param name="recordTypeCode"></param>
        /// <param name="category"></param>
        /// <param name="includeLinkData">if true then include link data validations in any case</param>
        /// <returns></returns>
        internal static List<Validation> GetValidations(string recordTypeCode, string category)
        {

            var filteredValidations =repositoryWrapper.Validation.GetAll().Where(t => t.RecordTypeID==recordTypeCode);
            if (category.Length > 0)
            {
                filteredValidations = filteredValidations.Where(t => t.Category == category);
            }

            return filteredValidations.OrderBy(t => t.FieldID).ThenBy(t => t.SequenceNo).ToList();
        }

        /// <summary>
        /// Get Validations by RecordTypeCode and Category
        /// </summary>
        /// <param name="recordTypeCode"></param>
        /// <param name="category"></param>
        /// <param name="includeLinkData">if true then include link data validations in any case</param>
        /// <returns></returns>
        internal static List<Validation> GetValidations( string category)
        {

            var filteredValidations =repositoryWrapper.Validation.GetAll().Where(t => t.Category == category);
            
            return filteredValidations.OrderBy(t => t.FieldID).ThenBy(t => t.SequenceNo).ToList();
        }



        #endregion


    }
}
