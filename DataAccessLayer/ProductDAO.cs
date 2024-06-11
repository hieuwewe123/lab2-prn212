using System;
using System.Collections.Generic;
using BusinessObjects;
using System.Linq;

namespace DataAccessLayer
{
    public class ProductDAO
    {
        private static List<Product> listProducts;

        static ProductDAO()
        {
            // Initializing the in-memory database
            listProducts = new List<Product>
            {
                new Product(1, "Chai", 3, 12, 18),
                new Product(2, "Chang", 1, 23, 19),
                new Product(3, "Aniseed Syrup", 2, 23, 10)
                // You can uncomment and add more products here if needed
                // new Product(4, "Chef Anton's Cajun Seasoning", 2, 34, 22),
                // new Product(5, "Chef Anton's Gumbo Mix", 2, 45, 34),
                // new Product(6, "Grandma's Boysenberry Spread", 2, 21, 25),
                // new Product(7, "Uncle Bob's Organic Dried Pears", 7, 22, 30),
                // new Product(8, "Northwoods Cranberry Sauce", 2, 10, 40),
                // new Product(9, "Mishi Kobe Niku", 6, 12, 97),
                // new Product(10, "Ikura", 8, 13, 32)
            };
        }

        public static List<Product> GetProducts()
        {
            return listProducts;
        }

        public static void SaveProduct(Product p)
        {
            listProducts.Add(p);
        }

        public static void UpdateProduct(Product product)
        {
            var existingProduct = listProducts.FirstOrDefault(p => p.ProductId == product.ProductId);
            if (existingProduct != null)
            {
                existingProduct.ProductName = product.ProductName;
                existingProduct.UnitPrice = product.UnitPrice;
                existingProduct.UnitsInStock = product.UnitsInStock;
                existingProduct.CategoryId = product.CategoryId;
            }
        }

        public static void DeleteProduct(Product product)
        {
            var existingProduct = listProducts.FirstOrDefault(p => p.ProductId == product.ProductId);
            if (existingProduct != null)
            {
                listProducts.Remove(existingProduct);
            }
        }

        public static Product GetProductById(int id)
        {
            return listProducts.FirstOrDefault(p => p.ProductId == id);
        }
    }
}
