namespace ContactDirectory.API.Migrations
{
    using ContactDirectory.API.DataObjects;
    using Microsoft.Azure.Mobile.Server.Tables;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ContactDirectory.API.Models.MobileServiceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            SetSqlGenerator("System.Data.SqlClient", new EntityTableSqlGenerator());
        }

        protected override void Seed(ContactDirectory.API.Models.MobileServiceContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            Address address = new Address()
            {
                Id = Guid.NewGuid().ToString(),
                AddressLine1 = "123 Kinzie St",
                AddressLine2 = "",
                City = "Chicago",
                State = "Illinois",
                PostalCode = "60657",
                Country = "United States"
            };
            context.Addresses.AddOrUpdate(address);
            context.SaveChanges();

            var myExampleAddress = context.Addresses.Where(u => u.PostalCode == "60657" && u.AddressLine1 == "123 Kinzie St").FirstOrDefault();
            Contact contact = new Contact()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Sean Tavakoli",
                Company = "Dow Chemical",
                ProfileImageId = new byte[0],
                BirthDate = new DateTime(1990, 1, 1),
                AddressId = myExampleAddress.Id

            };
            context.Contacts.AddOrUpdate(contact);
            context.SaveChanges();

            var myExampleContact = context.Contacts.Where(u => u.Name == "Sean Tavakoli" && u.Company == "Dow Chemical").FirstOrDefault();
            ContactInfo info = new ContactInfo()
            {
                Id = Guid.NewGuid().ToString(),
                ContactId = myExampleContact.Id,
                PhoneNumber = "123-111-1111",
                Type = Enums.PhoneNumberType.Work
            };
            context.ContactInfo.AddOrUpdate(info);
            context.SaveChanges();
        }
    }
}
