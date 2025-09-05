import sys
import os

sys.path.insert(0, os.path.abspath("../address-geocode-canada-python/REST"))

from get_geo_location_by_municipality_province_rest import get_geo_location_by_municipality_province


def get_geo_location_by_municipality_province_rest_sdk_go(is_live: bool, license_key: str) -> None:
    print("\r\n--------------------------------------------------------------------------")
    print("Address Geocode - Canada - GetGeoLocationByMunicipalityProvince - REST SDK")
    print("--------------------------------------------------------------------------")

    municipality = "Ottawa"
    province = "ON"
    timeout_seconds = 15

    print("\r\n* Input *\r\n")
    print(f"Municipality: {municipality}")
    print(f"Province    : {province}")
    print(f"License Key : {license_key}")
    print(f"Is Live     : {is_live}")

    try:
        response = get_geo_location_by_municipality_province(municipality, province, license_key, is_live)

        print("\r\n* Geocode Info *\r\n")
        if response and not response.Error:
            print(f"Latitude  : {response.Latitude}")
            print(f"Longitude : {response.Longitude}")
            print(f"PostalCode: {response.PostalCode}")
        else:
            print("No geocode info found.")

        if hasattr(response, "Error") and response.Error:
            print("\r\n* Error *\r\n")
            print(f"Error Desc    : {response.Error.Desc}")
            print(f"Error Number  : {response.Error.Number}")
            print(f"Error Location: {response.Error.Location}")

    except Exception as e:
        print("\r\n* Error *\r\n")
        print(f"Error Message : {str(e)}")
