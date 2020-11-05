using BiaBraga.Domain.Models.Entitys;
using BiaBraga.Repository.Classes;
using BiaBraga.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiaBraga.Repository.Repository
{
    public class StartDefaultRepository : BiaBragaRepository
    {
        public StartDefaultRepository(BiaBragaDbContext context) : base(context)
        {
        }

        public async Task CreateDefaultAsync()
        {
            var _userAdmin = await _context.Users.FirstOrDefaultAsync(x => x.Role == Domain.Enums.Role.Administrador);

            if(_userAdmin == null)
            {
                await AddAsync(new User
                {
                    Name = "Assistente administrativo virtual",
                    Role = Domain.Enums.Role.Administrador,
                    Birth = new DateTime(2000, 10, 11),
                    AboutMe = "Responsável por todos os processos automáticos da aplicação.",
                    Date = DateTime.UtcNow,
                    Password = Encript.HashValue("admin"),
                    CellPhone = "99999999999",
                    Email = "administrativo@biabraga.com",
                    CPF = "99999999999",
                    ReceiveCellPhoneMessage = false,
                    ReceiveEmailMessage = false,
                    Gener = new Genre
                    {
                        Name = "Indefinido"
                    }
                });

                await AddAsync(new User
                {
                    Name = "Beatriz Braga",
                    Role = Domain.Enums.Role.Administrador,
                    Nick = "Bia",
                    Birth = new DateTime(1993, 10, 11),
                    AboutMe = "Sou como você me vê. " +
                              "Posso ser leve como uma brisa ou forte como uma ventania, " +
                              "Depende de quando e como você me vê passar.",
                    Date = DateTime.UtcNow,
                    Password = Encript.HashValue("admin"),
                    CellPhone = "14981162437",
                    Email = "biabraga@gmail.com",
                    ReceiveCellPhoneMessage = false,
                    ReceiveEmailMessage = false,
                    Gener = new Genre
                    {
                        Name = "Feminino"
                    }
                });

                await AddAsync(new User
                {
                    Name = "Alex Matos",
                    Role = Domain.Enums.Role.Administrador,
                    Nick = "Alex",
                    Birth = new DateTime(1993, 01, 23),
                    AboutMe = "Ninguém tem o direito de me julgar a não ser eu mesmo.Eu me pertenço e de mim faço o que bem entender.",
                    Date = DateTime.UtcNow,
                    Password = Encript.HashValue("admin"),
                    CellPhone = "14981725654",
                    Email = "alexmatos@gmail.com",
                    ReceiveCellPhoneMessage = false,
                    ReceiveEmailMessage = false,
                    Gener = new Genre
                    {
                        Name = "Masculino"
                    }
                });
            }
        }
    }
}
