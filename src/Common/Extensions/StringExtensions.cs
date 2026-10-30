using JetBrains.Annotations;

namespace Extensions;

[PublicAPI]
public static class StringExtensions
{
    extension(string? value)
    {
        public bool IsNullOrEmpty()
        {
            return string.IsNullOrEmpty(value);
        }

        public bool IsNullOrWhiteSpace()
        {
            return string.IsNullOrWhiteSpace(value);
        }
    }
}