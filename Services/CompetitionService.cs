using Services.Abstractions;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class CompetitionService : ICompetitionService
    {
        private readonly IRandomizerService _randomizerService;

        public CompetitionService(IRandomizerService randomizerService)
        {
            _randomizerService = randomizerService;
        }

        public InstagramCommenterModel GetTagAFriendEventWinner(ICollection<InstagramCommenterModel> users)
        {
            ICollection<ulong> entries = BuildEntries(users);

            ulong winnerId = ScrambleListAndPickRandom(entries);

            return users.Where(c => c.Id == winnerId).First();
        }

        private ulong ScrambleListAndPickRandom(ICollection<ulong> entries)
        {
            var scrambledList = _randomizerService.Shuffle<ulong>(entries.ToList());

            return _randomizerService.PickRandom<ulong>(scrambledList.ToList());
        }

        private ICollection<ulong> BuildEntries(ICollection<InstagramCommenterModel> users)
        {
            var entries = users.Select(e => e.Id).Distinct().ToList();

            var extraEntries = BuildExtraEntries(users, numberOfTagsPerExtraEntry: 2);

            entries.AddRange(extraEntries);

            return entries;
        }

        private ICollection<ulong> BuildExtraEntries(ICollection<InstagramCommenterModel> users, int numberOfTagsPerExtraEntry)
        {
            var extraEntries = new List<ulong>();

            foreach (InstagramCommenterModel user in users)
            {
                int numberOfExtraEntries = GetNumberOfExtraEntries(user, numberOfTagsPerExtraEntry);

                for (int i = 0; i < numberOfExtraEntries; i++)
                {
                    extraEntries.Add(user.Id);
                }
            }
            return extraEntries;
        }

        private int GetNumberOfExtraEntries(InstagramCommenterModel user, int numberOfTagsPerExtraEntry)
        {
            return user.NumberOfUniqueTags / numberOfTagsPerExtraEntry;
        }
    }
}