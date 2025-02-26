using Microsoft.AspNetCore.Mvc;
using HomeEnergyApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeEnergyApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomesController : ControllerBase
    {
        private IReadRepository<int, Home> repository;

        private IOwnerLastNameQueryable<Home> homeByOwnerLastName;

        public HomesController(IReadRepository<int, Home> repository, IOwnerLastNameQueryable<Home> homeByOwnerLastName)
        {
            this.repository = repository;
            this.homeByOwnerLastName = homeByOwnerLastName;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string? ownerLastName)
        {
            if (ownerLastName != null)
            {
                return Ok(homeByOwnerLastName.FindByOwnerLastName(ownerLastName));
            } 
            else
            {
                return Ok(repository.FindAll());
            }

        }

        [HttpGet("{id}")]
        public IActionResult FindById(int id)
        {
            if (id > repository.FindAll().Count)
            {
                return NotFound();
            }
            var home = repository.FindById(id);

            return Ok(home);
        }
    }
}