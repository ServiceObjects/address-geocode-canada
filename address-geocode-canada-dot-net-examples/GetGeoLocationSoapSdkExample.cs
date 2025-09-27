using address_geocode_canada_dot_net.SOAP;
using AGCAService;

namespace address_geocode_ca_dot_net_examples
{
    public static class GetGeoLocationSoapSdkExample
    {
        public static void Go(string LicenseKey, bool IsLive)
        {
            Console.WriteLine("\r\n----------------------------------------------------");
            Console.WriteLine("Address Geocode – Canada - GetGeoLocation - SOAP SDK");
            Console.WriteLine("----------------------------------------------------");

            string Address = "1053 Carling Ave";
            string Municipality = "Ottawa";
            string Province = "ON";
            string PostalCode = "K1Y 4E9";

            Console.WriteLine("\r\n* Input *\r\n");
            Console.WriteLine($"Address     : {Address}");
            Console.WriteLine($"Municipality: {Municipality}");
            Console.WriteLine($"Province    : {Province}");
            Console.WriteLine($"Postal Code : {PostalCode}");
            Console.WriteLine($"License Key : {LicenseKey}");
            Console.WriteLine($"Is Live     : {IsLive}");

            GetGeoLocationValidation getGeoLocationValidation = new(IsLive);
            Location response = getGeoLocationValidation.GetGeoLocation(Address, Municipality, Province, PostalCode, LicenseKey).Result;

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