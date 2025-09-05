from get_geo_location_by_municipality_province_rest_sdk_example import get_geo_location_by_municipality_province_rest_sdk_go
from get_geo_location_by_municipality_province_soap_sdk_example import get_geo_location_by_municipality_province_soap_sdk_go
from get_geo_location_rest_sdk_example import get_geo_location_rest_sdk_go
from get_geo_location_soap_sdk_example import get_geo_location_soap_sdk_go
from get_postal_code_info_rest_sdk_example import get_postal_code_info_rest_sdk_go
from get_postal_code_info_soap_sdk_example import get_postal_code_info_soap_sdk_go

if __name__ == "__main__":
   # Your license key from Service Objects.  
    # Trial license keys will only work on the trial environments and production  
    # license keys will only work on production environments.
    #   
    license_key = "LICENSE KEY"  
    is_live = True

    # Address Geocode – Canada - GetGeoLocationByMunicipalityProvince - REST SDK"
    get_geo_location_rest_sdk_go(is_live, license_key)

    # Address Geocode – Canada - GetGeoLocationByMunicipalityProvince - SOAP SDK"
    get_geo_location_soap_sdk_go(is_live, license_key)

    # Address Geocode – Canada - GetGeoLocationByMunicipalityProvince - REST SDK"
    get_geo_location_by_municipality_province_rest_sdk_go(is_live, license_key)

    # Address Geocode – Canada - GetGeoLocationByMunicipalityProvince - SOAP SDK"
    get_geo_location_by_municipality_province_soap_sdk_go(is_live, license_key)

    # Address Geocode – Canada - GetPostalCodeInfo - REST SDK"
    get_postal_code_info_rest_sdk_go(is_live, license_key)

    # Address Geocode – Canada - GetPostalCodeInfo - SOAP SDK"
    get_postal_code_info_soap_sdk_go(is_live, license_key)