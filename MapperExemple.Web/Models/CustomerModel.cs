﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MapperExemple.Web.Models
{
    public class CustomerModel
    {
        
        public string CompanyName { get; set; } // CompanyName
        public string ContactName { get; set; } // ContactName
        public string ContactTitle { get; set; } // ContactTitle
        public string Address { get; set; } // Address
        public string City { get; set; } // City
        public string Region { get; set; } // Region
        public string PostalCode { get; set; } // PostalCode
        public string Country { get; set; } // Country
        public string Phone { get; set; } // Phone
        public string Fax { get; set; } // Fax
        public CustomerModel()
        {
            
        }
    }
}