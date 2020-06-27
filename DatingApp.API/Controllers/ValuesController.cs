using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DatingApp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _Context;
        public ValuesController(DataContext Context)
        {
            _Context = Context;

        }
        [HttpGet]
        public async Task<IActionResult> GetValues()
        {
            var values=await _Context.Values.ToListAsync();
            return Ok(values);
        }


        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValues(int? id)
        {
            var values=await _Context.Values.FirstOrDefaultAsync(x=>x.id==id);
            return Ok(values);
        }


        [HttpPost]
        public void Post([FromBody] string value)
        { }
        [HttpPut("{id}")]
        public void Put(int? id)
        { }

        [HttpDelete("{id}")]
        public void Delete(int? id)
        { }


    }
}