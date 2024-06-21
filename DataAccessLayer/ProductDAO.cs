using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects_.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObjects
{
    public class ProductDAO
    {
        public static List<Product> GetProducts()
        {
            var listProducts = new List<Product>();
            var listCartegory = new List<Category>();
            try
            {
                using var context = new MyStoreContext();
                listProducts = context.Products.ToList();
                 
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            return listProducts;

        }
        public static void SaveProduct(Product product)
        {
            try
            {
                using var context = new MyStoreContext();
                context.Products.Add(product);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }
        public static void UpdateProduct(Product p)
        {
            try
            {
                using var context = new MyStoreContext();
                context.Entry<Product>(p).State
                    = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }
        public static void DeleteProduct(Product p)
        {
            try
            {
                using var context = new MyStoreContext();

                // Update CategoryID to null for the product before deletion
                context.Database.ExecuteSqlRaw(
                    $"UPDATE Products SET CategoryID = NULL WHERE ProductID = {p.ProductId}; " +
                    $"DELETE FROM Products WHERE ProductID = {p.ProductId};"
                );
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }
        /*  public static void DeleteProduct(Product p)
          {
              try
              {
                  using var context = new MyStoreContext();
                  context.Products.SingleOrDefault(c => c.ProductId == p.ProductId);
                  context.Products.Remove(p);
                  context.SaveChanges();
              }
              catch (Exception ex)
              {
                  throw new Exception("Error: " + ex.Message);
              }
          }*/
        public static Product GetProductById(int id)
        {
            using var db = new MyStoreContext();
            return db.Products.FirstOrDefault(c => c.ProductId == id);
        }

    }

}
