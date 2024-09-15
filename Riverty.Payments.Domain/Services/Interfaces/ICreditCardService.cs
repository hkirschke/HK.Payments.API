using HK.Payments.Core.Models;

namespace HK.Payments.Core.Services.Interfaces;

public interface ICreditCardService
{
    Task<string> CreditCardValidationAsync(CreditCardModel creditCard);
}