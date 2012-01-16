using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for CeceliaDataProvider
/// </summary>
/// 
namespace Cecelia {
    public class CeceliaDataProvider {
        
        CeceliaDbLib _dbLib = null;

        #region Public Methods

        public CeceliaDataProvider() {
            _dbLib = new CeceliaDbLib("ConnectionString");
        }
        public List<Product> SearchProducts(string searchString, bool isCategory) {
            DataTable result = _dbLib.SearchProducts(searchString, isCategory);
            return ParseProducts(result);
        }
        public List<Product> SearchProducts(string Category, string Company, bool IsBoth) {
            DataTable result = _dbLib.SearchProducts(Category, Company, IsBoth);
            return ParseProducts(result);
        }
        public List<Product> GetAllProducts() {
            DataTable result = _dbLib.GetAllProducts();
            List<Product> products = this.ParseProducts(result);
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex(@"[ ]{2,}", options);
            foreach (Product p in products) {
                p.Category = regex.Replace(p.Category, @" ");
                p.CompanyName = regex.Replace(p.CompanyName, @" ");
                p.Flavor = regex.Replace(p.Flavor, @" ");
                p.Type1 = regex.Replace(p.Type1, @" ");
                p.Type2 = regex.Replace(p.Type2, @" ");
            }
            return products;
        }
        public bool AddProduct(Product p) {
            DataTable result = _dbLib.AddProduct(p);
            if (result != new DataTable()) {
                return true;
            }
            return false;
        }
        public bool UpdateProduct(Product p) {

            DataTable result = _dbLib.UpdateProduct(p);
            if (result != new DataTable()) {
                return true;
            }
            return false;
        }
        public bool DeleteProduct(long ProductId) {
            DataTable result = _dbLib.DeleteProduct(ProductId);
            if (result != new DataTable()) {
                return true;
            }
            return false;
        }
        public Users Login(string username) {
            try {
                DataTable result = _dbLib.Login(username);
                return ParseUser(result);
            } catch { throw; }
        }
        public bool AddUser(Users u) {
            DataTable result = _dbLib.AddUser(u);
            long Id = 0;
            if (result != null && result.Rows.Count > 0) {
                foreach (DataRow row in result.Rows) {
                    Id = long.Parse(row["Id"].ToString());
                }
            }
            if (Id == 0) {
                return false;
            } else {
                return true;
            }
        }

        #endregion

        #region Private Methods

        private Users ParseUser(DataTable result) {
            Users u;
            if (result != null && result.Rows.Count > 0) {
                foreach (DataRow row in result.Rows) {
                    u = new Users();
                    u.Id = int.Parse(row["Id"].ToString());
                    u.UserId = row["User_Id"].ToString();
                    u.UserName = row["User_Name"].ToString();
                    u.Password = row["Password"].ToString();
                    string role = row["Role"].ToString();
                    u.Role = (Role)Enum.Parse(typeof(Role), role);
                    try {
                        u.TimeStamp = DateTime.Parse(row["LastLogin"].ToString());
                    } catch {
                        u.TimeStamp = DateTime.Now;
                    }
                    return u;
                }
            }
            return new Users();
        }
        private List<Product> ParseProducts(DataTable result) {
            List<Product> products = new List<Product>();
            if (result != null && result.Rows.Count > 0) {
                foreach (DataRow row in result.Rows) {
                    products.Add(this.ParseProduct(row));
                }
            }
            return products;
        }
        private Product ParseProduct(DataRow row) {
            Product p = new Product();
            p.Id = long.Parse(row["Id"].ToString());
            p.Category = row["Category"].ToString();
            p.CompanyName = row["Company"].ToString();
            p.Flavor = row["Flavor"].ToString();
            p.Type1 = row["Typ1"].ToString();
            p.Type2 = row["Typ2"].ToString();
            p.CF = bool.Parse(row["CF"].ToString());
            p.SF = bool.Parse(row["SF"].ToString());
            p.FAC = bool.Parse(row["FC"].ToString());
            p.CRT = bool.Parse(row["CT"].ToString());

            try {
                p.LastUpdated = DateTime.Parse(row["Time"].ToString());
            } catch {
                p.LastUpdated = DateTime.MinValue;
            }
            try {
                p.User = row["User"].ToString();
            } catch {
                p.User = string.Empty;
            }
            return p;
        }

        #endregion
    }
}