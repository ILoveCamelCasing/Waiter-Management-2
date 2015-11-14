using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaiterManagement.Common.Views.Abstract;

namespace WaiterManagement.Common.Views
{
    [Table("UsersView")]
    public class UserView : IView
    {
        [Key]
        public int Id { get; set; }
        public Guid CommonId { get; set; }
        /// <summary>
        /// CommonId użytkownika (kelnera bądź stołu)
        /// </summary>
        public Guid UserId { get; set; }
        public string SecondHash { get; set; }
    }
}
