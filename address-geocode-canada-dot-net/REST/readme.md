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
// 1. Build the input
//
//  Required fields:
//               Address
//               Municipality 
//               Province
//               PostalCode
//               LicenseKey
//               IsLive
// 
// Optional:
//        TimeoutSeconds

using address_geocode_canada_dot_net.REST 

GetGeoLocationClient.GetGeoLocationInput getGeoLocationInput = new(
    Address: "1053 Carling Ave",
    Municipality: "Ottawa",
    Province: "ON",
    PostalCode: "K1Y 4E9",
    LicenseKey: licenseKey,
    IsLive: true,
    TimeoutSeconds: 15
);

// 2. Call the sync InvokeAsync() method.
GetGeoLocationResponse response = GetGeoLocationClient.Invoke(getGeoLocationInput);

// 3. Inspect results.
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

# AGCA - GetPostalCodeInfo 

This operation is almost exactly like GetGeoLocation, but rather than geocoding given a specific address, DOTS Address Geocode – CA will geocode given a postal code. The coordinates given are an average centroid of a given postal code and often times match precisely to the street location.

### [GetPostalCodeInfo  Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-geocode-canada/agca-operations/agca-getpostalcodeinfo/)

## Library Usage

```
// 1. Build the input
//
//  Required fields:
//               PostalCode
//               LicenseKey
//               IsLive
// 
// Optional:
//        TimeoutSeconds

using address_geocode_us_dot_net.REST;

GetPostalCodeInfoClient.GetPostalCodeInfoInput getPostalCodeInfoInput = new(
    PostalCode: "K1Y 4E9",
    LicenseKey: licenseKey,
    IsLive: true,
    TimeoutSeconds: 15
);

// 2. Call the sync InvokeAsync() method.
GetPostalCodeInfoResponse response = GetPostalCodeInfoClient.Invoke(getPostalCodeInfoInput);

// 3. Inspect results.
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
// 1. Build the input
//
//  Required fields:
//               Municipality
//               Province
//               LicenseKey
//               IsLive
// 
// Optional:
//        TimeoutSeconds

using address_geocode_us_dot_net.REST;

GetGeoLocationByMunicipalityProvinceClient.GetGeoLocationByMunicipalityProvinceInput getGeoLocationByMunicipalityProvinceInput = new(
    Municipality: "Ottawa",
    Province: "ON",
    LicenseKey: licenseKey,
    IsLive: true,
    TimeoutSeconds: 15
);

// 2. Call the sync InvokeAsync() method.
 GetGeoLocationByMunicipalityProvinceResponse response = GetGeoLocationByMunicipalityProvinceClient.Invoke(getGeoLocationByMunicipalityProvinceInput);

// 3. Inspect results.
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
    Console.WriteLine($"Error Number  : {response.Error.Number}");
    Console.WriteLine($"Error Desc    : {response.Error.Desc}");
    Console.WriteLine($"Error Location: {response.Error.Location}");
}
```
