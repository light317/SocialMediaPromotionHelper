using Newtonsoft.Json.Linq;
using Services.Abstractions;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Services
{
    public class InstagramService : IInstagramService
    {
        private readonly IDataProcessor _dataProcessor;

        public InstagramService(IDataProcessor dataProcessor)
        {
            _dataProcessor = dataProcessor;
        }

        public async Task<ICollection<InstagramPostCommentModel>> GetPostComments()
        {
            await Task.Delay(1);

            const string filePath = @"C:\Users\Mousa\Desktop\promotion.json";
            JObject jsonData = _dataProcessor.GetJsonStringFromFileAsync(filePath);
            var x = GetCommentModels(jsonData);
            return new List<InstagramPostCommentModel>();
        }

        private ICollection<InstagramPostCommentModel> GetCommentModels(JObject jsonData)
        {
            var comments = jsonData["graphql"]["shortcode_media"]["edge_media_to_parent_comment"]["edges"];

            var postComments = new List<InstagramPostCommentModel>();

            foreach (var comment in comments)
            {
                var node = comment["node"];
                InstagramPostCommentModel postComment = new InstagramPostCommentModel
                {
                    PosterId = node["owner"]["id"].ToString(),
                    PosterUserName = node["owner"]["username"].ToString(),
                    PostBodyText = node["text"].ToString()
                };
                //postComment.Tags = ExtractTagsFromText(postComment.PostBodyText);
                postComments.Add(postComment);
            }

            return postComments;
        }

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