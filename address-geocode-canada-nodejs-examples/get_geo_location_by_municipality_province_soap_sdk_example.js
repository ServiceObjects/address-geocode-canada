import { GetGeoLocationByMunicipalityProvinceSoap } from "../address-geocode-canada-nodejs/SOAP/get_geo_location_by_municipality_province_soap.js";

async function GetGeoLocationByMunicipalityProvinceSoapGo(licenseKey, isLive) {
    console.log("\n--------------------------------------------------------------------------");
    console.log("Address Geocode - Canada - GetGeoLocationByMunicipalityProvince - SOAP SDK");
    console.log("--------------------------------------------------------------------------");

    const municipality = "Ottawa";
    const province = "ON";
    const timeoutSeconds = 15;

    console.log("\n* Input *\n");
    console.log(`Municipality   : ${municipality}`);
    console.log(`Province       : ${province}`);
    console.log(`License Key    : ${licenseKey}`);
    console.log(`Is Live        : ${isLive}`);
    console.log(`Timeout Seconds: ${timeoutSeconds}`);

    try {
        const agca = new GetGeoLocationByMunicipalityProvinceSoap(
            municipality,
            province,
            licenseKey,
            isLive,
            timeoutSeconds
        );
        const response = await agca.invokeAsync();

        if (response.Error) {
            console.log("\n* Error *\n");
            console.log(`Error Desc    : ${response.Error.Desc}`);
            console.log(`Error Number  : ${response.Error.Number}`);
            console.log(`Error Location: ${response.Error.Location}`);
        }

        console.log("\n* Geocode Info *\n");
        if (response) {
            console.log(`Latitude  : ${response.Latitude}`);
            console.log(`Longitude : ${response.Longitude}`);
            console.log(`PostalCode: ${response.PostalCode}`);
        } else {
            console.log("No geocode info found.");
        }
    } catch (e) {
        console.log("\n* Error *\n");
        console.log(`Error Message: ${e.message}`);
    }
}

export { GetGeoLocationByMunicipalityProvinceSoapGo };
