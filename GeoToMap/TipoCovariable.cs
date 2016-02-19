using System;
using System.Runtime.Serialization;

namespace GeoToMap
{
    public enum TiposDatos
    {
        CADENA,
        ENTERO,
        DECIMAL,
        FECHA
    };

    [DataContract]
    public class TipoCovariable
    {
        [DataMember]
        public string Deno { set; get; }
        [DataMember]
        public TiposDatos Tipo { set; get; }
        public override string ToString()
        {
            return Deno;
        }


    }
}
