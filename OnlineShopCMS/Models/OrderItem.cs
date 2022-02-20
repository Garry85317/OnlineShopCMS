namespace OnlineShopCMS.Models
{
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
	}
}
