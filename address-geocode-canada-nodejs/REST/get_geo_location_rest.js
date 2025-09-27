import axios from 'axios';
import querystring from 'querystring';
import { GetGeoLocationResponse } from './agca_response.js';

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
 * Constructs a full URL for the GetGeoLocation API endpoint by combining the base URL
 * with query parameters derived from the input parameters.
 * </summary>
 * <param name="params" type="Object">An object containing all the input parameters.</param>
 * <param name="baseUrl" type="string">The base URL for the API service (live, backup, or trial).</param>
 * <returns type="string">The constructed URL with query parameters.</returns>
 */
const buildUrl = (params, baseUrl) =>
    `${baseUrl}GetGeoLocation?${querystring.stringify(params)}`;

/**
 * <summary>
 * Performs an HTTP GET request to the specified URL with a given timeout.
 * </summary>
 * <param name="url" type="string">The URL to send the GET request to.</param>
 * <param name="timeoutSeconds" type="number">The timeout duration in seconds for the request.</param>
 * <returns type="Promise<GetGeoLocationResponse>">A promise that resolves to a GetGeoLocationResponse object containing the API response data.</returns>
 * <exception cref="Error">Thrown if the HTTP request fails, with a message detailing the error.</exception>
 */
const httpGet = async (url, timeoutSeconds) => {
    try {
        const response = await axios.get(url, { timeout: timeoutSeconds * 1000 });
        return new GetGeoLocationResponse(response.data);
    } catch (error) {
        throw new Error(`HTTP request failed: ${error.message}`);
    }
};

/**
 * <summary>
 * Provides functionality to call the ServiceObjects Address Geocode CA (AGCA) API's GetGeoLocation endpoint,
 * retrieving geocoding information (e.g., latitude, longitude, postal code) for a given Canadian address with fallback to a backup endpoint for reliability in live mode.
 * </summary>
 */
const GetGeoLocationClient = {
    /**
     * <summary>
     * Asynchronously invokes the GetGeoLocation API endpoint, attempting the primary endpoint
     * first and falling back to the backup if the response is invalid (Error.Number == '4') in live mode.
     * </summary>
     * @param {string} Address - Address line of the address to geocode (e.g., "123 Main Street"). Required.
     * @param {string} Municipality - The municipality of the address to geocode (e.g., "Cayuga"). Optional if postal code is provided.
     * @param {string} Province - The province of the address to geocode (e.g., "ON"). Optional if postal code is provided.
     * @param {string} PostalCode - The postal code of the address to geocode. Optional if municipality and province are provided.
     * @param {string} LicenseKey - Your license key to use the service.
     * @param {boolean} IsLive - Value to determine whether to use the live or trial servers.
     * @param {number} TimeoutSeconds - Timeout, in seconds, for the call to the service.
     * @returns {Promise<GetGeoLocationResponse>} - A promise that resolves to a GetGeoLocationResponse object.
     */
    async invokeAsync(Address, Municipality, Province, PostalCode, LicenseKey, IsLive = true, TimeoutSeconds = 15) {
        const params = {
            Address,
            Municipality,
            Province,
            PostalCode,
            LicenseKey: LicenseKey
        };

        const url = buildUrl(params, IsLive ? LiveBaseUrl : TrialBaseUrl);
        let response = await httpGet(url, TimeoutSeconds);

        if (IsLive && !isValid(response)) {
            const fallbackUrl = buildUrl(params, BackupBaseUrl);
            const fallbackResponse = await httpGet(fallbackUrl, TimeoutSeconds);
            return fallbackResponse;
        }
        return response;
    },

    /**
     * <summary>
     * Synchronously invokes the GetGeoLocation API endpoint by wrapping the async call
     * and awaiting its result immediately.
     * </summary>
     * @param {string} Address - Address line of the address to geocode (e.g., "123 Main Street"). Required.
     * @param {string} Municipality - The municipality of the address to geocode (e.g., "Cayuga"). Optional if postal code is provided.
     * @param {string} Province - The province of the address to geocode (e.g., "ON"). Optional if postal code is provided.
     * @param {string} PostalCode - The postal code of the address to geocode. Optional if municipality and province are provided.
     * @param {string} LicenseKey - Your license key to use the service.
     * @param {boolean} IsLive - Value to determine whether to use the live or trial servers.
     * @param {number} TimeoutSeconds - Timeout, in seconds, for the call to the service.
     * @returns {GetGeoLocationResponse} - A GetGeoLocationResponse object with geocoding details or an error.
     */
    invoke(Address, Municipality, Province, PostalCode, LicenseKey, IsLive = true, TimeoutSeconds = 15) {
        return (async () => await this.invokeAsync(Address, Municipality, Province, PostalCode, LicenseKey, IsLive, TimeoutSeconds))();
    }
};

export { GetGeoLocationClient, GetGeoLocationResponse };