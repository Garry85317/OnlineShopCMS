using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopCMS.Models
{
	public class Order
	{
		[DisplayFormat(DataFormatString = "{0:000000}")]
		public int Id { get; set; }

		public DateTime OrderDate { get; set; }        //訂單建立時間
		public string Name { get; set; }             //付款者名稱
		public string UserName { get; set; }           //付款者帳號
		public string UserId { get; set; }          //付款者ID
		[Required]
		public string ReceiverName { get; set; }       //收貨者姓名
		[Required]
		public string ReceiverAdress { get; set; }     //收貨者地址
		[Required]
		public string ReceiverPhone { get; set; }      //收貨者電話

		public int Total { get; set; }                 //訂單總額
		public bool isPaid { get; set; }               //付款狀態
		public List<OrderItem> OrderItem { get; set; } //訂單內容
	}

	public class OrderItem
	{
		public int Id { get; set; }
		public int OrderId { get; set; }
		public int ProductId { get; set; }  //商品ID
		public int Amount { get; set; }     //數量
		public int SubTotal { get; set; }   //小計
	}

	public class CartItem : OrderItem
	{
		public Product Product { get; set; } //商品內容
		public string imageSrc { get; set; } //商品圖片
		public CartItem(OrderItem order)
		{
			this.OrderId = order.OrderId;
			this.ProductId = order.ProductId;
			this.Amount = order.Amount;
			this.SubTotal = order.SubTotal;
		}
		public CartItem() { }
	}
}
