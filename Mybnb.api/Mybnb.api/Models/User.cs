using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Mybnb.api.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string FullName { get; set; }
    }
}
