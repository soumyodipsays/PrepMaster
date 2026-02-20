using Dapper;
using PrepMaster.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrepMaster.Models
{
    public class TestModel
    {

        public int TestId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TargetClassId { get; set; }
        public string ClassName { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string TestStatus { get; set; }
        public string AttemptStatus { get; set; }
        public int ScoreObtained { get; set; }
        public int TotalMarks { get; set; }
        public string ResultStatus { get; set; }
    }

}

namespace PrepMaster.DAL
{
    public class TestDAL
    {
        private readonly DapperConn _conn;
        public TestDAL()
        {
            _conn = new DapperConn();
        }

        //public int CreateTest(DynamicParameters param)
        //{
        //    try
        //    {
        //        _conn.ExecuteWithoutReturn("sp_CreateTest", param);
        //        return 201;  // statuscode for successfully created
        //    }
        //    catch (SqlException ex)
        //    {
        //        throw ex;
        //    }
        //}
         public List<TestModel> GetTestsForStudent(int StudentId)
         {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@StudentId", StudentId);

                var sp = "sp_GetTestsDetailsByStudentId";
                return _conn.ExecuteMultipleRow<TestModel>(
                        sp, 
                        param
                    );
            }
            catch (SqlException ex)
            {
                throw ex;
            }
         }
    }
}