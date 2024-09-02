using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace UseApi.Controllers
{
    public class AdminController : Controller
    {
        [Authorize]
        public async Task<IActionResult> booklist()
        {

            /* string _token = HttpContext.Session.GetString("Token");*/

            string _token = User.FindFirstValue("Token").ToString();

          
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5065/api/Books/SelectBooks");
            /*var content = new StringContent(JsonConvert.SerializeObject(user), null, "application/json");*/
            request.Headers.Add("Authorization", $"Bearer {_token}");

            var response = await client.SendAsync(request);


            response.EnsureSuccessStatusCode();

            var responseModel = await response.Content.ReadFromJsonAsync<List<Book>>();
            return View(responseModel);
        }

        [HttpGet]
        public IActionResult NewBook()
        {
            BookNewModel book = new BookNewModel();
            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> NewBook(BookNewModel book)
        {
            if(book.BookName.Trim()!="" && book.Nevisande.Trim() != "" && book.Price != 0
                && book.Mozo.Trim() != "")
            {
                string _token = User.FindFirstValue("Token").ToString();

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5065/api/Books/InsertBook");

                var content = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");
                request.Content = content;

                request.Headers.Add("Authorization", $"Bearer {_token}");

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseModel = await response.Content.ReadFromJsonAsync<int>();
                return RedirectToAction("booklist");

            }
            else
            {
                ModelState.AddModelError("BookName", "اطلاعات را وارد نمایید");
                return View(book);

            }
                
        }


        [HttpGet]
        public async Task<IActionResult> EditBook(int _idBook)
        {
            BookById bookById = new BookById();
            bookById.IDBook = _idBook;


            string _token = User.FindFirstValue("Token").ToString();

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5065/api/Books/SelectBooksById");

            var content = new StringContent(JsonConvert.SerializeObject(bookById), Encoding.UTF8, "application/json");
            request.Content = content;

            request.Headers.Add("Authorization", $"Bearer {_token}");

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            Book book = await response.Content.ReadFromJsonAsync<Book>();

            return View(book);
        }
        [HttpPost]
        public async Task<IActionResult> EditBook(Book book)
        {
            if (book.BookName.Trim() != "" && book.Nevisande.Trim() != "" && book.Price != 0
                && book.Mozo.Trim() != "")
            {
                string _token = User.FindFirstValue("Token").ToString();

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5065/api/Books/UpdateBook");

                var content = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");
                request.Content = content;

                request.Headers.Add("Authorization", $"Bearer {_token}");

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseModel = await response.Content.ReadFromJsonAsync<int>();
                return RedirectToAction("booklist");

            }
            else
            {
                ModelState.AddModelError("BookName", "اطلاعات را وارد نمایید");
                return View(book);

            }

        }




        [HttpGet]
        public async Task<IActionResult> DeleteBook(int _idBook)
        {
            BookById bookById = new BookById();
            bookById.IDBook = _idBook;


            string _token = User.FindFirstValue("Token").ToString();

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5065/api/Books/DeleteBook");

            var content = new StringContent(JsonConvert.SerializeObject(bookById), Encoding.UTF8, "application/json");
            request.Content = content;

            request.Headers.Add("Authorization", $"Bearer {_token}");

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseModel = await response.Content.ReadFromJsonAsync<int>();
            return RedirectToAction("booklist");
        }


    }
}
