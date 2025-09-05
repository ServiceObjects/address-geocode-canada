import sys
import os

sys.path.insert(0, os.path.abspath("../address-geocode-canada-python/REST"))

from get_geo_location_rest import get_geo_location


def get_geo_location_rest_sdk_go(is_live: bool, license_key: str) -> None:
    print("\r\n----------------------------------------------------")
    print("Address Geocode - Canada - GetGeoLocation - REST SDK")
    print("----------------------------------------------------")

    address = "1053 Carling Ave"
    municipality = "Ottawa"
    province = "ON"
    postal_code = "K1Z 7K4"
    timeout_seconds = 15

    print("\r\n* Input *\r\n")
    print(f"Address     : {address}")
    print(f"Municipality: {municipality}")
    print(f"Province    : {province}")
    print(f"PostalCode  : {postal_code}")
    print(f"License Key : {license_key}")
    print(f"Is Live     : {is_live}")

    try:
        response = get_geo_location(address, municipality, province, postal_code, license_key, is_live)

        print("\r\n* Geocode Info *\r\n")
        if response and not response.Error:
            print(f"Latitude  : {response.Latitude}")
            print(f"Longitude : {response.Longitude}")
            print(f"PostalCode: {response.PostalCode}")
            print(f"MatchCode : {response.MatchCode}")
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
