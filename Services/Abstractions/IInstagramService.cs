using Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IInstagramService
    {
        //Task<ICollection<InstagramPostCommentModel>> GetPostComments();
        Task<ICollection<InstagramPostCommentModel>> GetPostComments(string postId);

        Task<ICollection<InstagramCommenterModel>> GetUniqueCommenters(string postId);
    }
}