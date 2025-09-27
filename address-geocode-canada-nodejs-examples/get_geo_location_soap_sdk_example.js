import { GetGeoLocationSoap } from "../address-geocode-canada-nodejs/SOAP/get_geo_location_soap.js";

async function GetGeoLocationSoapGo(licenseKey, isLive) {
    console.log("\n----------------------------------------------------");
    console.log("Address Geocode - Canada - GetGeoLocation - SOAP SDK");
    console.log("----------------------------------------------------");

    const address = "50 Coach Hill Dr";
    const municipality = "Kitchener";
    const province = "ON";
    const postalCode = "N2E 1P4";
    const timeoutSeconds = 15;

    console.log("\n* Input *\n");
    console.log(`Address        : ${address}`);
    console.log(`Municipality   : ${municipality}`);
    console.log(`Province       : ${province}`);
    console.log(`PostalCode     : ${postalCode}`);
    console.log(`License Key    : ${licenseKey}`);
    console.log(`Is Live        : ${isLive}`);
    console.log(`Timeout Seconds: ${timeoutSeconds}`);

    try {
        const agca = new GetGeoLocationSoap(
            address,
            municipality,
            province,
            postalCode,
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
            console.log(`MatchCode : ${response.MatchCode}`);
        } else {
            console.log("No geocode info found.");
        }
    } catch (e) {
        console.log("\n* Error *\n");
        console.log(`Error Message: ${e.message}`);
    }
}

export { GetGeoLocationSoapGo };
