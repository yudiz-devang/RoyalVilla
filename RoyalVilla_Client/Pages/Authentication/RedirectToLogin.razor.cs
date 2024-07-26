using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace RoyalVilla_Client.Pages.Authentication
{
    public partial class RedirectToLogin
    {
        [Inject]
        private NavigationManager navigationManager { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> authenticationState { get; set; }

        bool notauthorize { get; set; } = false;
        protected override async Task OnInitializedAsync()
        {
            var authstate = await authenticationState;
            if (authstate?.User.Identities is null || !authstate.User.Identity.IsAuthenticated)
            {
                var returnUrl = navigationManager.ToBaseRelativePath(navigationManager.Uri);
                if (string.IsNullOrEmpty(returnUrl))
                {
                    navigationManager.NavigateTo("login", true);
                }
                else
                {
                    navigationManager.NavigateTo($"login?returnUrl={returnUrl}", true);
                }
            }
            else
            {
                notauthorize= true; 
            }
        }
    }
}
