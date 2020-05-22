using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BusinessService.Domain.Services;
using BusinessService.Data.DBModel;

namespace BusinessService.Api.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class SchoolManagementController : ControllerBase
    {
        private readonly ISchoolManagementService _service;

        public SchoolManagementController(ISchoolManagementService service)
        {
            _service = service;
        }


        [HttpGet]
        public ActionResult<IEnumerable<SchoolItem>> Get()
        {
            var items = _service.GetAllItems();
            return Ok(items);
        }

        // GET api/schoolmanagement/5
        [HttpGet("{id}")]
        public ActionResult<SchoolItem> Get(Guid id)
        {
            var item = _service.GetById(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // POST api/schoolmanagement
        [HttpPost]
        public ActionResult Post([FromBody] SchoolItem value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = _service.Add(value);
            return CreatedAtAction("Get", new { id = item.Id }, item);
        }

        // DELETE api/schoomanagement/5
        [HttpDelete("{id}")]
        public ActionResult Remove(Guid id)
        {
            var existingItem = _service.GetById(id);

            if (existingItem == null)
            {
                return NotFound();
            }

            _service.Remove(id);
            return Ok();
        }
    }
}