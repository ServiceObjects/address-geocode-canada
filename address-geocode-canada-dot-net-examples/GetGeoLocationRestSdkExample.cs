using address_geocode_canada_dot_net.REST;

namespace address_geocode_ca_dot_net_examples
{
    public class GetGeoLocationRestSdkExample
    {
        public static void Go(string licenseKey, bool isLive)
        {
            Console.WriteLine("\r\n----------------------------------------------------");
            Console.WriteLine("Address Geocode – Canada - GetGeoLocation - REST SDK");
            Console.WriteLine("----------------------------------------------------");

            GetGeoLocationClient.GetGeoLocationInput getGeoLocationInput = new(
                Address: "1053 Carling Ave",
                Municipality: "Ottawa",
                Province: "ON",
                PostalCode: "K1Y 4E9",
                LicenseKey: licenseKey,
                IsLive: isLive,
                TimeoutSeconds: 15
            );

            Console.WriteLine("\r\n* Input *\r\n");
            Console.WriteLine($"Address     : {getGeoLocationInput.Address}");
            Console.WriteLine($"Municipality: {getGeoLocationInput.Municipality}");
            Console.WriteLine($"Province    : {getGeoLocationInput.Province}");
            Console.WriteLine($"Postal Code : {getGeoLocationInput.PostalCode}");
            Console.WriteLine($"License Key : {getGeoLocationInput.LicenseKey}");
            Console.WriteLine($"Is Live     : {getGeoLocationInput.IsLive}");

            GetGeoLocationResponse response = GetGeoLocationClient.Invoke(getGeoLocationInput);
            if (response.Error is null)
            {
                Console.WriteLine("\r\n* Geocode Info *\r\n");
                Console.WriteLine($"Latitude  : {response.Latitude}");
                Console.WriteLine($"Longitude : {response.Longitude}");
                Console.WriteLine($"PostalCode: {response.PostalCode}");
                Console.WriteLine($"MatchCode : {response.MatchCode}");
            }
            else
            {
                Console.WriteLine("\r\n* Error *\r\n");
                Console.WriteLine($"Error Desc    : {response.Error.Desc}");
                Console.WriteLine($"Error Number  : {response.Error.Number}");
                Console.WriteLine($"Error Location: {response.Error.Location}");
            }
        }
    }
}