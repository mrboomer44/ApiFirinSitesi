using AkademiqDinamikFırınSitesiApi.Dal.Entitys;
using DinamikFırınSitesi.Dal.Context;
using Microsoft.AspNetCore.Mvc;

namespace DinamikFırınSitesiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly FırınContext _context;
        public TeamController(FırınContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetTeams()
        {
            var teams = _context.Teams.ToList();
            return Ok(teams);
        }

        [HttpGet("{TeamId}")]
        public IActionResult GetTeam(int TeamId)
        {
            var team = _context.Teams.Find(TeamId);
            return Ok(team);
        }

        [HttpPost]
        public IActionResult CreateTeam(Team team)
        {
            _context.Teams.Add(team);
            _context.SaveChanges();
            return Ok("Ekleme işlemi tamamlandı");
        }

        [HttpPut]
        public IActionResult UpdateTeam(Team team)
        {
            _context.Teams.Update(team);
            _context.SaveChanges();
            return Ok("Güncelleme işlemi tamamlandı");
        }

        [HttpDelete("{TeamId}")]
        public IActionResult DeleteTeam(int TeamId)
        {
            var entity = _context.Teams.Find(TeamId);
            if (entity == null)
            {
                return NotFound("Kayıt bulunamadı.");
            }
            _context.Teams.Remove(entity);
            _context.SaveChanges();
            return Ok("Silme işlemi tamamlandı");

        }
    }
}
