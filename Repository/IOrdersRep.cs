using RestaurantManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantManagement.Repository
{
    public interface IOrdersRep
    {
        public List<Orders> GetDetails();
        public Orders GetDetail(int id);
        public int AddDetail(Orders order);
        public int Delete(int id);
    }
}
