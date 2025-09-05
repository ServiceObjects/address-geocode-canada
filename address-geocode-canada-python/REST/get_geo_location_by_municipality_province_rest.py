import requests
from agca_response import GetGeoLocationByMunicipalityProvinceResponse, Error

# Endpoint URLs for ServiceObjects Address Geocode CA (AGCA) API
primary_url = "https://sws.serviceobjects.com/GCC/api.svc/json/GetGeoLocationByMunicipalityProvince?"
backup_url = "https://swsbackup.serviceobjects.com/GCC/api.svc/json/GetGeoLocationByMunicipalityProvince?"
trial_url = "https://trial.serviceobjects.com/GCC/api.svc/json/GetGeoLocationByMunicipalityProvince?"

def get_geo_location_by_municipality_province(
    municipality: str,
    province: str,
    license_key: str,
    is_live: bool = True
) -> GetGeoLocationByMunicipalityProvinceResponse:
    """
    Call ServiceObjects Address Geocode CA (AGCA) API's GetGeoLocationByMunicipalityProvince endpoint
    to retrieve geocoding information (latitude, longitude, postal code) for a given Canadian municipality and province.

    Parameters:
        municipality: The municipality of the address to geocode (e.g., "Cayuga").
        province: The province of the address to geocode (e.g., "ON").
        license_key: Your ServiceObjects license key.
        is_live: Use live or trial servers.

    Returns:
        GetGeoLocationByMunicipalityProvinceResponse: Parsed JSON response with geocoding results or error details.

    Raises:
        RuntimeError: If the API returns an error payload.
        equests.RequestException: On network/HTTP failures (trial mode).
    """
    params = {
        "Municipality": municipality,
        "Province": province,
        "LicenseKey": license_key,
    }
    # Select the base URL: production vs trial
    url = primary_url if is_live else trial_url

    try:
        # Attempt primary (or trial) endpoint
        response = requests.get(url, params=params, timeout=10)
        response.raise_for_status()
        data = response.json()

        # If API returned an error in JSON payload, trigger fallback
        error = data.get('Error', None)
        if not (error is None or error.get('Number', None) != "4"):
            if is_live:
                # Try backup URL
                response = requests.get(backup_url, params=params, timeout=10)
                response.raise_for_status()
                data = response.json()

                # If still error, propagate exception
                if 'Error' in data:
                    raise RuntimeError(f"AGCA service error: {data['Error']}")

        # Convert JSON response to GetGeoLocationByMunicipalityProvinceResponse for structured access
        error = Error(**data.get("Error", {})) if data.get("Error") else None

        return GetGeoLocationByMunicipalityProvinceResponse(
            Latitude=data.get("Latitude"),
            Longitude=data.get("Longitude"),
            PostalCode=data.get("PostalCode"),
            Error=error,
        )

    except requests.RequestException as req_exc:
        # Network or HTTP-level error occurred
        if is_live:
            try:
                # Fallback to backup URL
                response = requests.get(backup_url, params=params, timeout=10)
                response.raise_for_status()
                data = response.json()
                if "Error" in data:
                    raise RuntimeError(f"AGCA backup error: {data['Error']}") from req_exc

                error = Error(**data.get("Error", {})) if data.get("Error") else None

                return GetGeoLocationByMunicipalityProvinceResponse(
                    Latitude=data.get("Latitude"),
                    Longitude=data.get("Longitude"),
                    PostalCode=data.get("PostalCode"),
                    Error=error,
                )
            except Exception as backup_exc:
                raise RuntimeError("AGCA service unreachable on both endpoints") from backup_exc
        else:
            raise RuntimeError(f"AGCA trial error: {str(req_exc)}") from req_exc