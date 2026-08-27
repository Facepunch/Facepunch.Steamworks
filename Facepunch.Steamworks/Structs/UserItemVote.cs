using Steamworks.Data;

namespace Steamworks.Ugc
{
    /// <summary>
    /// How the local user has voted on a Workshop item. Returned by <see cref="Item.GetUserVote"/>,
    /// so your UI can show the vote they already cast instead of offering a fresh one.
    /// </summary>
    /// <remarks>
    /// A user who has never interacted with the item has all three flags <see langword="false"/>.
    /// That is distinct from <see cref="VoteSkipped"/>, which means they actively chose not to vote.
    /// </remarks>
    /// <example>
    /// <code>
    /// var vote = await item.GetUserVote();
    /// if ( vote.HasValue &amp;&amp; !vote.Value.VotedUp &amp;&amp; !vote.Value.VotedDown )
    ///     ShowVotePrompt( item );
    /// </code>
    /// </example>
    public struct UserItemVote
    {
        /// <summary>Whether the user gave this item a thumbs up.</summary>
        public bool VotedUp;

        /// <summary>Whether the user gave this item a thumbs down.</summary>
        public bool VotedDown;

        /// <summary>
        /// Whether the user explicitly declined to vote &#8212; the "I'll rate this later" / skip
        /// action in the Workshop UI. Not the same as never having seen the item, which leaves all
        /// three flags false.
        /// </summary>
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
