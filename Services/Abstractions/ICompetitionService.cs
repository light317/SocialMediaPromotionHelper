using Services.Models;
using System.Collections.Generic;

namespace Services.Abstractions
{
    public interface ICompetitionService
    {
        InstagramCommenterModel GetTagAFriendEventWinner(ICollection<InstagramCommenterModel> users);
    }
}