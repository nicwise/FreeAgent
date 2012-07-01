using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace FreeAgent.Tests
{
    [TestFixture]
    public class UserFixture : ResourceFixture<UserWrapper, UsersWrapper, User>
    {

        public override ResourceClient<UserWrapper, UsersWrapper, User> ResourceClient
        {
            get { return Client.User; }
        }


        public override void CheckSingleItem(User item)
        {

            Assert.IsNotNullOrEmpty(item.url);
            Assert.IsNotNullOrEmpty(item.first_name);
            Assert.IsNotNullOrEmpty(item.last_name);
            Assert.IsNotNullOrEmpty(item.email);
            Assert.IsNotNullOrEmpty(item.role);
            
        }


        public override User CreateSingleItemForInsert()
        {
            return new User
            {
                url = "",
                first_name = "Nic TEST",
                last_name = "Wise",
                email = "nic.wise@mycompany.com",
                permission_level = (int)UserPermission.Full,
                role = UserRole.Director
            };

        }

        public override void CompareSingleItem(User originalItem, User newItem)
        {
            Assert.IsNotNull(newItem);
            Assert.IsNotNullOrEmpty(newItem.url);
            Assert.AreEqual(newItem.first_name, originalItem.first_name);
            Assert.AreEqual(newItem.last_name, originalItem.last_name);
            Assert.AreEqual(newItem.email, originalItem.email);
        }

        public override bool CanDelete(User item)
        {
            return item.first_name.Contains("TEST");

        }

        [Test]
        public void CanLoadMe()
        {
            var me = Client.User.Me;

            Assert.IsNotNull(me);
            Assert.IsNotNullOrEmpty(me.first_name);
            Assert.IsNotNullOrEmpty(me.last_name);
            Assert.IsNotNullOrEmpty(me.email);
        }



    }
}
