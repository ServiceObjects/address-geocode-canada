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
//               Municipality 
//               Province
//               PostalCode
//               LicenseKey
//               IsLive
// 
// Optional:
//        Address
//        TimeoutSeconds (default: 15)

import { GetGeoLocationClient } from "../address-geocode-canada-nodejs/REST/get_geo_location_rest.js";

const address = "1053 Carling Ave";
const municipality = "Ottawa";
const province = "ON";
const postalCode = "K1Z 7K4";
const timeoutSeconds = 15;

// 2. Call the sync Invoke() method.
  const response = await GetGeoLocationClient.invoke(address, municipality, province, postalCode, licenseKey, isLive, timeoutSeconds);

// 3. Inspect results.
if (response)
{
    console.log(`Latitude  : ${response.Latitude}`);
    console.log(`Longitude : ${response.Longitude}`);
    console.log(`PostalCode: ${response.PostalCode}`);
    console.log(`MatchCode : ${response.MatchCode}`);
} 
else
{
    console.log("No geocode info found.");
}

if (response.Error) 
{
    console.log("\n* Error *\n");
    console.log(`Error Desc    : ${response.Error.Desc}`);
    console.log(`Error Number  : ${response.Error.Number}`);
    console.log(`Error Location: ${response.Error.Location}`);
    return;
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
//        TimeoutSeconds (default: 15)

import { GetPostalCodeInfoClient } from "../address-geocode-canada-nodejs/REST/get_postal_code_info_rest.js";

const postalCode = "K1Y 4E9";
const timeoutSeconds = 15;

// 2. Call the sync Invoke() method.
 const response = await GetPostalCodeInfoClient.invoke(postalCode,licenseKey,isLive,timeoutSeconds);

// 3. Inspect results.
if (response) {
    console.log(`Latitude      : ${response.Latitude}`);
    console.log(`Longitude     : ${response.Longitude}`);
    console.log(`PostalCode    : ${response.PostalCode}`);
    console.log(`TimeZone      : ${response.TimeZone}`);
    console.log(`DST           : ${response.DST}`);
    console.log(`AreaCode      : ${response.AreaCode}`);
    console.log(`City          : ${response.City}`);
    console.log(`CityPopulation: ${response.CityPopulation}`);
    console.log(`Province      : ${response.Province}`);
} 
else 
{
    console.log("No geocode info found.");
}

if (response.Error)
{
    console.log("\n* Error *\n");
    console.log(`Error Desc    : ${response.Error.Desc}`);
    console.log(`Error Number  : ${response.Error.Number}`);
    console.log(`Error Location: ${response.Error.Location}`);
    return;
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
//               PostalCode
//               LicenseKey
//               IsLive
// 
// Optional:
//        Address
//        TimeoutSeconds (default: 15)

import { GetGeoLocationByMunicipalityProvinceClient } from "../address-geocode-canada-nodejs/REST/get_geo_location_by_municipality_province_rest.js";

const municipality = "Ottawa";
const province = "ON";
const timeoutSeconds = 15;

// 2. Call the sync Invoke() method.
  const response = await GetGeoLocationByMunicipalityProvinceClient.invoke(municipality, province, licenseKey, isLive, timeoutSeconds);

// 3. Inspect results.
if (response.Error) {
    console.log("\n* Error *\n");
    console.log(`Error Desc    : ${response.Error.Desc}`);
    console.log(`Error Number  : ${response.Error.Number}`);
    console.log(`Error Location: ${response.Error.Location}`);
    return;
}

console.log("\n* Geocode Info *\n");
if (response)
{
    console.log(`Latitude  : ${response.Latitude}`);
    console.log(`Longitude : ${response.Longitude}`);
    console.log(`PostalCode: ${response.PostalCode}`);
} else 
{
    console.log("No geocode info found.");
}
```
