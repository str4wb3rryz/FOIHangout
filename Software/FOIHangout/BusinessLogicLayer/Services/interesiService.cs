using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;

namespace BusinessLogicLayer.Services
{
    public class interesiService
    {
        //get al interests
        public List<interesi> GetAllInterests()
        {
            using (var db = new interesiRepository())
            {
                return db.GetAll().ToList();
            }
        }
        //get interest by id
        public List<interesi> GetInterestById(int id)
        {
            using (var db = new interesiRepository())
            {
                return db.GetInteresById(id).ToList();
            }
        }
        //add interest
        public void AddInterest(interesi interes)
        {
            using (var db = new interesiRepository())
            {
                db.Add(interes);
            }
        }
        //update interest
        public void UpdateInterest(interesi interes)
        {
            using (var db = new interesiRepository())
            {
                db.Update(interes);
            }
        }

    }
}
