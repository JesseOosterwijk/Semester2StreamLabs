using Data.Interfaces;
using Models;

namespace Logic
{
    public class AccountLogic
    {
        private readonly IAccountContext _account;

        public AccountLogic(IAccountContext account)
        {
            _account = account;
        }

        public string ChangePassword(int id)
        {
            return _account.ChangePassword(id);
        }

        public void DeleteUser(int userId)
        {
            _account.DeleteUser(userId);
        }

        public void UpdateStatus(int id, bool status)
        {
            _account.UpdateStatus(id, status);
        }
    }
}
