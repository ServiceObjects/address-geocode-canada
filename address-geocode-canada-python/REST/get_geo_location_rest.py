import requests
from agca_response import GetGeoLocationResponse, Error

# Endpoint URLs for ServiceObjects Address Geocode CA (AGCA) API
primary_url = "https://sws.serviceobjects.com/GCC/api.svc/json/GetGeoLocation?"
backup_url = "https://swsbackup.serviceobjects.com/GCC/api.svc/json/GetGeoLocation?"
trial_url = "https://trial.serviceobjects.com/GCC/api.svc/json/GetGeoLocation?"

def get_geo_location(
    address: str,
    municipality: str,
    province: str,
    postal_code: str,
    license_key: str,
    is_live: bool = True
) -> GetGeoLocationResponse:
    """
    Call ServiceObjects Address Geocode CA (AGCA) API's GetGeoLocation endpoint
    to retrieve geocoding information (latitude, longitude, postal code, match code) for a given Canadian address.

    Parameters:
        address: Address line of the address to geocode (e.g., "123 Main Street").
        municipality: The municipality of the address to geocode (e.g., "Cayuga").
        province: The province of the address to geocode (e.g., "ON").
        postal_code: The postal code of the address to geocode (e.g., "N0A 1E0").
        license_key: Your ServiceObjects license key.
        is_live: Use live or trial servers.

    Returns:
        GetGeoLocationResponse: Parsed JSON response with geocoding results or error details.

    Raises:
        RuntimeError: If the API returns an error payload.
        requests.RequestException: On network/HTTP failures (trial mode).
    """
    params = {
        "Address": address,
        "Municipality": municipality,
        "Province": province,
        "PostalCode": postal_code,
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

        # Convert JSON response to GetGeoLocationResponse for structured access
        error = Error(**data.get("Error", {})) if data.get("Error") else None

        return GetGeoLocationResponse(
            Latitude=data.get("Latitude"),
            Longitude=data.get("Longitude"),
            PostalCode=data.get("PostalCode"),
            MatchCode=data.get("MatchCode"),
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

                return GetGeoLocationResponse(
                    Latitude=data.get("Latitude"),
                    Longitude=data.get("Longitude"),
                    PostalCode=data.get("PostalCode"),
                    MatchCode=data.get("MatchCode"),
                    Error=error,
                )
            except Exception as backup_exc:
                raise RuntimeError("AGCA service unreachable on both endpoints") from backup_exc
        else:
            raise RuntimeError(f"AGCA trial error: {str(req_exc)}") from req_exc