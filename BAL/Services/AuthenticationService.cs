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
    private readonly CommonAuthentication _commonAuthentication;
    public AuthenticationService(IUnitOfWork unitOfWork, CommonAuthentication commonAuthentication)
    {
        _unitOfWork = unitOfWork;
        _commonAuthentication = commonAuthentication;
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

            
            bool isValid = _commonAuthentication.VerifyPasswordHash(loginDTO.Password, userdata.Password);


            if (isValid)
            {
                // Token and other info
                returndata.UserId = userdata.UserID;
                string userRole = userdata.Role; 
                returndata.Token = CommonTokenGenerator.GenerateToken(userdata, userRole);

                returndata.Email = userdata.Email;
                returndata.PasswordStatus = true;
            
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


