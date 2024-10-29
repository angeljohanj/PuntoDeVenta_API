using Mapster;
using Microsoft.AspNetCore.Mvc;
using PuntoDeVenta_API.ADMIN.DTOs.Responses;
using PuntoDeVenta_API.ADMIN.Models;
using PuntoDeVenta_API.ADMIN.Services;

namespace PuntoDeVenta_API.ADMIN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserServices _userServices;
        public UserController()
        {
            _userServices = new UserServices();
        }
        [HttpPost]
        [Route("/Create")]
        public async Task<JsonResult> Create(UserModel user)
        {
            var ans = await _userServices.CreateANewUser(user);
            if (ans)
                return new JsonResult(ans);

            return new JsonResult(false);
        }

        [HttpGet]
        [Route("/List")]
        public async Task<JsonResult> List()
        {
            List<UserModel> users = new List<UserModel>();
            try
            {
                users = await _userServices.GetUsers();
                if (ModelState.IsValid)
                {
                    var oUsers = users.Adapt<List<SendUsersDTO>>();
                    return new JsonResult(oUsers);
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
            return new JsonResult(users);
        }

        [HttpGet]
        [Route("/Get/{id}")]
        public async Task<JsonResult> Get([FromRoute] int id)
        {
            var user = await _userServices.GetASingleUser(id);
            try
            {
                if (ModelState.IsValid)
                {
                    var oUser = user.Adapt<SendUsersDTO>();
                    return new JsonResult(oUser);
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                user = null;
            }
            return new JsonResult(user);
        }

        [HttpPut]
        [Route("/Delete/{id}")]
        public async Task<JsonResult> Delete([FromRoute] int id)
        {
            var ans = await _userServices.DeleteAUser(id);
            try
            {
                if (ans)
                {
                    return new JsonResult(ans);
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                ans = false;
            }
            return new JsonResult(ans);
        }

        [HttpPost]
        [Route("/ValidateLogin")]
        public async Task<JsonResult> ValidateLogin(UserModel credentials)
        {
            var user = await _userServices.ValidateUserLogin(credentials);
            if (!ModelState.IsValid)
                throw new NullReferenceException();
            else
                return new JsonResult(user);
        }
    }
}
