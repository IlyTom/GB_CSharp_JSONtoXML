using System.Text.Json;
using System.Xml.Linq;

namespace GB_CSharp_JSONtoXML
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string json = "{\"name\":\"John\",\"age\":30,\"city\":\"New York\",\"hobbies\":[\"traveling\",\"photography\",\"reading\"],\"education\":{\"degree\":\"Master's\",\"field\":\"Computer Science\",\"university\":\"MIT\"}}";
            
            JsonDocument document = JsonDocument.Parse(json);

            XElement xml = ConvertJsonToXml(document.RootElement, "root");

            Console.WriteLine(xml);
        }

        static XElement ConvertJsonToXml (JsonElement elem, string name)
        {
            switch (elem.ValueKind)
            {
                case JsonValueKind.Object:
                    return new XElement(name, elem.EnumerateObject().Select(obj => ConvertJsonToXml(obj.Value, obj.Name)));
                case JsonValueKind.Array:
                    return new XElement(name, elem.EnumerateArray().Select((arr,i) => ConvertJsonToXml(arr,$"{name}{i+1}")));
                default:
                    return new XElement(name, elem.ToString());
            }                
        }
    }
}

