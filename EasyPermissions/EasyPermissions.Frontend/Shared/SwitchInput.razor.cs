using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace EasyPermissions.Frontend.Shared
{
        public partial class SwitchInput
        {

            [Parameter]
            public int? Value { get; set; }

            [Parameter]
            public EventCallback<int?> ValueChanged { get; set; }

            private async Task OnCheckboxChanged(ChangeEventArgs e)
            {
                Value = (bool)e.Value ? 1 : 0;
                await ValueChanged.InvokeAsync(Value);
            }
        }
}