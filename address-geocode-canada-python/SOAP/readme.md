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
# 1. Build the input
#
#  Required fields:
#               address
#               municipality 
#               province
#               postal_code
#               license_key
#               is_live
# 
# Optional:
#        timeout_seconds

from get_geo_location_soap import GetGeoLocationSoap

address = "1053 Carling Ave";
municipality = "Ottawa";
province = "ON";
postal_code = "K1Z 7K4";
timeout_seconds = 15;
is_live = True
license_key = "YOUR LICENSE KEY"

# 2. Call the method.
service = GetGeoLocationSoap(license_key, is_live, timeout_seconds)
response = service.get_geo_location(address, municipality, province, postal_code)

# 3. Inspect results.
if response and not response.Error:
    print(f"Latitude  : {response.Latitude}")
    print(f"Longitude : {response.Longitude}")
    print(f"PostalCode: {response.PostalCode}")
    print(f"MatchCode : {response.MatchCode}")
else:
    print("No geocode info found.")

if hasattr(response, 'Error') and response.Error:
    print("\r\n* Error *\r\n")
    print(f"Error Desc    : {response.Error.Desc}")
    print(f"Error Number  : {response.Error.Number}")
    print(f"Error Location: {response.Error.Location}")
```

# AGCA - GetPostalCodeInfo 

This operation is almost exactly like GetGeoLocation, but rather than geocoding given a specific address, DOTS Address Geocode – CA will geocode given a postal code. The coordinates given are an average centroid of a given postal code and often times match precisely to the street location.

### [GetPostalCodeInfo  Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-geocode-canada/agca-operations/agca-getpostalcodeinfo/)

## Library Usage

```
# 1. Build the input
#
#  Required fields:
#               postal_code
#               license_key
#               is_live
# 
# Optional:
#        timeout_seconds

from get_postal_code_info_soap import GetPostalCodeInfoSoap

postal_code = "K1Y 4E9";
timeout_seconds = 15;
is_live = True
license_key = "YOUR LICENSE KEY"

# 2. Call the sync Invoke() method.
service = GetPostalCodeInfoSoap(license_key, is_live, timeout_seconds)
response = service.get_postal_code_info(postal_code)

# 3. Inspect results.
if response and not response.Error:
    print(f"Latitude      : {response.Latitude}")
    print(f"Longitude     : {response.Longitude}")
    print(f"PostalCode    : {response.PostalCode}")
    print(f"TimeZone      : {response.TimeZone}")
    print(f"DST           : {response.DST}")
    print(f"AreaCode      : {response.AreaCode}")
    print(f"City          : {response.City}")
    print(f"CityPopulation: {response.CityPopulation}")
    print(f"Province      : {response.Province}")
else:
    print("No geocode info found.")

if hasattr(response, 'Error') and response.Error:
    print("\r\n* Error *\r\n")
    print(f"Error Desc     : {response.Error.Desc}")
    print(f"Error Number   : {response.Error.Number}")
    print(f"Error Location : {response.Error.Location}")
```

# AGCA - GetGeoLocationByMunicipalityProvince

This operation is almost exactly like GetGeoLocation except that it provides latitude/longitude coordinates based on provided municipality and province.

### [GetGeoLocationByMunicipalityProvince Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-geocode-canada/agca-operations/agca-getgeolocationbymunicipalityprovince/)

## Library Usage

```
# 1. Build the input
#
#  Required fields:
#               municipality 
#               province
#               license_key
#               is_live
# 
# Optional:
#        timeout_seconds

from get_geo_location_by_municipality_province_soap import GetGeoLocationByMunicipalityProvinceSoap

municipality = "Ottawa";
province = "ON";
timeout_seconds = 15;
is_live = True
license_key = "YOUR LICENSE KEY"

# 2. Call the sync Invoke() method.
service = GetGeoLocationByMunicipalityProvinceSoap(license_key, is_live, timeout_seconds)
response = service.get_geo_location_by_municipality_province(municipality, province)

# 3. Inspect results.
if response and not response.Error:
    print(f"Latitude  : {response.Latitude}")
    print(f"Longitude : {response.Longitude}")
    print(f"PostalCode: {response.PostalCode}")
else:
    print("No geocode info found.")

if hasattr(response, 'Error') and response.Error:
    print("\r\n* Error *\r\n")
    print(f"Error Desc    : {response.Error.Desc}")
    print(f"Error Number  : {response.Error.Number}")
    print(f"Error Location: {response.Error.Location}")
```
