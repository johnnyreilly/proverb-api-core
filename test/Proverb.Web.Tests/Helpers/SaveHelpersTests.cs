using Microsoft.VisualStudio.TestTools.UnitTesting;
using Proverb.Api.Core.Helpers;

namespace Proverb.Web.Tests.Helpers
{
    [TestClass]
    public class SaveHelpersTests
    {
        [TestMethod]
        public void CamelCasePropNames_given_delimited_string_should_return_camel_cased_string()
        {
            var camelCase = SaveHelpers.CamelCasePropNames("TestName.PropName");
            Assert.AreEqual(camelCase, "testName.propName");
        }
    }
}
