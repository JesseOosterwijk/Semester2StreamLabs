namespace Data.Interfaces
{
    public interface IAccountContext
    {
        string ChangePassword(int id);
        void DeleteUser(int userId);
        void UpdateStatus(int id, bool status);
    }
}
