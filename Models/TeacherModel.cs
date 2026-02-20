using Dapper;
using PrepMaster.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrepMaster.Models
{
    public class Teacher
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8)]
        public string PasswordHash { get; set; }

        [Required]
        public int Role { get; set; }
    }
    public class GetSubjectsAndClassesVM
    {
        public int MatchId { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
    }
    public class TeacherOnboardingVM
    {
        public int TeacherId { get; set; }
        public List<GetSubjectsAndClassesVM> AvailableSubjects { get; set; }
    }
    public class DbResponse
    {
        public int Success { get; set; }
        public string Message { get; set; }
    }
}

namespace PrepMaster.DAL
{
    public class TeacherDAL
    {
        private readonly DapperConn _conn;
        public TeacherDAL()
        {
            _conn = new DapperConn();
        }

        public List<GetSubjectsAndClassesVM> GetSubjectsAndClasses()
        {
            var proc = "sp_GetSubjectsAndClasses";
            return _conn.ExecuteMultipleRow<GetSubjectsAndClassesVM>(proc);
        }

        public DbResponse AddTeacherSpecialization(int TeacherId, string MatchIdList)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@TeacherId", TeacherId);
            parameters.Add("@MatchIdList", MatchIdList);

            var proc = "sp_AddTeacherSpecialization";

            return _conn.ExecuteSingleRow<DbResponse>(
                proc,
                parameters
            );
        }
    }
}