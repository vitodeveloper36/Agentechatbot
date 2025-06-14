using Agentechatbot.Models;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Agentechatbot.Controllers
{
    public class ChatController : Controller
    {
        private readonly string _connectionString;

        public ChatController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IActionResult Welcome()
        {
            return View();
        }

        public IActionResult Index(string agentName, string sessionId)
        {
            if (string.IsNullOrEmpty(agentName))
            {
                return RedirectToAction("Welcome");
            }

            TempData["agentName"] = agentName;
            TempData["sessionId"] = string.IsNullOrEmpty(sessionId) ? Guid.NewGuid().ToString() : sessionId;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetSessions()
        {
            using var connection = new SqlConnection(_connectionString);

            // Consulta todas las sesiones ordenadas por SessionId
            const string sql = @"SELECT SessionId, Usuario, StartedAt, LastActivity, IsClosed
                                 FROM ChatBot.dbo.ChatSession
                                 ORDER BY SessionId";

            IEnumerable<ChatSession> sessions = await connection.QueryAsync<ChatSession>(sql);
            return Ok(sessions);
        }
    }
}