using System.ComponentModel.DataAnnotations;

namespace Laba1.Models
{
    public class DepartmentsAndPostsOfWorker
    {
        public int Id { get; set; }


        public int WorkerId { get; set; }

        [Display(Name = "Сотрудник")]
        public Worker? Worker { get; set; }

        [Display(Name = "Отдел")]

        public int DepartmentId { get; set; }

        [Display(Name = "Отдел")]

        public Department? Department { get; set; }

        [Display(Name = "Должность")]
        public int PostId { get; set; }

        [Display(Name = "Должность")]

        public Post? Post { get; set; }
    }
}
