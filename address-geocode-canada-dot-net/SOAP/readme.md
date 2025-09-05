![Service Objects Logo](https://www.serviceobjects.com/wp-content/uploads/2021/05/SO-Logo-with-TM.gif "Service Objects Logo")

# AGCA - Address Geocode – Canada 

DOTS Address Geocode – CA is a publicly available XML web service that provides latitude/longitude and metadata information about a physical Canadian address. The service provides geocoding information, such as the latitude and longitude location of a Canadian address, along with demographic information, such as the census tract, block and other metadata.

This service can provide instant address locations to websites or enhancement to contact lists.

## [Service Objects Website](https://serviceobjects.com)

# AGCA - GetGeoLocation

This is the basic operation for finding the latitude/longitude coordinates of an address. This operation takes a standard Canadian address (Address, Municipality, Province, Postal Code) and will try to find the exact street location’s coordinates. 

First, DOTS Address Geocode – CA will attempt to correct and normalize the address to make it more likely to geocode properly. You don’t need to worry about fixing the address before sending it to DOTS Address Geocode – CA, unless you want to filter out invalid or non-existent addresses beforehand.

This operation requires the Address value, and either Municipality and Province, or the Zip code. Providing all inputs is recommended.

### [GetGeoLocation Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-geocode-canada/agca-operations/agca-getgeolocation-recommended/)

## Library Usage

```
// 1 Instantiate the service wrapper
GetGeoLocationValidation getGeoLocationValidation = new(true);

// 2 Provide your input data
//  Required fields:
//               Municipality 
//               Province
//               PostalCode
//               LicenseKey
//               IsLive
// 
// Optional:
//        Address
//        TimeoutSeconds (default: 15)
string Address = "1053 Carling Ave";
string Municipality = "Ottawa";
string Province = "ON";
string PostalCode = "K1Y 4E9";

// 3 Call the service
Location response = getGeoLocationValidation.GetGeoLocation(Address, Municipality, Province, PostalCode, licenseKey).Result;

// 4. Inspect results.
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
```

# AGCA - GetPostalCodeInfo 

This operation is almost exactly like GetGeoLocation, but rather than geocoding given a specific address, DOTS Address Geocode – CA will geocode given a postal code. The coordinates given are an average centroid of a given postal code and often times match precisely to the street location.

### [GetPostalCodeInfo  Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-geocode-canada/agca-operations/agca-getpostalcodeinfo/)

## Library Usage

```
// 1 Instantiate the service wrapper
GetPostalCodeInfoValidation getPostalCodeInfoValidation = new(true);

// 2 Provide your input data
//  Required fields:
//               PostalCode
//               LicenseKey
//               IsLive
// 
// Optional:
//        TimeoutSeconds (default: 15)
string PostalCode = "K1P5E3";

// 3 Call the service
PostalCodeInfo response = getPostalCodeInfoValidation.GetPostalCodeInfo(postalCode, licenseKey).Result;

// 4. Inspect results.
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
    Console.WriteLine($"Error Number  : {response.Error.Number}");
    Console.WriteLine($"Error Desc    : {response.Error.Desc}");
    Console.WriteLine($"Error Location: {response.Error.Location}");
}
```

# AGCA - GetGeoLocationByMunicipalityProvince

This operation is almost exactly like GetGeoLocation except that it provides latitude/longitude coordinates based on provided municipality and province.

### [GetGeoLocationByMunicipalityProvince Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-geocode-canada/agca-operations/agca-getgeolocationbymunicipalityprovince/)

## Library Usage

```
// 1 Instantiate the service wrapper
GetGeoLocationByMunicipalityProvinceValidation getGeoLocationByMunicipalityProvinceValidation = new(true);

// 2 Provide your input data
//  Required fields:
//               Municipality 
//               Province
//               PostalCode
//               LicenseKey
//               IsLive
// 
// Optional:
//        Address
//        TimeoutSeconds (default: 15)
string Municipality = "Ottawa";
string Province = "ON";

// 3 Call the service
Location response = getGeoLocationByMunicipalityProvinceValidation.GetGeoLocationByMunicipalityProvinceidation(Municipality, Province, licenseKey).Result;

// 4. Inspect results.
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
    Console.WriteLine($"Error Number  : {response.Error.Number}");
    Console.WriteLine($"Error Desc    : {response.Error.Desc}");
    Console.WriteLine($"Error Location: {response.Error.Location}");
}
```
