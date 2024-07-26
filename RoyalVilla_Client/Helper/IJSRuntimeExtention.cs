using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace RoyalVilla_Client.Helper
{
    public static class IJSRuntimeExtention
    {
        //public static async ValueTask ToastrSucess(this IJSRuntime JSRuntime, string message)
        //{
        //    await JSRuntime.InvokeVoidAsync("ShowToastr", "sucess", message);
        //}
        //public static async ValueTask ToastrError(this IJSRuntime JSRuntime, string message)
        //{
        //    await JSRuntime.InvokeVoidAsync("ShowToastr", "error",  message);
        //}
        public static async ValueTask ToastrSucess(this IJSRuntime JSRuntime, string message)
        {
            await JSRuntime.InvokeVoidAsync("ShowToastr", "success", message);
        }
        public static async ValueTask ToastrError(this IJSRuntime JSRuntime, string message)
        {
            await JSRuntime.InvokeVoidAsync("ShowToastr", "error", message);
        }
    }
}
