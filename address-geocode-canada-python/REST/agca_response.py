from dataclasses import dataclass
from typing import Optional

@dataclass
class GetGeoLocationInput:
    Address: Optional[str] = None
    Municipality: Optional[str] = None
    Province: Optional[str] = None
    PostalCode: Optional[str] = None
    LicenseKey: Optional[str] = None
    IsLive: bool = True
    TimeoutSeconds: int = 15

    def __str__(self) -> str:
        return (f"GetGeoLocationInput: Address={self.Address}, Municipality={self.Municipality}, "
                f"Province={self.Province}, PostalCode={self.PostalCode}, LicenseKey={self.LicenseKey}, "
                f"IsLive={self.IsLive}, TimeoutSeconds={self.TimeoutSeconds}")

@dataclass
class GetPostalCodeInfoInput:
    PostalCode: Optional[str] = None
    LicenseKey: Optional[str] = None
    IsLive: bool = True
    TimeoutSeconds: int = 15

    def __str__(self) -> str:
        return (f"GetPostalCodeInfoInput: PostalCode={self.PostalCode}, LicenseKey={self.LicenseKey}, "
                f"IsLive={self.IsLive}, TimeoutSeconds={self.TimeoutSeconds}")

@dataclass
class GetGeoLocationByMunicipalityProvinceInput:
    Municipality: Optional[str] = None
    Province: Optional[str] = None
    LicenseKey: Optional[str] = None
    IsLive: bool = True
    TimeoutSeconds: int = 15

    def __str__(self) -> str:
        return (f"GetGeoLocationByMunicipalityProvinceInput: Municipality={self.Municipality}, "
                f"Province={self.Province}, LicenseKey={self.LicenseKey}, "
                f"IsLive={self.IsLive}, TimeoutSeconds={self.TimeoutSeconds}")

@dataclass
class Error:
    Desc: Optional[str] = None
    Number: Optional[str] = None
    Location: Optional[str] = None

    def __str__(self) -> str:
        return f"Error: Desc={self.Desc}, Number={self.Number}, Location={self.Location}"

@dataclass
class GetGeoLocationResponse:
    Latitude: Optional[str] = None
    Longitude: Optional[str] = None
    PostalCode: Optional[str] = None
    MatchCode: Optional[str] = None
    Error: Optional['Error'] = None

    def __str__(self) -> str:
        error = str(self.Error) if self.Error else 'None'
        return (f"GetGeoLocationResponse: Latitude={self.Latitude}, Longitude={self.Longitude}, "
                f"PostalCode={self.PostalCode}, MatchCode={self.MatchCode}, Error={error}")

@dataclass
class GetPostalCodeInfoResponse:
    Latitude: Optional[str] = None
    Longitude: Optional[str] = None
    PostalCode: Optional[str] = None
    TimeZone: Optional[str] = None
    DST: Optional[str] = None
    AreaCode: Optional[str] = None
    City: Optional[str] = None
    CityPopulation: Optional[str] = None
    Province: Optional[str] = None
    Error: Optional['Error'] = None

    def __str__(self) -> str:
        error = str(self.Error) if self.Error else 'None'
        return (f"GetPostalCodeInfoResponse: Latitude={self.Latitude}, Longitude={self.Longitude}, "
                f"PostalCode={self.PostalCode}, TimeZone={self.TimeZone}, DST={self.DST}, "
                f"AreaCode={self.AreaCode}, City={self.City}, CityPopulation={self.CityPopulation}, "
                f"Province={self.Province}, Error={error}")

@dataclass
class GetGeoLocationByMunicipalityProvinceResponse:
    Latitude: Optional[str] = None
    Longitude: Optional[str] = None
    PostalCode: Optional[str] = None
    Error: Optional['Error'] = None

    def __str__(self) -> str:
        error = str(self.Error) if self.Error else 'None'
        return (f"GetGeoLocationByMunicipalityProvinceResponse: Latitude={self.Latitude}, "
                f"Longitude={self.Longitude}, PostalCode={self.PostalCode}, Error={error}")
