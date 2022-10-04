using HomeWorkCoreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace HomeWorkCoreMVC.Controllers
{
    public class UserController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddUser(UserInfo person)
        {
            string filename = "Users.json";

            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                if (fs.Length == 0) 
                {
                    char one = '[';
                    byte[] bytes = new byte[sizeof(char)];
                    bytes = BitConverter.GetBytes(one);
                    fs.Write(bytes,0, bytes.Length-1);
                    JsonSerializer.Serialize(fs, person);
                } else
                {
                    fs.Position = fs.Length - 1;
                    char one = ',';
                    byte[] bytes = new byte[sizeof(char)];
                    bytes = BitConverter.GetBytes(one);
                    fs.Write(bytes, 0, bytes.Length - 1);
                    JsonSerializer.Serialize(fs, person);
                }
                
                char two = ']';
                byte[] bytes2 = new byte[sizeof(char)];
                bytes2 = BitConverter.GetBytes(two);
                fs.Write(bytes2, 0, bytes2.Length - 1);
            }
            return View();

        } 

        public IActionResult TakeAllUsers()
        {
            string filename = "Users.json";
            
            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                List<UserInfo>? deserialeseUserInfoJson = JsonSerializer.Deserialize<List<UserInfo>>(fs);
                
                return View(deserialeseUserInfoJson);
            }
            
        }


        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
    
}