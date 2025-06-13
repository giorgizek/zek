namespace Zek.Options
{
    public class ConfigBase : ConfigBase<TokenOptions>
    {

    }
    public class ConfigBase<T>
        where T : TokenOptions, new()
    {
        public static T TokenOptions { get; set; } = new T();
    }
}
