using System;
using System.ComponentModel.DataAnnotations;


namespace BusinessService.Data.DBModel
{
    public class SchoolItem
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public decimal Fees { get; set; }
        public string Principal { get; set; }
    }
}
