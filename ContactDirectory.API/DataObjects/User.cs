using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactDirectory.API.DataObjects
{
    public class User
    {
        public Contact contact { get; set; }
        public ContactInfo info { get; set; }
        public Address address { get; set; }      
    }
}