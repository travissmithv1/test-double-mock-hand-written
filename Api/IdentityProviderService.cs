namespace Api
{
    public interface IIdentityProviderService
    {
        Identity GetIdentity();
    }
    
    public class IdentityProviderService : IIdentityProviderService
    {
        public Identity GetIdentity()
        {
            return new() { Username = "Fake Username", IdentityType = IdentityType.Viewer };
        }
    }
}