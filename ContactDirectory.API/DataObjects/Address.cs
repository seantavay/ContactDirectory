using Microsoft.Azure.Mobile.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContactDirectory.API.DataObjects
{
    public class Address : EntityData
    {
        public string City { get; set; }
        public string State { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }
}