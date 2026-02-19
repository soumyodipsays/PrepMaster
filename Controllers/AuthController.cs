using Dapper;
using PrepMaster.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BCrypt.Net;


namespace PrepMaster.Controllers
{
    public class AuthController : Controller
    {
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SignUp(string FullName,string Email, string Password,string Role)
        {
            DynamicParameters param = new DynamicParameters();
            string PasswordHash = BCrypt.Net.BCrypt.HashPassword(Password);
            param.Add("Name", FullName);
            param.Add("Email", Email);
            param.Add("PasswordHash", PasswordHash);
            param.Add("Role", Role);

            UserModel md = new UserModel();
            try
            {
                int statusCode = md.SignUp(param);
                return Json(
                        new
                        {
                            StatusCode = statusCode,
                            Message = "User Created Successfully",
                            Data = new { },
                            Error = "",
                        }
                );
            }
            catch(Exception ex)
            {
                return Json(
                    new
                    {
                        StatusCode = 409,
                        Message = "Email already exist",
                        Data = new {},
                        Error = "",
                    }
                );
            }          
            
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public JsonResult LogIn(string Email, string Password)
        {
            DynamicParameters param = new DynamicParameters();
            string PasswordHash = BCrypt.Net.BCrypt.HashPassword(Password);
            param.Add("Email", Email);
            param.Add("PasswordHash", PasswordHash);
            try
            {
                UserModel md = new UserModel();
                int UserId = md.LogIn(param);
                return Json(
                     new
                     {
                         StatusCode = 200,
                         Message = "User created suceesfully",
                         Data = new {UserId},
                         Error = "",
                     }
                );
            }
            catch (SqlException ex)
            {
                if(ex.Number == 5001)
                {
                    return Json(
                        new
                        {
                            StatusCode = 404,
                            Message = "Email does not exist",
                            Data = new {},
                            Error = "",
                        }
                    );
                }
                else if (ex.Number == 5002)
                {
                    return Json(
                        new
                        {
                            StatusCode = 401,
                            Message = "Password Incorrect",
                            Data = new {},
                            Error = "",
                        }
                    );
                }
                else
                {
                    return Json(
                        new
                        {
                            StatusCode = 5000,
                            Message = "Unexpected Error",
                            Data = new { },
                            Error = "",
                        }
                    );
                }

            }

        }

    }
}