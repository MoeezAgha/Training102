using System.ComponentModel.DataAnnotations;

namespace Training102.SharedModel.ViewModel
{
    public class TrainingViewModel
    {
        public int TrainingId { get; set; }

        [Required(ErrorMessage = "The Title field is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The Description field is required.")]
        public string Description { get; set; }

        public int CreatedByUserId { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }
    }

}