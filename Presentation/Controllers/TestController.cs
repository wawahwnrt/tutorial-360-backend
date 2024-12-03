using System;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace tutorial_backend_dotnet.Presentation.Controllers
{
    [ApiController]
    [Route("api/v1/360-tutorial")]
    public class TestController : ControllerBase
    {
        [HttpGet("test-connection")]
        public IActionResult TestPostgreSqlConnection()
        {
            string connectionString = "Host=192.168.1.118;Port=5432;Database=tutorial360dev;Username=tutorial_admin;Password=360Tutorial;";

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    return Ok("Connected successfully to PostgreSQL!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
