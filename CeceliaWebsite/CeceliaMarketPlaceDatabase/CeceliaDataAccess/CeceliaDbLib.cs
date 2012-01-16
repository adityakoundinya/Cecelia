using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

/// <summary>
/// Summary description for Class1
/// </summary>
/// 
namespace Cecelia {
    public partial class CeceliaDbLib : DataAccess {
        public CeceliaDbLib(string ConnectionString)
            : base(ConnectionString) {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataTable SearchProducts(string searchString, bool isCategory) {
            if (isCategory) {
                return this.ExecuteQuery("SearchCategory", new object[] { searchString });
            } else {
                return this.ExecuteQuery("SearchCompany", new object[] { searchString });
            }
        }

        public DataTable SearchProducts(string category, string company, bool isBoth) {
            if (isBoth) {
                return this.ExecuteQuery("SearchCategoryAndCompany", new object[] { category, company });
            } else {
                return this.ExecuteQuery("FindProducts", new object[] { category, company });
            }
        }

        public DataTable AddProduct(Product p) {
            try {
                return this.ExecuteQuery("AddProduct", new object[] { 0, p.Category, p.CompanyName, p.Flavor, p.Type1, p.Type2, p.CF, p.SF, p.CRT, p.FAC, p.LastUpdated, p.User });
            } catch { return new DataTable(); }
        }

        public DataTable UpdateProduct(Product p) {
            try {
                return this.ExecuteQuery("AddProduct", new object[] { p.Id, p.Category, p.CompanyName, p.Flavor, p.Type1, p.Type2, p.CF, p.SF, p.CRT, p.FAC, p.LastUpdated, p.User });
            } catch { return new DataTable(); }
        }

        public DataTable DeleteProduct(long ProductId) {
            try {
                return this.ExecuteQuery("DeleteProduct", new object[] { ProductId });
            } catch { return new DataTable(); }
        }

        public DataTable Login(string username) {
            try {
                return this.ExecuteQuery("Login", new object[] { username });
            } catch { throw; }
        }

        public DataTable AddUser(Users user) {
            try {
                return this.ExecuteQuery("AddUser", new object[] { user.UserName, user.UserId, user.Password, user.Role.ToString() });
            } catch { return new DataTable(); }
        }

        public DataTable GetAllProducts() {
            try {
                return this.ExecuteQuery("GetAllProducts", new object[] { });
            } catch {
                return new DataTable();
            }
        }
    }
}