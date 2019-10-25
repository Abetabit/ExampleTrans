using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Transavia.Core.Entities;
using Transavia.Core.Interfaces;
using Transavia.Web.Models;

namespace Transavia.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UsersController(IUserRepository repository, IMapper mapper, UserManager<User> userManager)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<UserModel[]>> GetAll()
        {
            try
            {
                var results = await _repository.GetAllAsync();

                return Ok(_mapper.Map<UserModel[]>(results));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> Get(string id)
        {
            User user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound("This user does't exist");

            return Ok(_mapper.Map<UserModel>(user));
        }
    }
}