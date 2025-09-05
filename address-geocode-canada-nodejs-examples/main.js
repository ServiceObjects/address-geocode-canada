import { GetGeoLocationByMunicipalityProvinceRestGo } from './get_geo_location_by_municipality_province_rest_sdk_example.js';
import { GetGeoLocationRestGo } from './get_geo_location_rest_sdk_example.js'
import { GetPostalCodeInfoRestGo } from './get_postal_code_info_rest_sdk_example.js'
import { GetGeoLocationByMunicipalityProvinceSoapGo } from './get_geo_location_by_municipality_province_soap_sdk_example.js'
import { GetGeoLocationSoapGo } from './get_geo_location_soap_sdk_example.js'
import { GetPostalCodeInfoSoapGo } from './get_postal_code_info_soap_sdk_example.js'

export async function main() {

    //Your license key from Service Objects.
    //Trial license keys will only work on the
    //trail environments and production license
    //keys will only work on production environments.
    const licenseKey = "LICENSE KEY";
    const isLive = false;

    // Address Geocode – Canada - GetGeoLocationByMunicipalityProvince - REST SDK"
    await GetGeoLocationByMunicipalityProvinceRestGo(licenseKey, isLive);

    // Address Geocode – Canada - GetGeoLocationByMunicipalityProvince - SOAP SDK
    await GetGeoLocationByMunicipalityProvinceSoapGo(licenseKey, isLive);

    // Address Geocode – Canada - GetGeoLocation - REST SDK
    await GetGeoLocationRestGo(licenseKey, isLive);

    // Address Geocode – Canada - GetGeoLocation - SOAP SDK
    await GetGeoLocationSoapGo(licenseKey, isLive);

    // Address Geocode – Canada - GetPostalCodeInfo - REST SDK
    await GetPostalCodeInfoRestGo(licenseKey, isLive);

    // Address Geocode – Canada - GetPostalCodeInfo - SOAP SDK
    await GetPostalCodeInfoSoapGo(licenseKey, isLive);
}
main().catch((error) => {
    console.error("An error occurred:", error);
    process.exit(1);
});