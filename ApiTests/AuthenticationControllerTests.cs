using Api;
using NUnit.Framework;

namespace ApiTests
{
    public class AuthenticationControllerTests
    {
        [Test]
        public void IsAuthenticated_ReturnsTrue_WhenIsAdmin()
        {
            var authenticationServiceMock = new AuthenticationServiceMock();
            authenticationServiceMock.Setup(1, "Admin");
            var authenticationController = new AuthenticationController(new IdentityProviderServiceAdminStub(), 
                authenticationServiceMock);
            var result = authenticationController.IsAuthenticated();
            
            authenticationServiceMock.Verify();
            Assert.AreEqual(true, result);
        }

        [Test]
        public void IsAuthenticated_ReturnsFalse_WhenIsViewer()
        {
            var authenticationServiceMock = new AuthenticationServiceMock();
            authenticationServiceMock.Setup(0);
            var authenticationController = new AuthenticationController(new IdentityProviderServiceViewerStub(), 
                authenticationServiceMock);
            var result = authenticationController.IsAuthenticated();
            
            authenticationServiceMock.Verify();
            Assert.AreEqual(false, result);
        }

        [Test]
        public void IsAuthenticated_ReturnsTrue_WhenIsEditor()
        {
            var authenticationServiceMock = new AuthenticationServiceMock();
            authenticationServiceMock.Setup(1, "Editor");
            var authenticationController = new AuthenticationController(new IdentityProviderServiceEditorStub(), 
                authenticationServiceMock);
            var result = authenticationController.IsAuthenticated();
            
            authenticationServiceMock.Verify();
            Assert.AreEqual(true, result);
        }
    }

    public class IdentityProviderServiceAdminStub : IIdentityProviderService
    {
        public Identity GetIdentity()
        {
            return new() { Username = "Admin", IdentityType = IdentityType.Admin };
        }
    }

    public class IdentityProviderServiceViewerStub : IIdentityProviderService
    {
        public Identity GetIdentity()
        {
            return new() { Username = "Viewer", IdentityType = IdentityType.Viewer };
        }
    }

    public class IdentityProviderServiceEditorStub : IIdentityProviderService
    {
        public Identity GetIdentity()
        {
            return new() { Username = "Editor", IdentityType = IdentityType.Editor };
        }
    }

    public class AuthenticationServiceMock : IAuthenticationService
    {
        private int ExpectedNumberOfCalls { get; set; }
        private string ExpectedUsername { get; set; }
        private int _actualNumberOfCalls;
        private string _actualUsername;
        
        public void Setup(int numberOfCalls, string username)
        {
            Setup(numberOfCalls);
            ExpectedUsername = username;
        }
        public void Setup(int numberOfCalls)
        {
            ExpectedNumberOfCalls = numberOfCalls;
        }
        
        public bool ValidateAuthorization(string username)
        {
            _actualNumberOfCalls++;
            _actualUsername = username;
            var authenticationService = new AuthenticationService();
            return authenticationService.ValidateAuthorization(username);
        }

        public void Verify()
        {
            Assert.AreEqual(ExpectedNumberOfCalls, _actualNumberOfCalls);
            Assert.AreEqual(ExpectedUsername, _actualUsername);
        }
    }
}