var openAsContent;

function onLoadedMenuItemsElements() {
	$(".orderItemButton").on("click", function () {
		var menuItemId = $(this).attr("id").replace('item', '');
		addItemToCart(menuItemId);
	});
}

function refreshCart() {
	$.ajax({
		type: 'GET',
		url: "/Order/Summary",
		success: function(result) {
			$("#cartSummary").replaceWith(result);
		}
	});
}

function addItemToCart(itemId) {
	$.ajax({
		type: 'POST',
		url: "/Order/AddElement?elementId="+itemId,
		success: function (result) {
			refreshCart();
			alert('Added');
		}
	});
}

function checkoutOrder() {
	$.ajax({
		type: 'POST',
		url: "/Order/Checkout",
		success: function(result) {
			location.reload();
		}
	})
}

