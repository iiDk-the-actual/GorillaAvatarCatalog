using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GorillaAvatarCatalog.Behaviours
{
    internal class Preferences : Singleton<Preferences>
    {
        private readonly string PreferencePath = Path.Combine(BepInEx.Paths.ConfigPath, $"Avatars ({Constants.Name}).json");

        private readonly Dictionary<string, object> session_data = [];
        private Dictionary<string, object> stored_data = [];

        public override void Initialize()
        {
            if (File.Exists(PreferencePath))
                stored_data = JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText(PreferencePath));
            else
                File.WriteAllText(PreferencePath, JsonConvert.SerializeObject(stored_data, Formatting.Indented));
        }

        public T GetValue<T>(string key, T defaultValue, EPreferenceLocation destination = EPreferenceLocation.Stored)
        {
            Dictionary<string, object> dictionary = destination == EPreferenceLocation.Session ? session_data : stored_data;
            if (dictionary.TryGetValue(key, out object obj))
            {
                if (obj is JObject jObject)
                {
                    T value = jObject.ToObject<T>();
                    SetKey(key, value, destination);
                    return value;
                }
                else if (obj is T value)
                    return value;
            }
            return defaultValue;
        }

        public bool HasKey(string key, EPreferenceLocation dataType = EPreferenceLocation.Stored)
        {
            Dictionary<string, object> dictionary = dataType == EPreferenceLocation.Session ? session_data : stored_data;
            return dictionary.ContainsKey(key);
        }

        public void SetKey(string key, object value, EPreferenceLocation destination = EPreferenceLocation.Stored)
        {
            Dictionary<string, object> dictionary = destination == EPreferenceLocation.Session ? session_data : stored_data;

            if (dictionary.ContainsKey(key)) dictionary[key] = value;
            else dictionary.Add(key, value);

            if (destination == EPreferenceLocation.Stored)
                File.WriteAllText(PreferencePath, JsonConvert.SerializeObject(stored_data, Formatting.Indented));
        }

        public void DeleteKey(string key, EPreferenceLocation destination = EPreferenceLocation.Stored)
        {
            Dictionary<string, object> dictionary = destination == EPreferenceLocation.Session ? session_data : stored_data;

            if (dictionary.ContainsKey(key))
            {
                dictionary.Remove(key);

                if (destination == EPreferenceLocation.Stored)
                    File.WriteAllText(PreferencePath, JsonConvert.SerializeObject(stored_data, Formatting.Indented));
            }
        }

        public enum EPreferenceLocation
        {
            Session,
            Stored
        }
    }
}
