using System.ComponentModel.DataAnnotations;

namespace BusinessService.Data.DBModel
{
    public class School
    {
        public int Id { get; set; }

        [Required]

        public string Name { get; set; }


    }


}