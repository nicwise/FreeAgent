using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace FreeAgent.Tests
{
    public class TimeslipFixture : ResourceFixture<TimeslipWrapper, TimeslipsWrapper, Timeslip>
    {

        public override void Configure()
        {
            ExecuteCanGetList = false;
            ExecuteCanGetListWithContent = false;
            //ExecuteCanDeleteAndCleanup = false;

        }


        [Test]
        public void CanGetListOfTimeslips()
        {

            var list = Client.Timeslip.All(DateTime.Now.AddMonths(-6).ModelDateTime(), DateTime.Now.ModelDateTime());

            Assert.IsNotNull(list);

        }

        [Test]
        public void CanGetListOfTimeslipWithContent()
        {

            var list = Client.Timeslip.All(DateTime.Now.AddMonths(-6).ModelDateTime(), DateTime.Now.ModelDateTime());

            CheckAllList(list);


            foreach (var item in list)
            {
                CheckSingleItem(item);
            }
        }

        public override ResourceClient<TimeslipWrapper, TimeslipsWrapper, Timeslip> ResourceClient
        {
            get { return Client.Timeslip; }
        }

        public override void CheckSingleItem(Timeslip item)
        {

            Assert.IsNotNullOrEmpty(item.url);
            Assert.IsNotNullOrEmpty(item.dated_on);
            Assert.IsNotNullOrEmpty(item.project);
            Assert.IsNotNullOrEmpty(item.task);
            Assert.IsNotNullOrEmpty(item.user);

        }


        public override Timeslip CreateSingleItemForInsert()
        {
            var user = Client.User.Me;
            var project = Client.Project.All().First();
            var task = Client.Task.AllByProject(project.Id()).First();


            return new Timeslip {
                url = "",
                user = user.UrlId(),
                project = project.UrlId(),
                task = task.UrlId(),
                dated_on = DateTime.Now.ModelDate(),
                hours = 6.5,
                comment = "This is a TEST"
            };

        }

        public override void CompareSingleItem(Timeslip originalItem, Timeslip newItem)
        {
            Assert.IsNotNull(newItem);
            Assert.IsNotNullOrEmpty(newItem.url);
            Assert.IsTrue(newItem.user.EndsWith(originalItem.user));
            Assert.IsTrue(newItem.project.EndsWith(originalItem.project));
            Assert.IsTrue(newItem.task.EndsWith(originalItem.task));
            Assert.AreEqual(newItem.dated_on, originalItem.dated_on);
            Assert.AreEqual(newItem.hours, originalItem.hours);
        }

        public override bool CanDelete(Timeslip item)
        {
            //return false;
            if (string.IsNullOrEmpty(item.comment)) return false;
            return (item.comment.Contains("TEST"));

        }
    }
}

