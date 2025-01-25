using MGSC;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace QM_AutoLoadGame
{
    public class ModConfig
    {
        public static string ConfigPath { get; set; }

        private static JsonSerializerSettings SerializerSettings { get; } = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented,
        };

        /// <summary>
        /// Disables the mod.  So the user doesn't have to unsubscribe from the mod
        /// to disable.
        /// </summary>
        public bool Disable { get; set; } = false;
        public int LastLoadedSlot { get; set; } = -1;

        public int CountDownSeconds { get; set; } = 3;

        public static ModConfig LoadConfig()
        {
            ModConfig config;

            if (File.Exists(ConfigPath))
            {
                try
                {
                    config = JsonConvert.DeserializeObject<ModConfig>(File.ReadAllText(ConfigPath), SerializerSettings);
                    return config;
                }
                catch (Exception ex)
                {
                    Debug.LogError("Error parsing configuration.  Ignoring config file and using defaults");
                    Debug.LogException(ex);

                    //Not overwriting in case the user just made a typo.
                    config = new ModConfig();
                    return config;
                }
            }
            else
            {
                config = new ModConfig();

                string json = JsonConvert.SerializeObject(config, SerializerSettings);
                File.WriteAllText(ConfigPath, json);

                return config;
            }
        }

        public void SaveConfig()
        {
            string json = JsonConvert.SerializeObject(this, SerializerSettings);
            File.WriteAllText(ConfigPath, json);
        }
    }
}
