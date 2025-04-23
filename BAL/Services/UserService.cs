using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BAL.IServices;
using MODEL.DTOs;
using REPOSITORY.UnitOfWork;
using System.Security.Cryptography;
using MODEL.Entities;

namespace BAL.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<UserResponseDTO> Ban(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<UserResponseDTO> CreateUser(UserRequestDTO requestDTO)
    {
        UserResponseDTO model = new UserResponseDTO();

        var existingUser = await _unitOfWork.User.GetByEmail(requestDTO.Email);

        if (existingUser != null)
        {
            model.IsSuccess = false;
            model.Message = "User already exists. Register Failed";
            model.Data = existingUser;
            return model;
        }

       

        var hashedPassword = HashPassword(requestDTO.Password);
        var otpCode = GenerateOTP();

        var user = new User
        {
            Name = requestDTO.UserName,
            Email = requestDTO.Email,
            Password = hashedPassword,
            CreateAt = DateTime.Now,
            UpdateAt = DateTime.Now,
            Status = "N",
            Role = "User",
            OTP = otpCode,
            OTP_Exp = DateTime.Now.AddMinutes(5)


        };

        await _unitOfWork.User.Add(user);
      var result =   await _unitOfWork.SaveAsync();

        if (result > 0)
        {
            bool emailSent = SendOTPEmail(user.Email, user.Name, otpCode);
            if (!emailSent)
            {
                model.IsSuccess = false;
                model.Message = "User registered but failed to send OTP email.";
                model.Data = user;
                return model;
            }

            model.IsSuccess = true;
            model.Message = "User Register Successful. OTP sent to email.";
            model.Data = user;
        }
        else
        {
            model.IsSuccess = false;
            model.Message = "User Register Failed";
            model.Data = user;
        }

        return model;

      
        
    }

    private static string HashPassword(string password)
    {
        using SHA256 sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }

    private static string GenerateOTP()
    {
        Random random = new Random();
        return random.Next(100000, 999999).ToString();
    }

    private static bool SendOTPEmail(string toEmail, string userName, string otpCode)
    {
        try
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("nnyi37389@gmail.com");
            mail.To.Add(toEmail);
            mail.Subject = "Your OTP Code from RetailManagement System";


            string htmlBody = $@"
            <div style='font-family: Arial, sans-serif; padding: 20px; border: 1px solid #ddd; border-radius: 10px; max-width: 500px; margin: auto; background-color: #f9f9f9;'>
                <h2 style='color: #007bff; text-align: center;'>Your OTP Code</h2>
                <p style='font-size: 16px; color: #333;'>Dear <strong>{userName}</strong>,</p>
                <p style='font-size: 16px; color: #333;'>Your One-Time Password (OTP) for verification is:</p>
                <p style='font-size: 24px; font-weight: bold; color: #28a745; text-align: center; padding: 10px; border: 2px dashed #28a745; display: inline-block;'>{otpCode}</p>
                <p style='font-size: 14px; color: #ff0000; text-align: center;'>This OTP will expire in 5 minutes.</p>
               
                <br>
                <p style='font-size: 14px; color: #666; text-align: center;'>Best regards,</p>
                <p style='font-size: 14px; color: #666; text-align: center;'><strong>TravelAgency Team</strong></p>
            </div>";

            mail.Body = htmlBody;
            mail.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("nnyi37389@gmail.com", "jbrq aqmv ukix sfdv"),
                EnableSsl = true
            };

            smtpClient.Send(mail);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<UserListResponseDTO> GetAllUsers()
    {
        UserListResponseDTO model = new UserListResponseDTO();
        var users = await _unitOfWork.User.GetAll();

        if (users == null)
        {
            model.IsSuccess = false;
            model.Message = "No users found.";
            model.Data = null;

            return model;
        }
        else
        {
            model.IsSuccess = true;
            model.Message = "Success";
            model.Data = users.ToList();
        }

        return model;
    }


    public async Task<UserResponseDTO> GetUserById(int id)
    {
        UserResponseDTO model = new UserResponseDTO();
        var user = await _unitOfWork.User.GetById(id);


        if (user == null)
        {
            model.IsSuccess = false;
            model.Message = "User not found.";
            model.Data = null;
            return model;
        }
       
           model.IsSuccess = true;
           model.Message = "Success";
           model.Data = user;

            return model;

        
    }

    public async Task<UserResponseDTO> GetUserByName(string name)
    {
        UserResponseDTO model = new UserResponseDTO();
        var user = await _unitOfWork.User.GetByName(name);

        if (user == null)
        {
            model.IsSuccess = false;
            model.Message = "User not found.";
            model.Data = null;

            return model;
        }

        model.IsSuccess = true;
        model.Message = "Success";
        model.Data = user;

        return model;
    }

    public async Task<UserResponseDTO> UpdateUser(int id, UserRequestDTO requestDTO)
    {
        UserResponseDTO model = new UserResponseDTO();

        var user = await _unitOfWork.User.GetById(id);
        if (user == null)
        {
            model.IsSuccess = false;
            model.Message = "User not found.";
            model.Data = null;

            return model;
        }

       

        if(requestDTO.UserName is not "string")
        {
            user.Name = requestDTO.UserName;
        }

       

        if (requestDTO.Password is not "string")
        {
            user.Password = HashPassword(requestDTO.Password);
        }

      

       if(requestDTO.Password is not "string")
        {
            user.Password = HashPassword(requestDTO.Password);
        }

        user.UpdateAt = DateTime.Now;

        _unitOfWork.User.Update(user);
        var result = _unitOfWork.SaveAsync();

        string message = result.Result > 0 ? "User updated successfully." : "User update failed.";

        model.IsSuccess = result.Result > 0;
        model.Message = message;
        model.Data = user;

        return model;


    }

    public async Task<UserResponseDTO> VerifyEmail(string email, string otp)
    {
        UserResponseDTO model = new UserResponseDTO();

        var user = await _unitOfWork.User.GetByEmail(email);

        if (user == null)
        {
            model.IsSuccess = false;
            model.Message = "User not found.";
            model.Data = null;

            return model;
        }

        if (user.OTP != otp)
        {
            model.IsSuccess = false;
            model.Message = "Invalid OTP.";
            model.Data = null;
           
            return model;
        }

        if (user.OTP_Exp < DateTime.Now)
        {
            model.IsSuccess = false;
            model.Message = "OTP expired.";
            model.Data = null;
           

            return model;
        }

        user.Status = "Y";
        user.OTP = null;
        user.OTP_Exp = DateTime.Now;
        _unitOfWork.User.Update(user);
        var result = await _unitOfWork.SaveAsync();

        string message = result > 0 ? "Email verified successfully." : "Email verification failed.";

        model.IsSuccess = result > 0;
        model.Message = message;
        model.Data = user;

        return model;
    }
}
