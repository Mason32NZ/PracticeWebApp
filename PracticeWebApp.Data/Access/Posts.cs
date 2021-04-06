using System;
using System.Collections.Generic;
using System.Linq;
using PracticeWebApp.Models;

namespace PracticeWebApp.Data.Access
{
    public class Posts
    {
        private AppDbContext _db = null;

        public Posts()
        {
            _db = Utility.GetDbContext();
        }

        public bool CheckIfPostValid(Post post)
        {
            return post != null && post.Id != 0 && !string.IsNullOrEmpty(post.Name) && !string.IsNullOrEmpty(post.Message);
        }

        public List<Post> GetPostsAll()
        {
            return _db.Posts.Where(p => !p.Deleted).ToList();
        }

        public List<Post> GetPostsByUser(string name)
        {
            return _db.Posts.Where(p => p.Name == name && !p.Deleted).ToList();
        }

        public Post GetPost(int id, out bool success)
        {
            var post = _db.Posts.FirstOrDefault(p => p.Id == id && !p.Deleted);
            success = post != null;
            return post;
        }

        public void AddPost(Post post, out bool success)
        {
            if (!CheckIfPostValid(post))
            {
                success = false;
            }
            else
            {
                post.CreatedTimeStampUtc = DateTime.UtcNow;
                post.Deleted = false;
                _db.Posts.Add(post);
                _db.SaveChangesAsync();
                success = true;
            }
        }

        public void UpdatePost(Post post, out bool success)
        {
            if (!CheckIfPostValid(post) || post.Deleted)
            {
                success = false;
            }
            else
            {
                post.UpdatedTimeStampUtc = DateTime.UtcNow;
                _db.Posts.Update(post);
                _db.SaveChangesAsync();
                success = true;
            }
        }

        public void DeletePost(int id, out bool success)
        {
            var post = GetPost(id, out success);

            if (!success || !CheckIfPostValid(post) || post.Deleted)
            {
                success = false;
            }
            else
            {
                post.Deleted = true;
                _db.Posts.Update(post);
                _db.SaveChangesAsync();
                success = true;
            }
        }
    }
}
