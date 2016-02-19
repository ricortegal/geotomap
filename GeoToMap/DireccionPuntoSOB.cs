using System;
using System.Collections.Generic;
using System.Linq;
using GeoToMap;

namespace GeoToMap
{
    public class DireccionPuntoSOB : IDictionary<string,DireccionPunto>
    {
        private Dictionary<string, DireccionPunto> _d;
        private int numId = 0;


        public event EventHandler<DireccionPuntoSOBEventArgs> ChangeCollection;

        public Dictionary<string, DireccionPunto> DireccionesPunto { get; private set; }

        public DireccionPuntoSOB()
        {
            DireccionesPunto = _d = new Dictionary<string,DireccionPunto>();
        }

        public void Add(string key, DireccionPunto direccionPunto)
        {
            try
            {
                //Nos aseguramos la key única
                if(this.ContainsKey(key))
                    key = key + String.Format("_{0}", this.numId);
                direccionPunto.key = key; //Esto puede tener efectos secundarios
                numId++;
                _d.Add(key, direccionPunto);
                if (ChangeCollection != null)
                {
                    Dictionary<string, DireccionPunto> e = new Dictionary<string, DireccionPunto>();
                    e.Add(key, direccionPunto);
                    DireccionPuntoSOBEventArgs arg = new DireccionPuntoSOBEventArgs(DireccionPuntoSOBEventArgs.TypeChange.NEW, e);
                    ChangeCollection(this, arg);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void AddRange(Dictionary<string,DireccionPunto> direccionPunto)
        {
            if (ChangeCollection != null)
            {
                DireccionPuntoSOBEventArgs arg = new DireccionPuntoSOBEventArgs(DireccionPuntoSOBEventArgs.TypeChange.NEW, direccionPunto);
            }
            foreach (KeyValuePair<string,DireccionPunto> dp in direccionPunto)
            {
                _d.Add(dp.Key, dp.Value);
            }
        }

        public void Remove(string key)
        {
            if (_d.Keys.Contains(key))
            {
                if (ChangeCollection != null)
                {
                    DireccionPunto value = _d[key];
                    Dictionary<string, DireccionPunto> e = new Dictionary<string, DireccionPunto>();
                    e.Add(key, value);
                    DireccionPuntoSOBEventArgs arg = new DireccionPuntoSOBEventArgs(DireccionPuntoSOBEventArgs.TypeChange.DELETE, e);
                    ChangeCollection(this, arg);
                }

                _d.Remove(key);

            }
            else
            {
                throw new Exception(String.Format("The element you are trying to remove, with {0} key, doesn't exits", key));
            }
        }

        public void Update(string key, DireccionPunto updatePunto)
        {
            if (_d.Keys.Contains(key))
            {
                if (ChangeCollection != null)
                {
                    DireccionPunto value = _d[key];
                    Dictionary<string, DireccionPunto> e = new Dictionary<string, DireccionPunto>();
                    e.Add(key, value);
                    Dictionary<string, DireccionPunto> u = new Dictionary<string, DireccionPunto>();
                    u.Add(key, updatePunto);
                    DireccionPuntoSOBEventArgs arg = new DireccionPuntoSOBEventArgs(DireccionPuntoSOBEventArgs.TypeChange.UPDATE, u, e);
                    ChangeCollection(this, arg);
                }
                _d[key] = updatePunto;
            }
            else
            {
                throw new Exception(String.Format("The element you are trying to update, with {0} key, doesn't exits", key));
            }
        }




        public bool ContainsKey(string key)
        {
            return _d.ContainsKey(key);
        }

        public ICollection<string> Keys
        {
            get { return _d.Keys; }
        }

        bool IDictionary<string, DireccionPunto>.Remove(string key)
        {
            return _d.Remove(key);
        }

        public bool TryGetValue(string key, out DireccionPunto value)
        {
           return  _d.TryGetValue(key, out value);
        }

        public ICollection<DireccionPunto> Values
        {
            get { return _d.Values; }
        }

        public DireccionPunto this[string key]
        {
            get
            {
                return _d[key];
            }
            set
            {
                _d[key]=value;
            }
        }

        public void Add(KeyValuePair<string, DireccionPunto> item)
        {
            this.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            if (ChangeCollection != null)
            {
                DireccionPuntoSOBEventArgs arg = new DireccionPuntoSOBEventArgs(DireccionPuntoSOBEventArgs.TypeChange.DELETE, _d);
                ChangeCollection(this, arg);
            }
            _d.Clear();
        }

        public bool Contains(KeyValuePair<string, DireccionPunto> item)
        {
            return _d.Contains(item);
        }

        public void CopyTo(KeyValuePair<string, DireccionPunto>[] array, int arrayIndex)
        {
           foreach(KeyValuePair<string,DireccionPunto> v in array)
           {
             
           }
        }

        public int Count
        {
            get { return _d.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(KeyValuePair<string, DireccionPunto> item)
        {
           return _d.Remove(item.Key);
        }

        public IEnumerator<KeyValuePair<string, DireccionPunto>> GetEnumerator()
        {
            return _d.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _d.GetEnumerator();
        }
    }
}
