using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace FreeAgent.Tests
{
    [TestFixture]
    public class TaskFixture : BaseFixture
    {
        [SetUp]
        public void Setup()
        {
            SetupClient();
        }


        [Test]
        public void CanGetTasksForProject()
        {
            var project = Client.Project.All().First();

            var tasks = Client.Task.AllByProject(project.Id());

            Assert.IsNotEmpty(tasks);

        }


        [Test]
        public void CanGetSingleTaskForProject()
        {
            var project = Client.Project.All().First();

            var tasks = Client.Task.AllByProject(project.Id());

            Assert.IsNotEmpty(tasks);

            foreach(var task in tasks)
            {
                var newtask = Client.Task.Get(task.Id());
                Assert.IsNotNull(newtask);
                Assert.IsNotNullOrEmpty(newtask.name);
            }
        }

        [Test]
        public void CanCreateSingleTaskForProject()
        {
            var project = Client.Project.All().First();

            var task = new Task
            {
                name = "Task TEST",
                is_billable = true,
                billing_rate = 400,
                billing_period = TaskBillingPeriod.Day,
                status = TaskStatus.Active,
                project = project.UrlId()

            };

            var newtask = Client.Task.Put(task, project.Id());

            CompareSingleItem(task, newtask);
          
        }

        public void CompareSingleItem(Task originalItem, Task newItem)
        {
            Assert.IsNotNull(newItem);
            Assert.IsNotNullOrEmpty(newItem.url);
     
            Assert.AreEqual(originalItem.name, newItem.name);
            Assert.AreEqual(originalItem.billing_period, newItem.billing_period);
            Assert.AreEqual(originalItem.billing_rate, newItem.billing_rate);
            Assert.AreEqual(originalItem.status, newItem.status);
            Assert.AreEqual(originalItem.project, newItem.project);
        }

        [Test]
        public void CanDeleteAndCleanup()
        {
            var project = Client.Project.All().First();

            var tasks = Client.Task.AllByProject(project.Id());

            foreach (var item in tasks)
            {
                if (!item.name.Contains("TEST")) continue;

                Client.Task.Delete(item.Id());

                var deleted = Client.Task.Get(item.Id());

                Assert.IsNull(deleted);
            }
        }


        /*
        public override ResourceClient<TaskWrapper, TasksWrapper, Task> ResourceClient
        {
            get { return Client.Task; }
        }


        public override void CheckSingleItem(Task item)
        {

            Assert.IsNotNullOrEmpty(item.url);

        }


        public override Task CreateSigleItemForInsert()
        {
            var project = Client.Project.All().First();

            Assert.IsNotNull(project);

            //find a project for this contact

            return new Task {
                name = "Task TEST",
                is_billable = true,
                billing_rate = 400,
                billing_period = TaskBillingPeriod.Day,
                status = TaskStatus.Active,
                project = project.UrlId()
            };

        }

        public override void CompareSingleItem(Task originalItem, Task newItem)
        {
            Assert.IsNotNull(newItem);
            Assert.IsNotNullOrEmpty(newItem.url);
     
        }

        public override bool CanDelete(Task item)
        {
            return (item.name.Contains("TEST"));
        }

*/
    }


}
