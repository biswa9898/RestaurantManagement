using RestaurantManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantManagement.Repository
{
    public class FoodRep : IFoodRep
    {
        private readonly DemoContext db;
        public FoodRep(DemoContext _db)
        {
            db = _db;
        }


        public List<Food> GetDetails()
        {
            return db.Foods.ToList();
        }
        public Food GetDetail(int id)
        {
            if(db!=null)
            {
                return (db.Foods.Where(p => p.Id == id)).FirstOrDefault();
            }
            return null;
        }

        public int AddDetail(Food food)
        {
            db.Foods.Add(food);
            db.SaveChanges();

            return food.Id;
        }

        public int Delete(int id)
        {
            int result = 0;
            
            if(db!=null)
            {
                var deletePost = db.Foods.FirstOrDefault(p => p.Id == id);

                if(deletePost != null)
                {
                    db.Foods.Remove(deletePost);
                    result = db.SaveChanges();
                    return 1;
                }
                return result;
            }
            return result;
        }    
       
    }
}
