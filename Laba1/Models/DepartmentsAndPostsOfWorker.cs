using System.ComponentModel.DataAnnotations;

namespace Laba1.Models
{
    public class DepartmentsAndPostsOfWorker
    {
        public int Id { get; set; }


        public int WorkerId { get; set; }

        [Display(Name = "Сотрудник")]
        public Worker? Worker { get; set; }

        public int DepartmentId { get; set; }

        [Display(Name = "Отдел")]

        public Department? Department { get; set; }

        public int PostId { get; set; }

        [Display(Name = "Должность")]

        public Post? Post { get; set; }
    }
}
