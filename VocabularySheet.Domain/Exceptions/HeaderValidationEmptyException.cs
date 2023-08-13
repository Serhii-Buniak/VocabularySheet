using CsvHelper;

namespace VocabularySheet.Domain.Exceptions;

public class HeaderValidationEmptyException : HeaderValidationException
{
    public HeaderValidationEmptyException(CsvContext context) : base(context, Array.Empty<InvalidHeader>(), "Csv file is empty")
    {
    }

    public HeaderValidationEmptyException(CsvContext context, string message) : base(context, Array.Empty<InvalidHeader>(), message)
    {
    }
}
