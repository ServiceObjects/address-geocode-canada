using System.Web;

namespace address_geocode_canada_dot_net.REST
{
    /// <summary>
    /// Provides functionality to call the ServiceObjects Address Geocode CA (AGCA) REST API's GetPostalCodeInfo endpoint,
    /// retrieving geocoding information (e.g., latitude, longitude, time zone, city) for a given Canadian postal code with fallback to a backup endpoint
    /// for reliability in live mode.
    /// </summary>
    public static class GetPostalCodeInfoClient
    {
        // Base URL constants: production, backup, and trial
        private const string LiveBaseUrl = "https://sws.serviceobjects.com/GCC/api.svc/json/";
        private const string BackupBaseUrl = "https://swsbackup.serviceobjects.com/GCC/api.svc/json/";
        private const string TrialBaseUrl = "https://trial.serviceobjects.com/GCC/api.svc/json/";

        /// <summary>
        /// Synchronously calls the GetPostalCodeInfo REST endpoint to retrieve geocoding information,
        /// attempting the primary endpoint first and falling back to the backup if the response is invalid
        /// (Error.Number == "4") in live mode.
        /// </summary>
        /// <param name="input">The input parameters including postal code and license key.</param>
        /// <returns>Deserialized <see cref="GetPostalCodeInfoResponse"/> containing geocoding data or an error.</returns>
        public static GetPostalCodeInfoResponse Invoke(GetPostalCodeInfoInput input)
        {
            // Use query string parameters so missing/optional fields don't break the URL
            string url = BuildUrl(input, input.IsLive ? LiveBaseUrl : TrialBaseUrl);
            GetPostalCodeInfoResponse response = Helper.HttpGet<GetPostalCodeInfoResponse>(url, input.TimeoutSeconds);

            // Fallback on error in live mode
            if (input.IsLive && !IsValid(response))
            {
                string fallbackUrl = BuildUrl(input, BackupBaseUrl);
                GetPostalCodeInfoResponse fallbackResponse = Helper.HttpGet<GetPostalCodeInfoResponse>(fallbackUrl, input.TimeoutSeconds);
                return fallbackResponse;
            }

            return response;
        }

        /// <summary>
        /// Asynchronously calls the GetPostalCodeInfo REST endpoint to retrieve geocoding information,
        /// attempting the primary endpoint first and falling back to the backup if the response is invalid
        /// (Error.Number == "4") in live mode.
        /// </summary>
        /// <param name="input">The input parameters including postal code and license key.</param>
        /// <returns>Deserialized <see cref="GetPostalCodeInfoResponse"/> containing geocoding data or an error.</returns>
        public static async Task<GetPostalCodeInfoResponse> InvokeAsync(GetPostalCodeInfoInput input)
        {
            // Use query string parameters so missing/optional fields don't break the URL
            string url = BuildUrl(input, input.IsLive ? LiveBaseUrl : TrialBaseUrl);
            GetPostalCodeInfoResponse response = await Helper.HttpGetAsync<GetPostalCodeInfoResponse>(url, input.TimeoutSeconds).ConfigureAwait(false);

            // Fallback on error in live mode
            if (input.IsLive && !IsValid(response))
            {
                string fallbackUrl = BuildUrl(input, BackupBaseUrl);
                GetPostalCodeInfoResponse fallbackResponse = await Helper.HttpGetAsync<GetPostalCodeInfoResponse>(fallbackUrl, input.TimeoutSeconds).ConfigureAwait(false);
                return fallbackResponse;
            }

            return response;
        }

        // Build the full request URL, including URL-encoded query string
        public static string BuildUrl(GetPostalCodeInfoInput input, string baseUrl)
        {
            // Construct query string with URL-encoded parameters
            string qs = $"GetPostalCodeInfo?" +
                        $"PostalCode={HttpUtility.UrlEncode(input.PostalCode)}" +
                        $"&LicenseKey={HttpUtility.UrlEncode(input.LicenseKey)}";
            return baseUrl + qs;
        }

        private static bool IsValid(GetPostalCodeInfoResponse response) => response?.Error == null || response.Error.Number != "4";

        /// <summary>
        /// Input parameters for the GetPostalCodeInfo API call. Represents a Canadian postal code to geocode
        /// and returns latitude, longitude, time zone, DST, area code, city, city population, and province.
        /// </summary>
        /// <param name="PostalCode">The postal code to geocode. Required.</param>
        /// <param name="LicenseKey">The license key to authenticate the API request.</param>
        /// <param name="IsLive">Indicates whether to use the live service (true) or trial service (false).</param>
        /// <param name="TimeoutSeconds">Timeout duration for the API call, in seconds.</param>
        public record GetPostalCodeInfoInput(
             string PostalCode = "",
             string LicenseKey = "",
             bool IsLive = true,
             int TimeoutSeconds = 15
        );
    }
}
