import sys
import os

sys.path.insert(0, os.path.abspath("../address-geocode-canada-python/SOAP"))

from get_geo_location_by_municipality_province_soap import GetGeoLocationByMunicipalityProvinceSoap


def get_geo_location_by_municipality_province_soap_sdk_go(is_live: bool, license_key: str) -> None:
    print("\r\n--------------------------------------------------------------------------")
    print("Address Geocode - Canada - GetGeoLocationByMunicipalityProvince - SOAP SDK")
    print("--------------------------------------------------------------------------")

    municipality = "Ottawa"
    province = "ON"
    timeout_seconds = 15

    print("\r\n* Input *\r\n")
    print(f"Municipality   : {municipality}")
    print(f"Province       : {province}")
    print(f"License Key    : {license_key}")
    print(f"Is Live        : {is_live}")
    print(f"Timeout Seconds: {timeout_seconds}")

    try:
        service = GetGeoLocationByMunicipalityProvinceSoap(license_key, is_live, timeout_seconds)
        response = service.get_geo_location_by_municipality_province(municipality, province)

        if not hasattr(response, "Error") or not response.Error:
            print("\r\n* Geocode Info *\r\n")
            print(f"Latitude  : {response.Latitude}")
            print(f"Longitude : {response.Longitude}")
            print(f"PostalCode: {getattr(response, 'PostalCode', 'None')}")
        else:
            print("No geocode info found.")

        if hasattr(response, "Error") and response.Error:
            print("\r\n* Error *\r\n")
            print(f"Error Desc    : {response.Error.Desc}")
            print(f"Error Number  : {response.Error.Number}")
            print(f"Error Location: {response.Error.Location}")

    except Exception as e:
        print("\r\n* Error *\r\n")
        print(f"Exception occurred: {str(e)}")
