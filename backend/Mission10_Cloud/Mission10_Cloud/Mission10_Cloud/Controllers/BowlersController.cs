using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mission10_Cloud.Data;

namespace Mission10_Cloud.Controllers

{
    [Route("api/[controller]")]
    [ApiController]

    public class BowlersController : ControllerBase
    {
        private BowlingLeagueContext _bowlercontext;

        public BowlersController(BowlingLeagueContext temp)
        {
            _bowlercontext = temp;
        }
        
        
        [HttpGet(Name = "GetBowlers")]
        public IEnumerable<Bowler> Get()
        {
            var bowlersList = _bowlercontext.Bowlers
                .Include(b => b.Team) // This ensures that the related Team data is fetched
                .ToList();

            return bowlersList;
        }
        
    }
}