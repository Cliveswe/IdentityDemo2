using System.ComponentModel.DataAnnotations;

namespace IdentityDemo.Web.Views.Car
{
    public class CreateVM
    {
        [Required(ErrorMessage = "You must specify a Make")]
        public required string Make { get; set; }
        public required string Model { get; set; }
        public required int Year { get; set; }
    }
}
