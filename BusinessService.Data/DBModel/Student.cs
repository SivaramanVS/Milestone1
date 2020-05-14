using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessService.Data.DBModel
{
    public class Student
    {
        public int StudentId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Gender { get; set; }

        //public int? SchoolRefId { get; set; }

        [ForeignKey("SchoolRefId")] public int School { get; set; }
    }
}