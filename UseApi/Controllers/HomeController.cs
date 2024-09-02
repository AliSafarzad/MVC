using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Reflection;
using UseApi.Models;
using UseApi.Security;

namespace UseApi.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            User user =new User();
            
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {

            var client = new HttpClient();


            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5065/api/Account/Login");
            var content = new StringContent(JsonConvert.SerializeObject(user), null, "application/json");
            //request.Headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiIxIiwiUm9sZUlkIjoiMSIsImV4cCI6MTcyNDgzMDc0NX0.J2NdHJ8BaKQsw-MJVmoAGaPS87-GcKDeeo6kag5kdUo");
            request.Content = content;
            var response = await client.SendAsync(request);


            response.EnsureSuccessStatusCode();

            var responseModel = await response.Content.ReadFromJsonAsync<Result<string>>();
            Result<string>? responseModel1 = responseModel;
            if (responseModel1.Success==true)
            {
                SecurityManager securityManager = new SecurityManager();
                securityManager.SignIn(this.HttpContext, user.username, responseModel1.Token);

               /* HttpContext.Session.SetString("Token", responseModel1.Token);*/
                  
                return RedirectToAction("booklist", "Admin");
            }
            else
            {
                if (responseModel.Error == 2)
                {
                    ModelState.AddModelError("password", "کلمه عبور اشتباه است");
                }
                if (responseModel.Error == 0)
                {
                    ModelState.AddModelError("username", "نام کاربری اشتباه است");
                }
                user.username = "";
                user.password = "";
                return View(user);
            }

            
        }

        [HttpGet]
        public IActionResult Logout()
        {
            SecurityManager securityManager = new SecurityManager();
            securityManager.SignOut(this.HttpContext);
            return RedirectToAction("Login");

           
        }
    }
}
