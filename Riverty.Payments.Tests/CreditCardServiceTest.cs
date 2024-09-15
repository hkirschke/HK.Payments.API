using HK.Payments.Core.Consts;
using HK.Payments.Core.Exceptions;
using HK.Payments.Core.Services;
using HK.Payments.Core.Services.Interfaces;
using HK.Payments.Tests.Factories;

namespace HK.Payments.Tests;

public class CreditCardServiceTest
{
    private CreditCardService _creditCardService;

    [SetUp]
    public void Setup()
    {
        _creditCardService = new CreditCardService();
    }

    [Test]
    public async Task Validate_Visa_Credit_Card_With_Sucess()
    {
        //Arrange
        var creditCard = CreditCardFactory.CreateVisaCreditCardValidModel();

        // Act
        var result = await _creditCardService.CreditCardValidationAsync(creditCard);

        //Assert
        Assert.IsTrue(result == "Visa");
    }

    [Test]
    public async Task Validate_MasterCard_Credit_Card_With_Sucess()
    {
        //Arrange
        var creditCard = CreditCardFactory.CreateMasterCardCreditCardValidModel();

        // Act
        var result = await _creditCardService.CreditCardValidationAsync(creditCard);

        //Assert
        Assert.IsTrue(result == "MasterCard");
    }

    [Test]
    public void CheckError_MasterCard_Credit_Card_Without_Card_Owner_Name()
    {
        //Arrange 
        var errorMessage = string.Format(ConstsMessagesValidation.INVALID_OPERATION, nameof(ICreditCardService.CreditCardValidationAsync), ConstsMessagesValidation.CARD_OWNER_NOT_VALID);

        // Act 
        var ex = Assert.Throws<DomainException>(() => CreditCardFactory.CreateInvalidCardOwnerCreditCardValidModel());

        var containsErrorMessage = ex.ExceptionsMessages.Contains(errorMessage);

        //Assert
        Assert.IsTrue(containsErrorMessage);
    }

    [Test]
    public void CheckError_DinnersClub_Credit_Card_Without_Support()
    {
        //Arrange 
        var errorMessage = string.Format(ConstsMessagesValidation.INVALID_OPERATION, nameof(ICreditCardService.CreditCardValidationAsync), ConstsMessagesValidation.CARD_ISSUER_NOT_SUPPORTED);

        // Act 
        var creditCard = CreditCardFactory.CreateDinnerClubCreditCardValidModel();

        var ex = Assert.ThrowsAsync<DomainException>(async () => await _creditCardService.CreditCardValidationAsync(creditCard));

        var containsErrorMessage = ex.ExceptionsMessages.Contains(errorMessage);

        //Assert
        Assert.IsTrue(containsErrorMessage);
    }
}