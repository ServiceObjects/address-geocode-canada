import axios from 'axios';
import querystring from 'querystring';
import { GetGeoLocationByMunicipalityProvinceResponse } from './agca_response.js';

/**
 * @constant
 * @type {string}
 * @description The base URL for the live ServiceObjects Address Geocode CA (AGCA) API service.
 */
const LiveBaseUrl = 'https://sws.serviceobjects.com/GCC/api.svc/json/';

/**
 * @constant
 * @type {string}
 * @description The base URL for the backup ServiceObjects Address Geocode CA (AGCA) API service.
 */
const BackupBaseUrl = 'https://swsbackup.serviceobjects.com/GCC/api.svc/json/';

/**
 * @constant
 * @type {string}
 * @description The base URL for the trial ServiceObjects Address Geocode CA (AGCA) API service.
 */
const TrialBaseUrl = 'https://trial.serviceobjects.com/GCC/api.svc/json/';

/**
 * <summary>
 * Checks if a response from the API is valid by verifying that it either has no Error object
 * or the Error.Number is not equal to '4'.
 * </summary>
 * <param name="response" type="Object">The API response object to validate.</param>
 * <returns type="boolean">True if the response is valid, false otherwise.</returns>
 */
const isValid = (response) => !response?.Error || response.Error.Number !== '4';

/**
 * <summary>
 * Constructs a full URL for the GetGeoLocationByMunicipalityProvince API endpoint by combining the base URL
 * with query parameters derived from the input parameters.
 * </summary>
 * <param name="params" type="Object">An object containing all the input parameters.</param>
 * <param name="baseUrl" type="string">The base URL for the API service (live, backup, or trial).</param>
 * <returns type="string">The constructed URL with query parameters.</returns>
 */
const buildUrl = (params, baseUrl) =>
    `${baseUrl}GetGeoLocationByMunicipalityProvince?${querystring.stringify(params)}`;

/**
 * <summary>
 * Performs an HTTP GET request to the specified URL with a given timeout.
 * </summary>
 * <param name="url" type="string">The URL to send the GET request to.</param>
 * <param name="timeoutSeconds" type="number">The timeout duration in seconds for the request.</param>
 * <returns type="Promise<GetGeoLocationByMunicipalityProvinceResponse>">A promise that resolves to a GetGeoLocationByMunicipalityProvinceResponse object containing the API response data.</returns>
 * <exception cref="Error">Thrown if the HTTP request fails, with a message detailing the error.</exception>
 */
const httpGet = async (url, timeoutSeconds) => {
    try {
        const response = await axios.get(url, { timeout: timeoutSeconds * 1000 });
        return new GetGeoLocationByMunicipalityProvinceResponse(response.data);
    } catch (error) {
        throw new Error(`HTTP request failed: ${error.message}`);
    }
};

/**
 * <summary>
 * Provides functionality to call the ServiceObjects Address Geocode CA (AGCA) API's GetGeoLocationByMunicipalityProvince endpoint,
 * retrieving geocoding information (e.g., latitude, longitude, postal code) for a given Canadian municipality and province with fallback to a backup endpoint for reliability in live mode.
 * </summary>
 */
const GetGeoLocationByMunicipalityProvinceClient = {
    /**
     * <summary>
     * Asynchronously invokes the GetGeoLocationByMunicipalityProvince API endpoint, attempting the primary endpoint
     * first and falling back to the backup if the response is invalid (Error.Number == '4') in live mode.
     * </summary>
     * @param {Object} input - Input parameters for the API call.
     * @param {string} input.Municipality - The municipality to geocode (e.g., "Cayuga"). Required.
     * @param {string} input.Province - The province to geocode (e.g., "ON"). Required.
     * @param {string} input.LicenseKey - Your license key to use the service.
     * @param {boolean} input.IsLive - Value to determine whether to use the live or trial servers.
     * @param {number} input.TimeoutSeconds - Timeout, in seconds, for the call to the service.
     * @returns {Promise<GetGeoLocationByMunicipalityProvinceResponse>} - A promise that resolves to a GetGeoLocationByMunicipalityProvinceResponse object.
     */
    async invokeAsync(input) {
        const params = {
            Municipality: input.Municipality,
            Province: input.Province,
            LicenseKey: input.LicenseKey
        };

        const url = buildUrl(params, input.IsLive ? LiveBaseUrl : TrialBaseUrl);
        let response = await httpGet(url, input.TimeoutSeconds);

        if (input.IsLive && !isValid(response)) {
            const fallbackUrl = buildUrl(params, BackupBaseUrl);
            const fallbackResponse = await httpGet(fallbackUrl, input.TimeoutSeconds);
            return fallbackResponse;
        }
        return response;
    },

    /**
     * <summary>
     * Synchronously invokes the GetGeoLocationByMunicipalityProvince API endpoint by wrapping the async call
     * and awaiting its result immediately.
     * </summary>
     * @param {Object} input - Input parameters for the API call.
     * @param {string} input.Municipality - The municipality to geocode (e.g., "Cayuga"). Required.
     * @param {string} input.Province - The province to geocode (e.g., "ON"). Required.
     * @param {string} input.LicenseKey - Your license key to use the service.
     * @param {boolean} input.IsLive - Value to determine whether to use the live or trial servers.
     * @param {number} input.TimeoutSeconds - Timeout, in seconds, for the call to the service.
     * @returns {GetGeoLocationByMunicipalityProvinceResponse} - A GetGeoLocationByMunicipalityProvinceResponse object with geocoding details or an error.
     */
    invoke(Municipality, Province, LicenseKey, IsLive = true, TimeoutSeconds = 15) {
        return (async () => await this.invokeAsync({ Municipality, Province, LicenseKey, IsLive, TimeoutSeconds }))();
    }
};

export { GetGeoLocationByMunicipalityProvinceClient, GetGeoLocationByMunicipalityProvinceResponse };