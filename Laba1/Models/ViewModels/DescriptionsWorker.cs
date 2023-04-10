namespace Laba1.Models.ViewModels
{
    public class DescriptionsWorker
    {
        public int Id { get; set; }
        public Worker worker { get; set; }

        public IEnumerable<LaborBook> laborBooks { get; set; }

        public IEnumerable<Education> educations { get; set; }

        public IEnumerable<Vacation> vacations { get; set; }

        public IEnumerable<MedicalBook> medicalBooks { get; set; }

        public IEnumerable<DepartmentsAndPostsOfWorker> departmentsAndPostsOfWorkers { get; set; }

       
    }
}
