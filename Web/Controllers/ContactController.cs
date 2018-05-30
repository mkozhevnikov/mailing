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
        [ProducesResponseType(typeof(IEnumerable<Contact>), 200)]
        public IActionResult List()
        {
            return Ok(repository.Query().ToList());
        }

        [HttpPost]
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
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> Delete(long id)
        {
            var contact = repository.Get(id);
            await repository.DeleteAsync(contact);

            return Ok(true);
        }

        [HttpPut]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> Update([FromBody] Contact contact)
        {
            await repository.UpdateAsync(contact);

            return Ok(true);
        }

        public ContactController(IRepository<Contact> repository)
        {
            this.repository = repository;
        }
    }
}