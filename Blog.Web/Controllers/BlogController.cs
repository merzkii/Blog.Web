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

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/blog");
            if (!response.IsSuccessStatusCode)
                return View("Error");

            var json = await response.Content.ReadAsStringAsync();
            var posts = JsonSerializer.Deserialize<List<BlogPostViewModel>>(json, _jsonOptions);
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
                return RedirectToAction(nameof(Index));

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
            Console.WriteLine($"Route ID: {id}, Model ID: {model.Id}");
            if (id != model.Id)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage); // or use a logger
                }
                return View(model);
            }
              

            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"api/blog?id={id}", content);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));
            var errorBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"API Error (Status: {(int)response.StatusCode}): {errorBody}");

            // Optionally, add to ModelState for display in view
            ModelState.AddModelError(string.Empty, "Failed to update blog post. See console for details.");


            return View(model);
        }

        
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/blog?id={id}");
            return RedirectToAction(nameof(Index));
        }

    }
}
