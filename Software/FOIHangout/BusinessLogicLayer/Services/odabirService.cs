using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class odabirService
    {
        //get all odabir_selekcije
        public List<odabir_selekcije> GetAllOdabirSelekcije()
        {
            using (var db = new odabir_selekcijeRepository())
            {
                return db.GetAll().ToList();
            }
        }
        //get odabir_selekcije by id
        public List<odabir_selekcije> GetOdabirSelekcijeById(int id)
        {
            using (var db = new odabir_selekcijeRepository())
            {
                return db.GetOdabirSelekcijeById(id).ToList();
            }
        }
        //update odabir_selekcije
        public void UpdateOdabirSelekcije(odabir_selekcije odabir_selekcije)
        {
            using (var db = new odabir_selekcijeRepository())
            {
                db.Update(odabir_selekcije);
            }
        }
        //delete odabir_selekcije
        public void DeleteOdabirSelekcije(odabir_selekcije odabir_selekcije)
        {
            using (var db = new odabir_selekcijeRepository())
            {
                db.Remove(odabir_selekcije);
            }
        }
        //add odabir_selekcije
        public void AddOdabirSelekcije(odabir_selekcije odabir_selekcije)
        {
            using (var db = new odabir_selekcijeRepository())
            {
                db.Add(odabir_selekcije);
            }
        }

    }
}
