using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Laba1.Models
{
    public class MedicalBook
    {

        public int Id { get; set; }

        [Display(Name = "Место обследования")]
        [Required(ErrorMessage = "Введите название места обследования!")]
        public string placeExam { get; set; }

        [Display(Name = "Дата обследования")]
        [Required(ErrorMessage = "Введите дату обследования!")]
        public DateTime dateExam  { get; set; }

        [Display(Name = "Заключение")]
        [Required(ErrorMessage = "Введите заключение!")]
        public string conclusion { get; set; }

        [Display(Name = "Сотрудник")]
        public int? WorkerId { get; set; }

        [ForeignKey("WorkerId")]
        [Display(Name = "Сотрудник")]
        public Worker? Worker { get; set; }
    }
}
