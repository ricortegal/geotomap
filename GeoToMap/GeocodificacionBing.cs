using System;
using GeoToMap.GeocodeService;

namespace GeoToMap
{
    class GeocodificadorBingMaps : GeocodificadorBase
    {
        private GeocodeRequest geocodeRequest;

    public GeocodificadorBingMaps()
    {

        string key = GeoToMap.Properties.Settings.Default.BingMapsKey;

        geocodeRequest = new GeocodeRequest();

        // Asignamos los credenciales en base a la clave de Bing Maps
        geocodeRequest.Credentials = new GeocodeService.Credentials();
        geocodeRequest.Credentials.ApplicationId = key;

    }


        public override string Deno
        {
            get { return "BING MAPS"; }
        }

        public override ImplementacionGeocodificador Implementacion
        {
            get { return ImplementacionGeocodificador.BING_MAPS; }
        }


     public override DireccionPunto GetCoordenadas(string direccion)
     {
        try
        {
            double lat, lon;

            // Asignamos las opciones
            ConfidenceFilter[] filters = new ConfidenceFilter[1];
            filters[0] = new ConfidenceFilter();
            filters[0].MinimumConfidence = GeocodeService.Confidence.High;

            // Añadimos los filtros
            GeocodeOptions geocodeOptions = new GeocodeOptions();
            geocodeOptions.Filters = filters;
            geocodeRequest.Options = geocodeOptions;

            geocodeRequest.Query = direccion;

            // Realizamos la petición
            GeocodeServiceClient geocodeService = new GeocodeServiceClient("BasicHttpBinding_IGeocodeService");
            GeocodeResponse geocodeResponse = geocodeService.Geocode(geocodeRequest);
            DireccionPunto dp = new DireccionPunto();

            if (geocodeResponse.Results.Length > 0)
            {
                lat = geocodeResponse.Results[0].Locations[0].Latitude;
                lon = geocodeResponse.Results[0].Locations[0].Longitude;
                dp.Direccion = direccion;
                dp.Latitud = lat;
                dp.Longitud = lon;
                dp.Pais = geocodeResponse.Results[0].Address.CountryRegion;
                dp.NivelRegional2 = geocodeResponse.Results[0].Address.District;
                dp.NivelRegional1 = geocodeResponse.Results[0].Address.AdminDistrict;
                dp.Municipio = geocodeResponse.Results[0].Address.Locality;
            }
            else
            {
                dp.Latitud = double.NaN;
                dp.Longitud = double.NaN;
            }
            utm30Ntransformacion(dp);
            dp.IsGeocodificado = true;
            return dp;
        }
        catch(Exception ex)
        {
            RaiseMensajeEvent("Bing MAPS - " + ex.Message);
            return null;
        }
    }

    public delegate EventHandler<EventArgsGeocodificador> Mensaje(object sender);
   
    }
}
