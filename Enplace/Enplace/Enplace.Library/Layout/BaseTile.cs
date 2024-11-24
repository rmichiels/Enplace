using Blazored.LocalStorage;
using Blazored.Modal.Services;
using Enplace.Service.Contracts;
using Enplace.Service.DTO;
using Enplace.Service.Services.API;
using Enplace.Service.Services.Managers;
using Microsoft.AspNetCore.Components;

namespace Enplace.Library.Layout
{
    public class BaseTile<TItem> : ComponentBase where TItem : class, ILabeled, new()
    {
        [CascadingParameter] public IModalService ModalService { get; set; } = default!;
        [Inject]
        public virtual required ApiService<TItem> API { get; set; }
        [Inject]
        public required AsyncEventManager<TItem> ItemEvents { get; set; }
        [Inject]
        public required AsyncEventManager<Notification> NotificationManager { get; set; }
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

        [Parameter]
        public virtual EventCallback<TItem> OnItemSelected { get; set; }

        [Parameter]
        public string Class { get; set; } = string.Empty;
    }
}
