using ContactDirectory.API.DataObjects;
using ContactDirectory.API.Models;
using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;

namespace ContactDirectory.API.Controllers
{
    public class ContactsController : TableController<Contact>
    {
        private MobileServiceContext context;

        public ContactsController()
        {
            context = new MobileServiceContext();
        }
        // GET: tables/Contact
        /// <summary>
        /// Returns all User objects from Contact, ContactInfo, and Address tables
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Contact> Get()
        {
            IEnumerable<Contact> contacts = context.Contacts.Include("Address").ToList();
            return contacts;
        }

        // GET: tables/Contact/5
        /// <summary>
        /// Return signel user object based on Contact table PK
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Contact Get(string id)
        {
            Contact contact = context.Contacts.Include("Address").Where(u => u.Id == id).FirstOrDefault();
            return contact;
        }

        // POST: tables/Contact
        /// <summary>
        /// Creates new user row in Contact, ContactInfo, and Address Tables
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostAsync(User user)
        {
            //if (!Request.Content.IsMimeMultipartContent())
            //{
            //    return Request.CreateErrorResponse(HttpStatusCode.UnsupportedMediaType, "The request doesn't contain valid content!");
            //}
            try
            {
                if (user != null)
                {
                    var provider = new MultipartMemoryStreamProvider();
                    await Request.Content.ReadAsMultipartAsync(provider);
                    foreach (var file in provider.Contents)
                    {
                        var photoByteArray = await file.ReadAsByteArrayAsync();
                        user.contact.ProfileImageId = photoByteArray;

                        var addressId = Guid.NewGuid().ToString();
                        var contactId = Guid.NewGuid().ToString();

                        //setup PK/FK relationship
                        user.address.Id = addressId;
                        user.contact.Id = contactId;
                        user.contact.AddressId = addressId;
                        user.info.ContactId = contactId;

                        //add payload to DB
                        context.Addresses.Add(user.address);
                        context.Contacts.Add(user.contact);
                        context.ContactInfo.Add(user.info);

                        //async save changes to DB
                        await context.SaveChangesAsync();

                        var response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent("Successful upload", Encoding.UTF8, "text/plain");
                        response.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue(@"text/html");
                        return response;
                    }
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No contact to add!");
                }

            }
            catch(Exception e)
            {
                //still add contact row to DB even if issue with photo upload request
                try
                {
                    context.Contacts.Add(user.contact);
                    await context.SaveChangesAsync();
                }
                catch(Exception err)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, err.Message);
                }
            }
            return
                Request.CreateResponse(HttpStatusCode.OK, user);
        }

        /// <summary>
        /// Updates delta of current resource
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patch"></param>
        /// <returns></returns>
        public Task<Contact> PatchContact(string id, Delta<Contact> patch)
        {
            return UpdateAsync(id, patch);
        }


        // DELETE: api/Contact/5
        /// <summary>
        /// Delete user row from Contact, ContactInfo, and Address Tables
        /// </summary>
        /// <param name="id"></param>
        public Object DeleteContact(string id)
        {
            Contact currentCandidate = context.Contacts.Include("Address").FirstOrDefault(c => c.Id == id);

            try
            {
                var info = context.ContactInfo.Where(u => u.ContactId == currentCandidate.Id).FirstOrDefault();
                context.Addresses.Remove(currentCandidate.Address);
                context.Contacts.Remove(currentCandidate);
                context.ContactInfo.Remove(info);
                context.SaveChanges();

            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.InnerException);
            }

            return Ok();
        }
    }
}
