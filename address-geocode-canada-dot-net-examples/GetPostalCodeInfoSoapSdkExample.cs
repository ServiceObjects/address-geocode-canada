using address_geocode_canada_dot_net.SOAP;
using AGCAService;

namespace address_geocode_ca_dot_net_examples
{
    public static class GetPostalCodeInfoSoapSdkExample
    {
        public static void Go(string LicenseKey, bool IsLive)
        {
            Console.WriteLine("\r\n-------------------------------------------------------");
            Console.WriteLine("Address Geocode – Canada - GetPostalCodeInfo - SOAP SDK");
            Console.WriteLine("-------------------------------------------------------");

            string PostalCode = "K1Y 4E9";

            Console.WriteLine("\r\n* Input *\r\n");
            Console.WriteLine($"Postal Code: {PostalCode}");
            Console.WriteLine($"License Key: {LicenseKey}");
            Console.WriteLine($"Is Live    : {IsLive}");

            GetPostalCodeInfoValidation getPostalCodeInfoValidation = new(IsLive);

            PostalCodeInfo response = getPostalCodeInfoValidation.GetPostalCodeInfo(PostalCode, LicenseKey).Result;

            if (response.Error is null)
            {
                Console.WriteLine("\r\n* Geocode Info *\r\n");
                Console.WriteLine($"Latitude            : {response.Latitude}");
                Console.WriteLine($"Longitude           : {response.Longitude}");
                Console.WriteLine($"PostalCode          : {response.PostalCode}");
                Console.WriteLine($"TimeZone            : {response.TimeZone}");
                Console.WriteLine($"DST                 : {response.DST}");
                Console.WriteLine($"AreaCode            : {response.AreaCode}");
                Console.WriteLine($"City                : {response.City}");
                Console.WriteLine($"CityPopulation      : {response.CityPopulation}");
                Console.WriteLine($"Province            : {response.Province}");
                Console.WriteLine($"ProvinceAbbreviation: {response.ProvinceAbbreviation}");
                Console.WriteLine($"DMA                 : {response.DMA}");
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