using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class prijateljiService
    {
        public void Add(prijatelji prijatelj)
        {
            using (var db = new prijateljiRepository())
            {
                db.Add(prijatelj);
            }
        }

        public List<prijatelji> GetFriends(int id)
        {
            using (var db = new prijateljiRepository())
            {
                var result = db.GetPrijateljById(id);
                return result.ToList();
            }
        }
    }
}
