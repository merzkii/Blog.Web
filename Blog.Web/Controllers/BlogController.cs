using Blog.Web.Models;
using Blog.Web.Models.Blog;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace Blog.Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public BlogController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("BlogApi");
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<IActionResult> Index(string? searchTerm)
        {
            var url = string.IsNullOrWhiteSpace(searchTerm)
         ? "api/blog"
         : $"api/blog/search?title={Uri.EscapeDataString(searchTerm)}";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return View("Error");

            var json = await response.Content.ReadAsStringAsync();
            var posts = JsonSerializer.Deserialize<List<BlogPostViewModel>>(json, _jsonOptions);
            ViewBag.CurrentFilter = searchTerm;

            return View(posts);
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"api/blog/{id}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var post = JsonSerializer.Deserialize<BlogPostViewModel>(json, _jsonOptions);
            return View(post);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BlogPostCreateModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/blog", content);
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Post created successfully!";
                return RedirectToAction(nameof(Index));
            }

            var errorBody = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                AddApiValidationErrors(errorBody);
            else
                ModelState.AddModelError(string.Empty, "Unexpected error occurred.");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"api/blog/{id}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var post = JsonSerializer.Deserialize<BlogPostEditModel>(json, _jsonOptions);
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, BlogPostEditModel model)
        {
           
            if (id != model.Id)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/blog/{id}", content);
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Post updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            var errorBody = await response.Content.ReadAsStringAsync();
            AddApiValidationErrors(errorBody);

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/blog/{id}");
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Post deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Failed to delete Post.";

            var error = await response.Content.ReadAsStringAsync();
            return RedirectToAction(nameof(Index));
        }



        #region Helper

        private void AddApiValidationErrors(string errorBody)
        {
            try
            {
                var errorResponse = JsonSerializer.Deserialize<ValidationErrorResponse>(errorBody, _jsonOptions);

                if (errorResponse?.Errors != null && errorResponse.Errors.Any())
                {
                    foreach (var error in errorResponse.Errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, errorResponse?.Message ?? "Unexpected error occurred.");
                }
            }
            catch (Exception ex)
            {
                
                ModelState.AddModelError(string.Empty, "Unexpected error occurred.");
                Console.WriteLine($"Failed to parse validation error: {ex.Message}");
            }
        }
        #endregion
    }
}
