using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Schema;
using getJsonPlaceHolder.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace getJsonPlaceHolder.Controllers
{   [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {  
        public readonly HttpClient _client = new HttpClient();
        private static List<Post> _posts = new List<Post>();

        [HttpGet("[action]")]
        public async Task<IActionResult> GetPosts()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("https://jsonplaceholder.typicode.com/posts");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                 var data = JsonConvert.DeserializeObject<List<Post>>(responseBody);
                 
                 if(data != null)
                    _posts.AddRange(data);
                // return Ok(data.Select(x => new {Id = x.Id}));
                return Ok(_posts);

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message : {0}",e.Message);
                return BadRequest();
            }
        }
        [HttpGet("[action]/{userId}")]
        public IActionResult GetPosts(int userId)
        {   
            var userIdPost = _posts.FindAll(x => x.UserId == userId);
            return Ok(userIdPost);
        }
        [HttpGet("[action]/{userId}/{id}")]
        public IActionResult GetPosts(int userId,int id)
        {   
            var userIdPost = _posts.FindAll(x => x.UserId == userId);
            var lastUserIdPost = userIdPost.Find(y => y.Id == id);
            return Ok(lastUserIdPost);


        }
        






    }
    
}