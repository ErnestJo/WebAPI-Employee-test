using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using EmployeeTestWebApi.Models;

namespace EmployeeTestWebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
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
                            select EmployeeId, EmployeeName, Derpatment,
                            convert(varchar(10), DateOfJoining, 120) as DateOfJoining, PhotoFileName
                           from
                            dbo.Employee
                            ";
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
        public JsonResult Post(Employee Emp)
        {
            string query = @"
                            insert into dbo.Employee
                            (EmployeeName,Derpatment,DateOfJoining,PhotoFileName)
                            values (@EmployeeName, @Derpartment, @DateOfJoining, @PhotoFileName)
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@EmployeeName", Emp.EmployeeName);
                    myCommand.Parameters.AddWithValue("@Derpartment", Emp.Derpatment);
                    myCommand.Parameters.AddWithValue("@DateOfJoining", Emp.DateOfJoining);
                    myCommand.Parameters.AddWithValue("@PhotoFileName", Emp.PhotoFileName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Succefuly added");
        }

        [HttpPut]
        public JsonResult Put(Employee Emp) 
        {
            string query = @"
                            update  dbo.Employee
                            set EmployeeName= (@EmployeeName),
                            Derpatment= (@Derpatment),
                            DateOfJoining= (@DateOfJoining),
                            PhotoFileName= (@PhotoFileName)
                            where EmployeeId=@EmployeeId
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@EmployeeId", Emp.EmployeeId);
                    myCommand.Parameters.AddWithValue("@EmployeeName", Emp.EmployeeName);
                    myCommand.Parameters.AddWithValue("@Derpatment", Emp.Derpatment);
                    myCommand.Parameters.AddWithValue("@DateOfJoining", Emp.DateOfJoining);
                    myCommand.Parameters.AddWithValue("@PhotoFileName", Emp.PhotoFileName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("updated succeffuly");
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            string query = @"
                            delete from dbo.Employee
                            where EmployeeId=@EmployeeId
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@EmployeeId", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Deleted succeffuly");
        }
    }
}
