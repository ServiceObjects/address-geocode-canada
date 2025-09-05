using System.Web;

namespace address_geocode_canada_dot_net.REST
{
    /// <summary>
    /// Provides functionality to call the ServiceObjects Address Geocode CA (AGCA) REST API's GetGeoLocationByMunicipalityProvince endpoint,
    /// retrieving geocoding information (e.g., latitude, longitude, postal code) for a given Canadian municipality and province with fallback to a backup endpoint
    /// for reliability in live mode.
    /// </summary>
    public static class GetGeoLocationByMunicipalityProvinceClient
    {
        // Base URL constants: production, backup, and trial
        private const string LiveBaseUrl = "https://sws.serviceobjects.com/GCC/api.svc/json/";
        private const string BackupBaseUrl = "https://swsbackup.serviceobjects.com/GCC/api.svc/json/";
        private const string TrialBaseUrl = "https://trial.serviceobjects.com/GCC/api.svc/json/";

        /// <summary>
        /// Synchronously calls the GetGeoLocationByMunicipalityProvince REST endpoint to retrieve geocoding information,
        /// attempting the primary endpoint first and falling back to the backup if the response is invalid
        /// (Error.Number == "4") in live mode.
        /// </summary>
        /// <param name="input">The input parameters including municipality, province, and license key.</param>
        /// <returns>Deserialized <see cref="GetGeoLocationByMunicipalityProvinceResponse"/> containing geocoding data or an error.</returns>
        public static GetGeoLocationByMunicipalityProvinceResponse Invoke(GetGeoLocationByMunicipalityProvinceInput input)
        {
            // Use query string parameters so missing/optional fields don't break the URL
            string url = BuildUrl(input, input.IsLive ? LiveBaseUrl : TrialBaseUrl);
            GetGeoLocationByMunicipalityProvinceResponse response = Helper.HttpGet<GetGeoLocationByMunicipalityProvinceResponse>(url, input.TimeoutSeconds);

            // Fallback on error in live mode
            if (input.IsLive && !IsValid(response))
            {
                string fallbackUrl = BuildUrl(input, BackupBaseUrl);
                GetGeoLocationByMunicipalityProvinceResponse fallbackResponse = Helper.HttpGet<GetGeoLocationByMunicipalityProvinceResponse>(fallbackUrl, input.TimeoutSeconds);
                return fallbackResponse;
            }

            return response;
        }

        /// <summary>
        /// Asynchronously calls the GetGeoLocationByMunicipalityProvince REST endpoint to retrieve geocoding information,
        /// attempting the primary endpoint first and falling back to the backup if the response is invalid
        /// (Error.Number == "4") in live mode.
        /// </summary>
        /// <param name="input">The input parameters including municipality, province, and license key.</param>
        /// <returns>Deserialized <see cref="GetGeoLocationByMunicipalityProvinceResponse"/> containing geocoding data or an error.</returns>
        public static async Task<GetGeoLocationByMunicipalityProvinceResponse> InvokeAsync(GetGeoLocationByMunicipalityProvinceInput input)
        {
            // Use query string parameters so missing/optional fields don't break the URL
            string url = BuildUrl(input, input.IsLive ? LiveBaseUrl : TrialBaseUrl);
            GetGeoLocationByMunicipalityProvinceResponse response = await Helper.HttpGetAsync<GetGeoLocationByMunicipalityProvinceResponse>(url, input.TimeoutSeconds).ConfigureAwait(false);

            // Fallback on error in live mode
            if (input.IsLive && !IsValid(response))
            {
                string fallbackUrl = BuildUrl(input, BackupBaseUrl);
                GetGeoLocationByMunicipalityProvinceResponse fallbackResponse = await Helper.HttpGetAsync<GetGeoLocationByMunicipalityProvinceResponse>(fallbackUrl, input.TimeoutSeconds).ConfigureAwait(false);
                return fallbackResponse;
            }

            return response;
        }

        // Build the full request URL, including URL-encoded query string
        public static string BuildUrl(GetGeoLocationByMunicipalityProvinceInput input, string baseUrl)
        {
            // Construct query string with URL-encoded parameters
            string qs = $"GetGeoLocationByMunicipalityProvince?" +
                        $"Municipality={HttpUtility.UrlEncode(input.Municipality)}" +
                        $"&Province={HttpUtility.UrlEncode(input.Province)}" +
                        $"&LicenseKey={HttpUtility.UrlEncode(input.LicenseKey)}";
            return baseUrl + qs;
        }

        private static bool IsValid(GetGeoLocationByMunicipalityProvinceResponse response) => response?.Error == null || response.Error.Number != "4";

        /// <summary>
        /// Input parameters for the GetGeoLocationByMunicipalityProvince API call. Represents a Canadian municipality and province to geocode
        /// and returns latitude, longitude, and postal code.
        /// </summary>
        /// <param name="Municipality">The municipality to geocode. Required.</param>
        /// <param name="Province">The province to geocode. Required.</param>
        /// <param name="LicenseKey">The license key to authenticate the API request.</param>
        /// <param name="IsLive">Indicates whether to use the live service (true) or trial service (false).</param>
        /// <param name="TimeoutSeconds">Timeout duration for the API call, in seconds.</param>
        public record GetGeoLocationByMunicipalityProvinceInput(
            string Municipality = "",
            string Province = "",
            string LicenseKey = "",
            bool IsLive = true,
            int TimeoutSeconds = 15
        );
    }
}
