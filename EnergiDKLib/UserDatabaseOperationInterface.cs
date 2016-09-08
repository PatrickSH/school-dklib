namespace EnergiDKLib
{
    internal interface UserDatabaseOperationInterface
    {
        bool addUser(string username, string password, bool free);
        bool validateLogin(string username, string password);
    }
}