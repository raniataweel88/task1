using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using task1.DTO;
using static Azure.Core.HttpHeader;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace task1.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class mainController : ControllerBase
    {
        private readonly IConfiguration _configuration;

    public mainController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    [HttpGet]
    [Route("getalllexam")]
    public IActionResult getexam()
    {
        //fetch connection information with database
        SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        //setup sql command 
        SqlCommand com = new SqlCommand("SELECT * FROM [EXAM]", connection);
            //open connrction 
            connection.Open();
        //execute query command
        SqlDataAdapter adapter = new SqlDataAdapter(com);
        DataTable dt = new DataTable();
        adapter.Fill(dt);
        return Ok(dt);

    }
       //read from view 
        [HttpGet]
        [Route("readview")]
        public IActionResult readexam()
        {
            //fetch connection information with database
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            //setup sql command 
            SqlCommand com = new SqlCommand("SELECT * FROM GetAllExam", conn);
            //execute query command
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return Ok(dt);

        }
        [HttpPost]
        [Route("[action]")]

        public IActionResult insertexam([FromBody] createxam ex)
       {
           // execute non query command(rows effected)
           //fetch connection information with database
           SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            //setup sql command Title { get; set; }

            string comm = $"INSERT INTO [dbo].[EXAM](Title,Date,Mark,QuestionCount,Duration,IsPassed,Level,EXAMID,USERID,NationalityId,ACTUALMARK ,SUBMITTIME,NOTES,IsActive) VALUES ('{ex.Title}','{ex.Date}',{ex.Mark},{ex.QuestionCount},{ex.Duration},1,'{ex.Level}',{ex.EXAMID},{ex.USERID},'{ex.NationalityId}',{ex.ACTUALMARK},'{ex.SUBMITTIME}','{ex.NOTES}',1)";
            SqlCommand com = new SqlCommand(comm, conn);
           //open connrction 
           conn.Open();
            int row = com.ExecuteNonQuery();
           //close query ===its important
           conn.Close();
           if (row > 0)
               return Ok();
           else
               return BadRequest("faild");
}
        [HttpPost]
        [Route("[action]")]
        public IActionResult GetExamById( [FromBody] createxam ex)
        {

            //fetch connection information with database
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand com = new SqlCommand("GetExamById", connection);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@EXAMID", ex.EXAMID);

            connection.Open();
            int row = com.ExecuteNonQuery();

            connection.Close();
            if (row > 0)
            {
                com.Parameters.AddWithValue("@Title", ex.Title);
                com.Parameters.AddWithValue("@Date", ex.Date);
                com.Parameters.AddWithValue("@Mark", ex.Mark);
                com.Parameters.AddWithValue("@QuestionCount", ex.QuestionCount);
                com.Parameters.AddWithValue("@Duration", ex.Duration);
                com.Parameters.AddWithValue("@IsPassed", ex.IsPassed);
                com.Parameters.AddWithValue("@Level", ex.Level);
                com.Parameters.AddWithValue("@USERID", ex.USERID);
                com.Parameters.AddWithValue("@NationalityId", ex.NationalityId);
                com.Parameters.AddWithValue("@ACTUALMARK", ex.ACTUALMARK);
                com.Parameters.AddWithValue("@SUBMITTIME", ex.SUBMITTIME);
                com.Parameters.AddWithValue("@NOTES", ex.NOTES);
                return Ok();
            }
               
            else
                return BadRequest("faild");

        }

            [HttpPut]
                 [Route("[action]/{Id}")]
                 public IActionResult udate([FromRoute] int Id, [FromBody] createxam ex)
                 {
                     // execute non query command(rows effected)
                     //fetch connection information with database
                     SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                     //setup sql command 
                     string comm = $"UPDATE  EXAM SET Title='{ex.Title}',Date='{ex.Date}',Mark='{ex.Mark}',QuestionCount='{ex.QuestionCount}',Duration='{ex.Duration}',IsPassed='{ex.IsPassed}',Level='{ex.Level}',USERID='{ex.USERID}',NationalityId='{ex.NationalityId}',ACTUALMARK='{ex.ACTUALMARK}',SUBMITTIME='{ex.SUBMITTIME}',NOTES='{ex.NOTES} WHERE EXAMID={Id}";
                     SqlCommand com = new SqlCommand(comm, conn);
                     //open connrctionNationalityId
                     conn.Open();
                     int row = com.ExecuteNonQuery();
                     //close query ===its important
                     conn.Close();
                     if (row > 0)
                         return Ok();
                     else
                         return BadRequest("faild");

                 }
                 [HttpDelete]
             [Route("[action]/{Id}")]
             public IActionResult deleteuser([FromRoute] int Id)
             {

                 SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                 //setup sql command 
                 string comm = $"DELETE FROM EXAM WHARE EXAMID={Id}";
                 SqlCommand com = new SqlCommand(comm, conn);
                 //open connrction 
                 conn.Open();
                 int row = com.ExecuteNonQuery();
                 //close query ===its important
                 conn.Close();
                 if (row > 0)
                     return Ok();
                 else
                     return BadRequest("faild");

             }
      
        }
    }
