using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;

namespace Game.Connection
{
    public class UnityAuthenticationService : IAuthenticationService
    {
        public string PlayerId { get; private set; }
        public string PlayerName { get; set; }

        public async Task AuthenticateAsync()
        {
            await InitializeServicesAsync();
            await SignInAnonymouslyAsync();

            PlayerId = AuthenticationService.Instance.PlayerId;
        }

        public bool IsAuthenticated()
        {
            return UnityServices.State == ServicesInitializationState.Initialized;
        }

        private static async Task InitializeServicesAsync()
        {
            if (UnityServices.State != ServicesInitializationState.Uninitialized)
                throw new InvalidOperationException($"{nameof(UnityServices)} already initialized or in process.");

            var initializationOptions = new InitializationOptions();

//#if UNITY_EDITOR
//            var profile = ClonesManager.IsClone() ? ClonesManager.GetArgument() : "Primary";
//            initializationOptions.SetProfile(profile);
//#endif

            await UnityServices.InitializeAsync(initializationOptions);
        }

        private static async Task SignInAnonymouslyAsync()
        {
            if (AuthenticationService.Instance.IsSignedIn)
                throw new InvalidOperationException($"{AuthenticationService.Instance.PlayerId} already signed in.");

            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }
}