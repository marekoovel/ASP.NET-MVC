using DAL.Interfaces;
using DAL.Repositories;
using Domain;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class OrderRepository : EFRepository<Order>, IOrderRepository
    {
        public OrderRepository(IDbContext dbContext)
            : base(dbContext)
        {
        }

        public bool GetCustomerOrder(int orderId, string name)
        {
            return DbSet.Any(
                o => o.OrderId == orderId &&
                o.Username == name);
        }
    }
}
