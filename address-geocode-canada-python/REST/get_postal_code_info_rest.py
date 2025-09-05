import requests
from agca_response import GetPostalCodeInfoResponse, Error

# Endpoint URLs for ServiceObjects Address Geocode CA (AGCA) API
primary_url = "https://sws.serviceobjects.com/GCC/api.svc/json/GetPostalCodeInfo?"
backup_url = "https://swsbackup.serviceobjects.com/GCC/api.svc/json/GetPostalCodeInfo?"
trial_url = "https://trial.serviceobjects.com/GCC/api.svc/json/GetPostalCodeInfo?"

def get_postal_code_info(
    postal_code: str,
    license_key: str,
    is_live: bool = True
) -> GetPostalCodeInfoResponse:
    """
    Call ServiceObjects Address Geocode CA (AGCA) API's GetPostalCodeInfo endpoint
    to retrieve geocoding information (latitude, longitude, city, province, etc.) for a given Canadian postal code.

    Parameters:
        postal_code: The postal code to geocode (e.g., "K1A 0A1").
        license_key: Your ServiceObjects license key.
        is_live: Use live or trial servers.

    Returns:
        GetPostalCodeInfoResponse: Parsed JSON response with geocoding results or error details.

    Raises:
        RuntimeError: If the API returns an error payload.
        requests.RequestException: On network/HTTP failures (trial mode).
    """
    params = {
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

        # Convert JSON response to GetPostalCodeInfoResponse for structured access
        error = Error(**data.get("Error", {})) if data.get("Error") else None

        return GetPostalCodeInfoResponse(
            Latitude=data.get("Latitude"),
            Longitude=data.get("Longitude"),
            PostalCode=data.get("PostalCode"),
            TimeZone=data.get("TimeZone"),
            DST=data.get("DST"),
            AreaCode=data.get("AreaCode"),
            City=data.get("City"),
            CityPopulation=data.get("CityPopulation"),
            Province=data.get("Province"),
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

                return GetPostalCodeInfoResponse(
                    Latitude=data.get("Latitude"),
                    Longitude=data.get("Longitude"),
                    PostalCode=data.get("PostalCode"),
                    TimeZone=data.get("TimeZone"),
                    DST=data.get("DST"),
                    AreaCode=data.get("AreaCode"),
                    City=data.get("City"),
                    CityPopulation=data.get("CityPopulation"),
                    Province=data.get("Province"),
                    Error=error,
                )
            except Exception as backup_exc:
                raise RuntimeError("AGCA service unreachable on both endpoints") from backup_exc
        else:
            raise RuntimeError(f"AGCA trial error: {str(req_exc)}") from req_exc