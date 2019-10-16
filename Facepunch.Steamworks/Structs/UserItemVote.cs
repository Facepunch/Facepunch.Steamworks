using Steamworks.Data;

namespace Steamworks.Ugc
{
    public struct UserItemVote
    {
        public bool VotedUp;
        public bool VotedDown;
        public bool VoteSkipped;

        internal static UserItemVote? From(GetUserItemVoteResult_t result)
        {
            return new UserItemVote
            {
                VotedUp = result.VotedUp,
                VotedDown = result.VotedDown,
                VoteSkipped = result.VoteSkipped
            };
        }
    }
}
