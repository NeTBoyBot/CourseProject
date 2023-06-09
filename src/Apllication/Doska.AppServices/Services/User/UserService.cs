﻿using AutoMapper;
using Doska.AppServices.IRepository;
using Doska.Contracts.UserDto;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Doska.AppServices.Services.User
{
    public class UserService : IUserService
    {
        public readonly IUserRepository _userRepository;
        public IConfiguration _configuration;
        public IClaimAcessor claimAccessor;
        public readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper,IClaimAcessor acessor,IConfiguration conf)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            claimAccessor = acessor;
            _configuration = conf;
        }

        public async Task<Guid> CreateUserAsync(RegisterRequest registerUser)
        {
            var newUser = _mapper.Map<Domain.User>(registerUser);
            await _userRepository.AddAsync(newUser);

            return newUser.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var existingUser = await _userRepository.FindById(id);
            await _userRepository.DeleteAsync(existingUser);
        }

        public async Task<InfoUserResponse> EditUserAsync(Guid Id, RegisterRequest editUser)
        {
            var existingUser = await _userRepository.FindById(Id);
            await _userRepository.EditUserAsync(_mapper.Map(editUser, existingUser));

            return _mapper.Map<InfoUserResponse>(editUser);
        }

        public async Task<IReadOnlyCollection<InfoUserResponse>> GetAll(int take, int skip)
        {
            return await _userRepository.GetAll()
                .Select(a => new InfoUserResponse
                {
                    Id = a.Id,
                    Name = a.Name
                }).OrderBy(a => a.Id).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<InfoUserResponse> GetByIdAsync(Guid id)
        {
            var existingUser = await _userRepository.FindById(id);
            return _mapper.Map<InfoUserResponse>(existingUser);
        }

        public async Task<InfoUserResponse> GetCurrentUser(CancellationToken cancellationToken)
        {
            var claim = await claimAccessor.GetClaims(cancellationToken);
            var claimId = claim.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(claimId))
            {
                return null;
            }

            var id = Guid.Parse(claimId);
            var user = await _userRepository.FindById(id);

            if (user == null)
            {
                throw new Exception($"Не найдент пользователь с идентификаторром {id}");
            }

            return _mapper.Map<InfoUserResponse>(user);
        }

        public async Task<Guid> GetCurrentUserId(CancellationToken cancellationToken)
        {
            var claim = await claimAccessor.GetClaims(cancellationToken);
            var claimId = claim.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(claimId))
            {
                throw new Exception("Не найдент пользователь с идентификаторром");
            }

            var id = Guid.Parse(claimId);
            var user = await _userRepository.FindById(id);

            if (user == null)
            {
                throw new Exception($"Не найдент пользователь с идентификаторром {id}");
            }

            return user.Id;
        }

        public async Task<string> Login(LoginRequest loginRequest, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.FindWhere(user => user.Name == loginRequest.Name,cancellationToken); 

            if (existingUser == null)
            {
                throw new Exception($"Пользователь с логином '{loginRequest.Name}' не существует");
            }

            if (!existingUser.Password.Equals(loginRequest.Password))
            {
                throw new Exception($"Указан неверный логин или пароль");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, existingUser.Id.ToString()),
                new Claim(ClaimTypes.Name, existingUser.Name)
               // new Claim(ClaimTypes.Email, existingUser.Email)
            };

            var secretKey = _configuration["Token:SecretKey"];

            var token = new JwtSecurityToken
                (
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    SecurityAlgorithms.HmacSha256
                    )
                );

            var result = new JwtSecurityTokenHandler().WriteToken(token);

            return result;
        }

        public async Task<Guid> Register(RegisterRequest registerRequest, CancellationToken cancellationToken)
        {

            var user = _mapper.Map<Domain.User>(registerRequest);
      
            var existinguser = await _userRepository.FindWhere(user => user.Name == registerRequest.Name, cancellationToken);
            
            if (existinguser != null)
            {
                throw new Exception($"Такой пользователь уже существует! ");
            }
            await _userRepository.AddAsync(user);
            
            return user.Id;
        }


    }
}
