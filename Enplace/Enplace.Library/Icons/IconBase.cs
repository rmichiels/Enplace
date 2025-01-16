using Microsoft.AspNetCore.Components;
namespace Enplace.Library.Icons
{
    public class IconBase : ComponentBase
    {
        [Parameter]
        public string Class { get; set; } = string.Empty;
    }
}
