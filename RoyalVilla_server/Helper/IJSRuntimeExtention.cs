using Blazored.Toast;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace RoyalVilla_server.Helper
{
    public static class IJSRuntimeExtention
    {
        public static async ValueTask ToastrSucess(this IJSRuntime JSRuntime, string message)
        {
            await JSRuntime.InvokeVoidAsync("ShowToastr", "sucess", message);
        }
        public static async ValueTask ToastrError(this IJSRuntime JSRuntime, string message)
        {
            await JSRuntime.InvokeVoidAsync("ShowToastr", "error",  message);
        }
        public static async ValueTask ToastSuccess(this IJSRuntime JSRuntime,string message)
        {
            await JSRuntime.InvokeVoidAsync("ShowSwal", "success", message);
        }
        public static async ValueTask ToastError(this IJSRuntime JSRuntime, string message)
        {
            await JSRuntime.InvokeVoidAsync("ShowSwal", "error", message);
        }
        public static async ValueTask ToastCheck(this IJSRuntime JSRuntime, string message)
        {
            await JSRuntime.InvokeVoidAsync("ShowSwal", "check", message);
        }

    }
}
