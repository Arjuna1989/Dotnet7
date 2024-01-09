using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{

    public class BuggyController : BaseApiController
    {
        private readonly ILogger<BuggyController> _logger;

        private readonly DataContext _context;

        public BuggyController(DataContext context, ILogger<BuggyController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            return "secret text";
        }



 
        [HttpGet("not-found")]
        public ActionResult<User> GetNotFound()
        {
           var thing = _context.Users.Find(-2);

           if(thing == null) return NotFound();

           return thing;
        }


        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
           var thing = _context.Users.Find(-2);

          var thingToReturn = thing.ToString();

           return thingToReturn;
        }


        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
         return BadRequest("This was not a good request");
        }




    }
}