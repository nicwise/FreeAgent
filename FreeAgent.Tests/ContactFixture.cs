using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace FreeAgent.Tests
{
    [TestFixture]
    public class ContactFixture : ResourceFixture<ContactWrapper, ContactsWrapper, Contact>
    {

        public override ResourceClient<ContactWrapper, ContactsWrapper, Contact> ResourceClient
        {
            get { return Client.Contact; }
        }


        public override void CheckSingleItem(Contact item)
        {

            Assert.IsNotNullOrEmpty(item.url);
            //Assert.IsNotNullOrEmpty(contact.organisation_name);
            //Assert.IsNotNullOrEmpty(contact.first_name);
            //Assert.IsNotNullOrEmpty(contact.last_name);
        }


        public override Contact CreateSigleItemForInsert()
        {
            return new Contact
            {
                url = "",
                first_name = "Nic TEST",
                last_name = "Wise",
                organisation_name = "foo",
                address1 = DateTime.Now.ToLongTimeString()
            };

        }

        public override void CompareSingleItem(Contact originalItem, Contact newItem)
        {
            Assert.IsNotNull(newItem);
            Assert.IsNotNullOrEmpty(newItem.url);
            Assert.AreEqual(newItem.first_name, originalItem.first_name);
            Assert.AreEqual(newItem.last_name, originalItem.last_name);
            Assert.AreEqual(newItem.address1, originalItem.address1);
        }

        public override bool CanDelete(Contact item)
        {
            return item.first_name.Contains("TEST");

        }
        

        
    }
}
