using System.Collections.Generic;

namespace Services.Models
{
    public class InstagramCommenterModel
    {
        public ulong Id { get; set; }
        public string UserName { get; set; }
        public ICollection<InstagramPostCommentModel> Comments { get; set; }
        public ICollection<string> AllTags { get; set; } = new List<string>();
        public ICollection<string> UniqueTags { get; set; } = new List<string>();
        public int NumberOfUniqueTags => UniqueTags.Count;
    }
}