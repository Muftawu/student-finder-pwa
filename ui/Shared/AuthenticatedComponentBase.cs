using Microsoft.AspNetCore.Components;

public class AuthenticatedComponentBase : ComponentBase
{
    [Inject] protected CustomAuthenticationStateProvider? CustomAuthenticationStateProvider { get; set; }
    [Inject] protected NavigationManager? NavigationManager { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var authState = await CustomAuthenticationStateProvider!.GetAuthenticationStateAsync();
        var user = authState.User;

        if (!user.Identity?.IsAuthenticated ?? true)
        {
            NavigationManager?.NavigateTo("/login", true);
        }
    }
}
