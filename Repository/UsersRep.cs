using RestaurantManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantManagement.Repository
{
    public class UsersRep : IUsersRep
    {
        private readonly DemoContext db;
        public UsersRep(DemoContext _db)
        {
            db = _db;
        }
        public List<Users> GetDetails()
        {
            return db.Users.ToList();
        }

        public Users GetDetail(string id)
        {
            if (db != null)
            {
                return (db.Users.Where(p => p.Email == id)).FirstOrDefault();
            }
            return null;
        }
        public string AddDetail(Users user)
        {
            db.Users.Add(user);
            db.SaveChanges();

            return user.Email;
        }



        public int UpdateDetail(string id, Users user)
        {
            if (db != null)
            {
                var myVar = (db.Users.Where(p => p.Email == id)).FirstOrDefault();
                if (myVar != null)
                {
                    myVar.FirstName = user.FirstName;
                    myVar.LastName = user.LastName;
                    myVar.Password = user.Password;
                    db.SaveChanges();
                    return 1;

                }
                return 0;
            }
            return 0;
        }
        public int Delete(string id)
        {

            int result = 0;

            if (db != null)
            {
                var deletePost = db.Users.FirstOrDefault(p => p.Email == id);

                if (deletePost != null)
                {
                    db.Users.Remove(deletePost);
                    result = db.SaveChanges();
                    return 1;
                }
                return result;
            }
            return result;
        }
    }
}
