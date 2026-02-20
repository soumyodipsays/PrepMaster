using Dapper;
using PrepMaster.DAL;
using PrepMaster.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace PrepMaster.Controllers
{
    public class TeacherController : Controller
    {
        private readonly TeacherDAL _dal;
        public TeacherController()
        {
            _dal = new TeacherDAL();
        }
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        // Onboard the Teacher : Teacher must fulfill the details
        // Details : Select Subjects and Classes
        public ActionResult Onboarding(int id)
        {
            var availableSubjectsAndClasses = _dal.GetSubjectsAndClasses();
            var viewmodel = new TeacherOnboardingVM
            {
                TeacherId = id,
                AvailableSubjects = availableSubjectsAndClasses
            };

            return View(viewmodel);
        }

        // Post req : for adding TeacherSpealization
        // TeacherId and MatchIdList will come from Jquery ajax call
        // MatchIdList = "1,2,3,4"
        [HttpPost]
        public ActionResult AddSpecialization(int TeacherId, string MatchIdList)
        {
            var result = _dal.AddTeacherSpecialization(TeacherId, MatchIdList);

            return Json(new
            {
                success = result.Success == 1,
                message = result.Message
            });
        }
    }
}