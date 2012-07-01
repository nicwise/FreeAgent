using System;
using NUnit.Framework;

namespace FreeAgent.Tests
{
    [TestFixture]
    public class CategoryFixture : BaseFixture
    {
        [SetUp]
        public void Setup()
        {
            SetupClient();
        }

        [Test]
        public void CanGetListOfCategories()
        {
            var cats = Client.Categories.All();
            Assert.IsNotEmpty(cats.admin_expenses_categories);
            Assert.IsNotEmpty(cats.cost_of_sales_categories);
            Assert.IsNotEmpty(cats.general_categories);
            Assert.IsNotEmpty(cats.income_categories);
        }

        /*
         * Disabled as I dont need it, and the API design is.... interesting.
        [Test]
        public void CanGetSingleCategory()
        {
            var cats = Client.Categories.Single("249");

            Assert.IsNotEmpty(cats.income_categories);
        }
        */

    }
}

