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
            var userAdmin = await _context.Users.AnyAsync(x => x.Role == Domain.Enums.Role.Administrador);

            if (!userAdmin)
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
                    CPF = "354.452.593-31",
                    ReceiveCellPhoneMessage = false,
                    ReceiveEmailMessage = false,
                    Image = string.Empty,
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
                    CPF = "626.374.475-83",
                    CellPhone = "14981162437",
                    Email = "biabraga@gmail.com",
                    ReceiveCellPhoneMessage = false,
                    ReceiveEmailMessage = false,
                    Image = string.Empty,
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
                    CPF = "982.286.824-33",
                    CellPhone = "14981725654",
                    Email = "alexmatos@gmail.com",
                    ReceiveCellPhoneMessage = false,
                    ReceiveEmailMessage = false,
                    Image = string.Empty,
                    Gener = new Genre
                    {
                        Name = "Masculino"
                    }
                });
            }

            var contacts = await _context.Contacts.AnyAsync();

            if (!contacts)
            {
                await AddAsync(new Contact
                {
                    Name = "João Vitor",
                    Email = "joaovitor@teste.com",
                    Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    Date = DateTime.UtcNow.AddDays(-1).AddMinutes(-90),
                    New = true,
                    Important = false
                });

                await AddAsync(new Contact
                {
                    Name = "Maria da silva",
                    Email = "mariasilva@teste.com",
                    Message = "Auctor augue mauris augue neque gravida in. Integer quis auctor elit sed vulputate. In hendrerit gravida rutrum quisque non. Sem integer vitae justo eget. Quis risus sed vulputate odio ut enim blandit volutpat. Phasellus faucibus scelerisque eleifend donec pretium. Amet nisl suscipit adipiscing bibendum est. Metus vulputate eu scelerisque felis. Ut etiam sit amet nisl purus in mollis nunc sed. Ipsum consequat nisl vel pretium lectus quam id leo. Donec ac odio tempor orci dapibus ultrices in iaculis. Id ornare arcu odio ut sem nulla. Odio morbi quis commodo odio aenean sed. Gravida rutrum quisque non tellus orci ac. Pretium lectus quam id leo in vitae turpis massa.",
                    Date = DateTime.UtcNow.AddDays(-2),
                    New = true,
                    Important = false
                });

                await AddAsync(new Contact
                {
                    Name = "Pedro Henrique",
                    Email = "pedrohenrique@teste.com",
                    Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    Date = DateTime.UtcNow.AddDays(-2).AddMinutes(-41),
                    New = true,
                    Important = false
                });

                await AddAsync(new Contact
                {
                    Name = "Marcos Santos",
                    Email = "marcossantos@teste.com",
                    Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    Date = DateTime.UtcNow.AddDays(-10),
                    New = true,
                    Important = false
                });

                await AddAsync(new Contact
                {
                    Name = "Luisa da silva santos",
                    Email = "luisasilvasantos@teste.com",
                    Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    Date = DateTime.UtcNow.AddDays(-20).AddMinutes(-200),
                    New = true,
                    Important = false
                });

                await AddAsync(new Contact
                {
                    Name = "Guilherme Joel",
                    Email = "guilhermejoel@teste.com",
                    Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    Date = DateTime.UtcNow.AddDays(-7).AddMinutes(-240),
                    New = true,
                    Important = false
                });

                await AddAsync(new Contact
                {
                    Name = "Sabrina",
                    Email = "sabrina@teste.com",
                    Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    Date = DateTime.UtcNow.AddDays(-1).AddMinutes(-68),
                    New = true,
                    Important = false
                });

                await AddAsync(new Contact
                {
                    Name = "Lucas pedroso",
                    Email = "lucaspedroso@teste.com",
                    Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    Date = DateTime.UtcNow.AddDays(-1).AddMinutes(-30),
                    New = true,
                    Important = false
                });

                await AddAsync(new Contact
                {
                    Name = "Mathias pereira",
                    Email = "mathiaspereira@teste.com",
                    Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    Date = DateTime.UtcNow.AddHours(-7).AddMinutes(-40),
                    New = true,
                    Important = false
                });

                await AddAsync(new Contact
                {
                    Name = "Daivid batista",
                    Email = "davidbatista@teste.com",
                    Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    Date = DateTime.UtcNow.AddDays(-5).AddMinutes(-80),
                    New = true,
                    Important = false
                });
            }


            var products = await _context.Products.AnyAsync();

            if (!products)
            {
                await AddAsync(new Product
                {
                    Name = "Creme para as mãos TOP",
                    Description = "Creme maravilhoso para as suas mãos ficarem top",
                    Active = true,
                    Categoria = new Category
                    {
                        Name = "Gel feminino",
                        Department = new Department
                        {
                            Name = "Cosmético",
                            Description = "Tudo em cosméticos"
                        }
                    },
                    Date = DateTime.UtcNow,
                    OldPrice = 0.00M,
                    Price = 1.00M,
                    Quantity = 10
                });

                await AddAsync(new Product
                {
                    Name = "Vibrador para casal multifuncional",
                    Description = "Vibrador sensacional para o casal curtir",
                    Active = true,
                    Categoria = new Category
                    {
                        Name = "Vibrador para casal",
                        Department = new Department
                        {
                            Name = "Vibrador"
                        }
                    },
                    Date = DateTime.UtcNow,
                    OldPrice = 50.00M,
                    Price = 39.90M,
                    Quantity = 50
                });

                await AddAsync(new Product
                {
                    Name = "Conjunto sensual",
                    Description = "Conjunto sensual com cropped em renda e saia preto",
                    Active = true,
                    Categoria = new Category
                    {
                        Name = "Roupa Sensual",
                        Department = new Department
                        {
                            Name = "Lingerie"
                        }
                    },
                    Date = DateTime.UtcNow,
                    OldPrice = 100.00M,
                    Price = 99.90M,
                    Quantity = 5
                });
            }
        }
    }
}
