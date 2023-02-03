using CustomerService.DTOs;
using FluentValidation;
using Models;

namespace CustomerService.Validators
{
    public class CustomerValidator : AbstractValidator<CustomerDTO>
    {
        private static CustomerValidator? _instance;

        public static CustomerValidator Instance
        {
            get
            {
                if
                    (_instance is null)
                {
                    _instance = new CustomerValidator();
                }
                return _instance;
            }

            private set
            {
                _instance = value;
            }
        }

        public CustomerValidator()
        {
            RuleFor(x => x.Email).Matches("^\\S+@\\S+\\.\\S+$");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Please specify a first name");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Please specify a last name");
        }
    }
}
