using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SocialMedia.Dto;
using SocialMedia.Models;
using SocialMedia.Repositories;

namespace SocialMedia.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly IPostRespository _postRespository;

        private readonly ICommentRepository _commentRepository;

        public PostController(
            IConfiguration configuration,
            IPostRespository postRespository,
            ICommentRepository commentRepository
        )
        {
            _configuration = configuration;
            _postRespository = postRespository;
            _commentRepository = commentRepository;
        }

        [HttpPost("new")]
        public async Task<IActionResult> AddPost([FromBody] Post post)
        {
            try
            {
                await _postRespository.Create(post);
            }
            catch (Exception e)
            {
                return new JsonResult(new {
                        status = false,
                        message = e.ToString()
                    })
                { StatusCode = 400 };
            }

            return new JsonResult(new { status = true, message = "success" })
            { StatusCode = 201 };
        }

        [HttpPost("new/comment")]
        public async Task<IActionResult> AddComment([FromBody] Comment comment)
        {
            try
            {
                await _commentRepository.Create(comment);
            }
            catch (Exception e)
            {
                return new JsonResult(new {
                        status = false,
                        message = e.ToString()
                    })
                { StatusCode = 400 };
            }

            return new JsonResult(new { status = true, message = "success" })
            { StatusCode = 201 };
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                IEnumerable<Post> allPost = await _postRespository.GetAll();

                return new JsonResult(new {
                        status = true,
                        message = "success",
                        data = allPost
                    })
                { StatusCode = 200 };
            }
            catch (Exception e)
            {
                return new JsonResult(new {
                        status = false,
                        message = e.ToString()
                    })
                { StatusCode = 500 };
            }
        }

        [HttpPut("like")]
        public async Task<IActionResult> Like([FromBody] Like like)
        {
            try
            {
                await _postRespository.AddLike(like);
            }
            catch (Exception e)
            {
                return new JsonResult(new {
                        status = false,
                        message = e.ToString()
                    });
            }

            return new JsonResult(new { status = true, message = "success" });
        }
    }
}
