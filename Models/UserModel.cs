using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrepMaster.Models
{
    public class UserModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(150)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8,
            ErrorMessage = "Password must be at least 8 characters")]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public int Role { get; set; }

        public int SignUp(DynamicParameters param)
        {
            try {
                DapperConn conn = new DapperConn();
                conn.ExecuteWithoutReturn("sp_SignUp", param);
                return 201;  // statuscode for successfully created
            }catch(SqlException ex)
            {
                if (ex.Number == 5001)
                {
                    throw new Exception ("Email already Exist");
                }
                throw;
            }
            
        }

        public int LogIn(DynamicParameters param)
        {
            try
            {
                DapperConn conn = new DapperConn();
                int user_id =  (conn.ExecuteSingleRow<UserModel>("sp_LogIn", param)).UserId;
                return user_id;
            }
            catch(SqlException ex)
            {
                throw ex;
            }
        }

    }
}
    