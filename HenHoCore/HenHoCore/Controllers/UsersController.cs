using HenHoCore.Dto;
using HenHoCore.Service;
using HenHoCore.Ulti;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HenHoCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UserService _usersService;
        private readonly IHttpContextAccessor _contextAccessor;

        public UsersController(UserService usersService, IHttpContextAccessor contextAccessor)
        {
            this._usersService = usersService;
            this._contextAccessor = contextAccessor;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            ResponseAPI responseAPI = new ResponseAPI();
            try
            {
                string userSession = SystemAuthorization.GetCurrentUser(this._contextAccessor);
                UserDto currentUser = this._usersService.GetProfile(userSession);
                //string gender = currentUser.Gender == "Nam" ? "Nữ" : "Nam";
                string gender = "";
                if (currentUser.Gender == "Nam")
                {
                    gender = "Nữ";
                } else
                {
                    gender = "Nam";
                }
                responseAPI.Data = this._usersService.GetUsers(gender);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }

        [Route("profile")]
        [HttpGet]
        public IActionResult GetProfile()
        {
            ResponseAPI responseAPI = new ResponseAPI();
            try
            {
                string userSession = SystemAuthorization.GetCurrentUser(this._contextAccessor);
                responseAPI.Data = this._usersService.GetProfile(userSession);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }

        [Route("profile")]
        [HttpPut]
        public IActionResult UpdateProfile(UserDto user)
        {
            ResponseAPI responseAPI = new ResponseAPI();
            try
            {
                string userSession = SystemAuthorization.GetCurrentUser(this._contextAccessor);
                responseAPI.Data = this._usersService.UpdateProfile(userSession, user);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }

        [Route("likeMe")]
        [HttpGet]
        public IActionResult GetUsersLikeMe()
        {
            ResponseAPI responseAPI = new ResponseAPI();
            try
            {
                string userSession = SystemAuthorization.GetCurrentUser(this._contextAccessor);
                responseAPI.Data = this._usersService.GetUserLikeMe(userSession);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }

        [HttpPost("like/{recipientId}")]
        public IActionResult LikeUser(string recipientId)
        {
            ResponseAPI responseAPI = new ResponseAPI();
            try
            {
                string userSession = SystemAuthorization.GetCurrentUser(this._contextAccessor);
                UserDto user = this._usersService.GetProfile(recipientId);
                if (user == null)
                {
                    responseAPI.Message = "Cannot found Recipient User";
                    return BadRequest(responseAPI);
                }
                ContactDto contact = this._usersService.GetLike(userSession, recipientId);
                if (contact.isMatch == 1)
                {
                    responseAPI.Message = "You already like user";
                } else
                {
                    contact.isMatch = 1;
                    contact = this._usersService.UpdateContact(contact);
                }
                responseAPI.Data = contact;
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }

        [HttpPost("unlike/{recipientId}")]
        public IActionResult UnLikeUser(string recipientId)
        {
            ResponseAPI responseAPI = new ResponseAPI();
            try
            {
                string userSession = SystemAuthorization.GetCurrentUser(this._contextAccessor);
                UserDto user = this._usersService.GetProfile(recipientId);
                if (user == null)
                {
                    responseAPI.Message = "Cannot found Recipient User";
                    return BadRequest(responseAPI);
                }
                ContactDto contact = this._usersService.GetLike(userSession, recipientId);
                if (contact.isMatch == 0)
                {
                    responseAPI.Message = "You already unlike user";
                }
                else
                {
                    contact.isMatch = 0;
                    contact = this._usersService.UpdateContact(contact);
                }
                responseAPI.Data = contact;
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
    }
}
