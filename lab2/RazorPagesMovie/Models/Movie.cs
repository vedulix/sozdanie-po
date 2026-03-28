using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorPagesMovie.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название обязательно")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Длина от 3 до 60 символов")]
        [Display(Name = "Название")]
        public string Title { get; set; } = string.Empty;

        [Display(Name = "Дата выхода")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "Жанр обязателен")]
        [StringLength(30)]
        [Display(Name = "Жанр")]
        public string Genre { get; set; } = string.Empty;

        [Display(Name = "Цена")]
        [Range(1, 100, ErrorMessage = "Цена от 1 до 100")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Display(Name = "Рейтинг")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        [StringLength(5)]
        public string? Rating { get; set; }
    }
}
