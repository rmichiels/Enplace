using Blazored.Modal.Services;
using Enplace.Service.Contracts;
using Enplace.Service.Services.API;
using Microsoft.AspNetCore.Components;

namespace Enplace.Library.Layout
{
    public class BaseTile<TItem> : ComponentBase where TItem : class, ILabeled
    {
        [CascadingParameter] public IModalService Modal { get; set; } = default!;
        [Inject]
        public ApiService<TItem> API { get; set; }
        [Inject]
        public required NavigationManager Navigation { get; set; }

        [Parameter]
        public TItem? Item { get; set; } = default;
        [Parameter]
        public EventCallback<TItem> ItemChanged { get; set; }

        [Parameter]
        public ComponentState State { get; set; } = ComponentState.Details;
        [Parameter]
        public EventCallback<ComponentState> StateChanged { get; set; }
    }
}
