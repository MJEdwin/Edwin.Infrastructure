using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Edwin.Infrastructure.Serializer
{
    public class XmlSerializer<T> : ISerializer<T, string>, ISerializer<T, Stream>
    {
        private readonly XmlSerializer xmlSer = new XmlSerializer(typeof(T));

        public XmlSerializer()
        {

        }


        public T Deserialize(string destination)
        {
            using (var sr = new StringReader(destination))
            {
                return (T)xmlSer.Deserialize(sr);
            }
        }

        public T Deserialize(Stream destination)
        {
            destination.Seek(0, SeekOrigin.Begin);
            return (T)xmlSer.Deserialize(destination);
        }

        public string Serialize(T sourse)
        {
            using (Stream stream = (this as ISerializer<T, Stream>).Serialize(sourse))
            {
                using (var sr = new StreamReader(stream))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        Stream ISerializer<T, Stream>.Serialize(T sourse)
        {
            var stream = new MemoryStream();
            //序列化对象  
            xmlSer.Serialize(stream, sourse);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
    }
}
