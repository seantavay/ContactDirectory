using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContactDirectory.API.DataObjects
{
    public class Contact : EntityData
    {
        public string Name { get; set; }
        public string Company { get; set; }
        public byte[] ProfileImageId { get; set; }
        public DateTime BirthDate { get; set; }
        [ForeignKey("Address")]
        public string AddressId { get; set; }
        public virtual Address Address { get; set; }
        public virtual ICollection<ContactInfo> PhoneNumbers { get; set; }

    }
}