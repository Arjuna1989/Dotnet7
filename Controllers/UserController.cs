using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SQLitePCL;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController: ControllerBase
{
 private readonly DataContext _context;
  public UserController(DataContext context)
  {
            _context = context;
    
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<User>>> GetUsers()
  { 
    var users = await _context.Users.ToListAsync();

    return users;
  }

 [HttpGet("{id}")]
  public async Task<ActionResult<User>> GetUser(int id)
  {
    var user = await _context.Users.FindAsync(id);
    return user;
  }
}
