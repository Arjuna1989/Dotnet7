using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SQLitePCL;

namespace API.Controllers;

[Authorize]
public class UsersController : BaseApiController
{
     
        private readonly IUserRepository _repository;
 
  public UsersController(IUserRepository repository)
  {
           _repository = repository;
   

  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<User>>> GetUsers()
  {
   return Ok(await _repository.GetUsersAsync());
  }

  // [HttpGet("{id}")]
  // public async Task<ActionResult<User>> GetUser(int id)
  // {
  //   return Ok(await _repository.GetUserByIdAsync(id));
    
  // }

 
 [HttpGet("{username}")]
  public async Task<ActionResult<User>> GetUser(string username)
  {
    return Ok(await _repository.GetUserByNameAsync(username));
    
  }
}
