using AGCAService;

namespace address_geocode_canada_dot_net.SOAP
{
    /// <summary>
    /// Provides functionality to call the ServiceObjects Address Geocode CA (AGCA) SOAP service's GetGeoLocation operation,
    /// retrieving geocoding information (e.g., latitude, longitude, postal code) for a given Canadian address with fallback to a backup endpoint
    /// for reliability in live mode.
    /// </summary>
    public class GetGeoLocationValidation
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
        public GetGeoLocationValidation(bool isLive)
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
        /// Async, returns the latitude and longitude for a given Canadian address. This operation will use cascading logic when an exact address match is not found and it will return the next closest level match available. The operation output is designed to allow the service to return new pieces of data as they become available without having to re-integrate.
        /// </summary>
        /// <param name="Address">Address line of the address to geocode. For example, “124 Main Street”. Required.</param>
        /// <param name="Municipality">The municipality of the address to geocode. For example, “Cayuga”. Optional if postal code is provided.</param>
        /// <param name="Province">The province of the address to geocode. For example, “ON”. Optional if postal code is provided.</param>
        /// <param name="PostalCode">The postal code of the address to geocode. Optional if municipality and province are provided.</param>
        /// <param name="LicenseKey">Your license key to use the service.</param>
        public async Task<Location> GetGeoLocation(string Address, string Municipality, string Province, string PostalCode, string LicenseKey)
        {
            GCCSoapServiceClient clientPrimary = null;
            GCCSoapServiceClient clientBackup = null;

            try
            {
                // Attempt Primary
                clientPrimary = new GCCSoapServiceClient();
                clientPrimary.Endpoint.Address = new System.ServiceModel.EndpointAddress(_primaryUrl);
                clientPrimary.InnerChannel.OperationTimeout = TimeSpan.FromMilliseconds(_timeoutMs);

                Location response = await clientPrimary.GetGeoLocationAsync(
                    Address, Municipality, Province, PostalCode, LicenseKey).ConfigureAwait(false);

                if (_isLive && !IsValid(response))
                {
                    throw new InvalidOperationException("Primary endpoint returned null or a fatal Number=4 error for GetGeoLocation");
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

                    return await clientBackup.GetGeoLocationAsync(
                        Address, Municipality, Province, PostalCode, LicenseKey).ConfigureAwait(false);
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
