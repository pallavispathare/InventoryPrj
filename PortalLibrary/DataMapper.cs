using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalLibrary
{
    public class DataMapper
    {
        public class GenericMapper<T, V>
        {
            private T t;
            private V v;

            public T MapData(V value)
            {
                Mapper.CreateMap<V, T>();
                return Mapper.Map<V, T>(value);
            }

            public List<T> MapDataList(List<V> value)
            {
                List<T> response = new List<T>();
                Mapper.CreateMap<V, T>();
                response = Mapper.Map<List<V>, List<T>>(value);

                return response;
            }
        }
    }
}
