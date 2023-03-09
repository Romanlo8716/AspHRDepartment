using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Laba1.Models
{
    public class Worker
    {
        public int Id { get; set; }

        [Display(Name = "Имя")]
        [RegularExpression(@"^[А-ЯЁ]{1}[а-яё]+$", ErrorMessage = "Некорректное имя!")]
        [Required(ErrorMessage = "Введите имя!")]
        public string Name { get; set; }

        [Display(Name = "Фамилия")]
        [RegularExpression(@"^[А-ЯЁ]{1}[а-яё]+$", ErrorMessage = "Некорректная фамилия!")]
        [Required(ErrorMessage = "Введите фамилию!")]
        public string Surname { get; set; }

        [Display(Name = "Отчество")]
        [Required(ErrorMessage = "Введите Отчество!")]
        [RegularExpression(@"^[А-ЯЁ]{1}[а-яё]+$", ErrorMessage = "Некорректное отчество!")]
        public string Middlename { get; set; }

        [Display(Name = "Телефон")]
        [Phone]
        [Required(ErrorMessage = "Введите номер телефона!")]
        [RegularExpression(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", ErrorMessage = "Некорректный номер телефона!")]
        public string Phone { get; set; }

        [Display(Name = "Пол")]
        public string Gender { get; set; }

        [Display(Name = "Должность")]
        
        public int idPost { get; set; }

        [Display(Name = "Должность")]
        [ForeignKey("idPost")]
        public Post? Post { get; set; }

        [Display(Name = "Отдел")]
       
        public int idDepartment { get; set; }

        [Display(Name = "Отдел")]
        [ForeignKey("idDepartment")]
        public Department? Department { get; set; }


    }
}
