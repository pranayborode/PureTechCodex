using PureTechCodex.Models;
using PureTechCodex.Repositorie.IRepositories;
using PureTechCodex.Service.IService;

namespace PureTechCodex.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository _productRepository)
        {
            this._productRepository = _productRepository;
        }

        public int AddProduct(Product product)
        {
           return _productRepository.AddProduct(product);
        }

        public int DeleteProduct(int id)
        {
           return (_productRepository.DeleteProduct(id));
        }

        public int EditProduct(Product product)
        {
            return _productRepository.EditProduct(product);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }

        public Product GetProductById(int id)
        {
            return _productRepository.GetProductById(id);
        }
    }
}
