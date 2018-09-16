
using API.ViewModels.Validations;
using FluentValidation;

namespace API.ViewModels
{
    
    //[Validator(typeof(CredentialsViewModelValidator))]
    public class CredentialsViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
