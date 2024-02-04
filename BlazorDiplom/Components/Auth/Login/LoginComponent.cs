using BlazorDiplom.Middleware;
using BlazorDiplom.ViewModels;
using BlazorSpinner;
using IdentityServerCore.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using System.Collections.Concurrent;

namespace BlazorDiplom.Components.Login
{
    public class LoginComponent : ComponentBase
    {
        public LoginModel LoginModel { get; set; } = new();

        protected EditContext? _editContext;
        private ValidationMessageStore? _messageStore;
        private BlockingCollection<Task> _concurrentTaskCollection = new BlockingCollection<Task>();

#nullable disable

        [Inject]
        protected SignInManager<User> SignInManager { get; set; }

        [Inject]
        protected UserManager<User> UserManager { get; set; }

        [Inject]
        protected SpinnerService SpinnerService { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected AuthenticationStateProvider AuthProvider { get; set; }


#nullable enable

        protected override void OnInitialized()
        {
            base.OnInitialized();

            _editContext = new(LoginModel);
            _messageStore = new(_editContext);
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            var state = AuthProvider.GetAuthenticationStateAsync().GetAwaiter().GetResult();

            var user = state.User;

            if (user.Identity != null && user.Identity.IsAuthenticated)
                NavigationManager.NavigateTo("/main", true);
        }

        public async Task SignIn()
        {
            Task.WaitAll(_concurrentTaskCollection.Select(item => item).ToArray());

            try
            {
                SpinnerService.Show();

                if (LoginModel is null)
                    throw new NullReferenceException(nameof(LoginModel));

                if (_messageStore is null)
                    throw new NullReferenceException(nameof(_messageStore));

                _messageStore.Clear();

                if (_editContext is null)
                    throw new NullReferenceException(nameof(_editContext));

                if (!_editContext.Validate())
                    return;

                var userTask = UserManager.FindByEmailAsync(LoginModel.Email);

                _concurrentTaskCollection.Add(userTask);

                var user = await userTask; 
                
                if (user == null)
                {
                    _messageStore.Add(() => LoginModel.Email, "Пользователь не найден");

                    return;
                }

                var resultTask = SignInManager.CheckPasswordSignInAsync(user, LoginModel.Password, false);
                _concurrentTaskCollection.Add(resultTask);

                var result = await resultTask;

                if (!result.Succeeded)
                {
                    _messageStore.Add(() => LoginModel.Password, "Пароль неверный");

                    return;
                }

                Guid key = Guid.NewGuid();
                AuthMiddleWare.AuthInfo[key] = (user, LoginModel.Password);
                NavigationManager.NavigateTo($"/login?key={key}", true);
            }
            finally
            {
                SpinnerService.Hide();
            }
        }
    }
}
