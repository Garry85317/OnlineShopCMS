using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShopCMS.Data;
using OnlineShopCMS.Models;

namespace OnlineShopCMS.Controllers
{
    public class CollectionsController : Controller
    {
        private readonly OnlineShopContext _context;

        public CollectionsController(OnlineShopContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index(int? cId, string searchString)
        {
            List<DetailViewModel> dvm = new List<DetailViewModel>();
            List<Product> products = new List<Product>();
            if (cId != null)
            {
                var result = await _context.Category.SingleAsync(x => x.Id.Equals(cId));
                products = await _context.Entry(result).Collection(x => x.Products).Query().ToListAsync();
            }
            else
            {
                products = await _context.Product.Include(p => p.Category).ToListAsync();
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                foreach (var product in products)
                {
                    if (product.Name.Contains(searchString))
                    {
                        DetailViewModel item = new DetailViewModel
                        {
                            product = product,
                            imgsrc = ViewImage(product.Image)
                        };
                        dvm.Add(item);
                    }
                }
                ViewBag.count = dvm.Count;

                return View(dvm);
            }

            foreach (var product in products)
            {
                DetailViewModel item = new DetailViewModel
                {
                    product = product,
                    imgsrc = ViewImage(product.Image)
                };
                dvm.Add(item);
            }
            ViewBag.count = dvm.Count;

            return View(dvm);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            DetailViewModel dvm = new DetailViewModel();

            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                dvm.product = product;
                dvm.imgsrc = ViewImage(product.Image);
            }

            return View(dvm);
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
        
        private string ViewImage(byte[] arrayImage)
        {
            // 二進位圖檔轉字串
            string base64String = Convert.ToBase64String(arrayImage, 0, arrayImage.Length);
            return "data:image/png;base64," + base64String;
        }
    }
}
