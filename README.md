![Service Objects Logo](https://www.serviceobjects.com/wp-content/uploads/2021/05/SO-Logo-with-TM.gif "Service Objects Logo")

# AGCA - Address Geocode Canada

DOTS Address Geocode – CA is a publicly available XML web service that provides latitude/longitude and metadata information about a physical Canadian address. The service provides geocoding information, such as the latitude and longitude location of a Canadian address, along with demographic information, such as the census tract, block and other metadata.

This service can provide instant address locations to websites or enhancement to contact lists.

## [Service Objects Website](https://serviceobjects.com)
## [Developer Guide/Documentation](https://www.serviceobjects.com/docs/)

# AGCA - GetGeoLocation

This is the basic operation for finding the latitude/longitude coordinates of an address. This operation takes a standard Canadian address (Address, Municipality, Province, Postal Code) and will try to find the exact street location’s coordinates. 

First, DOTS Address Geocode – CA will attempt to correct and normalize the address to make it more likely to geocode properly. You don’t need to worry about fixing the address before sending it to DOTS Address Geocode – CA, unless you want to filter out invalid or non-existent addresses beforehand.

This operation requires the Address value, and either Municipality and Province, or the Zip code. Providing all inputs is recommended.

## [GetGeoLocation Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-geocode-canada/agca-operations/agca-getgeolocation-recommended/)

## GetGeoLocation Request URLs (Query String Parameters)

>[!CAUTION]
>### *Important - Use query string parameters for all inputs.  Do not use path parameters since it will lead to errors due to optional parameters and special character issues.*


### JSON
#### Trial

https://trial.serviceobjects.com/GCC/api.svc/json/GetGeoLocation?Address=1053+Carling+Ave&Municipality=Ottawa&Province=ON&PostalCode=K1Y+4E9&LicenseKey={LicenseKey}

#### Production

https://sws.serviceobjects.com/GCC/api.svc/json/GetGeoLocation?Address=1053+Carling+Ave&Municipality=Ottawa&Province=ON&PostalCode=K1Y+4E9&LicenseKey={LicenseKey}

#### Production Backup

https://swsbackup.serviceobjects.com/GCC/api.svc/json/GetGeoLocation?Address=1053+Carling+Ave&Municipality=Ottawa&Province=ON&PostalCode=K1Y+4E9&LicenseKey={LicenseKey}

### XML
#### Trial

https://trial.serviceobjects.com/GCC/api.svc/xml/GetGeoLocation?Address=1053+Carling+Ave&Municipality=Ottawa&Province=ON&PostalCode=K1Y+4E9&LicenseKey={LicenseKey}

#### Production

https://sws.serviceobjects.com/GCC/api.svc/xml/GetGeoLocation?Address=1053+Carling+Ave&Municipality=Ottawa&Province=ON&PostalCode=K1Y+4E9&LicenseKey={LicenseKey}

#### Production Backup

https://swsbackup.serviceobjects.com/GCC/api.svc/xml/GetGeoLocation?Address=1053+Carling+Ave&Municipality=Ottawa&Province=ON&PostalCode=K1Y+4E9&LicenseKey={LicenseKey}

# AGCA - GetPostalCodeInfo

This operation is almost exactly like GetGeoLocation, but rather than geocoding given a specific address, DOTS Address Geocode – CA will geocode given a postal code. The coordinates given are an average centroid of a given postal code and often times match precisely to the street location.

## [GetPostalCodeInfo Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-geocode-canada/agca-operations/agca-getpostalcodeinfo/)

## GetPostalCodeInfo Request URLs (Query String Parameters)

>[!CAUTION]
>#### *Important - Use query string parameters for all inputs.  Do not use path parameters since it will lead to errors due to optional parameters and special character issues.*

### JSON
#### Trial

https://trial.serviceobjects.com/GCC/api.svc/json/GetPostalCodeInfo?PostalCode=L5M+4Z5&LicenseKey={LicenseKey}

#### Production

https://sws.serviceobjects.com/GCC/api.svc/json/GetPostalCodeInfo?PostalCode=L5M+4Z5&LicenseKey={LicenseKey}

#### Production Backup

https://swsbackup.serviceobjects.com/GCC/api.svc/json/GetPostalCodeInfo?PostalCode=L5M+4Z5&LicenseKey={LicenseKey}

### XML
#### Trial

https://trial.serviceobjects.com/GCC/api.svc/xml/GetPostalCodeInfo?PostalCode=L5M+4Z5&LicenseKey={LicenseKey}

#### Production

https://sws.serviceobjects.com/GCC/api.svc/xml/GetPostalCodeInfo?PostalCode=L5M+4Z5&LicenseKey={LicenseKey}

#### Production Backup

https://swsbackup.serviceobjects.com/GCC/api.svc/xml/GetPostalCodeInfo?PostalCode=L5M+4Z5&LicenseKey={LicenseKey}

# AGCA - GetGeoLocationByMunicipalityProvince

This operation is almost exactly like GetGeoLocation except that it provides latitude/longitude coordinates based on provided municipality and province.

## [GetGeoLocationByMunicipalityProvince Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-geocode-canada/agca-operations/agca-getgeolocationbymunicipalityprovince/)

## GetGeoLocationByMunicipalityProvince Request URLs (Query String Parameters)

>[!CAUTION]
>#### *Important - Use query string parameters for all inputs.  Do not use path parameters since it will lead to errors due to optional parameters and special character issues.*

### JSON
#### Trial

https://trial.serviceobjects.com/GCC/api.svc/json/GetGeoLocationByMunicipalityProvince?Municipality=Laval&Province=QC&LicenseKey={LicenseKey}

#### Production

https://sws.serviceobjects.com/GCC/api.svc/json/GetGeoLocationByMunicipalityProvince?Municipality=Laval&Province=QC&LicenseKey={LicenseKey}

#### Production Backup

https://swsbackup.serviceobjects.com/GCC/api.svc/json/GetGeoLocationByMunicipalityProvince?Municipality=Laval&Province=QC&LicenseKey={LicenseKey}

### XML
#### Trial

https://trial.serviceobjects.com/GCC/api.svc/xml/GetGeoLocationByMunicipalityProvince?Municipality=Laval&Province=QC&LicenseKey={LicenseKey}

#### Production

https://sws.serviceobjects.com/GCC/api.svc/xml/GetGeoLocationByMunicipalityProvince?Municipality=Laval&Province=QC&LicenseKey={LicenseKey}

#### Production Backup

https://swsbackup.serviceobjects.com/GCC/api.svc/xml/GetGeoLocationByMunicipalityProvince?Municipality=Laval&Province=QC&LicenseKey={LicenseKey}
