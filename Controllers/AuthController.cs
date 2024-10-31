using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Data;
using dotnet_rpg.dtos.AuthRepository;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AuthController: ControllerBase
    {
        private IAuthRepository _authRepository;
        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
            
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceRespose<int>>> Register(RegisterDto request){
            var response = await _authRepository.Register(
                new User(){UserName = request.Username}, request.Password
            );

            if(response.Success){
                return Ok(response);
            } else{
                return BadRequest(response);
            }
        }


        [HttpPost("Login")]
        public async Task<ActionResult<ServiceRespose<string>>> Login(LoginDto request){
            var response = await _authRepository.Login(request.Username, request.Password);

            if(response.Success){
                return Ok(response);
            } else{
                return BadRequest(response);
            }
        }
    }
}