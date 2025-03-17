using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mission10_Cloud.Data;

namespace Mission10_Cloud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly BowlingLeagueContext _context;

        // Constructor to inject the DbContext
        public TeamsController(BowlingLeagueContext context)
        {
            _context = context;
        }

        // GET: api/Teams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            // Fetch all teams from the database
            var teams = await _context.Teams.ToListAsync();
            return Ok(teams);  // Return the list of teams
        }
    }
}

