/**
 * Input parameters for the GetGeoLocation API call.
 */
export class GetGeoLocationInput {
    constructor(data = {}) {
        this.Address = data.Address;
        this.Municipality = data.Municipality;
        this.Province = data.Province;
        this.PostalCode = data.PostalCode;
        this.LicenseKey = data.LicenseKey;
        this.IsLive = data.IsLive !== undefined ? data.IsLive : true;
        this.TimeoutSeconds = data.TimeoutSeconds !== undefined ? data.TimeoutSeconds : 15;
    }

    toString() {
        return `GetGeoLocationInput: Address = ${this.Address}, Municipality = ${this.Municipality}, Province = ${this.Province}, PostalCode = ${this.PostalCode}, LicenseKey = ${this.LicenseKey}, IsLive = ${this.IsLive}, TimeoutSeconds = ${this.TimeoutSeconds}`;
    }
}

/**
 * Input parameters for the GetPostalCodeInfo API call.
 */
export class GetPostalCodeInfoInput {
    constructor(data = {}) {
        this.PostalCode = data.PostalCode;
        this.LicenseKey = data.LicenseKey;
        this.IsLive = data.IsLive !== undefined ? data.IsLive : true;
        this.TimeoutSeconds = data.TimeoutSeconds !== undefined ? data.TimeoutSeconds : 15;
    }

    toString() {
        return `GetPostalCodeInfoInput: PostalCode = ${this.PostalCode}, LicenseKey = ${this.LicenseKey}, IsLive = ${this.IsLive}, TimeoutSeconds = ${this.TimeoutSeconds}`;
    }
}

/**
 * Input parameters for the GetGeoLocationByMunicipalityProvince API call.
 */
export class GetGeoLocationByMunicipalityProvinceInput {
    constructor(data = {}) {
        this.Municipality = data.Municipality;
        this.Province = data.Province;
        this.LicenseKey = data.LicenseKey;
        this.IsLive = data.IsLive !== undefined ? data.IsLive : true;
        this.TimeoutSeconds = data.TimeoutSeconds !== undefined ? data.TimeoutSeconds : 15;
    }

    toString() {
        return `GetGeoLocationByMunicipalityProvinceInput: Municipality = ${this.Municipality}, Province = ${this.Province}, LicenseKey = ${this.LicenseKey}, IsLive = ${this.IsLive}, TimeoutSeconds = ${this.TimeoutSeconds}`;
    }
}

/**
 * Error object for API responses.
 */
export class Error {
    constructor(data = {}) {
        this.Desc = data.Desc;
        this.Number = data.Number;
        this.Location = data.Location;
    }

    toString() {
        return `Error: Desc = ${this.Desc}, Number = ${this.Number}, Location = ${this.Location}`;
    }
}

/**
 * Response from GetGeoLocation API, containing the latitude, longitude, postal code, 
 * and match code for the given Canadian address.
 */
export class GetGeoLocationResponse {
    constructor(data = {}) {
        this.Latitude = data.Latitude;
        this.Longitude = data.Longitude;
        this.PostalCode = data.PostalCode;
        this.MatchCode = data.MatchCode;
        this.Error = data.Error ? new Error(data.Error) : null;
    }

    toString() {
        return `GetGeoLocationResponse: Latitude = ${this.Latitude}, Longitude = ${this.Longitude}, PostalCode = ${this.PostalCode}, MatchCode = ${this.MatchCode}, Error = ${this.Error ? this.Error.toString() : 'null'}`;
    }
}

/**
 * Response from GetPostalCodeInfo API, containing the latitude, longitude, postal code, 
 * time zone, DST, area code, city, city population, province, and error for the given Canadian postal code.
 */
export class GetPostalCodeInfoResponse {
    constructor(data = {}) {
        this.Latitude = data.Latitude;
        this.Longitude = data.Longitude;
        this.PostalCode = data.PostalCode;
        this.TimeZone = data.TimeZone;
        this.DST = data.DST;
        this.AreaCode = data.AreaCode;
        this.City = data.City;
        this.CityPopulation = data.CityPopulation;
        this.Province = data.Province;
        this.Error = data.Error ? new Error(data.Error) : null;
    }

    toString() {
        return `GetPostalCodeInfoResponse: Latitude = ${this.Latitude}, Longitude = ${this.Longitude}, PostalCode = ${this.PostalCode}, TimeZone = ${this.TimeZone}, DST = ${this.DST}, AreaCode = ${this.AreaCode}, City = ${this.City}, CityPopulation = ${this.CityPopulation}, Province = ${this.Province}, Error = ${this.Error ? this.Error.toString() : 'null'}`;
    }
}

/**
 * Response from GetGeoLocationByMunicipalityProvince API, containing the latitude, longitude, 
 * postal code, and error for the given Canadian municipality and province.
 */
export class GetGeoLocationByMunicipalityProvinceResponse {
    constructor(data = {}) {
        this.Latitude = data.Latitude;
        this.Longitude = data.Longitude;
        this.PostalCode = data.PostalCode;
        this.Error = data.Error ? new Error(data.Error) : null;
    }

    toString() {
        return `GetGeoLocationByMunicipalityProvinceResponse: Latitude = ${this.Latitude}, Longitude = ${this.Longitude}, PostalCode = ${this.PostalCode}, Error = ${this.Error ? this.Error.toString() : 'null'}`;
    }
}

export default GetGeoLocationResponse;