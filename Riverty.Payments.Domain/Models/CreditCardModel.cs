using HK.Payments.Core.Consts;
using HK.Payments.Core.Helpers;
using HK.Payments.Core.Services.Interfaces;
using Riverty.Payments.Core.Extensions;
using System.Text.Json.Serialization;

namespace HK.Payments.Core.Models;

public sealed class CreditCardModel
{
    [JsonPropertyName("creditCardNumber")]
    public string? CreditCardNumber { get; set; }

    [JsonPropertyName("cardOwner")]
    public string? CardOwner { get; set; }

    [JsonPropertyName("issueDate")]
    public DateTime IssueDate { get; set; }

    [JsonPropertyName("cvc")]
    public string? CVC { get; set; }

    public CreditCardModel() { }

    [JsonConstructor]
    public CreditCardModel(string creditCardNumber, string cardOwner, DateTime issueDate, string cvc)
    {
        Validations.Create()
            .IsMandatory(creditCardNumber, string.Format(ConstsMessagesValidation.MANDATORY_PROPERTY, nameof(creditCardNumber)))
            .IsMandatory(issueDate, string.Format(ConstsMessagesValidation.MANDATORY_PROPERTY, nameof(issueDate)))
            .IsMandatory(cvc, string.Format(ConstsMessagesValidation.MANDATORY_PROPERTY, nameof(cvc)))
            .IsMandatory(cardOwner, string.Format(ConstsMessagesValidation.MANDATORY_PROPERTY, nameof(cardOwner)))
            .When(cardOwner.HasJustLetter() is false, string.Format(ConstsMessagesValidation.INVALID_OPERATION, nameof(ICreditCardService.CreditCardValidationAsync), ConstsMessagesValidation.CARD_OWNER_NOT_VALID))
            .When(creditCardNumber?.HasJustNumbers() is false, string.Format(ConstsMessagesValidation.INVALID_OPERATION, nameof(ICreditCardService.CreditCardValidationAsync), ConstsMessagesValidation.CREDIT_CARD_NUMBER_NOT_VALID))
            .When(cvc.HasJustNumbers() is false, string.Format(ConstsMessagesValidation.INVALID_OPERATION, nameof(ICreditCardService.CreditCardValidationAsync), ConstsMessagesValidation.CVC_NOT_VALID))
       .ThrowIfHasExceptions();

        CreditCardNumber = creditCardNumber;
        CardOwner = cardOwner;
        IssueDate = issueDate;
        CVC = cvc;
    }
}
