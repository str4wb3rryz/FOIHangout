using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class anketaService
    {
        //get all anketa
        public List<anketa> GetAllAnketa()
        {
            using (var db = new anketaRepository())
            {
                return db.GetAll().ToList();
            }
        }
        //get anketa by id
        public List<anketa> GetAnketaById(int id)
        {
            using (var db = new anketaRepository())
            {
                return db.GetAnketaById(id).ToList();
            }
        }
        //update anketa
        public void UpdateAnketa(anketa anketa)
        {
            //dodati provjeru za podatke
            using (var db = new anketaRepository())
            {
                db.Update(anketa);
            }
        }
        //delete anketa
        public void DeleteAnketa(anketa anketa)
        {
            using (var db = new anketaRepository())
            {
                db.Remove(anketa);
            }
        }
        //add anketa
        public void AddAnketa(anketa anketa)
        {
            using (var db = new anketaRepository())
            {
                db.Add(anketa);
            }
        }
        //get anketa choices
        public List<odabir_selekcije> GetPollChoicesById(int id)
        {
            using (var db = new anketaRepository())
            {
                return db.GetPollChoicesById(id).ToList();
            }
        }

        public async Task<List<anketa>> GetAllAnketaAsync()
        {
            using (var db = new anketaRepository())
            {
                var result = await db.GetAllAsync();
                return result.ToList();
            }
        }
    }
}
