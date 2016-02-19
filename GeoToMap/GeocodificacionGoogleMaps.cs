using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Web;
using System.Xml.Linq;
using System.Threading;

namespace GeoToMap
{
    class GeocodificadorGoogleMaps : GeocodificadorBase
    {
        private const string sUrl1 = "http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false";

        public GeocodificadorGoogleMaps()
        {
            
        }

        public override string Deno
        {
            get { return "GOOGLE MAPS"; }
        }

        public override ImplementacionGeocodificador Implementacion
        {
            get { return ImplementacionGeocodificador.GOOGLE_MAPS; }
        }


        public override DireccionPunto GetCoordenadas(string direccion)
        {
            try
            {
                DireccionPunto dp = new DireccionPunto();
                string lat = "", lon = "";
                WebRequest request = WebRequest.Create(string.Format(sUrl1, HttpUtility.UrlEncode(direccion)));
                request.Method = "GET";
                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                if (stream != null)
                {
                    StreamReader sr = new StreamReader(stream);
                    XDocument xdoc = XDocument.Load(sr);

                    String status =
                        (from nReg in xdoc.Descendants() where nReg.Name == "status" select nReg.Value).FirstOrDefault();

                    if (status == "OVER_QUERY_LIMIT")
                    {
                        RaiseMensajeEvent("Over Query recibido desde Google Maps. Esperamos 2 segundos");
                        Thread.Sleep(2000);
                        return this.GetCoordenadas(direccion);
                    }

                    IEnumerable<XElement> AddressComponents = from nReg in xdoc.Descendants()
                                                              where nReg.Name == "address_component"
                                                              select nReg;

                    //Localidad de salida
                    IEnumerable<XElement> municipioNodos = (from nReg in xdoc.Descendants()
                                                            where nReg.Name == "type" && nReg.Value == "locality"
                                                            select
                                                                nReg.ElementsBeforeSelf().FirstOrDefault(
                                                                    obj => obj.Name == "long_name")).Where(
                                                                        obj => obj != null);

                    var nombreMunicipio = municipioNodos.FirstOrDefault();
                    if (nombreMunicipio != null) dp.Municipio = nombreMunicipio.Value;


                    //Provincia
                    IEnumerable<XElement> provinciaNodos = (from nReg in xdoc.Descendants()
                                                            where
                                                                nReg.Name == "type" &&
                                                                nReg.Value == "administrative_area_level_2"
                                                            select
                                                                nReg.ElementsBeforeSelf().FirstOrDefault(
                                                                    obj => obj.Name == "long_name")).Where(
                                                                        obj => obj != null);

                    var nombreProvincia = provinciaNodos.FirstOrDefault();
                    if (nombreProvincia != null) dp.NivelRegional2 = nombreProvincia.Value;


                    //Region
                    IEnumerable<XElement> regionNodos = (from nReg in xdoc.Descendants()
                                                         where
                                                             nReg.Name == "type" &&
                                                             nReg.Value == "administrative_area_level_1"
                                                         select
                                                             nReg.ElementsBeforeSelf().FirstOrDefault(
                                                                 obj => obj.Name == "short_name")).Where(
                                                                     obj => obj != null);

                    var nombreRegion = regionNodos.FirstOrDefault();
                    if (nombreRegion != null) dp.NivelRegional1 = nombreRegion.Value;


                    //Pais 
                    IEnumerable<XElement> paisNodos = (from nReg in xdoc.Descendants()
                                                       where nReg.Name == "type" && nReg.Value == "country"
                                                       select
                                                           nReg.ElementsBeforeSelf().FirstOrDefault(
                                                               obj => obj.Name == "short_name")).Where(
                                                                   obj => obj != null);

                    var nombrePais = paisNodos.FirstOrDefault();
                    if (nombrePais != null) dp.Pais = nombrePais.Value;


                    IEnumerable<XElement> xgeometry = from nReg in xdoc.Descendants()
                                                      where nReg.Name == "location"
                                                      select nReg;

                    XElement xLatLong = xgeometry.FirstOrDefault();
                    if (xLatLong != null)
                    {
                        var firstOrDefault = xLatLong.Descendants().FirstOrDefault(obj => obj.Name == "lat");
                        if (firstOrDefault != null)
                            lat = firstOrDefault.Value;
                        var orDefault = xLatLong.Descendants().FirstOrDefault(obj => obj.Name == "lng");
                        if (orDefault != null)
                            lon = orDefault.Value;
                    }
                    dp.Direccion = direccion;
                    if (!String.IsNullOrEmpty(lat))
                        dp.Latitud = Double.Parse(lat.Replace('.', ','));

                    if (!String.IsNullOrEmpty(lon))
                        dp.Longitud = Convert.ToDouble(lon.Replace('.', ','));

                    utm30Ntransformacion(dp);

                    dp.IsGeocodificado = true;

                    return dp;
                }
                return null;
            }
            catch (Exception ex)
            {
                RaiseMensajeEvent(ex.Message);
                return null;
            }
        }

      



    }
}
