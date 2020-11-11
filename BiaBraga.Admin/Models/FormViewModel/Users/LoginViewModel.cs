using BiaBraga.Business.Dtos;
using BiaBraga.Domain.Models;

namespace BiaBraga.Admin.Models.FormViewModel.Users
{
    public class LoginViewModel
    {
        public LoginDto Login { get; set; }
        public ResultDefault ResultDefault { get; set; }
    }
}
