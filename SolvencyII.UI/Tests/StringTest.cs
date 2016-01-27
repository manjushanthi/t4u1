using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolvencyII.Data.Shared.Entities;
using SolvencyII.Domain.Extensions;


namespace Test.WindowsUI
{
    
    
    /// <summary>
    ///This is a test class for StringTest and is intended
    ///to contain all StringTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StringTest
    {

        private List<string> cutltureInfos = new List<string> { "af-ZA", "sq-AL", "ar-DZ", "ar-BH", "ar-EG", "ar-IQ", "ar-JO", "ar-KW", "ar-LB", "ar-LY", "ar-MA", "ar-OM", "ar-QA", "ar-SA", "ar-SY", "ar-TN", "ar-AE", "ar-YE", "hy-AM", "az-Cyrl-AZ", "az-Latn-AZ", "eu-ES", "be-BY", "bg-BG", "ca-ES", "zh-CN", "zh-HK", "zh-MO", "zh-SG", "zh-TW", "zh-CHS", "zh-CHT", "hr-HR", "cs-CZ", "da-DK", "div-MV", "nl-BE", "nl-NL", "en-AU", "en-BZ", "en-CA", "en-CB", "en-IE", "en-JM", "en-NZ", "en-PH", "en-ZA", "en-TT", "en-GB", "en-US", "en-ZW", "et-EE", "fo-FO", "fa-IR", "fi-FI", "fr-BE", "fr-CA", "fr-FR", "fr-LU", "fr-MC", "fr-CH", "gl-ES", "ka-GE", "de-AT", "de-DE", "de-LI", "de-LU", "de-CH", "el-GR", "gu-IN", "he-IL", "hi-IN", "hu-HU", "is-IS", "id-ID", "it-IT", "it-CH", "ja-JP", "kn-IN", "kk-KZ", "kok-IN", "ko-KR", "ky-KZ", "lv-LV", "lt-LT", "mk-MK", "ms-BN", "ms-MY", "mr-IN", "mn-MN", "nb-NO", "nn-NO", "pl-PL", "pt-BR", "pt-PT", "pa-IN", "ro-RO", "ru-RU", "sa-IN", "sr-Cyrl-SP", "sr-Latn-SP", "sk-SK", "sl-SI", "es-AR", "es-BO", "es-CL", "es-CO", "es-CR", "es-DO", "es-EC", "es-SV", "es-GT", "es-HN", "es-MX", "es-NI", "es-PA", "es-PY", "es-PE", "es-PR", "es-ES", "es-UY", "es-VE", "sw-KE", "sv-FI", "sv-SE", "syr-SY", "ta-IN", "tt-RU", "te-IN", "th-TH", "tr-TR", "uk-UA", "ur-PK", "uz-Cyrl-UZ", "uz-Latn-UZ", "vi-VN" };
        private List<string> notFoundCultures = new List<string> { "div-MV", "en-CB", "ky-KZ", "sr-Cyrl-SP", "sr-Latn-SP" };
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for IsNumeric
        ///</summary>
        [TestMethod()]
        public void RegularExpressionTest()
        {

            //Regex _regex = new Regex(@"^.*\:(\w)\(\*\)$");

            Regex _regex = new Regex(@"\:(\w+)\(");

            // (.*)\:([^\(]*)
            string searchMe = "s2c_dim:AX(*)";

            Match match = _regex.Match(searchMe);
            if (match.Success)
            {
                Assert.AreEqual("AX", match.Groups[1].ToString());
            }
            else
            {
                Assert.Fail("Not Found");
            }


        }


        //[TestMethod()]
        //public void BuildingSignature()
        //{

        //    string searchMe = "s2c_dim:XA(1)";

        //    Signature result = searchMe.GetSignatureDimensionMember();
        //    Assert.AreEqual("s2c", result.OwnerPrefix); // Dimension
        //    Assert.AreEqual("dim", result.DomainCode);
        //    Assert.AreEqual("XA", result.DimensionCode);
        //    Assert.AreEqual("1", result.Inner);


        //    searchMe = @"MET(s2c_met:mi1)=""123""";

        //    result = searchMe.GetSignatureMetric();
        //    Assert.AreEqual("s2c", result.OwnerPrefix); // Domain Member
        //    Assert.AreEqual("met", result.DomainCode);
        //    Assert.AreEqual("mi1", result.MemberCode);
        //    Assert.AreEqual("123", result.Inner);


        //    searchMe = @"MET(s2md_met:pi1051)=1000001;0;xbrli:pure";

        //    result = searchMe.GetSignatureMetric();
        //    Assert.AreEqual("s2md", result.OwnerPrefix); // Domain Member
        //    Assert.AreEqual("met", result.DomainCode);
        //    Assert.AreEqual("pi1051", result.MemberCode);
        //    Assert.AreEqual("1000001", result.Inner);


        //    // Used for HD
        //    //searchMe = @"s2c_dim:PO(s2c_PU:x12)";
        //    //result = searchMe.GetSignatureDimensionMember();
        //    //Assert.AreEqual("s2c", result.OwnerPrefix); // Dimension
        //    //Assert.AreEqual("dim", result.DomainCode);
        //    //Assert.AreEqual("PO", result.DimensionCode);
        //    //Assert.AreEqual("s2c_PU:x12", result.Inner);
        //    //result = result.Inner.GetSignatureMember();
        //    //Assert.AreEqual("s2c", result.OwnerPrefix); // Domain Member
        //    //Assert.AreEqual("PU", result.DomainCode);
        //    //Assert.AreEqual("x12", result.MemberCode);


        //}


        [TestMethod()]
        public void StringExtensionMyJoin()
        {
            List<string> myCollection = new List<string>();
            myCollection.Add("One");
            myCollection.Add("Two");
            myCollection.Add("Three");
            myCollection.Add("Four");
            string result = myCollection.MyJoin("|");
            Assert.AreEqual("One|Two|Three|Four", result);


        }

        [TestMethod()]
        public void CheckCulturalSettingsNumbers()
        {
            decimal checkDecimal = 1234567890.12m;
            bool error = false;
            StringBuilder sb = new StringBuilder();

            foreach (string cutltureInfo in cutltureInfos)
            {
                try
                {
                    CultureInfo cultureInfo = new CultureInfo(cutltureInfo);
                    // Application.CurrentCulture = cultureInfo;
                    // Thread.CurrentThread.CurrentCulture = cultureInfo;
                    // Thread.CurrentThread.CurrentUICulture = cultureInfo;


                    string resultString = checkDecimal.ToString(cultureInfo);
                    if (!resultString.IsNumeric())
                    {
                        error = true;
                        sb.AppendLine(cutltureInfo + " not numeric  ");
                    }
                    else
                    {
                        // Problem comes when the string needs to be converted to a double and we do not know where it has come from.
                        // The string can be 1234567890.12 or it can be 1234567890,12
                        // The conversion back to a double needs to know the locale or it can return 123456789012
                        decimal value = Convert.ToDecimal(resultString, cultureInfo);
                        decimal value2;
                        decimal.TryParse(resultString, NumberStyles.Any, cultureInfo, out value2);
                        if (value2 != checkDecimal)
                        {
                            sb.AppendLine(cutltureInfo + " values not equal 1 ");
                        }



                        if(value != checkDecimal)
                        {
                            sb.AppendLine(cutltureInfo + " values not equal 2 ");
                        }
                        Assert.AreEqual(checkDecimal, value);
                    }
                }
                catch (CultureNotFoundException ex)
                {
                    if (!notFoundCultures.Contains(cutltureInfo))
                        error = true;
                    sb.AppendLine(cutltureInfo + " not found  ");
                    Console.WriteLine(ex);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            Assert.IsFalse(error, sb.ToString());

        }
    }
}


/*
 // get culture names
List<string> list = new List<string>();
foreach (CultureInfo ci in CultureInfo.GetCultures(CultureTypes.AllCultures))
{
  string specName = "(none)";
  try { specName = CultureInfo.CreateSpecificCulture(ci.Name).Name; } catch { }
  list.Add(String.Format("{0,-12}{1,-12}{2}", ci.Name, specName, ci.EnglishName));
}

list.Sort();  // sort by name

// write to console
Console.WriteLine("CULTURE   SPEC.CULTURE  ENGLISH NAME");
Console.WriteLine("--------------------------------------------------------------");
foreach (string str in list)
  Console.WriteLine(str);
 */
