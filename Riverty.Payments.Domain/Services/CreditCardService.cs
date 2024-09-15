using CreditCardValidator;
using HK.Payments.Core.Consts;
using HK.Payments.Core.Enums;
using HK.Payments.Core.Helpers;
using HK.Payments.Core.Models;
using HK.Payments.Core.Services.Interfaces;

namespace HK.Payments.Core.Services;

public sealed class CreditCardService : ICreditCardService
{
    public async Task<string> CreditCardValidationAsync(CreditCardModel creditCard)
    {
        var detector = new CreditCardDetector(creditCard.CreditCardNumber);

        var cardIssuerInt = detector.Brand.GetEnumIntValue();

        var isCardIssuerSupported = cardIssuerInt.IsDefined<EValidCardIssuer>();

        var cardExpirationDate = new DateTime(creditCard.IssueDate.Year, creditCard.IssueDate.Month, 1);

        var now = DateTimeOffset.UtcNow;

        var dateToCompare = new DateTime(now.Year, now.Month, 1);

        var isLimitBroken = creditCard.CreditCardNumber.Length > Common.CARD_NUMBER_MAX_LENGHT;

        var isCardExpired = cardExpirationDate.Date < dateToCompare;

        Validations.Create()
           .When(isCardIssuerSupported is false || detector.Brand == CardIssuer.Unknown, string.Format(ConstsMessagesValidation.INVALID_OPERATION, nameof(CreditCardValidationAsync), ConstsMessagesValidation.CARD_ISSUER_NOT_SUPPORTED))
           .When(isCardExpired, ConstsMessagesValidation.CARD_EXPIRED)
           .When(isLimitBroken, string.Format(ConstsMessagesValidation.INVALID_OPERATION, nameof(CreditCardValidationAsync), ConstsMessagesValidation.CREDIT_CARD_NUMBER_NOT_VALID))
           .ThrowIfHasExceptions();

        int cvcMaxLength = (EValidCardIssuer)cardIssuerInt == EValidCardIssuer.AmericanExpress ? Common.CVC_LENGHT_4 : Common.CVC_LENGHT_3;

        Validations.Create()
          .When(creditCard?.CVC?.Length != cvcMaxLength, ConstsMessagesValidation.CVC_NOT_VALID)
          .ThrowIfHasExceptions();

        return await Task.FromResult(detector.BrandName);
    }
}