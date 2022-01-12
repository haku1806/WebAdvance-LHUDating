using HenHoCore.Dto;
using HenHoCore.Model;
using HenHoCore.Service;
using HenHoCore.Ulti;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HenHoCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private UserService _usersService;
        private readonly IHttpContextAccessor _contextAccessor;

        public AdminController(UserService usersService, IHttpContextAccessor contextAccessor)
        {
            this._usersService = usersService;
            this._contextAccessor = contextAccessor;
        }

        [Route("getUsers")]
        [HttpGet]
        public IActionResult GetUsersWithRole()
        {
            ResponseAPI responseAPI = new ResponseAPI();
            try
            {
                string userSession = SystemAuthorization.GetCurrentUser(this._contextAccessor);
                User user = this._usersService.GetUserWithRole(userSession);
                if (user.Role == 1)
                {
                    responseAPI.Data = this._usersService.GetUsersWithRole();
                    return Ok(responseAPI);
                }
                else
                {
                    responseAPI.Message = "Lỗi xác thực role!";
                    return BadRequest(responseAPI);
                }
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
    }
}
