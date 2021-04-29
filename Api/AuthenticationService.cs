namespace Api
{
    public interface IAuthenticationService
    {
        bool ValidateAuthorization(string username);
    }
    
    public class AuthenticationService : IAuthenticationService
    {
        public bool ValidateAuthorization(string username)
        {
            return true;
        }
    }
}