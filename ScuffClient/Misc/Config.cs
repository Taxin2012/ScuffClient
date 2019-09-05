using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Reflection;

namespace ScuffClient.Misc
{
    public class Config
    {
        public float[] nuisanceNameColor;
        public float[] visitorNameColor;
        public float[] new_UserNameColor;
        public float[] userNameColor;
        public float[] known_UserNameColor;
        public float[] trusted_UserNameColor;
        public float[] friendNameColor;
        public float[] blocked_UserNameColor;
        public string CustomSteamId;
        public float fieldOfView;
        public bool display_Client_UserColor;
        public KeyCode desktopMenuKey;
        public const string ConfigFilePath = "ScuffClient\\Config.json";

        //The default config that will be created with the json file
        public static Config Default = new Config()
        {
            nuisanceNameColor = new float[] { 0.8f, 0.8f, 0.8f },
            visitorNameColor = new float[] { 0.8f, 0.8f, 0.8f },
            new_UserNameColor = new float[] { 0.09019608f, 0.470588237f, 1f },
            userNameColor = new float[] { 0.168627456f, 0.8117647f, 0.360784322f },
            known_UserNameColor = new float[] { 1f, 0.482352942f, 0.258823544f },
            trusted_UserNameColor = new float[] { 0.5058824f, 0.2627451f, 0.9019608f },
            blocked_UserNameColor = new float[] { 1f, 0f, 0f },
            friendNameColor = new float[] { 1f, 1f, 0f },
            fieldOfView = 60,
            display_Client_UserColor = true,
            desktopMenuKey = KeyCode.M,
            CustomSteamId = "0"
        };

        public static Config DeserializeConfig()
        {
            try
            {
                Config cfg = new Config();
                using (StreamReader reader = new StreamReader(Config.ConfigFilePath))
                {
                    var json = reader.ReadToEnd();
                    cfg = JsonConvert.DeserializeObject<Config>(json);
                    reader.Close();
                }
                return cfg;
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Directory for Config.json not found");
                return null;
            }
        }

        public static void CreateConfig()
        {
            try
            {
                if (!File.Exists(ConfigFilePath))
                {
                    if (!Directory.Exists("ScuffClient"))
                        Directory.CreateDirectory("ScuffClient");

                    File.Create(Config.ConfigFilePath).Close();
                    var serializeConfig = JsonConvert.SerializeObject(Default, Formatting.Indented);
                    File.WriteAllText(ConfigFilePath, serializeConfig);
                }
            }
            catch (JsonSerializationException) { Console.WriteLine("Error encountered while serializing config"); }
        }
    }
    public class ConfigHandler : MonoBehaviour
    {
        private static Config Cfg = Config.DeserializeConfig();
        private static float UpdateInterval = 2.0f;
        public void Start()
        {
            try
            {
                VRCPlayer.untrustedNamePlateColor = FloatArrayToColor(Cfg.visitorNameColor);
                VRCPlayer.basicNamePlateColor = FloatArrayToColor(Cfg.new_UserNameColor);
                VRCPlayer.knownNamePlateColor = FloatArrayToColor(Cfg.userNameColor);
                VRCPlayer.trustedNamePlateColor = FloatArrayToColor(Cfg.known_UserNameColor);
                VRCPlayer.veteranNamePlateColor = FloatArrayToColor(Cfg.trusted_UserNameColor);
                VRCPlayer.friendNamePlateColor = FloatArrayToColor(Cfg.friendNameColor);
                Variables.blockedColor = FloatArrayToColor(Cfg.blocked_UserNameColor);
                Variables.steamId = Cfg.CustomSteamId;
            }
            catch (NullReferenceException) { Console.WriteLine("[ScuffClient]: One or more values in config were null"); }
            catch(Exception e) { Console.WriteLine(e); }
        }
        public void Update()
        {
            if (VRCPlayer.Instance != null && Camera.main.fieldOfView != Cfg.fieldOfView)
                Camera.main.fieldOfView = Cfg.fieldOfView;

            UpdateInterval -= Time.deltaTime;

            if(VRCPlayer.Instance != null)
                UpdateConfig(UpdateInterval);
        }
        private static Color FloatArrayToColor(float[] values)
        {
            return new Color(values[0], values[1], values[2]);
        }
        private static void UpdateConfig(float time)
        {
            if (time < 0)
            {
                UpdateInterval = 1.0f;
                Cfg = Config.DeserializeConfig();
                VRCPlayer.untrustedNamePlateColor = FloatArrayToColor(Cfg.visitorNameColor);
                VRCPlayer.basicNamePlateColor = FloatArrayToColor(Cfg.new_UserNameColor);
                VRCPlayer.knownNamePlateColor = FloatArrayToColor(Cfg.userNameColor);
                VRCPlayer.trustedNamePlateColor = FloatArrayToColor(Cfg.known_UserNameColor);
                VRCPlayer.veteranNamePlateColor = FloatArrayToColor(Cfg.trusted_UserNameColor);
                VRCPlayer.friendNamePlateColor = FloatArrayToColor(Cfg.friendNameColor);
                Variables.blockedColor = FloatArrayToColor(Cfg.blocked_UserNameColor);
                Variables.steamId = Cfg.CustomSteamId;
            }
        }
    }
}
