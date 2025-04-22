using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BAL.IServices;
using BAL.Shared;
using MODEL.DTOs;
using REPOSITORY.UnitOfWork;

namespace BAL.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUnitOfWork _unitOfWork;
    public AuthenticationService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<ResponseUserLoginDTO> LoginWeb(UserLoginDTO loginDTO)
    {
        try
        {
            var returndata = new ResponseUserLoginDTO();

            var userdata = (await _unitOfWork.User
                .GetByCondition(x => x.Email == loginDTO.Email && x.Status == "Y"))
                .FirstOrDefault();

            if (userdata == null)
            {
                returndata.EmailStatus = false;
                return returndata;
            }

            returndata.EmailStatus = true;

            // Hash input password using SHA256 to compare with stored password
            //using var SHA512 = SHA256.Create();
            //var inputPasswordHash = Convert.ToBase64String(
            //    SHA512.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password))
            //);
            //VerifyPasswordHash(loginDTO.Password, userdata.Password);
            bool isValid = VerifyPasswordHash(loginDTO.Password, userdata.Password);


            if (userdata.Password == inputPasswordHash)
            {
                // Token and other info
                returndata.Token = CommonTokenGenerator.GenerateToken(userdata, "User");
                returndata.Email = userdata.Email;
            
                return returndata;
            }
            else
            {
                returndata.PasswordStatus = false;
                return returndata;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

}


