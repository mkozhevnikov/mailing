using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Model;
using Web.Repository;

namespace Web.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IRepository<Contact> repository;

        [HttpGet]
        [Route("List")]
        [ProducesResponseType(typeof(IEnumerable<Contact>), 200)]
        public IActionResult List()
        {
            return Ok(repository.Query().ToList());
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(typeof(Contact), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] Contact contact)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            await repository.CreateAsync(contact);

            return Ok(contact);
        }

        [HttpDelete]
        [Route("Delete")]
        [ProducesResponseType(typeof(bool), 202)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Delete([FromBody] Contact contact)
        {
            await repository.DeleteAsync(contact);

            return Ok(true);
        }

        public ContactController(IRepository<Contact> repository)
        {
            this.repository = repository;
        }
    }
}