//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using BO = BITLogix.MWCIA.ManageUSR3.Data.BO;
//using Model = BITLogix.MWCIA.ManageUSR3.Data.Model;
//using BITLogix.MWCIA.ManageUSR3.Utilities.Extensions;


//namespace MWCIA.OAR.Validations
//{
//    /// <summary>
//    /// Lookup Manager.
//    /// </summary>
//    public static class LookupManager
//    {

//        #region Declaration
//        const string STATECODE = "22";
        
//        static readonly IQueryable<Model.LookupItem> lookupItems = null;
//        static readonly List<Model.SystemSetting> settings = null;
//        static readonly List<Model.BureauAttribute> bureauAttributes = null;

       

//        static List<string> carriers = null;
//        static List<string> stateCodes = null;
//        #endregion
//        static LookupManager()
//        {
//            stateCodes = new List<string>();
//            lookupItems = BO.Lookup.ActiveItems;
//            // settings = BO.SystemSetting.Instance.Get();
            
//            bureauAttributes = BO.BureauAttribute.Instance.Get();
//        }
//        #region [ Internal Classes ] 
//        public static class ClassManager
//        {
//            public static bool AboveLine(string classCode)
//            {
//                var cls = GetClass(classCode);
//                if (cls != null)
//                {
//                    return "Y,y".Contains(cls.AboveLine);
//                }
//                return false;
//            }
//        }
//        #endregion

//        #region [ Methods ]
//        public static void Init(List<string> carriers, params string[] stateCodes)
//        {
//            LookupManager.carriers = carriers;
//            if (stateCodes.Length > 0)
//                LookupManager.stateCodes.AddRange(stateCodes);


//        }

//        /// <summary>
//        /// Checks the class code.
//        /// </summary>
//        /// <param name="classCode">The class code.</param>
//        /// <param name="effectiveDate">The effective date.</param>
//        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
//        public static bool CheckClass(string classCode, DateTime? effectiveDate)
//        {
//            return BO.Class.Items.Count(t => t.ClassCode == classCode && effectiveDate >= t.EffectiveDate && effectiveDate <= t.ExpiraionDate) > 0;
//        }
//        internal static Model.Class GetClass(string classCode)
//        {
//            return BO.Class.Items.FirstOrDefault(t => t.ClassCode == classCode);
//        }

//        /// <summary>
//        /// Get System Setting
//        /// </summary>
//        /// <param name="settingName"></param>
//        /// <returns></returns>
//        public static string GetSystemSetting(string settingName)
//        {
//            var systemSetting = settings.Where(t => t.Name == settingName).SingleOrDefault();
//            if (systemSetting != null)
//                return systemSetting.Value;
//            return string.Empty;
//        }
       
//        public static bool CheckCarrier(string carrierID)
//        {
//            return carriers.Contains(carrierID);
//        }
//        #endregion
//    }
//}
