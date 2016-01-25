using CTS;

namespace ProjektGrupowy.Models
{
    public class UserViewModel
    {
        public int Score { get; private set; }
        public AspNetUser User { get; private set; }

        public UserViewModel(int score, AspNetUser user)
        {
            Score = score;
            User = user;
        }
    }
}