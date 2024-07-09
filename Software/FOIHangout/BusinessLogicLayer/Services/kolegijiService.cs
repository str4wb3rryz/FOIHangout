using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class kolegijiService
    {
        public List<kolegij> GetAllUserCourses()
        {
            using (var db = new kolegijRepository())
            {
                return db.GetAll().ToList();
            }
        }
    }
}
