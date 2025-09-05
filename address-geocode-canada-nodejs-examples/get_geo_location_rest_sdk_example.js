import { GetGeoLocationClient } from "../address-geocode-canada-nodejs/REST/get_geo_location_rest.js";

async function GetGeoLocationRestGo(licenseKey, isLive) {
    console.log("\n----------------------------------------------------");
    console.log("Address Geocode - Canada - GetGeoLocation - REST SDK");
    console.log("----------------------------------------------------");

    const address = "1053 Carling Ave";
    const municipality = "Ottawa";
    const province = "ON";
    const postalCode = "K1Z 7K4";
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
        const response = await GetGeoLocationClient.invoke(
            address,
            municipality,
            province,
            postalCode,
            licenseKey,
            isLive,
            timeoutSeconds
        );

        console.log("\n* Geocode Info *\n");
        if (response) {
            console.log(`Latitude  : ${response.Latitude}`);
            console.log(`Longitude : ${response.Longitude}`);
            console.log(`PostalCode: ${response.PostalCode}`);
            console.log(`MatchCode : ${response.MatchCode}`);
        } else {
            console.log("No geocode info found.");
        }

        if (response.Error) {
            console.log("\n* Error *\n");
            console.log(`Error Desc    : ${response.Error.Desc}`);
            console.log(`Error Number  : ${response.Error.Number}`);
            console.log(`Error Location: ${response.Error.Location}`);
            return;
        }
    } catch (e) {
        console.log("\n* Error *\n");
        console.log(`Error Message: ${e.message}`);
    }
}

export { GetGeoLocationRestGo };
