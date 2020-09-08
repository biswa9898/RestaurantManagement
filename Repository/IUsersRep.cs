using RestaurantManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantManagement.Repository
{
    public interface IUsersRep
    {
        public List<Users> GetDetails();
        public Users GetDetail(string id);
        public string AddDetail(Users user);
        public int UpdateDetail(string id, Users user);
        public int Delete(string id);
    }
}
