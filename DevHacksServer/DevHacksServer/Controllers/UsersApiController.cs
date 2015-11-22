using DevHacksServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DevHacksServer.Controllers
{
    public class LoginModel
    {
        public string email { get; set; }
    }
    public class UsersApiController : ApiController
    {
        Entities db = new Entities();
        [HttpPost]
        public int LoginUser([FromBody] LoginModel email)
        {
            if (email.email == null) return -1;

            var user=db.Users.Where(x => x.Email == email.email).FirstOrDefault();
            if (user != null)
            {
                return user.Id;
            }
            else
            {
                db.Users.Add(new Users() { Email = email.email });
                db.SaveChanges();
                var thisUser = db.Users.Where(x => x.Email == email.email).FirstOrDefault();
                return thisUser.Id;
            }
        }
    }
}
