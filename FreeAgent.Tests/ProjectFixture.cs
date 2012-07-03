using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace FreeAgent.Tests
{
    [TestFixture]
    public class ProjectFixture : ResourceFixture<ProjectWrapper, ProjectsWrapper, Project>
    {

        public override ResourceClient<ProjectWrapper, ProjectsWrapper, Project> ResourceClient
        {
            get { return Client.Project; }
        }


        public override void CheckSingleItem(Project item)
        {

            Assert.IsNotNullOrEmpty(item.url);
            Assert.IsNotNullOrEmpty(item.name);
            Assert.IsNotNullOrEmpty(item.contact);
            Assert.IsNotNullOrEmpty(item.status);
        }


        public override Project CreateSingleItemForInsert()
        {
            var contact = Client.Contact.All().First();

            Assert.IsNotNull(contact);


            return new Project
                       {
                           url = "",
                           contact = contact.UrlId(),
                           name = "project TEST",
                           status = ProjectStatus.Active,
                           budget_units = ProjectBudgetUnits.Days,
                           hours_per_day = 7.5,
                           billing_period = ProjectBillingPeriod.Day,
                            normal_billing_rate = 450,
                           currency = "GBP"
                       };

        }

        public override void CompareSingleItem(Project originalItem, Project newItem)
        {
            Assert.IsNotNull(newItem);
            Assert.IsNotNullOrEmpty(newItem.url);
            Assert.AreEqual(newItem.name, originalItem.name);
            Assert.AreEqual(newItem.status, originalItem.status);
            Assert.AreEqual(newItem.budget_units, originalItem.budget_units);
            Assert.AreEqual(newItem.currency, originalItem.currency);
        }

        public override bool CanDelete(Project item)
        {
            return item.name.Contains("TEST");

        }
    }


}
