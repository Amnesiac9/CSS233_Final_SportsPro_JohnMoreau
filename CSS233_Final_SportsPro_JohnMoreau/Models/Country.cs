using System.ComponentModel.DataAnnotations;

/*
* John Moreau
* CSS233
* 10/29/2023
*
*
*/


namespace john_moreau_MidTerm.Models
{
    public class Country
    {

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Slug => Name?.Replace(' ', '-').ToLower() + '-' + Code?.Replace(' ', '-').ToLower(); 
    }
}
