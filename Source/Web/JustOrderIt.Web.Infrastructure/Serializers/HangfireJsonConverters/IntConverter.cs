namespace JustOrderIt.Web.Infrastructure.Serializers.HangfireJsonConverters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class IntConverter : JsonConverter
    {
        public override bool CanWrite
        {
            get
            {
                return /*base.CanWrite*/false;
            }
        }

        public override bool CanConvert(Type objectType)
        {
            // return objectType == typeof(int);
            return typeof(int).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // JValue jsonValue = serializer.Deserialize<JValue>(reader);

            // if (jsonValue.Type == JTokenType.Float)
            // {
            //    return (int)Math.Round(jsonValue.Value<double>());
            // }
            // else if (jsonValue.Type == JTokenType.Integer)
            // {
            //    return jsonValue.Value<int>();
            // }

            // again, very concrete
            Dictionary<string, object> result = new Dictionary<string, object>();
            reader.Read();

            while (reader.TokenType == JsonToken.PropertyName)
            {
                string propertyName = reader.Value as string;
                reader.Read();

                object value;
                if (reader.TokenType == JsonToken.Integer)
                {
                    value = Convert.ToInt32(reader.Value);      // convert to Int32 instead of Int64
                }
                else
                {
                    value = serializer.Deserialize(reader);     // let the serializer handle all other cases
                }

                result.Add(propertyName, value);
                reader.Read();
            }

            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();

            // serializer.Serialize(writer, value);

            // JToken t = JToken.FromObject(value);

            // if (t.Type != JTokenType.Object)
            // {
            //    t.WriteTo(writer);
            // }
            // else
            // {
            //    JObject o = (JObject)t;
            //    IList<string> propertyNames = o.Properties().Select(p => p.Name).ToList();
            //    o.AddFirst(new JProperty("Keys", new JArray(propertyNames)));
            //    o.WriteTo(writer);
            // }
        }
    }
}
