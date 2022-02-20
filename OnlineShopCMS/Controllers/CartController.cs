using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShopCMS.Models;
using OnlineShopCMS.Data;
using OnlineShopCMS.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopCMS.Controllers
{
	public class CartController : Controller
	{
		private readonly OnlineShopContext _context;

		public CartController(OnlineShopContext context)
		{
			_context = context;
		}

		public IActionResult AddtoCart(int id)
		{
			var product = _context.Product.Single(x => x.Id.Equals(id));
			//取得商品資料
			CartItem item = new CartItem
			{
				ProductId = product.Id,
				Product = product,
				Amount = 1,
				SubTotal = product.Price,
				imageSrc = ViewImage(product.Image)
			};

			//判斷 Session 內有無購物車
			if (SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "Cart") == null)
			{
				//如果沒有已存在購物車: 建立新的購物車
				List<CartItem> cart = new List<CartItem>();
				cart.Add(item);
				SessionHelper.SetObjectAsJson(HttpContext.Session, "Cart", cart);
			}
			else
			{
				//如果已存在購物車: 檢查有無相同的商品，有的話只調整數量
				List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "Cart");

				int index = cart.FindIndex(x => x.Product.Id.Equals(id));
				if (index != -1)
				{
					cart[index].Amount += item.Amount;
					cart[index].SubTotal += item.SubTotal;
				}
				else
				{
					cart.Add(item);
				}

				SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
			}

			return NoContent(); // HttpStatus 204: 請求成功但不更新畫面
		}

		public IActionResult RemoveItem(int id)
		{
			//向 Session 取得商品列表
			List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "Cart");

			//用FindIndex查詢目標在List裡的位置
			int index = cart.FindIndex(x => x.Product.Id.Equals(id));
			cart.RemoveAt(index);

			if (cart.Count < 1)
				SessionHelper.Remove(HttpContext.Session, "cart");
			else
				SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

			return RedirectToAction("Index");
		}

		public IActionResult Index()
		{
			//向 Session 取得商品列表
			List<CartItem> CartItems = SessionHelper.
				GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");

			//計算商品總額
			if (CartItems != null)
			{
				ViewBag.Total = CartItems.Sum(m => m.SubTotal);
			}
			else
			{
				ViewBag.Total = 0;
			}

			return View(CartItems);
		}

		private string ViewImage(byte[] arrayImage)
		{
			// 二進位圖檔轉字串
			string base64String = Convert.ToBase64String(arrayImage, 0, arrayImage.Length);
			return "data:image/png;base64," + base64String;
		}

	}
}
