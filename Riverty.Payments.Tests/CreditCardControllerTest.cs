using HK.Payments.API.Controllers;
using HK.Payments.Core.Services;
using HK.Payments.Tests.Factories;
using Microsoft.AspNetCore.Mvc;

namespace HK.Payments.Tests;

public class CreditCardControllerTest
{
    private CreditCardService _creditCardService;
    private CreditCardController _creditCardController;

    [SetUp]
    public void Setup()
    {
        _creditCardService = new CreditCardService();
        _creditCardController = new CreditCardController(_creditCardService);
    }

    [Test]
    public async Task Test1()
    {
        //Arrange 
        var creditCard = CreditCardFactory.CreateVisaCreditCardValidModel();

        // Act
        var result = await _creditCardController.CreditCardValidationAsync(creditCard);

        var resultObj = result.Result as CreatedResult;

        //Assert
        Assert.IsTrue(resultObj?.Value == "Visa");
    }
}