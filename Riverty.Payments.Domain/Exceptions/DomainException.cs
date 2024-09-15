namespace HK.Payments.Core.Exceptions;

public class DomainException : Exception
{
    public List<string>? ExceptionsMessages { get; set; }

    public void ThrowExceptionWithMessages(List<string> messages)
    {
        ExceptionsMessages = messages;

        throw this;
    }
}