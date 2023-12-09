/*
* John Moreau
* CSS233
* 12/9/2023
* 
*/

using System.ComponentModel.DataAnnotations;

namespace CSS233_Final_SportsPro_JohnMoreau.Models
{
    public class Country
    {

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Slug => Name?.Replace(' ', '-').ToLower() + '-' + Code?.Replace(' ', '-').ToLower(); 
    }
}
