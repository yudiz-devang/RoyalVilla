using Microsoft.AspNetCore.Components;
using RoyalVilla_Client.Service.IService;

namespace RoyalVilla_Client.Pages.Authentication
{
    public partial class Logout
    {
        [Inject] 
        public IAuthenticationServices authenticationServices { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await authenticationServices.Logout();
            navigationManager.NavigateTo("/");
        }
    }
}
