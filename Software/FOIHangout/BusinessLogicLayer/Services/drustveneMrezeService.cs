using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class drustveneMrezeService
    {
        //get all social networks
        public List<drustvene_mreze> GetAllSocialNetworks()
        {
            using (var db = new drustvene_mrezeRepository())
            {
                return db.GetAll().ToList();
            }
        }
        //get social network by id
        public List<drustvene_mreze> GetSocialNetworkById(int id)
        {
            using (var db = new drustvene_mrezeRepository())
            {
                return db.GetDrustvenaMrezaById(id).ToList();
            }
        }
        //add social network
        public void AddSocialNetwork(drustvene_mreze drustvenaMreza)
        {
            using (var db = new drustvene_mrezeRepository())
            {
                db.Add(drustvenaMreza);
            }
        }
        //update social network
        public void UpdateSocialNetwork(drustvene_mreze drustvenaMreza)
        {
            using (var db = new drustvene_mrezeRepository())
            {
                db.Update(drustvenaMreza);
            }
        }
    }
}
