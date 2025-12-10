namespace Application.Exceptions;

public class DataMismatchException : Exception
{
    public DataMismatchException() { }

    public DataMismatchException(string message)
        : base(message) { }
}
