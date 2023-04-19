using Mis_Management_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mis_Management_system.Controllers
{
    // before we start with this controller we first have to write route prefix or giving the user api name  
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        // after we are going to import MisModel Entities
        MisDatabaseEntities db = new MisDatabaseEntities();
        // type of method we will use
        [HttpPost,Route("signup")]

        // sign up api 
        public HttpResponseMessage Signup([FromBody] User user)
        {
            try
            {
                // first of all we are going to find that particular user which we want to create that account is already exist with
                // particular email exist or not we are going to search it
                User userObj = db.Users
                    .Where(u => u.email == user.email).FirstOrDefault();
                if (userObj == null)
                {
                    // by default i choose user to be setted false for the first time akoze registration
                    user.role = "user";
                    user.status = "false";
                    db.Users.Add(user);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, new {message ="Successfully Registered"});
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new {message = "Email Already Exist"});
                }
            }
            catch(Exception error) 
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,error);
            }
        }
        //Login api
        [HttpPost, Route("login")]
        public HttpResponseMessage Login([FromBody] User user)
        {
            try
            {
                User userObj = db.Users
                    .Where(u => (u.email == user.email && u.password == user.password)).FirstOrDefault();
                if (userObj != null)
                {
                    if(userObj.status == "true")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,new {token = TokenManager.GenerateToken(userObj.email,userObj.role)});
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, new { message = "Wait for Admin Approval" });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new { message = "Incorrect Username or Password" });
                }
            }
            catch (Exception error)
            {
                
                return Request.CreateResponse(HttpStatusCode.InternalServerError, error);
            }
        }
    }
}
