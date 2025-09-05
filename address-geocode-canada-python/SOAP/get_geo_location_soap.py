from suds.client import Client
from suds import WebFault
from suds.sudsobject import Object


class GetGeoLocationSoap:
    def __init__(self, license_key: str, is_live: bool = True, timeout_ms: int = 15000):
        """
        license_key: Service Objects AGCA license key.
        is_live: Whether to use live or trial endpoints
        timeout_ms: SOAP call timeout in milliseconds
        """
        self.is_live = is_live
        self.timeout = timeout_ms / 1000.0
        self.license_key = license_key

        # WSDL URLs
        self._primary_wsdl = (
            "https://sws.serviceobjects.com/GCC/soap.svc?wsdl"
            if is_live
            else "https://trial.serviceobjects.com/GCC/soap.svc?wsdl"
        )
        self._backup_wsdl = (
            "https://swsbackup.serviceobjects.com/GCC/soap.svc?wsdl"
            if is_live
            else "https://trial.serviceobjects.com/GCC/soap.svc?wsdl"
        )

    def get_geo_location(
        self, address: str, municipality: str, province: str, postal_code: str
    ) -> Object:
        """
        Calls the GetGeoLocation SOAP  API to retrieve the information.

        Parameters:
            address: Address line of the address to geocode (e.g., "123 Main Street").
            municipality: The municipality of the address to geocode (e.g., "Cayuga").
            province: The province of the address to geocode (e.g., "ON").
            postal_code: The postal code of the address to geocode (e.g., "N0A 1E0").
            license_key: Your ServiceObjects license key.
            is_live: Determines whether to use the live or trial servers.
            timeout_ms: Timeout, in milliseconds, for the call to the service.

        Returns:
            suds.sudsobject.Object: SOAP response containing geocoding details or error.
        """
        # Common kwargs for both calls
        call_kwargs = dict(
            Address=address,
            Municipality=municipality,
            Province=province,
            PostalCode=postal_code,
            LicenseKey=self.license_key,
        )

        # Attempt primary
        try:
            client = Client(self._primary_wsdl)
            # Override endpoint URL if needed:
            # client.set_options(location=self._primary_wsdl.replace('?wsdl','/soap'))
            response = client.service.GetGeoLocation(**call_kwargs)

            # If response invalid or Error.Number == "4", trigger fallback
            if response is None or (
                hasattr(response, "Error")
                and response.Error
                and response.Error.Number == "4"
            ):
                raise ValueError("Primary returned no result or Error.Number=4")

            return response

        except (WebFault, ValueError, Exception) as primary_ex:
            try:
                client = Client(self._backup_wsdl)
                response = client.service.GetGeoLocation(**call_kwargs)
                if response is None:
                    raise ValueError("Backup returned no result")
                return response
            except (WebFault, Exception) as backup_ex:
                msg = (
                    "Both primary and backup endpoints failed.\n"
                    f"Primary error: {str(primary_ex)}\n"
                    f"Backup error: {str(backup_ex)}"
                )
                raise RuntimeError(msg)