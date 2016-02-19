using System;

namespace GeoToMap
{
    

    public interface IGeocodificador
    {
        String Deno { get; }
        ImplementacionGeocodificador Implementacion { get; }
        DireccionPunto GetCoordenadas(string direccion);
        event EventHandler<EventArgsGeocodificador> MensajeEvent;
    }
}
