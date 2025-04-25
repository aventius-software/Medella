using FluentValidation;

namespace Services.Shared.Validation;

public static class FluentValidationExtensions    
{
    public static IRuleBuilderOptions<T, string> Url<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        bool UrlIsValidUri(string url) => Uri.TryCreate(url, UriKind.Absolute, out var outUri)
           && (outUri.Scheme == Uri.UriSchemeHttp || outUri.Scheme == Uri.UriSchemeHttps);

        return ruleBuilder.Must(UrlIsValidUri);
    }
}
