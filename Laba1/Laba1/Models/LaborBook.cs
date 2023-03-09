using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Laba1.Models
{
    public class LaborBook
    {

        public int Id { get; set; }

        [Display(Name = "Дата")]
        [Required(ErrorMessage = "Введите дату записи!")]
        public DateTime dateRecord { get; set; }

        [Display(Name = "Наименование работы")]
        [Required(ErrorMessage = "Введите наименование работы!")]
        public string nameWork { get; set; }

        [Display(Name = "Сведения")]
        [Required(ErrorMessage = "Введите сведения о работе!")]
        public string intelligenceWork { get; set; }

        [Display(Name = "Название документа")]
        [Required(ErrorMessage = "Введите название документа!")]
        public string nameDocument { get; set; }

        [Display(Name = "Номер документа")]
        [Required(ErrorMessage = "Введите номер документа!")]
        public string numberDocument  { get; set; }

        [Display(Name = "Сотрудник")]
        public int? WorkerId { get; set; }

        [ForeignKey("WorkerId")]
        [Display(Name = "Сотрудник")]
        public Worker? Worker { get; set; }
    }
}
