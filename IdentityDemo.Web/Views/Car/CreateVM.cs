using System.ComponentModel.DataAnnotations;

namespace IdentityDemo.Web.Views.Car
{
    public class CreateVM
    {
        [Required(ErrorMessage = "You must specify a Make")]
        [Length(1, 30)]
        public required string Make { get; set; }

        [Required(ErrorMessage = "You must specify a Model")]
        [Length(1, 30)]
        public required string Model { get; set; }

        [Required(ErrorMessage = "You must specify a Year")]
        [Range(1920, 2026)]
        public required int Year { get; set; }

        public IEnumerable<int> YearOptions { get; set; } = Enumerable.Empty<int>();
    }
}
