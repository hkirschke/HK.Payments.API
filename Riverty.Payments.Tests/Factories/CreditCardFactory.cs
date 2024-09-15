using HK.Payments.Core.Models;

namespace HK.Payments.Tests.Factories;

public static class CreditCardFactory
{
    public static CreditCardModel CreateVisaCreditCardValidModel()
    {
        return new CreditCardModel("4920724575405968", "Brooklyn Walker", new DateTime(2025, 5, 1), "313");
    }

    public static CreditCardModel CreateMasterCardCreditCardValidModel()
    {
        return new CreditCardModel("5556280000490438", "Joseph Hernandez", new DateTime(2025, 5, 1), "415");
    }

    public static CreditCardModel CreateAmexCreditCardValidModel()
    {
        return new CreditCardModel("377710039895478", "Sophia Hill", new DateTime(2025, 5, 1), "4508");
    }

    public static CreditCardModel CreateDinnerClubCreditCardValidModel()
    {
        return new CreditCardModel("3651910427639113", "Natalie Jones", new DateTime(2025, 5, 1), "445");
    }

    public static CreditCardModel CreateInvalidNumberCreditCardValidModel()
    {
        return new CreditCardModel("", "Brooklyn Walker", new DateTime(2025, 5, 1), "313");
    }

    public static CreditCardModel CreateInvalidCardOwnerCreditCardValidModel()
    {
        return new CreditCardModel("4920724575405968", "", new DateTime(2025, 5, 1), "313");
    }

    public static CreditCardModel CreateInvalidCVCCreditCardValidModel()
    {
        return new CreditCardModel("4920724575405968", "Brooklyn Walker", new DateTime(2025, 5, 1), "");
    }

    public static CreditCardModel CreateInvalidDateCreditCardValidModel()
    {
        return new CreditCardModel()
        {
            CardOwner = "Brooklyn Walker",
            CreditCardNumber = "4920724575405968",
            CVC = "313"
        };
    }
}