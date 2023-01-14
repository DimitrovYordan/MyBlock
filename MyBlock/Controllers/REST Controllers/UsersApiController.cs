using System.Collections.Generic;

using MyBlock.Exceptions;
using MyBlock.Helpers;
using MyBlock.Models;
using MyBlock.Models.DataTransferObjects;
using MyBlock.Services.Interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyBlock.Controllers.REST_Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersApiController : ControllerBase
    {
        private readonly IUsersService usersService;
        private readonly IMapper modelMapper;
        private readonly AuthManager authManager;

        public UsersApiController(IUsersService usersService, IMapper modelMapper, AuthManager authManager)
        {
            this.usersService = usersService;
            this.modelMapper = modelMapper;
            this.authManager = authManager;
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                User user = this.usersService.GetById(id);

                return this.StatusCode(StatusCodes.Status200OK, user);
            }
            catch (EntityNotFoundException ex)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }

        }

    }
}