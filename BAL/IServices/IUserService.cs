using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL.DTOs;

namespace BAL.IServices
{
    public interface IUserService
    {
        Task<UserListResponseDTO> GetAllUsers();
        Task<UserResponseDTO> GetUserById(int id);
        Task<UserResponseDTO> GetUserByName(string name);
        Task<UserResponseDTO> CreateUser(UserRequestDTO requestDTO);
        Task<UserResponseDTO> UpdateUser(int id, UserRequestDTO requestDTO);
        Task<UserResponseDTO> Ban(int id);

        Task<UserResponseDTO> VerifyEmail(string email, string otp);
    }
}
