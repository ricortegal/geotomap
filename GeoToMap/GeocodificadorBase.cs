using System;
using SharpMap.CoordinateSystems;
using SharpMap.CoordinateSystems.Transformations;

namespace GeoToMap
{
    abstract class GeocodificadorBase : IGeocodificador
    {
        public abstract string Deno { get; }
        public abstract ImplementacionGeocodificador Implementacion { get;  }
        public abstract DireccionPunto GetCoordenadas(string direccion);
     

        public void utm30Ntransformacion(DireccionPunto dp)
        {
            double lat = dp.Latitud;
            double lon = dp.Longitud;

            if (!double.IsNaN(lat))
            {
                try
                {
                    //string wktTo = "PROJCS[\"ED50 / UTM zone 30N\",GEOGCS[\"ED50\",DATUM[\"D_European_1950\",SPHEROID[\"International_1924\",6378388,297]],PRIMEM[\"Greenwich\",0],UNIT[\"Degree\",0.017453292519943295]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"latitude_of_origin\",0],PARAMETER[\"central_meridian\",-3],PARAMETER[\"scale_factor\",0.9996],PARAMETER[\"false_easting\",500000],PARAMETER[\"false_northing\",0],UNIT[\"Meter\",1]]";
                    //string wktTo = "PROJCS[\"WGS_1984_UTM_Zone_30N\",GEOGCS[\"GCS_WGS_1984\",DATUM[\"D_WGS_1984\",SPHEROID[\"WGS_1984\",6378137,298.257223563]],PRIMEM[\"Greenwich\",0],UNIT[\"Degree\",0.017453292519943295]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"latitude_of_origin\",0],PARAMETER[\"central_meridian\",-3],PARAMETER[\"scale_factor\",0.9996],PARAMETER[\"false_easting\",500000],PARAMETER[\"false_northing\",0],UNIT[\"Meter\",1]]";

                    string wktTo = GeoToMap.Properties.Settings.Default.Proyeccion;

                    ICoordinateSystem sistema1 = GeographicCoordinateSystem.WGS84;

                    CoordinateTransformationFactory factoriaTransformaciones = new CoordinateTransformationFactory();

                    ICoordinateSystem sistema2 =
                        (ICoordinateSystem) SharpMap.Converters.WellKnownText.CoordinateSystemWktReader.Parse(wktTo);

                    ICoordinateTransformation transformacion =
                        factoriaTransformaciones.CreateFromCoordinateSystems(sistema1, sistema2);

                    double[] coords = transformacion.MathTransform.Transform(new double[] {lon, lat});

                    //Proyeccion

                    dp.X = coords[0];
                    dp.Y = coords[1];
                }
                catch(Exception ex)
                {
                    RaiseMensajeEvent(String.Format("ERROR EN LA TRANSFORMACIÓN DE COORDENADAS: {0}",ex.Message));
                }
            }

        }


        protected void RaiseMensajeEvent(string mensaje)
        {
            if (MensajeEvent != null)
            {
                MensajeEvent(this, new EventArgsGeocodificador() { Mensaje = mensaje });
            }
        }



        public event EventHandler<EventArgsGeocodificador> MensajeEvent;
    }
}
