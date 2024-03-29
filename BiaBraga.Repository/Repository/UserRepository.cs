﻿using BiaBraga.Business.Dtos;
using BiaBraga.Domain.Models;
using BiaBraga.Domain.Models.Entitys;
using BiaBraga.Repository.Classes;
using BiaBraga.Repository.Context;
using BiaBraga.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BiaBraga.Repository.Repository
{
    public class UserRepository : BiaBragaRepository, IUserRepository
    {
        public UserRepository(BiaBragaDbContext context) : base(context)
        {
        }

        public async Task<List<User>> GetAllUsersAsync()
        => await _context.Users.Include(x => x.Gener).ToListAsync();

        public async Task<User> GetByIdAsync(int id)
        => await _context.Users.Include(x => x.Gener).FirstOrDefaultAsync(x => x.Id == id);

        public async Task<ResultDefault> VerifyLoginAsync(LoginDto login)
        {
            ResultDefault resultDefault = new ResultDefault
            {
                Message = "Não foi possível fazer a verificação de login",
                HttpStatusCode = System.Net.HttpStatusCode.BadRequest,
                Entity = login
            };

            var user = await _context.Users.FirstOrDefaultAsync(x =>
            x.Email == login.LoginUser || x.CPF == login.LoginUser);

            if(user != null)
            {
                if(user.Password == Encript.HashValue(login.Password))
                {
                    resultDefault = new ResultDefault
                    {
                        HttpStatusCode = System.Net.HttpStatusCode.OK,
                        Message = "Logado com sucesso!",
                        Entity = user
                    };
                }
                else
                {
                    resultDefault.Message = "Senha incorreta";
                    resultDefault.HttpStatusCode = System.Net.HttpStatusCode.Unauthorized;
                }
            }
            else
            {
                resultDefault.Message = "Usuario não identificado";
                resultDefault.HttpStatusCode = System.Net.HttpStatusCode.Unauthorized;
            }

            return resultDefault;
        }
    }
}
