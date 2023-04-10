using System.ComponentModel.DataAnnotations;

namespace Laba1.Models
{
    public class AdressDepartment
    {
        public int Id { get; set; }

        [Display(Name = "Город")]
        [Required(ErrorMessage = "Введите название города!")]
        [RegularExpression(@"^[А-ЯЁ]{1}[а-яё ]+", ErrorMessage = "Некорректное название города!")]
        public string City { get; set; }

        [Display(Name = "Улица")]
        [Required(ErrorMessage = "Введите название улицы!")]
        [RegularExpression(@"^[А-ЯЁ]{1}[а-яё ]+", ErrorMessage = "Некорректное название улицы!")]
        public string Street { get; set; }

        [Display(Name = "Дом")]
        [Required(ErrorMessage = "Введите номер дома!")]
        [RegularExpression(@"^[0-9]{1}[0-9а-я/]+", ErrorMessage = "Некорректный номер дома!")]
        public string House { get; set; }


    }
}
