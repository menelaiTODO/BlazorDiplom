using BlazorDiplom.ViewModels;
using BlazorSpinner;
using IdentityServerCore.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;

namespace BlazorDiplom.Components.Auth.SignUp
{
    public class SignUpComponent : ComponentBase, IDisposable
    {
        #region fields

        protected EditContext? _editContext;
        private ValidationMessageStore? _messageStore;

#nullable disable

        [Inject]
        protected UserManager<User> UserManager { get; set; }

#nullable enable

        #endregion

        public SignUpModel SignUpModel { get; set; } = new();

        [Inject]
        protected SpinnerService SpinnerService { get; set; } = new();

        protected override void OnInitialized()
        {
            base.OnInitialized();

            _editContext = new(SignUpModel);
            _editContext.OnFieldChanged += EditContext_OnFieldChanged;
            _messageStore = new(_editContext);
        }

        private void EditContext_OnFieldChanged(object? sender, FieldChangedEventArgs e)
        {
            if (SignUpModel is null)
                throw new NullReferenceException(nameof(SignUpModel));

            if (_messageStore is null)
                throw new NullReferenceException(nameof(_messageStore));


            if (e.FieldIdentifier.FieldName.Equals(nameof(SignUpModel.SecondPassword)))
            {
                if (SignUpModel.SecondPassword == string.Empty)
                {
                    _messageStore.Clear(() => SignUpModel.SecondPassword);

                    return;
                }

                _messageStore.Clear(() => SignUpModel.SecondPassword);

                if (!SignUpModel.SecondPassword.Equals(SignUpModel.FirstPassword))
                {
                    _messageStore.Add(() => SignUpModel.SecondPassword, "Введенные пароли не совпадают");
                }
            }
        }

        public async Task SignUp()
        {
            try
            {
                SpinnerService.Show();

                SignUpModel.Messages.Clear();

                if (_editContext is null)
                    throw new NullReferenceException(nameof(_editContext));

                if (_editContext.Validate())
                {
                    var identityResult = await UserManager.CreateAsync(new User { UserName = SignUpModel.Email.Trim().Split("@")[0], Email = SignUpModel.Email }, SignUpModel.FirstPassword);

                    if (!identityResult.Succeeded)
                    {
                        foreach (var item in identityResult.Errors)
                            SignUpModel.Messages.Add(item.Description);
                    }
                    else
                    {
                        SignUpModel.Messages.Add("Вы зарегистрированы, выполните вход");
                    }
                }
            }
            finally
            {
                SpinnerService.Hide();
            }
        }

        public void Dispose()
        {
            if (_editContext != null)
                _editContext.OnFieldChanged -= EditContext_OnFieldChanged;
        }
    }
}
