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
        public required int Year { get; set; }

        public IEnumerable<int> YearOptions { get; set; } = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateVM"/> class and sets up default values for year-related
        /// properties.
        /// </summary>
        /// <remarks>The constructor initializes the <see cref="YearOptions"/> property with a range of
        /// years from 1920 to the current year,  inclusive, in descending order. The <see cref="Year"/> property is set
        /// to the current year by default.</remarks>
        public CreateVM() {
            int currentYear = DateTime.Now.Year;
            YearOptions = Enumerable.Range(1920, currentYear - 1920 + 2).Reverse();

        }
    }
}
