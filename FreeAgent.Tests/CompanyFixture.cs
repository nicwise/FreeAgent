using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace FreeAgent.Tests
{
    [TestFixture]
    public class CompanyFixture : BaseFixture
    {
        [SetUp]
        public void Setup()
        {
            SetupClient();
        }

        [Test]
        public void CanLoadCompany()
        {
            Company company = Client.Company.Single();

            Assert.IsNotNull(company);
            Assert.IsNotNullOrEmpty(company.name);
        }

        [Test]
        public void CanGetTaxTimeline()
        {
            List<TaxTimeline> timeline = Client.Company.TaxTimeline();

            Assert.IsNotNull(timeline);
            Assert.IsNotEmpty(timeline);

        }

        [Test]
        public void TaxTimelineHasContent()
        {
            List<TaxTimeline> timeline = Client.Company.TaxTimeline();

            Assert.IsNotNull(timeline);
            Assert.IsNotEmpty(timeline);

            foreach (TaxTimeline t in timeline)
            {
                Assert.IsNotNullOrEmpty(t.description);
            }
        }

    }
}
