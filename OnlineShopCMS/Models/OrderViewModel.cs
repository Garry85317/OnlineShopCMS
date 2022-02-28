using System.Collections.Generic;

namespace OnlineShopCMS.Models
{
	public class OrderViewModel
	{
		public Order Order { get; set; }
		public List<CartItem> CartItems { get; set; }
	}
}
