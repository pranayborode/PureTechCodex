using Microsoft.EntityFrameworkCore;
using PureTechCodex.Data;
using PureTechCodex.Models;
using PureTechCodex.Repositorie.IRepositories;

namespace PureTechCodex.Repositorie
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext _context)
        {
            this._context = _context;
        }
        public int AddProduct(Product product)
        {
            _context.Products.Add(product);
            int result = _context.SaveChanges();
            return result;
        }

        public int DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                int result = _context.SaveChanges();
                return result;
            }
            else
            {
                return 0;
            }
        }

        public int EditProduct(Product product)
        {
            var pro = _context.Products.Find(product.Id);
            if (pro != null)
            {
                pro.Name = product.Name;
                pro.Price = product.Price;
                pro.Description = product.Description;
                pro.ProductImage = product.ProductImage;
                int result = _context.SaveChanges();
                return result;

            }
            else
            {
                return 0;
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public Product GetProductById(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                return product;
            }
            else
            {
                return null;
            }
        }
    }
}
