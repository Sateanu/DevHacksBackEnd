using DevHacksServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DevHacksServer.Controllers
{
    public class UsersApiController : ApiController
    {
        Entities db = new Entities();
        [HttpPost]
        public int LoginUser([FromBody] string email)
        {
            var user=db.Users.Where(x => x.Email == email).FirstOrDefault();
            if (user != null)
            {
                return user.Id;
            }
            else
            {
                db.Users.Add(new Users() { Email = email });
                db.SaveChanges();
                var thisUser = db.Users.Where(x => x.Email == email).FirstOrDefault();
                return thisUser.Id;
            }
        }
    }
}
