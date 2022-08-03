using System.ComponentModel.DataAnnotations;

namespace ToDo_List_Task.Models
{
    public class ToDoListViewModel
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        [Display(Name = "To do task")]
        public string Name { get; set; }

        [Display(Name = "Completed")]
        public bool IsCompleted { get; set; }
    }
}
