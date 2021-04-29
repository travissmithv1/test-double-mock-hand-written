namespace Api
{
    public class AuthenticationController
    {
        private readonly IIdentityProviderService _identityProvideService;
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController() : this(new IdentityProviderService(), new AuthenticationService())
        {
        }

        public AuthenticationController(IIdentityProviderService identityProvideService, 
            IAuthenticationService authenticationService)
        {
            _identityProvideService = identityProvideService;
            _authenticationService = authenticationService;
        }
        
        public bool IsAuthenticated()
        {
            var identity = _identityProvideService.GetIdentity();
            if (identity.IdentityType is IdentityType.Admin or IdentityType.Editor)
            {
                return _authenticationService.ValidateAuthorization(identity.Username);
            }
            return false;
        }
    }
}