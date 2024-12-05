namespace OpenTelemetryWebAPI
{
    public interface IUserService
    {
        bool Login(string username, string password);
    }
}
