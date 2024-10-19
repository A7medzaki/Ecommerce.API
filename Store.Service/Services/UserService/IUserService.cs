using Store.Service.Services.UserService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.UserService
{
    public interface IUserService
    {
        Task<UserDTO> Login(LoginDTO input);
        Task<UserDTO> Register(RegisterDTO input);
    }
}
