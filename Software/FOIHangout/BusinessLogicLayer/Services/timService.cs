using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class timService
    {
        public int Add(tim team)
        {
            using (var db = new timRepository())
            {
                return db.Add(team);
            }
        }

        public void AddClanoveTima(tim team, korisnik teammate)
        {
            using (var db = new timRepository())
            {
                db.AddClanoveTima(team, teammate);
            }
        }

        public List<tim> GetTeamsByUserId(int id)
        {
            using (var db = new timRepository())
            {
                var result = db.GetTimById(id);
                return result.ToList();
            }
        }
    }
}
