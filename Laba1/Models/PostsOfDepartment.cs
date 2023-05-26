using System.ComponentModel.DataAnnotations;

namespace Laba1.Models
{
    public class PostsOfDepartment
    {
        public int Id { get; set; }

        [Display(Name = "Должность")]
        public int PostId { get; set; }

        [Display(Name = "Должность")]
        public Post? Post { get; set; }

        [Display(Name = "Отдел")]

        public int DepartmentId { get; set; }

        [Display(Name = "Отдел")]

        public Department? Department { get; set; }

        [Display(Name = "Количество вакансий")]
        [Required(ErrorMessage = "Введите количество мест на данную должность!")]
        public int Count { get; set; }
    }
}
