using address_geocode_canada_dot_net.REST;


namespace address_geocode_canada_dot_net_examples
{
    public class GetGeoLocationByMunicipalityProvinceRestSdkExample
    {
        public static void Go(string licenseKey, bool isLive)
        {
            Console.WriteLine("\r\n--------------------------------------------------------------------------");
            Console.WriteLine("Address Geocode – Canada - GetGeoLocationByMunicipalityProvince - REST SDK");
            Console.WriteLine("--------------------------------------------------------------------------");

            GetGeoLocationByMunicipalityProvinceClient.GetGeoLocationByMunicipalityProvinceInput getGeoLocationByMunicipalityProvinceInput = new(
                Municipality: "Ottawa",
                Province: "ON",
                LicenseKey: licenseKey,
                IsLive: isLive,
                TimeoutSeconds: 15
            );

            Console.WriteLine("\r\n* Input *\r\n");
            Console.WriteLine($"Municipality: {getGeoLocationByMunicipalityProvinceInput.Municipality}");
            Console.WriteLine($"Province    : {getGeoLocationByMunicipalityProvinceInput.Province}");
            Console.WriteLine($"License Key : {getGeoLocationByMunicipalityProvinceInput.LicenseKey}");
            Console.WriteLine($"Is Live     : {getGeoLocationByMunicipalityProvinceInput.IsLive}");

            GetGeoLocationByMunicipalityProvinceResponse response = GetGeoLocationByMunicipalityProvinceClient.Invoke(getGeoLocationByMunicipalityProvinceInput);
            if (response.Error is null)
            {
                Console.WriteLine("\r\n* Geocode Info *\r\n");
                Console.WriteLine($"Latitude  : {response.Latitude}");
                Console.WriteLine($"Longitude : {response.Longitude}");
                Console.WriteLine($"PostalCode: {response.PostalCode}");
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
