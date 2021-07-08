using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Services.Models
{
    public class InstagramPostCommentModel
    {
        public string PosterId { get; set; }
        public string PosterUserName { get; set; }
        public string PostBodyText { get; set; }
        public ICollection<string> Tags => ExtractTagsFromText(PostBodyText);
        public int NumberOfTags => Tags.Count;

        private ICollection<string> ExtractTagsFromText(string postBodyText)
        {
            var matches = Regex.Matches(postBodyText, @"(?<!\w)@\w+");//extract with regext

            var tags = new List<string>();

            foreach (Match match in matches)
            {
                tags.Add(match.Value);
            }

            return tags;
        }
    }
}