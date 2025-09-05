using address_geocode_ca_dot_net_examples;
using address_geocode_canada_dot_net_examples;

//Your license key from Service Objects.
//Trial license keys will only work on the
//trail environments and production license
//keys will only work on production environments.
string LicenseKey = "LICENSE KEY";

bool IsProductionKey = false;

// Address Geocode – Canada - GetGeoLocationByMunicipalityProvince - REST SDK"
GetGeoLocationByMunicipalityProvinceRestSdkExample.Go(LicenseKey, IsProductionKey);

// Address Geocode – Canada - GetGeoLocationByMunicipalityProvince - SOAP SDK
GetGeoLocationByMunicipalityProvinceSoapSdkExample.Go(LicenseKey, IsProductionKey);

// Address Geocode – Canada - GetGeoLocation - REST SDK
GetGeoLocationRestSdkExample.Go(LicenseKey, IsProductionKey);

// Address Geocode – Canada - GetGeoLocation - SOAP SDK
GetGeoLocationSoapSdkExample.Go(LicenseKey, IsProductionKey);

// Address Geocode – Canada - GetPostalCodeInfo - REST SDK
GetPostalCodeInfoRestSdkExample.Go(LicenseKey, IsProductionKey);

// Address Geocode – Canada - GetPostalCodeInfo - SOAP SDK
GetPostalCodeInfoSoapSdkExample.Go(LicenseKey, IsProductionKey);
