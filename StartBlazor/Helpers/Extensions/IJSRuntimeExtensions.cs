using Microsoft.JSInterop;

namespace StartBlazor.Helpers.Extensions
{
    public static class IJSRuntimeExtensions
    {
        public static async Task Toastr(this IJSRuntime js, ToastrType type, string message)
        {
            await js.InvokeVoidAsync("ShowToastr", type.ToString().ToLower(), message);
        }
    }

    public enum ToastrType
    {
        Success = 0,
        Warning = 1,
        Error = 2,
        Info = 3
    }
}
