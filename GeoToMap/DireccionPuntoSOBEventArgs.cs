using System;
using System.Collections.Generic;

namespace GeoToMap
{
    public class DireccionPuntoSOBEventArgs : EventArgs
    {
     

        public enum TypeChange { UPDATE, NEW, DELETE }
        public TypeChange Type { get; private set; }
        public Dictionary<string, DireccionPunto> PuntosChangeNew { get; private set; }
        public Dictionary<string, DireccionPunto> PuntosChangeOldValueBeforeUpdate { get; private set; }

        public DireccionPuntoSOBEventArgs(TypeChange t, Dictionary<string, DireccionPunto> d)
        {
            Type = t;
            PuntosChangeNew = d;
            PuntosChangeOldValueBeforeUpdate = null;
        }

        public DireccionPuntoSOBEventArgs(TypeChange t, Dictionary<string, DireccionPunto> d, Dictionary<string,DireccionPunto> dOld)
        {
            Type = t;
            PuntosChangeNew = d;
            PuntosChangeOldValueBeforeUpdate = dOld;
        }
    }
}
