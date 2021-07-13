using Newtonsoft.Json.Linq;
using Services.Abstractions;
using Services.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Services
{
    public class InstagramService : IInstagramService
    {
        private readonly IDataProcessor _dataProcessor;
        private readonly ICompetitionService _competitionService;
        private readonly IRandomizerService _randomizerService;

        public InstagramService(IDataProcessor dataProcessor, ICompetitionService competitionService, IRandomizerService randomizerService)
        {
            _dataProcessor = dataProcessor;
            _competitionService = competitionService;
            _randomizerService = randomizerService;
        }

        public async Task<ICollection<InstagramPostCommentModel>> GetPostComments(string postId)
        {
            const string filePath = @"C:\Users\Mousa\Desktop\promotion.json";
            string url = $"https://www.instagram.com/p/{postId}/?__a=1";

            await Task.Delay(1);

            var comments = await GetCommentModels(url);
            var users = await GetUniqueCommenters(url);

            InstagramCommenterModel winner = _competitionService.GetTagAFriendEventWinner(users);
            return comments;
        }

        public async Task<ICollection<InstagramCommenterModel>> GetUniqueCommenters(string postId)
        {
            await Task.Delay(1);

            string url = $"https://www.instagram.com/p/{postId}/?__a=1";

            var comments = await GetCommentModels(url);

            // get a list of unique user ids
            ICollection<ulong> userIds = comments.Select(c => c.PosterId).Distinct().ToList();

            var users = new List<InstagramCommenterModel>();
            foreach (ulong userId in userIds)
            {
                var user = new InstagramCommenterModel
                {
                    Id = userId,
                    UserName = comments.Where(c => c.PosterId == userId).Select(e => e.PosterUserName).First(),
                    Comments = comments.Where(c => c.PosterId == userId).ToList(),
                };

                foreach (InstagramPostCommentModel commentModel in user.Comments)
                {
                    foreach (string tag in commentModel.Tags)
                    {
                        user.AllTags.Add(tag);
                    }
                }

                user.UniqueTags = user.AllTags.Distinct().ToList();

                users.Add(user);
            }

            return users;
        }

        private async Task<ICollection<InstagramPostCommentModel>> GetCommentModels(string url)
        {
            JObject jsonData = await _dataProcessor.GetJsonFromURLAsync(url);

            var comments = jsonData["graphql"]["shortcode_media"]["edge_media_to_parent_comment"]["edges"];

            var postComments = new List<InstagramPostCommentModel>();

            foreach (var comment in comments)
            {
                var node = comment["node"];
                InstagramPostCommentModel postComment = new InstagramPostCommentModel
                {
                    CommentId = (ulong)node["id"],
                    PosterId = (ulong)node["owner"]["id"],
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