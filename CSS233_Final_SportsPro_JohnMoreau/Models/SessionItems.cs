using System.ComponentModel.DataAnnotations;

namespace CSS233_Final_SportsPro_JohnMoreau.Models
{
    public class SessionItems
    {
        public string SortBy { get; set; } = string.Empty;

        public string SortOrder { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a technician")]
        public  int CurrentTechnicianId { get; set; }


        public SessionItems () { }

        public SessionItems (string sortBy, string sortOrder, int techId)
        {
            SortBy = sortBy;
            SortOrder = sortOrder;
            CurrentTechnicianId = techId;
        }
    }
}
