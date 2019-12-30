namespace Core.Validations
{
    /// <summary>
    /// 
    /// </summary>
    public class Common
    {


        #region Constructor
        static Common()
        {

            //endorsementCodes = BO.EndorsementCode.GetAll();
            //carriers = BO.Carrier.GetAll();
        }
        #endregion

       
        /// <summary>
        /// Validate N/A fields 
        /// </summary>
        /// <param name="value"></param>
        public static bool ValidateEmpty(string value)
        {
            return value.Trim().Length == 0;
        }

        //    /// <summary>
        //    /// Returns true if Carrier is available in MPS
        //    /// </summary>
        //    /// <param name="carrierID"></param>
        //    /// <returns></returns>
        //    public static bool ValidateCarrier(string carrierID)
        //    {
        //        return carriers.Where(t => t.CARRID == carrierID).Count() > 0;
        //    }

        //    /// <summary>
        //    /// Returns Carrier List for Group 
        //    /// </summary>
        //    /// <param name="groupID"></param>
        //    /// <returns></returns>
        //    public static List<string> GetCarriersList(string groupID)
        //    {
        //        return carriers.Where(t => t.GROUPID == groupID).Select(t => t.CARRID).ToList();
        //    }
        //    /// <summary>
        //    /// Validates Lookup Value
        //    /// </summary>
        //    /// <param name="lookupid"></param>
        //    /// <param name="code"></param>
        //    /// <returns></returns>
        //    public static bool ValidateLookupValue(string lookupid, string code)
        //    {
        //        Model.Transaction.TrxnLookupItem trxnLookupItem = null;

        //        if (lookupid.ToLower().StartsWith("an?"))
        //        {
        //            lookupid = lookupid.Substring(3);
        //            var lookup = lookupItems.Where(t => t.LookupID == lookupid && t.Status == false);

        //            trxnLookupItem = lookup.Where(v => v.Value.ToLower() == code.ToLower()).SingleOrDefault();
        //        }
        //        else if (lookupid.ToLower().StartsWith("pre?"))
        //        {

        //            lookupid = lookupid.Substring(4);
        //            var lookup = lookupItems.Where(t => t.LookupID == lookupid && t.Status == false);

        //            trxnLookupItem = lookup.Where(v => code.StartsWith(v.Code)).SingleOrDefault();
        //        }
        //        else if (lookupid.StartsWith("00?"))
        //        {
        //            if (code.IsEmpty())
        //                return true;

        //            lookupid = lookupid.Substring(3);
        //            var lookup = lookupItems.Where(t => t.LookupID == lookupid && t.Status == false && t.Code != "00" && t.Code != "99");

        //            trxnLookupItem = lookup.Where(v => code.StartsWith(v.Code)).SingleOrDefault();
        //        }
        //        else
        //        {
        //            if (lookupid.StartsWith("?"))
        //            {

        //                if (string.IsNullOrWhiteSpace(code) || code.ToNumber() == 0)
        //                    return true;

        //                lookupid = lookupid.Substring(1);
        //            }



        //            var lookup = lookupItems.Where(t => t.LookupID == lookupid && t.Status == false);
        //            if (!string.IsNullOrEmpty(code))
        //                trxnLookupItem = lookup.Where(v => v.Code.PadLeft(2, '0') == code.PadLeft(2, '0')).SingleOrDefault();
        //            else
        //                trxnLookupItem = lookup.Where(v => v.Code.ToLower() == "blank").SingleOrDefault();
        //        }
        //        return (trxnLookupItem != null);



        //    }

      


        //    /// <summary>
        //    /// Get Endorment Records based on Number
        //    /// </summary>
        //    /// <param name="number"></param>
        //    /// <returns></returns>
        //    public static List<Model.Transaction.EndorsementCode> GetEndorsementRecords(string number)
        //    {
        //        return endorsementCodes.Where(t => t.Number == number.NTrim()).ToList();
        //    }



        //    #region Private WCPOL Format Methods
        //    /// <summary>
        //    /// Format to 6 Byte Date
        //    /// </summary>
        //    /// <param name="value"></param>
        //    /// <returns></returns>
        //    internal static string ToDate6(string value)
        //    {
        //        if (value.Length == 6)
        //        {
        //            DateTime date = new DateTime();
        //            var substring = value.SplitByWidth(2);
        //            value = string.Format("{0}/{1}/{2}", substring[1], substring[2], substring[0]);
        //            if (DateTime.TryParse(value, out date))
        //                return date.ToString("MM/dd/yyyy");
        //        }

        //        return value;

        //    }
        //    /// <summary>
        //    /// Returns DateTime Object from Julian Date
        //    /// </summary>
        //    /// <param name="value"></param>
        //    /// <returns></returns>
        //    internal static string ConvertJulianToDate(string value)
        //    {
        //        if (value.Trim().Length == 5)
        //        {
        //            DateTime newDate = new DateTime();
        //            string date = "20";
        //            string year = string.Empty;
        //            string day = string.Empty;
        //            date += value;
        //            int iyear = -1;
        //            if (int.TryParse(date.Substring(0, 4), out iyear))
        //                newDate = newDate.AddYears(iyear - 1);
        //            else
        //                return value;
        //            int iday = -1;
        //            if (int.TryParse(date.Substring(4, 3), out iday))
        //                newDate = newDate.AddDays(iday - 1);
        //            else
        //                return value;
        //            return newDate.ToString("MM/dd/yyyy");
        //        }
        //        return value;
        //    }
        //    #endregion

        //    internal static bool ValidateCarrier(string carrierID, DateTime effectiveDate)
        //    {
        //        var carrier = carriers.Where(t => t.CARRID == carrierID).OrderByDescending(t => t.EffectiveDate).FirstOrDefault();
        //        if (carrier != null)

        //            return carrier.EffectiveDate <= effectiveDate && carrier.ExpirationDate.GetValueOrDefault(effectiveDate) >= effectiveDate;

        //        return true;
        //    }
    }
}
