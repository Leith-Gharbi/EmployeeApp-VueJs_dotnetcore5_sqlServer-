using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PersonManagementApi.Models;
using System.Data.SqlClient;
using System.Data;

namespace PersonManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        [HttpGet]
        public JsonResult Get()
        {
            string query = @" 
select EmployeeId, convert(varchar(10),DateofJoining,120) as DateofJoining,  EmployeeName , Departement ,PhotoFileName from 
dbo.Employee;";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post([FromBody] Employe employe)
        {

            string query = @"insert into dbo.Employee values (@EmployeeName , @Departement ,@DateofJoining,@PhotoFileName); ";
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@EmployeeName", employe.EmployeeName);
                    myCommand.Parameters.AddWithValue("@Departement", employe.Departement);
                    myCommand.Parameters.AddWithValue("@DateofJoining", employe.DateofJoining);
                    myCommand.Parameters.AddWithValue("@PhotoFileName", employe.PhotoFileName);
                    myReader = myCommand.ExecuteReader();
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Successfully");


        }

        [HttpPut]
        public JsonResult Put([FromBody] Employe employe)
        {

            string query = @"update dbo.Employee 
set EmployeeName=@EmployeeName  , Departement=@Departement ,DateofJoining=@DateofJoining,PhotoFileName=@PhotoFileName
where EmployeeId=@EmployeeId ; ";
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@EmployeeName", employe.EmployeeName);
                    myCommand.Parameters.AddWithValue("@Departement", employe.Departement);
                    myCommand.Parameters.AddWithValue("@DateofJoining", employe.DateofJoining);
                    myCommand.Parameters.AddWithValue("@PhotoFileName", employe.PhotoFileName);
                    myCommand.Parameters.AddWithValue("@EmployeeId", employe.EmployeeId);
                    myReader = myCommand.ExecuteReader();
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Updated Successfully");


        }



        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {

            string query = @"delete from dbo.Employee 
where EmployeeId=@EmployeeId ; ";
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@EmployeeId", id);
                    myReader = myCommand.ExecuteReader();
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Deleted Successfully");


        }
    }
}

