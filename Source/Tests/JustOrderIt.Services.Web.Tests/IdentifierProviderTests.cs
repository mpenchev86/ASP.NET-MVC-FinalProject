namespace MvcProject.Services.Web.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using JustOrderIt.Services.Web;
    using NUnit.Framework;

    [TestFixture]
    public class IdentifierProviderTests
    {
        [Test]
        public void EncodingAndDecodingPreserveOriginalValue()
        {
            // Arrange
            const int intId = 213;
            const string stringId = "213";
            IIdentifierProvider provider = new IdentifierProvider();

            // Act
            var encodedIntId = provider.EncodeIntId(intId);
            var decodedIntId = provider.DecodeToIntId(encodedIntId);
            var encodedStringId = provider.EncodeStringId(stringId);
            var decodedStringId = provider.DecodeToStringId(encodedStringId);

            // Assert
            Assert.AreEqual(decodedIntId, intId);
            Assert.AreEqual(decodedStringId, stringId);
        }
    }
}
