using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Laba1.Models
{
    public class Education
    {
        public int Id { get; set; }

        [Display(Name = "Серия диплома")]
        [RegularExpression(@"^[0-9]{6}$", ErrorMessage = "Некорректная серия диплома!")]
        [Required(ErrorMessage = "Введите серию диплома!")]
        public string diplomSeries { get; set; }

        [Display(Name = "Номер диплома")]
        [RegularExpression(@"^[0-9]{7}$", ErrorMessage = "Некорректная серия диплома!")]
        [Required(ErrorMessage = "Введите номер диплома!")]

        public string diplomNumber { get; set; }

        [Display(Name = "Специальность")]
        [RegularExpression(@"^[А-ЯЁ]{1}[а-яёА-ЯЁ ]+$", ErrorMessage = "Некорректная специальность!")]
        [Required(ErrorMessage = "Введите специальность!")]
        public string special { get; set; }

        [Display(Name = "Дата выдачи")]
        [Required(ErrorMessage = "Введите год окончания!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

        public DateTime yearEnd { get; set; }

        [Display(Name = "Сотрудник")]
        public int? WorkerId { get; set; }

        [ForeignKey("WorkerId")]
        [Display(Name = "Сотрудник")]
        public Worker? Worker { get; set; }
    }
}
