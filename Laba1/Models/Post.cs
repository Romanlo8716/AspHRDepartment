using System.ComponentModel.DataAnnotations;

namespace Laba1.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Введите название должности!")]
        [RegularExpression(@"^[А-ЯЁ]{1}[а-яё ]+", ErrorMessage = "Название должности должно быть с заглавной буквы!")]
        public string Title { get; set; }

        [Display(Name = "Начальная зарплата")]
        [Required(ErrorMessage = "Введите зарплату!")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Некорректное значение зарплаты!")]
        [DataType(DataType.Currency)]
        public double Salary { get; set; }
    }
}
