using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PureTechCodex.Models;
using PureTechCodex.Service.IService;
using PureTechCodex.ViewModels;

namespace PureTechCodex.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _iHostEnv;

        public ProductController(
            IProductService _productService,
            Microsoft.AspNetCore.Hosting.IHostingEnvironment _iHostEnv)
        {
            this._productService = _productService;
            this._iHostEnv = _iHostEnv;
        }

        // GET: ProductController
        public ActionResult Ind ex()
        {
            var model = _productService.GetAllProducts();
            return View(model);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            var model = _productService.GetProductById(id);
            return View(model);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel product, IFormFile file)
        {
            try
            {
                using (var fs = new FileStream(_iHostEnv.WebRootPath + "\\uploads\\" + file.FileName, FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(fs);
                }

                product.ImagePath = "~/uploads/" + file.FileName;

                var pro = new Product
                {
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    ProductImage = product.ImagePath
                };

                int result = _productService.AddProduct(pro);

                if (result >= 1)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.ErrorMsg = "Something went wrong...";
                    return View(product);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            var product = _productService.GetProductById(id);
            HttpContext.Session.SetString("oldImageUrl", product.ProductImage);
            if (product == null)
            {
                return NotFound();
            }
            var viewModel = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImagePath = product.ProductImage
            };
            return View(viewModel);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductViewModel product, IFormFile file)
        {
            try
            {
                string oldImageUrl = HttpContext.Session.GetString("oldImageUrl");

                if (file != null)
                {
                    using (var fs = new FileStream(_iHostEnv.WebRootPath + "\\uploads\\" + file.FileName, FileMode.Create, FileAccess.Write))
                    {
                        file.CopyTo(fs);
                    }

                    product.ImagePath = "~/uploads/" + file.FileName;

                    string[] str = oldImageUrl.Split("/");
                    string str1 = (str[str.Length - 1]);
                    string path = _iHostEnv.WebRootPath + "\\uploads\\" + str1;
                    System.IO.File.Delete(path);
                }
                else
                {
                    product.ImagePath = oldImageUrl;
                }

                Product pro = new Product
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    ProductImage = product.ImagePath
                };

                int result = _productService.EditProduct(pro);

                if (result >= 1)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.ErrorMsg = "Something went wrong...";
                    return View(product);
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(product);
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = _productService.GetProductById(id);
            HttpContext.Session.SetString("oldImageUrl", model.ProductImage);
            return View(model);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                string oldImageUrl = HttpContext.Session.GetString("oldImageUrl");

                if (id == null || _productService.GetAllProducts == null)
                {
                    return NotFound();
                }

                var result = _productService.DeleteProduct(id);

                if (result >= 1)
                {
                    string[] str = oldImageUrl.Split("/");
                    string str1 = (str[str.Length - 1]);
                    string path = _iHostEnv.WebRootPath + "\\uploads\\" + str1;
                    System.IO.File.Delete(path);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.ErrorMsg = "Somthing went wrong...";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View();
            }
        }
    }
}
