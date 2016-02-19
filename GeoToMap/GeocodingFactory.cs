using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoToMap
{
    public static class GeocodingFactory
    {
        public static IGeocodificador GetGeodificadorInstancia(ImplementacionGeocodificador implementacionGeocodificador)
        {
            switch (implementacionGeocodificador)
            {
                case ImplementacionGeocodificador.BING_MAPS:
                    return new GeocodificadorBingMaps();
                case ImplementacionGeocodificador.GOOGLE_MAPS:
                    return new GeocodificadorGoogleMaps();
                case ImplementacionGeocodificador.OPEN_STREET_NOMINATIM:
                    return new GeocodificadorOpenStreetMapNominatim();
                default:
                    return null;
            }
        }
    }
}
