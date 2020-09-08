using RestaurantManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantManagement.Repository
{
    public interface IFoodRep
    {
        public List<Food> GetDetails();
        public Food GetDetail(int id);
        public int AddDetail(Food food);
        public int Delete(int id);
    }
}
