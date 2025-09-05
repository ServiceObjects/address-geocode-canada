using System.Runtime.Serialization;

namespace address_geocode_canada_dot_net.REST
{
    /// <summary>
    /// Response from GetGeoLocation API, containing the latitude, longitude, postal code, 
    /// and match code for the given Canadian address.
    /// </summary>
    [DataContract]
    public class GetGeoLocationResponse
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string PostalCode { get; set; }
        public string MatchCode { get; set; }
        public Error Error { get; set; }

        public override string ToString()
        {
            return $"Latitude: {Latitude}\n" +
                   $"Longitude: {Longitude}\n" +
                   $"PostalCode: {PostalCode}\n" +
                   $"MatchCode: {MatchCode}\n" +
                   $"Error: {(Error != null ? Error.ToString() : "null")}";
        }
    }

    [DataContract]
    public class GetPostalCodeInfoResponse
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string PostalCode { get; set; }
        public string TimeZone { get; set; }
        public string DST { get; set; }
        public string AreaCode { get; set; }
        public string City { get; set; }
        public string CityPopulation { get; set; }
        public string Province { get; set; }
        public Error Error { get; set; }

        public override string ToString()
        {
            return $"Latitude: {Latitude}\n" +
                   $"Longitude: {Longitude}\n" +
                   $"PostalCode: {PostalCode}\n" +
                   $"TimeZone: {TimeZone}\n" +
                   $"DST: {DST}\n" +
                   $"AreaCode: {AreaCode}\n" +
                   $"City: {City}\n" +
                   $"CityPopulation: {CityPopulation}\n" +
                   $"Province: {Province}\n" +
                   $"Error: {(Error != null ? Error.ToString() : "null")}";
        }
    }

    /// <summary>
    /// Response from GetGeoLocationByMunicipalityProvince API, containing the latitude, longitude, 
    /// postal code, and error for the given Canadian municipality and province.
    /// </summary>
    [DataContract]
    public class GetGeoLocationByMunicipalityProvinceResponse
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string PostalCode { get; set; }
        public Error Error { get; set; }
        public override string ToString()
        {
            return $"Latitude: {Latitude}\n" +
                   $"Longitude: {Longitude}\n" +
                   $"PostalCode: {PostalCode}\n" +
                   $"Error: {(Error != null ? Error.ToString() : "null")}";
        }
    }

    /// <summary>
    /// Error object for API responses
    /// </summary>
    [DataContract]
    public class Error
    {
        public string Desc { get; set; }
        public string Number { get; set; }

        public string Location { get; set; }

        public override string ToString()
        {
            return $"Desc: {Desc}, Number: {Number}, Location: {Location}";
        }
    }
}
