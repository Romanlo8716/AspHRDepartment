using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Laba1.Models
{
    public class Vacation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Дата начала отпуска")]
        public DateTime dateStart { get; set; }

        [Display(Name = "Дата окончания отпуска")]
        public DateTime dateEnd   { get; set; }

        [Display(Name = "Тип отпуска")]
        public string typeVacation { get; set; }

        [Display(Name = "Сотрудник")]
        public int? WorkerId { get; set; }

        [ForeignKey("WorkerId")]
        [Display(Name = "Сотрудник")]
        public Worker? Worker { get; set; } 
    }
}
