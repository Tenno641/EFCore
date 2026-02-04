using System.Globalization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFCore.API.Data.ValueConvertors;

public class DateTimeConvertor : ValueConverter<DateTime, string>
{
    private const string DateTimeFormat = "yyyyMMdd";
    public DateTimeConvertor() : base(dateTimeValue => dateTimeValue.ToString(DateTimeFormat, CultureInfo.InvariantCulture), 
        dateTimeValue => DateTime.ParseExact(dateTimeValue, DateTimeFormat, CultureInfo.InvariantCulture)) { }
}