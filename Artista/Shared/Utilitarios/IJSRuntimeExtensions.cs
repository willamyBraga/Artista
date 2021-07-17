using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artista.Shared.Utilitarios
{
    public static class IJSRuntimeExtensions
    {
        public static ValueTask<object> SaveAs(this IJSRuntime js, string fileName, byte[] content)
        {
            return js.InvokeAsync<object>("saveAsFile", fileName, Convert.ToBase64String(content));
        }
    }
}
