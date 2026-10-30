using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleBlogger.Business;
using SimpleBlogger.Models;
using SimpleBlogger.Models.ViewModels;

namespace SimpleBlogger.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;

        private readonly UserManager<ApplicationUser> _userManager;
        public PostController(IPostService postService, ICommentService commentService, UserManager<ApplicationUser> userManager)
        {
            _postService = postService;
            _commentService = commentService;
            _userManager = userManager;
        }
        // GET: PostController
        public async Task<IActionResult> Index()
        {
            var posts = await _postService.GetAllPostsAsync();
            return View(posts);
        }

        // GET: PostController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var post = await _postService.GetSinglePostAsync(id);
            if (post == null) return NotFound();

            var comments = await _commentService.GetAllCommentsOnPostAsync(id);

            var viewModel = new PostDetailsViewModel
            {
                Id = id,
                Title = post.Title,
                PostBody = post.Body,
                CreatedAt = post.CreatedAt,
                User = post.User,
                Comments = comments
            };

            return View(viewModel);
        }

        // GET: PostController/Create
        public IActionResult Create()
        {
            return View();
        }

        //public IActionResult Manage()
        //{
        //    return View();
        //}

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> CreatePOST(Post post)
        {
            post.UserId = _userManager.GetUserId(User);

            if (!ModelState.IsValid)
            {
                //return View(post);
                throw new Exception("Didn't work");
            }

            await _postService.CreatePostAsync(post);
            return RedirectToAction("Index");
        }

        // POST: PostController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id)
        {
            await _postService.DeletePostAsync(Id);
            return RedirectToAction("Index");
        }


        //// POST: PostController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: PostController/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: PostController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
