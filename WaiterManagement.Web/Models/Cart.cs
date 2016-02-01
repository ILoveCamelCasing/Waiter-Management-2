using System.Collections.Generic;
using System.Linq;

namespace WaiterManagement.Web.Models
{
	public class Cart
	{
		private readonly Dictionary<int,CartItem> _cartItems;
		public IEnumerable<CartItem> CartItems => _cartItems.Select(x => x.Value);

		public Cart()
		{
			_cartItems = new Dictionary<int, CartItem>();
		}

		public decimal ComputeTotalValue()
		{
			return _cartItems.Sum(x => x.Value.Quantity*x.Value.ItemPrice);
		}

		public void Add(int itemId, string itemname, decimal itemPrice, int quantity = 1)
		{
			CartItem item;
			if (!_cartItems.TryGetValue(itemId,out item))
			{
				item = new CartItem() {ItemId = itemId, ItemName = itemname, ItemPrice = itemPrice};
				_cartItems.Add(itemId,item);
			}

			item.Quantity += quantity;
		}
	}

	public class CartItem
	{
		public string ItemName { get; set; }
		public int ItemId { get; set; }
		public int Quantity { get; set; }
		public decimal ItemPrice { get; set; }
	}
}