using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Product
/// </summary>
/// 
namespace Cecelia {
    public class Product {
        public long Id { get; set; }
        public string CompanyName { get; set; }
        public string Category { get; set; }
        public string Type1 { get; set; }
        public string Type2 { get; set; }
        public string Flavor { get; set; }
        public bool CF { get; set; }
        public bool SF { get; set; }
        public bool CRT { get; set; }
        public bool FAC { get; set; }
        public DateTime LastUpdated { get; set; }
        public string User { get; set; }

        public Product() {

        }
    }

    
}