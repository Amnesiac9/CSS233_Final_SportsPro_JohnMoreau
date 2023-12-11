using System.ComponentModel.DataAnnotations;

namespace CSS233_Final_SportsPro_JohnMoreau.Models
{
    public class IncidentSessionItems
    {
        [Required(ErrorMessage = "Please select a technician")]
        [Range(-1, int.MaxValue, ErrorMessage = "Please select a technician")]
        public int Id { get; set; }

        public string SortBy { get; set; } = string.Empty;

        public string SortOrder { get; set; } = string.Empty;

        public IncidentSessionItems () { }

        public IncidentSessionItems (string sortBy, string sortOrder, int techId)
        {
            SortBy = sortBy;
            SortOrder = sortOrder;
            Id = techId;
        }
    }
}
