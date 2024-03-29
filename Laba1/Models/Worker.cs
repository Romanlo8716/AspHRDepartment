﻿using Microsoft.VisualBasic;
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
        [Required(ErrorMessage = "Введите дату рождения!")]
        public DateTime DateOfBirth { get; set; }

        //Начало конца

        [Display(Name = "Пункт")]
        [Required(ErrorMessage = "Введите населенный пункт!")]
        [RegularExpression(@"^[А-ЯЁ]{1}[а-яё]+$", ErrorMessage = "Некорректный населенный пункт!")]
        public string CityHabitation { get; set; }

        [Display(Name = "Улица")]
        [Required(ErrorMessage = "Введите названию улицы!")]
        [RegularExpression(@"^[А-ЯЁ]{1}[а-яё]+$", ErrorMessage = "Некорректная улица!")]
        public string StreetHabitation { get; set; }

        [Display(Name = "Дом")]
        [Required(ErrorMessage = "Введите номер дома!")]
        [RegularExpression(@"^[0-9/а-я]+$", ErrorMessage = "Некорректный номер дома!")]
        public string HouseHabitation { get; set; }

        [Display(Name = "Семейное положение")]
        [Required(ErrorMessage = "Введите семейное положение!")]
        public string FamilyStatus { get; set; }

        [Display(Name = "Количество детей")]
        [Required(ErrorMessage = "Введите количество детей!")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Некорректое количество детей!")]
        public int Children { get; set; }

        [Display(Name = "Электронная почта")]
        [EmailAddress]
        public string? Email { get; set; }

        [Display(Name = "Серия паспорта")]
        [Required(ErrorMessage = "Введите серию паспорта!")]
        [RegularExpression(@"^([0-9]{2}\s{1}[0-9]{2})?$", ErrorMessage = "Некорректая серия паспорта!")]
        public string SeriesPass { get; set; }

        [Display(Name = "Номер паспорта")]
        [Required(ErrorMessage = "Введите номер паспорта!")]
        [RegularExpression(@"^([0-9]{6})?$", ErrorMessage = "Некорректный номер паспорта!")]
        public string NumberPass { get; set; }

        [Display(Name = "Кем выдан")]
        [Required(ErrorMessage = "Заполните поле!")]
        [RegularExpression(@"^[А-ЯЁ]{1}[А-ЯЁа-яё ]+$", ErrorMessage = "Некорректное поле!")]
        public string IssuedByWhom { get; set; }

        [Display(Name = "Дата выдачи")]
        [Required(ErrorMessage = "Введите дату выдачи!")]

        public DateTime DateOfIssue { get; set; }

        [Display(Name = "Код подразделения")]
        [Required(ErrorMessage = "Введите код подразделения!")]
        [RegularExpression(@"^[0-9]{3}[-]{1}[0-9]{3}$", ErrorMessage = "Некорректный код подразделения!")]
        public string DivisionCode{ get; set; }

        [Display(Name = "Номер Снилс")]
        [Required(ErrorMessage = "Введите номер снилс!")]
        [RegularExpression(@"(^\d{3}-\d{3}-\d{3}[ ]{1}\d{2}$)|(^\d{3}-\d{3}-\d{3}-\d{2}$)", ErrorMessage = "Некорректный номер снилс!")]
        public string NumberSnils { get; set; }

        [Display(Name = "Номер ИНН")]
        [Required(ErrorMessage = "Введите номер ИНН!")]
        [RegularExpression(@"^[0-9]{12}?$", ErrorMessage = "Некорректный номер ИНН!")]
        public string NumberInn { get; set; }

        [Display(Name = "Пол")]
        [Required(ErrorMessage = "Введите пол!")]
        public string Gender { get; set; }

        [Display(Name = "Звание")]
       
        public string? military_title { get; set; }

        [Display(Name = "Категория годности")]
        public string?  shelf_life { get; set; }

        [Display(Name = "Категория запаса")]
        public int? stock_category { get; set; }

        [Display(Name = "Состав")]
        [RegularExpression(@"^[А-ЯЁ]{1}[а-яё ]+$", ErrorMessage = "Некорректный состав!")]
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
