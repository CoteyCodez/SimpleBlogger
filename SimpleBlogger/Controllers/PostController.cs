using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SimpleBlogger.Business.Interfaces;
using SimpleBlogger.Models;
using SimpleBlogger.Models.ViewModels;

namespace SimpleBlogger.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        private readonly ITagService _tagService;
        private readonly IPostTagService _postTagService;


        private readonly UserManager<ApplicationUser> _userManager;
        public PostController(
            IPostService postService, 
            ICommentService commentService, 
            ITagService tagService, 
            UserManager<ApplicationUser> userManager,
            IPostTagService postTagService)
        {
            _postService = postService;
            _commentService = commentService;
            _tagService = tagService;
            _userManager = userManager;
            _postTagService = postTagService;

        }
        // GET: PostController
        public async Task<IActionResult> Index(List<PostWithTagsViewModel> vmList)
        {
            var posts = await _postService.GetAllPostsAsync();
            foreach(var post in posts)
            {
                var assignedTag = await _postTagService.GetPostTagAsync(post.Id);
                if (assignedTag != null)
                {
                    var tag = await _tagService.GetSingleTagAsync(assignedTag.TagId);
                    var vm = new PostWithTagsViewModel
                    {
                        Id = post.Id,
                        Title = post.Title,
                        Body = post.Body,
                        CreatedAt = post.CreatedAt,
                        UserId = post.UserId,
                        AssignedTag = tag
                    };
                    vmList.Add(vm);
                }
            }
            return View(vmList);
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
        //public IActionResult Manage()
        //{
        //    return View();
        //}

        // GET: PostController/Create
        public IActionResult Create(PostCreateViewModel vm)
        {
            vm.TagSelectList = new SelectList(_tagService.GetAllTagsAsync().Result, "Id", "Name");
            return View(vm);
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> CreatePOST(PostCreateViewModel vm) // FIXME: Not saving psotTag properly
        {
            var post = new Post
            {
                //Id = vm.Id, 
                Title = vm.Title,
                Body = vm.Body,
                CreatedAt = vm.CreatedAt,
                UserId = _userManager.GetUserId(User)                
            };

            if (!ModelState.IsValid)
            {
                throw new Exception("Didn't work");
            }

            var postTag = new PostTag(); 

            if (vm.SelectedTagId != null)
            {
                int tagId = vm.SelectedTagId ?? 0;

                postTag.Post = post;
                postTag.Tag = await _tagService.GetSingleTagAsync(tagId);

                await _postService.CreatePostAsync(post);
                await _postTagService.CreatePostTagAsync(postTag);
                return RedirectToAction("Index");

            }
            await _postService.CreatePostAsync(post);


            //Fixme: Add CreatePostTagAsync here to save the PostTag relationship if a tag is selected
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
