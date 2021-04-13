using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using dotnetcore_api.Models;
using dotnetcore_api.Data;

namespace dotnetcore_api.Controllers
{
    [ApiController]
    [Route("v1/config/profiles")]
    public class ProfileController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Profile>>> Get([FromServices] DataContext context)
        {
            var profiles = await context.Profiles.ToListAsync();

            return profiles;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Profile>> GetById([FromServices] DataContext context, int id)
        {
            var profile = await context.Profiles.FirstOrDefaultAsync(x => x.Id == id);

            return profile;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Profile>> Post
        (
            [FromServices] DataContext context,
            [FromBody] Profile model
        )
        {
            if (ModelState.IsValid)
            {
                context.Profiles.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
