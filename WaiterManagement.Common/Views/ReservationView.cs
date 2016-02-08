using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WaiterManagement.Common.Entities;
using WaiterManagement.Common.Views.Abstract;

namespace WaiterManagement.Common.Views
{
	[Table("ReservationsView")]
	public class ReservationView : IView
	{
		[Key]
		public int ReservationId { get; set; }
		public string UnlockCode { get; set; }
		public string ClientLogin { get; set; }
		public DateTime Created { get; set; }
		public DateTime ReservationTime { get; set; }
		public ReservationOrderStatus Status { get; set; }
		public string Comment { get; set; }
		public int Quantity { get; set; }
		public string MenuItem { get; set; }
		public int TableId { get; set; }
		public string TableTitle { get; set; }
	}
}