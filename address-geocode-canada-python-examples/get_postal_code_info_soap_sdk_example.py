import sys
import os

sys.path.insert(0, os.path.abspath("../address-geocode-canada-python/SOAP"))

from get_postal_code_info_soap import GetPostalCodeInfoSoap


def get_postal_code_info_soap_sdk_go(is_live: bool, license_key: str) -> None:
    print("\r\n-------------------------------------------------------")
    print("Address Geocode - Canada - GetPostalCodeInfo - SOAP SDK")
    print("-------------------------------------------------------")

    postal_code = "K1Y 4E9"
    timeout_seconds = 15

    print("\r\n* Input *\r\n")
    print(f"PostalCode     : {postal_code}")
    print(f"License Key    : {license_key}")
    print(f"Is Live        : {is_live}")
    print(f"Timeout Seconds: {timeout_seconds}")

    try:
        service = GetPostalCodeInfoSoap(license_key, is_live, timeout_seconds)
        response = service.get_postal_code_info(postal_code)

        if not hasattr(response, 'Error') or not response.Error:
            print("\r\n* Geocode Info *\r\n")
            print(f"Latitude      : {response.Latitude}")
            print(f"Longitude     : {response.Longitude}")
            print(f"PostalCode    : {response.PostalCode}")
            print(f"TimeZone      : {response.TimeZone}")
            print(f"DST           : {response.DST}")
            print(f"AreaCode      : {response.AreaCode}")
            print(f"City          : {response.City}")
            print(f"CityPopulation: {response.CityPopulation}")
            print(f"Province      : {response.Province}")
        else:
            print("No geocode info found.")

        if hasattr(response, 'Error') and response.Error:
            print("\r\n* Error *\r\n")
            print(f"Error Desc    : {response.Error.Desc}")
            print(f"Error Number  : {response.Error.Number}")
            print(f"Error Location: {response.Error.Location}")

    except Exception as e:
        print("\r\n* Error *\r\n")
        print(f"Exception occurred: {str(e)}")
