using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace FreeAgent.Tests
{
   
    public class ResourceFixture<TSingleWrapper, TListWrapper, TSingle> : BaseFixture
        where TSingle : BaseModel
        where TListWrapper : new()
        where TSingleWrapper : new()
    {
        protected bool ExecuteCanGetList = true, ExecuteCanGetListWithContent = true, ExecuteCanLoadById = true, ExecuteCanCreateSingle = true, ExecuteCanDeleteAndCleanup = true;

        public override void Configure()
        {

        }

        [SetUp]
        public void Setup()
        {
            SetupClient();
        }

        [Test]
        public void CanGetList()
        {
            if (!ExecuteCanGetList) 
            {
                Assert.Ignore("CanGetList is being ignored");
                return;
            }

            var list = ResourceClient.All();

            Assert.IsNotNull(list);

        }

        [Test]
        public void CanGetListWithContent()
        {
            if (!ExecuteCanGetListWithContent) 
            {
                Assert.Ignore("ExecuteCanGetListWithContent is being ignored");
                return;
            }

            var list = ResourceClient.All();

            CheckAllList(list);


            foreach (var item in list)
            {
                CheckSingleItem(item);
            }
        }

        public void CheckAllList(IEnumerable<TSingle> list)
        {
            Assert.IsNotNull(list);
            Assert.IsNotEmpty(list);

        }

        public virtual void CheckSingleItem(TSingle item)
        {
            
        }

        [Test]
        public void CanCreateSingle()
        {
            if (!ExecuteCanCreateSingle)
            {
                Assert.Ignore("ExecuteCanCreateSingle is being ignored");
                return;
            }


            TSingle item = CreateSingleItemForInsert();

            TSingle result = ResourceClient.Put(item);

            CompareSingleItem(item, result);

        }

        [Test]
        public void CanLoadById()
        {
            if (!ExecuteCanLoadById)
            {
                Assert.Ignore("ExecuteCanLoadById is being ignored");
                return;
            }

            var items = ResourceClient.All();
            CheckAllList(items);

            foreach (var item in items)
            {
                var newitem = ResourceClient.Get(item.Id());

                CompareSingleItem(item, newitem);
            }
        }


        public virtual TSingle CreateSingleItemForInsert()
        {
            throw new NotImplementedException("needs to be overridden");
        }

        public virtual void CompareSingleItem(TSingle originalItem, TSingle newItem)
        {
            throw new NotImplementedException("needs to be overridden");
        }





        [Test]
        public void CanDeleteAndCleanup()
        {

            if (!ExecuteCanDeleteAndCleanup)
            {
                Assert.Ignore("ExecuteCanDeleteAndCleanup is being ignored");
                return;
            }

            var items = ResourceClient.All();

            CheckAllList(items);
            foreach (var item in items)
            {
                if (!CanDelete(item)) continue;

                ResourceClient.Delete(item.Id());

                var deletedclient = ResourceClient.Get(item.Id());

                Assert.IsNull(deletedclient);
            }
        }

        public virtual bool CanDelete(TSingle item)
        {
            return false;
        }

        public virtual ResourceClient<TSingleWrapper, TListWrapper, TSingle> ResourceClient { get { throw new NotImplementedException("oops!");} }
    }
}
