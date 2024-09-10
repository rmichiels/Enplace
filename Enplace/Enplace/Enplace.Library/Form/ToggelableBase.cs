using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enplace.Library.Form
{
    public class ToggelableBase<TField> : ComponentBase
    {
        [CascadingParameter]
        public EditContext Context { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public TField Field { get; set; }
        [Parameter]
        public EventCallback<TField> FieldChanged { get; set; }

        [Parameter]
        public ComponentState State { get; set; }
        [Parameter]
        public EventCallback<ComponentState> StateChanged { get; set; }

        protected virtual async void Callback()
        {
            await FieldChanged.InvokeAsync(Field);
            await StateChanged.InvokeAsync(State);
            StateHasChanged();
        }

        protected virtual async void Toggle(ComponentState toggleTo)
        {
            State = toggleTo;
            await StateChanged.InvokeAsync(State);
            StateHasChanged();
        }
    }
}
