﻿namespace MvcProject.Services.Web.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NUnit.Framework;

    [TestFixture]
    public class IdentifierProviderTests
    {
        [Test]
        public void EncodingAndDecodingPreserveContent()
        {
            const int Id = 213;
            IIdentifierProvider provider = new IdentifierProvider();
            var encoded = provider.EncodeIntId(Id);
            var actual = provider.DecodeIdToInt(encoded);
            Assert.AreEqual(Id, actual);
        }
    }
}