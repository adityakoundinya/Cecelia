using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProductWorker
/// </summary>
/// 
namespace Cecelia {
    public class ProductWorker {

        CeceliaDataProvider _dp = null;

        public ProductWorker() {
            _dp = new CeceliaDataProvider();
        }

        public bool AddProduct(Product p) {

            return _dp.AddProduct(p);
        }
        public bool UpdateProduct(Product p) {
            return _dp.UpdateProduct(p);
        }
        public List<Product> SearchProduct(string searchString, bool isCategory) {
            return _dp.SearchProducts(searchString, isCategory);
        }
        public List<Product> SearchProduct(string Category, string Company, bool IsBoth) {
            return _dp.SearchProducts(Category, Company, IsBoth);
        }
        public bool DeleteProduct(long ProductId) {
            return this._dp.DeleteProduct(ProductId);
        }
        public List<Product> GetAllProducts() {
            return _dp.GetAllProducts();
        }

        public List<Product> GetUnEditedProducts() {
            return _dp.GetUnEditedProducts();
        }
        public int ResetDatabase() {
            return _dp.ResetDatabase();
        }
    }
}