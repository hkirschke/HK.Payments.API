using HK.Payments.Core.Exceptions;

namespace HK.Payments.Core.Helpers;

public sealed class Validations
{
    private readonly List<string> _messages;

    public Validations()
    {
        _messages = new List<string>();
    }

    public static Validations Create()
    {
        return new Validations();
    }

    public void ThrowIfHasExceptions()
    {
        if (!_messages.Any())
        {
            return;
        }

        new DomainException().ThrowExceptionWithMessages(_messages);
    }

    public Validations IsMandatory(DateTime value, string message)
    {
        if (value <= DateTime.MinValue || value >= DateTime.MaxValue)
        {
            _messages.Add(message);
        }

        return this;
    }

    public Validations IsMandatory(string? value, string message)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            _messages.Add(message);
        }

        return this;
    }

    public Validations When(bool condition, string message)
    {
        if (condition)
        {
            _messages.Add(message);
        }

        return this;
    }
}