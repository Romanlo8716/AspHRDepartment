namespace Laba1.Models.ViewModels
{
    public class DescriptionDepartment
    {
        public int Id { get; set; }

        public Department department { get; set; }

        public IEnumerable<DepartmentsAndPostsOfWorker> departmentsAndPostsOfWorkers { get; set; }

        public IEnumerable<PostsOfDepartment> postsOfDepartments { get; set; }
    }
}
