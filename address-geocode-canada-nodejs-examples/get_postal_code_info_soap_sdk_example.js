import { GetPostalCodeInfoSoap } from "../address-geocode-canada-nodejs/SOAP/get_postal_code_info_soap.js";

async function GetPostalCodeInfoSoapGo(licenseKey, isLive) {
    console.log("\n-------------------------------------------------------");
    console.log("Address Geocode - Canada - GetPostalCodeInfo - SOAP SDK");
    console.log("-------------------------------------------------------");

    const postalCode = "K1Y 4E9";
    const timeoutSeconds = 15;

    console.log("\n* Input *\n");
    console.log(`PostalCode      : ${postalCode}`);
    console.log(`License Key     : ${licenseKey}`);
    console.log(`Is Live         : ${isLive}`);
    console.log(`Timeout Seconds : ${timeoutSeconds}`);

    try {
        const agca = new GetPostalCodeInfoSoap(postalCode, licenseKey, isLive, timeoutSeconds);
        const response = await agca.invokeAsync();

        if (response.Error) {
            console.log("\n* Error *\n");
            console.log(`Error Desc    : ${response.Error.Desc}`);
            console.log(`Error Number  : ${response.Error.Number}`);
            console.log(`Error Location: ${response.Error.Location}`);
        }

        console.log("\n* Geocode Info *\n");
        if (response) {
            console.log(`Latitude       : ${response.Latitude}`);
            console.log(`Longitude      : ${response.Longitude}`);
            console.log(`PostalCode     : ${response.PostalCode}`);
            console.log(`TimeZone       : ${response.TimeZone}`);
            console.log(`DST            : ${response.DST}`);
            console.log(`AreaCode       : ${response.AreaCode}`);
            console.log(`City           : ${response.City}`);
            console.log(`CityPopulation : ${response.CityPopulation}`);
            console.log(`Province       : ${response.Province}`);
        } else {
            console.log("No geocode info found.");
        }
    } catch (e) {
        console.log("\n* Error *\n");
        console.log(`Error Message: ${e.message}`);
    }
}

export { GetPostalCodeInfoSoapGo };
