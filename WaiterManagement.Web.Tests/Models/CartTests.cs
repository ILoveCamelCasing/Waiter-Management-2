using WaiterManagement.Web.Models;
using Xunit;

namespace WaiterManagement.Web.Tests.Models
{
	public class CartTests
	{
		[Fact]
		public void ComputeTotalValue_EmptyCart_ReturnZero()
		{
			Assert.Equal(0, new Cart().ComputeTotalValue());
		}

		[Fact]
		public void ComputeTotalValue_ItemsWithQuantitySetAsOne_ReturnCalculated()
		{
			var cart = new Cart();
			cart.Add(2, "itemName", (decimal)20.30);
			cart.Add(3, "itemName", (decimal)15.30);
			
			Assert.Equal((decimal) 35.60, cart.ComputeTotalValue());
		}

		[Fact]
		public void ComputeTotalValue_ItemsWithQuantitySetWithVariousNumbers_ReturnCalculated()
		{
			var cart = new Cart();
			cart.Add(2, "itemName", (decimal)20.30,2);
			cart.Add(3, "itemName", (decimal)15.30,3);

			Assert.Equal((decimal)86.50, cart.ComputeTotalValue());
		}
	}
}