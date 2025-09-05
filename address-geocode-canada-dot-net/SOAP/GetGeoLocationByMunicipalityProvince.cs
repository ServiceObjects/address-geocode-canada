using AGCAService;

namespace address_geocode_canada_dot_net.SOAP
{
    /// <summary>
    /// Provides functionality to call the ServiceObjects Address Geocode CA (AGCA) SOAP service's GetGeoLocationByMunicipalityProvince operation,
    /// retrieving geocoding information (e.g., latitude, longitude, postal code) for a given Canadian municipality and province with fallback to a backup endpoint
    /// for reliability in live mode.
    /// </summary>
    public class GetGeoLocationByMunicipalityProvinceValidation
    {
        private const string LiveBaseUrl = "https://sws.serviceobjects.com/GCC/soap.svc/SOAP";
        private const string BackupBaseUrl = "https://swsbackup.serviceobjects.com/GCC/soap.svc/SOAP";
        private const string TrialBaseUrl = "https://trial.serviceobjects.com/GCC/soap.svc/SOAP";

        private readonly string _primaryUrl;
        private readonly string _backupUrl;
        private readonly int _timeoutMs;
        private readonly bool _isLive;

        /// <summary>
        /// Initializes URLs/timeout/IsLive.
        /// </summary>
        public GetGeoLocationByMunicipalityProvinceValidation(bool isLive)
        {
            _timeoutMs = 10000;
            _isLive = isLive;

            _primaryUrl = isLive ? LiveBaseUrl : TrialBaseUrl;
            _backupUrl = isLive ? BackupBaseUrl : TrialBaseUrl;

            if (string.IsNullOrWhiteSpace(_primaryUrl))
                throw new InvalidOperationException("Primary URL not set.");
            if (string.IsNullOrWhiteSpace(_backupUrl))
                throw new InvalidOperationException("Backup URL not set.");
        }

        /// <summary>
        /// Async, returns the latitude and longitude for a given Canadian municipality and province. This operation will return geocoding information based on the provided municipality and province.
        /// </summary>
        /// <param name="Municipality">The municipality to geocode. Required.</param>
        /// <param name="Province">The province to geocode. Required.</param>
        /// <param name="LicenseKey">Your license key to use the service.</param>
        public async Task<Location> GetGeoLocationByMunicipalityProvince(string Municipality, string Province, string LicenseKey)
        {
            GCCSoapServiceClient clientPrimary = null;
            GCCSoapServiceClient clientBackup = null;

            try
            {
                // Attempt Primary
                clientPrimary = new GCCSoapServiceClient();
                clientPrimary.Endpoint.Address = new System.ServiceModel.EndpointAddress(_primaryUrl);
                clientPrimary.InnerChannel.OperationTimeout = TimeSpan.FromMilliseconds(_timeoutMs);

                Location response = await clientPrimary.GetGeoLocationByMunicipalityProvinceAsync(
                    Municipality, Province, LicenseKey).ConfigureAwait(false);

                if (_isLive && !IsValid(response))
                {
                    throw new InvalidOperationException("Primary endpoint returned null or a fatal Number=4 error for GetGeoLocationByMunicipalityProvince");
                }
                return response;
            }
            catch (Exception primaryEx)
            {
                try
                {
                    clientBackup = new GCCSoapServiceClient();
                    clientBackup.Endpoint.Address = new System.ServiceModel.EndpointAddress(_backupUrl);
                    clientBackup.InnerChannel.OperationTimeout = TimeSpan.FromMilliseconds(_timeoutMs);

                    return await clientBackup.GetGeoLocationByMunicipalityProvinceAsync(
                        Municipality, Province, LicenseKey).ConfigureAwait(false);
                }
                catch (Exception backupEx)
                {
                    throw new InvalidOperationException(
                        $"Both primary and backup endpoints failed.\n" +
                        $"Primary error: {primaryEx.Message}\n" +
                        $"Backup error: {backupEx.Message}");
                }
                finally
                {
                    clientBackup?.Close();
                }
            }
            finally
            {
                clientPrimary?.Close();
            }
        }

        private static bool IsValid(Location response) => response?.Error == null || response.Error.Number != "4";
    }
}
