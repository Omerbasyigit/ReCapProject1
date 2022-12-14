using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Core.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using Entity.DTOs;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IResult UserExist(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);
        IDataResult<UserDetailDto> GetUser(int userId);
    }
}
