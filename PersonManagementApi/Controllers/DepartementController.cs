using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PersonManagementApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace PersonManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartementController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public DepartementController(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
select DepartementId,DepartementName from 
dbo.Departement;";

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
        public JsonResult Post([FromBody] Departement departement)
        {

            string query = @"insert into dbo.Departement values (@DepartementName); ";
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DepartementName", departement.DepartementName);
                    myReader = myCommand.ExecuteReader();
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Successfully");


        }

        [HttpPut]
        public JsonResult Put([FromBody] Departement departement)
        {

            string query = @"update dbo.Departement 
set DepartementName=@DepartementName 
where DepartementId=@DepartementId ; ";
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DepartementName", departement.DepartementName);
                    myCommand.Parameters.AddWithValue("@DepartementId", departement.DepartementId);
                    myReader = myCommand.ExecuteReader();
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Updated Successfully");


        }



        [HttpDelete("{id}")]
        public JsonResult Delete(int departementId)
        {

            string query = @"delete from dbo.Departement 
where DepartementId=@DepartementId ; ";
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            DataTable table = new DataTable();

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DepartementId", departementId);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Deleted Successfully");


        }
    }
}
