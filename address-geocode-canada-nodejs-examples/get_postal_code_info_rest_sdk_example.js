import { GetPostalCodeInfoClient } from "../address-geocode-canada-nodejs/REST/get_postal_code_info_rest.js";

async function GetPostalCodeInfoRestGo(licenseKey, isLive) {
    console.log("\n-------------------------------------------------------");
    console.log("Address Geocode - Canada - GetPostalCodeInfo - REST SDK");
    console.log("-------------------------------------------------------");

    const postalCode = "N2E 1P4";
    const timeoutSeconds = 15;

    console.log("\n* Input *\n");
    console.log(`PostalCode     : ${postalCode}`);
    console.log(`License Key    : ${licenseKey}`);
    console.log(`Is Live        : ${isLive}`);
    console.log(`Timeout Seconds: ${timeoutSeconds}`);

    try {
        const response = await GetPostalCodeInfoClient.invoke(
            postalCode,
            licenseKey,
            isLive,
            timeoutSeconds
        );

        console.log("\n* Geocode Info *\n");
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

export { GetPostalCodeInfoRestGo };
