using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Laba1.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Введите название отдела!")]
        [RegularExpression(@"^[А-ЯЁ]{1}[а-яё ]+", ErrorMessage = "Название отдела должно быть с заглавной буквы!")]
        public string Name { get; set; }

        [Display(Name = "Телефон")]
        [Required(ErrorMessage = "Введите номер телефона!")]
        [RegularExpression(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", ErrorMessage = "Некорректный номер телефона!")]
        public string Phone { get; set; }

        [Display(Name = "Адрес отдела")]
        public int idAdressDepartment { get; set; }

        [Display(Name = "Адрес отдела")]
        [ForeignKey("idAdressDepartment")]
        public AdressDepartment? AdressDepartment { get; set; }
    }
}
