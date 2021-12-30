using System.ComponentModel.DataAnnotations;

namespace EcommShop.Contracts.Dtos.User
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "Please Enter Username..")]
        public string userName { get; set; }

        [Required(ErrorMessage = "Please Enter Password...")]
        public string userPassword { get; set; }
    }
}
