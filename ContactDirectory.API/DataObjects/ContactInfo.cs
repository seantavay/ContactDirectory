using ContactDirectory.API.Enums;
using Microsoft.Azure.Mobile.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContactDirectory.API.DataObjects
{
    public class ContactInfo : EntityData
    {
        [ForeignKey("Contact")]
        public string ContactId { get; set; }
        [JsonIgnore]
        public virtual Contact Contact { get; set; }
        public string PhoneNumber { get; set; }
        public PhoneNumberType Type { get; set; }
    }
}