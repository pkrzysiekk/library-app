using Microsoft.Extensions.Options;
using Wypozyczalnia.Models;

namespace Wypozyczalnia.Validators.reCAPTCHA;

public class ReCaptchaValidator
{
    private readonly string _secret;
    private readonly HttpClient _httpClient;

    public ReCaptchaValidator(IOptions<ReCaptchaSettings> settings)
    {
        _secret = settings.Value.SecretKey;
        _httpClient = new HttpClient();
    }

    public async Task<bool> IsValid(string token)
    {
        var response = await _httpClient.PostAsync(
            $"https://www.google.com/recaptcha/api/siteverify?secret={_secret}&response={token}",
            null);

        var json = await response.Content.ReadAsStringAsync();
        return json.Contains("\"success\": true");
    }
}