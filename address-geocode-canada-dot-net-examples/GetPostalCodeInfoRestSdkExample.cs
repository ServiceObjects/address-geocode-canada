using address_geocode_canada_dot_net.REST;

namespace address_geocode_ca_dot_net_examples
{
    public class GetPostalCodeInfoRestSdkExample
    {
        public static void Go(string LicenseKey, bool IsLive)
        {
            Console.WriteLine("\r\n-------------------------------------------------------");
            Console.WriteLine("Address Geocode – Canada - GetPostalCodeInfo - REST SDK");
            Console.WriteLine("-------------------------------------------------------");

            GetPostalCodeInfoClient.GetPostalCodeInfoInput getPostalCodeInfoInput = new(
                PostalCode: "K1Y 4E9",
                LicenseKey: LicenseKey,
                IsLive: IsLive,
                TimeoutSeconds: 15
            );

            Console.WriteLine("\r\n* Input *\r\n");
            Console.WriteLine($"Postal Code: {getPostalCodeInfoInput.PostalCode}");
            Console.WriteLine($"License Key: {getPostalCodeInfoInput.LicenseKey}");
            Console.WriteLine($"Is Live    : {getPostalCodeInfoInput.IsLive}");

            GetPostalCodeInfoResponse response = GetPostalCodeInfoClient.Invoke(getPostalCodeInfoInput);
            if (response.Error is null)
            {
                Console.WriteLine("\r\n* Geocode Info *\r\n");
                Console.WriteLine($"Latitude      : {response.Latitude}");
                Console.WriteLine($"Longitude     : {response.Longitude}");
                Console.WriteLine($"PostalCode    : {response.PostalCode}");
                Console.WriteLine($"TimeZone      : {response.TimeZone}");
                Console.WriteLine($"DST           : {response.DST}");
                Console.WriteLine($"AreaCode      : {response.AreaCode}");
                Console.WriteLine($"City          : {response.City}");
                Console.WriteLine($"CityPopulation: {response.CityPopulation}");
                Console.WriteLine($"Province      : {response.Province}");
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