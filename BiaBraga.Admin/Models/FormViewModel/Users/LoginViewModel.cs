using BiaBraga.Domain.Models;
using BiaBraga.Domain.Models.Dtos;

namespace BiaBraga.Admin.Models.FormViewModel.Users
{
    public class LoginViewModel
    {
        public LoginDto Login { get; set; }
        public ResultDefault ResultDefault { get; set; }
    }
}
