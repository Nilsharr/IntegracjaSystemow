namespace IntegracjaSystemow.Utils;

public static class ExtensionMethods
{
    public static string Capitalize(this string str)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return str;
        }

        return char.ToUpper(str[0]) + str[1..];
    }
}