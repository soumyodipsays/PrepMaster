using PrepMaster.DAL;
using PrepMaster.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrepMaster.Controllers
{
    public class StudentController : Controller
    {
        private readonly TestDAL _dal;
        public StudentController()
        {
            _dal = new TestDAL();
        }
        // GET: Student/id
        public ActionResult Index(int id)
        {
            List<TestModel> testList = _dal.GetTestsForStudent(id);
            return View(testList);
        }
    }
}