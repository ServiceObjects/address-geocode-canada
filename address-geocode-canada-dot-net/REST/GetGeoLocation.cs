using System.Web;


namespace address_geocode_canada_dot_net.REST
{
    /// <summary>
    /// Provides functionality to call the ServiceObjects Address Geocode CA (AGCA) REST API's GetGeoLocation endpoint,
    /// retrieving geocoding information (e.g., latitude, longitude, postal code) for a given Canadian address with fallback to a backup endpoint
    /// for reliability in live mode.
    /// </summary>
    public static class GetGeoLocationClient
    {
        private const string LiveBaseUrl = "https://sws.serviceobjects.com/GCC/api.svc/json/";
        private const string BackupBaseUrl = "https://swsbackup.serviceobjects.com/GCC/api.svc/json/";
        private const string TrialBaseUrl = "https://trial.serviceobjects.com/GCC/api.svc/json/";

        /// <summary>
        /// Synchronously calls the GetGeoLocation REST endpoint to retrieve geocoding information,
        /// attempting the primary endpoint first and falling back to the backup if the response is invalid
        /// (Error.Number == "4") in live mode.
        /// </summary>
        /// <param name="input">The input parameters including address, municipality, province, postal code, and license key.</param>
        /// <returns>Deserialized <see cref="GetGeoLocationResponse"/> containing geocoding data or an error.</returns>
        public static GetGeoLocationResponse Invoke(GetGeoLocationInput input)
        {
            // Use query string parameters so missing/optional fields don't break the URL
            string url = BuildUrl(input, input.IsLive ? LiveBaseUrl : TrialBaseUrl);
            GetGeoLocationResponse response = Helper.HttpGet<GetGeoLocationResponse>(url, input.TimeoutSeconds);

            // Fallback on error in live mode
            if (input.IsLive && !IsValid(response))
            {
                string fallbackUrl = BuildUrl(input, BackupBaseUrl);
                GetGeoLocationResponse fallbackResponse = Helper.HttpGet<GetGeoLocationResponse>(fallbackUrl, input.TimeoutSeconds);
                return fallbackResponse;
            }

            return response;
        }

        /// <summary>
        /// Asynchronously calls the GetGeoLocation REST endpoint to retrieve geocoding information,
        /// attempting the primary endpoint first and falling back to the backup if the response is invalid
        /// (Error.Number == "4") in live mode.
        /// </summary>
        /// <param name="input">The input parameters including address, municipality, province, postal code, and license key.</param>
        /// <returns>Deserialized <see cref="GetGeoLocationResponse"/> containing geocoding data or an error.</returns>
        public static async Task<GetGeoLocationResponse> InvokeAsync(GetGeoLocationInput input)
        {
            // Use query string parameters so missing/optional fields don't break the URL
            string url = BuildUrl(input, input.IsLive ? LiveBaseUrl : TrialBaseUrl);
            GetGeoLocationResponse response = await Helper.HttpGetAsync<GetGeoLocationResponse>(url, input.TimeoutSeconds).ConfigureAwait(false);

            // Fallback on error in live mode
            if (input.IsLive && !IsValid(response))
            {
                string fallbackUrl = BuildUrl(input, BackupBaseUrl);
                GetGeoLocationResponse fallbackResponse = await Helper.HttpGetAsync<GetGeoLocationResponse>(fallbackUrl, input.TimeoutSeconds).ConfigureAwait(false);
                return fallbackResponse;
            }

            return response;
        }

        // Build the full request URL, including URL-encoded query string
        public static string BuildUrl(GetGeoLocationInput input, string baseUrl)
        {
            // Construct query string with URL-encoded parameters
            string qs = $"GetGeoLocation?" +
                        $"Address={HttpUtility.UrlEncode(input.Address)}" +
                        $"&Municipality={HttpUtility.UrlEncode(input.Municipality)}" +
                        $"&Province={HttpUtility.UrlEncode(input.Province)}" +
                        $"&PostalCode={HttpUtility.UrlEncode(input.PostalCode)}" +
                        $"&LicenseKey={HttpUtility.UrlEncode(input.LicenseKey)}";
            return baseUrl + qs;
        }

        private static bool IsValid(GetGeoLocationResponse response) => response?.Error == null || response.Error.Number != "4";

        /// <summary>
        /// Input parameters for the GetGeoLocation API call. Represents a Canadian address to geocode
        /// and returns latitude, longitude, postal code, and match code with cascading logic for partial matches.
        /// </summary>
        /// <param name="Address">Address line of the address to geocode (e.g., "124 Main Street"). Required.</param>
        /// <param name="Municipality">The municipality of the address to geocode (e.g., "Cayuga"). Optional if postal code is provided.</param>
        /// <param name="Province">The province of the address to geocode (e.g., "ON"). Optional if postal code is provided.</param>
        /// <param name="PostalCode">The postal code of the address to geocode. Optional if municipality and province are provided.</param>
        /// <param name="LicenseKey">The license key to authenticate the API request.</param>
        /// <param name="IsLive">Indicates whether to use the live service (true) or trial service (false).</param>
        /// <param name="TimeoutSeconds">Timeout duration for the API call, in seconds.</param>
        public record GetGeoLocationInput(
             string Address = "",
             string Municipality = "",
             string Province = "",
             string PostalCode = "",
             string LicenseKey = "",
             bool IsLive = true,
             int TimeoutSeconds = 15
        );
    }
}