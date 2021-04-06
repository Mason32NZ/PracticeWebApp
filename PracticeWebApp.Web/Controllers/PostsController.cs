using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PracticeWebApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PracticeWebApp.Data.Access;
using PracticeWebApp.Models;
using PracticeWebApp.Web.Models.Extended;

namespace PracticeWebApp.Web.Controllers
{
    public class PostsController : Controller
    {
        private Posts _posts;

        public PostsController()
        {
            _posts = new Posts();
        }

        public IActionResult Index()
        {
            var model = new PostsViewModel
            {
                Posts = _posts.GetPostsAll().Select(Helpers.Utility.Clone<Post, PostExtended>).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult AddPost(string json)
        {
            var post = JsonConvert.DeserializeObject<PostExtended>(json);
            _posts.AddPost(post, out var success);

            if (!success)
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }
        }

        [HttpPut]
        public IActionResult UpdatePost(string json)
        {
            var post = JsonConvert.DeserializeObject<PostExtended>(json);
            _posts.UpdatePost(post, out var success);

            if (!success)
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }
        }
    }
}
