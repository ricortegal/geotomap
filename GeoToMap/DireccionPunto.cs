namespace GeoToMap
{
    public class DireccionPunto
    {
        public string key { set; get; }
        public bool IsGeocodificado { set; get; }
        public string Direccion { set; get; }
        public string NivelRegional2 { set; get; }
        public string NivelRegional1 { set; get; }
        public string Pais { set; get; }
        public string Municipio { set; get; }
        public string DenoTopo { set; get; }
        public double Latitud { set; get; }
        public double Longitud { set; get; }
        public double X { set; get; }
        public double Y { set; get; }
        public string GeoreferenciadorDeno { set; get; }
    }
}
