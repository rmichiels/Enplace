using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Enplace.Library.Form
{
    public partial class ToggelableBase<TField> : ComponentBase where TField : IComparable
    {
        [CascadingParameter]
        public EditContext? EditContext { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Parameter]
        public required TField Field
        {
            get => _field;
            set
            {
                // Make comparsion to avoid 2-way-binding loop
                if (Comparer<TField>.Default.Compare(_field, value) != 0)
                {
                    _field = value;
                    FieldChanged.InvokeAsync(value);
                }
            }
        }
        private TField _field;
        [Parameter]
        public EventCallback<TField> FieldChanged { get; set; }

        [Parameter]
        public ComponentState State { get; set; }
        [Parameter]
        public EventCallback<ComponentState> StateChanged { get; set; }

        [Parameter]
        public string Placeholder { get; set; } = string.Empty;

        [Parameter]
        public StateManagement StateManagement { get; set; } = StateManagement.External;
        [Parameter]
        public string Class { get; set; } = string.Empty;

        public virtual async Task Callback()
        {
            await FieldChanged.InvokeAsync(Field);
            await StateChanged.InvokeAsync(State);
            StateHasChanged();
        }

        protected virtual async Task Toggle(ComponentState toggleTo)
        {
            State = toggleTo;
            await StateChanged.InvokeAsync(State);
            StateHasChanged();
        }
    }
}
