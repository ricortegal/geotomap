using System.Runtime.Serialization;

namespace GeoToMap
{
    [DataContract]
    public class Covariable
    {
        [DataMember]
        public string Nombre { set; get; }
        [DataMember]
        public TiposDatos Tipo { set; get; }
    }
}
