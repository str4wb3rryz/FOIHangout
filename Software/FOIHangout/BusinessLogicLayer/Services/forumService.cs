using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class forumService
    {
        //get all posts
        public List<forum> GetAllPosts()
        {
            using (var db = new forumRepository())
            {
                return db.GetAll().ToList();
            }
        }
        //get post by id
        public List<forum> GetPostById(int id)
        {
            using (var db = new forumRepository())
            {
                return db.GetForumById(id).ToList();
            }
        }
        //add post
        public void AddPost(forum forum)
        {
            using (var db = new forumRepository())
            {
                db.Add(forum);
            }
        }
        //get all replies
        public List<forum> GetAllRepliesByPostId(int id)
        {
            using (var db = new forumRepository())
            {
                return db.GetForumOdgovorById(id).ToList();
            }
        }
        public async Task<List<forum>> LoadForumAsync()
        {
            using (var db = new forumRepository())
            {
                var result = await db.GetAllAsync();
                return result.ToList();
            }
        }

        public async Task<List<forum>> GetAllRepliesByPostIdAsync(int id)
        {
            using (var db = new forumRepository())
            {
                return await Task.Run(() => db.GetForumOdgovorById(id).ToList());
            }
        }
    }
}
