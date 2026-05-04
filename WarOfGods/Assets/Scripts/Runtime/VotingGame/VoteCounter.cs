using Mirror;
using TMPro;
using UnityEngine;

namespace VotingGame
{
    public class VoteCounter : NetworkBehaviour
    {
        [SyncVar(hook = nameof(OnVoteCountChanged))]
        public int votesForOptionA;

        [SyncVar(hook = nameof(OnVoteCountChanged))]
        public int votesForOptionB;

        [SerializeField] private TMP_Text uiVoteA;
        [SerializeField] private TMP_Text uiVoteB;

        // Hook to update UI when votes change
        private void OnVoteCountChanged(int oldVotes, int newVotes)
        {
            //this wil be called on clients only and called when the hooked value is changed on the server
            // Update UI here
        }

        [Command(requiresAuthority = false)]
        public void CmdCastVote(int option)
        {
            if (option == 1) votesForOptionA++;
            else if (option == 2) votesForOptionB++;

            Debug.Log("Vote casted on server");
            UpdateServerUI();
        }

        private void UpdateServerUI()
        {
            uiVoteA.text = $"A has: '{votesForOptionA}' Votes!";
            uiVoteB.text = $"B has: '{votesForOptionB}' Votes!";
        }

        public void VoteEndButton()
        {
            Debug.Log($"A:{votesForOptionA} && B:{votesForOptionB}");
        }
    }
}
