using RestaurantManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantManagement.Repository
{
    public class OrdersRep : IOrdersRep
    {
        private readonly DemoContext db;
        public OrdersRep(DemoContext _db)
        {
            db = _db;
        }

        public List<Orders> GetDetails()
        {
            return db.Orders.ToList();
        }
        public Orders GetDetail(int id)
        {
            if (db != null)
            {
                return (db.Orders.Where(p => p.OrderId == id)).FirstOrDefault();
            }
            return null;
        }
        public int AddDetail(Orders order)
        {
            db.Orders.Add(order);
            db.SaveChanges();

            return order.OrderId;
        }

        public int Delete(int id)
        {
            int result = 0;

            if (db != null)
            {
                var deletePost = db.Orders.FirstOrDefault(p => p.OrderId == id);

                if (deletePost != null)
                {
                    db.Orders.Remove(deletePost);
                    result = db.SaveChanges();
                    return 1;
                }
                return result;
            }
            return result;
        }

       

       
    }
}
