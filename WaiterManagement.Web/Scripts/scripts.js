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
	var date = $("#orderDate").val().split('/');
	var time = $("#orderHour").val();

	var year = parseInt(date[2]);
	var month = parseInt(date[0]) - 1;
	var day = parseInt(date[1]);

	var hour = parseInt(time.split(':')[0]);
	if (hour !== 12 && time.split(':')[1].substring(2) === 'pm')
		hour += 12;
	var minutes = parseInt(time.split(':')[1].substring(0, 2));
	var orderDate = new Date(year, month, day, hour, 0);

	$.ajax({
		type: 'POST',
		url: "/Order/Checkout?date="+orderDate.toISOString(),
		success: function(result) {
			location.reload();
		}
	});
}

function getPickers() {
	$("#orderDate").datepicker();
	$("#orderHour").timepicker({
		'minTime': '10:00am',
		'maxTime': '10:00pm'
	});
}

