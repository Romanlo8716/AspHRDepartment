using Microsoft.VisualBasic;
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
        public string? Middlename { get; set; }

        [Display(Name = "Телефон")]
        [Phone]
        [Required(ErrorMessage = "Введите номер телефона!")]
        [RegularExpression(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", ErrorMessage = "Некорректный номер телефона!")]
        public string Phone { get; set; }


        [Display(Name = "Дата рождения")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Пункт")]
        public string CityHabitation { get; set; }

        [Display(Name = "Улица")]
        public string StreetHabitation { get; set; }

        [Display(Name = "Дом")]
        public string HouseHabitation { get; set; }

        [Display(Name = "Семейное положение")]
        public string FamilyStatus { get; set; }

        [Display(Name = "Количество детей")]
        public int Children { get; set; }

        [Display(Name = "Электронная почта")]
        [EmailAddress]
        public string? Email { get; set; }

        [Display(Name = "Серия паспорта")]
        public int SeriesPass { get; set; }

        [Display(Name = "Номер паспорта")]
        public int NumberPass { get; set; }

        [Display(Name = "Кем выдан")]
        public string IssuedByWhom { get; set; }

        [Display(Name = "Дата выдачи")]

        public DateTime DateOfIssue { get; set; }

        [Display(Name = "Код подразделения")]
        public string DivisionCode{ get; set; }

        [Display(Name = "Номер Снилс")]
        public int NumberSnils { get; set; }

        [Display(Name = "Номер ИНН")]
        public int NumberInn { get; set; }

        [Display(Name = "Пол")]
        public string Gender { get; set; }

        [Display(Name = "Звание")]
        public string? military_title { get; set; }

        [Display(Name = "Категория годности")]
        public string?  shelf_life { get; set; }

        [Display(Name = "Категория запаса")]
        public int? stock_category { get; set; }

        [Display(Name = "Состав")]
        public string? profile { get; set; }

        [Display(Name = "ВУС")]
        public string? vus { get; set; }

        [Display(Name = "Название коммисариата")]
        public string? name_kommis { get; set; }

        [Display(Name = "Фото сотрудника")]
        public string? Photo { get; set; }

        [Display(Name = "Описание сотрудника")]
        public string? DescriptionWorker { get; set; }

        public bool dissmisStatus { get; set; }


    }
}
