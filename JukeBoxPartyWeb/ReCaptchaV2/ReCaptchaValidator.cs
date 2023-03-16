using Newtonsoft.Json;

namespace JukeBoxPartyWeb.ReCaptchaV2
{
    public class ReCaptchaValidator
    {
        public static ReCaptchaValidationResult IsValid(string captchaResponse)
        {/*
            if (string.IsNullOrWhiteSpace(captchaResponse))
            {
                return new ReCaptchaValidationResult(){ Success = false };
            }*/

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://www.google.com");

            var values = new List<KeyValuePair<string, string>>();
            values.Add(new KeyValuePair<string, string>("secret", "6LcSm4gkAAAAAI5uOV_tpbCWCNHe8jZL_8e1s7RD"));
            values.Add(new KeyValuePair<string, string>("response", captchaResponse));
            FormUrlEncodedContent content = new FormUrlEncodedContent(values);

            HttpResponseMessage response = client.PostAsync("/recaptcha/api/siteverify", content).Result;

            string verificationResponse = response.Content.ReadAsStringAsync().Result;

            var verificationResult = JsonConvert.DeserializeObject<ReCaptchaValidationResult>(verificationResponse);

            return verificationResult;
        }
    }
}
