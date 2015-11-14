using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaiterManagement.Common.Entities.Abstract;

namespace WaiterManagement.Common.Entities
{
    public class ActiveUser : IEntity
    {
        public int Id { get; private set; }
        public Guid UserId { get; set; }
        public Guid UserToken { get; set; }
        public DateTime TokenCreation { get; set; }
    }
}
