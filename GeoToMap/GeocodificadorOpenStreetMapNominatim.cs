using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using System.Xml.Linq;
using GeoToMap;

namespace GeoToMap
{
    class GeocodificadorOpenStreetMapNominatim : GeocodificadorBase
    {

        private const string sUrl1 = " http://nominatim.openstreetmap.org/search?q={0}&format=xml&polygon=0&addressdetails=1";

        public override string Deno
        {
            get { return "Open Street Nominatim"; } 
        }

        public override ImplementacionGeocodificador Implementacion
        {
            get { return ImplementacionGeocodificador.OPEN_STREET_NOMINATIM; }
        }

        public override DireccionPunto GetCoordenadas(string direccion)
        {
            try
            {
                DireccionPunto dp = new DireccionPunto();
                WebRequest request = WebRequest.Create(string.Format(sUrl1, HttpUtility.UrlEncode(direccion)));
                request.Method = "GET";
                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                if (stream != null)
                {
                    StreamReader sr = new StreamReader(stream);
                    XDocument xdoc = XDocument.Load(sr);

                    List<XElement> Place = (from nReg in xdoc.Descendants()
                                            where nReg.Name == "place"
                                            select nReg).ToList();

                    string sLat =
                        (from nReg in Place.Attributes() where nReg.Name == "lat" select nReg.Value).FirstOrDefault();
                    string sLong =
                        (from nReg in Place.Attributes() where nReg.Name == "lon" select nReg.Value).FirstOrDefault();
                    double lat = 0.0;
                    double lon = 0.0;

                    if (!string.IsNullOrEmpty(sLat))
                        lat = double.Parse(sLat, CultureInfo.InvariantCulture);

                    if (!string.IsNullOrEmpty(sLong))
                        lon = double.Parse(sLong, CultureInfo.InvariantCulture);

                    string pais = "";
                    string localidad =
                        Place.Descendants().Where(obj => obj.Name == "city").Select(obj => obj.Value).FirstOrDefault();
                    string provincia =
                        Place.Descendants().Where(obj => obj.Name == "county").Select(obj => obj.Value).FirstOrDefault();
                    string region =
                        Place.Descendants().Where(obj => obj.Name == "state").Select(obj => obj.Value).FirstOrDefault();
                    var paisNodo =
                        Place.Descendants().Where(obj => obj.Name == "country_code").Select(obj => obj.Value).
                            FirstOrDefault();
                    if (
                        paisNodo != null)
                    {
                        pais = paisNodo.ToUpperInvariant();
                    }
                    dp.Latitud = lat;
                    dp.Longitud = lon;
                    dp.Municipio = localidad;
                    dp.NivelRegional2 = provincia;
                    dp.NivelRegional1 = region;
                    dp.Pais = pais;
                    Console.WriteLine(Place.FirstOrDefault());
                    dp.IsGeocodificado = true;
                    return dp;
                }
                return null;
            }
            catch(Exception ex)
            {
                RaiseMensajeEvent(ex.Message);
                return null;
            }
        }
    }
}
