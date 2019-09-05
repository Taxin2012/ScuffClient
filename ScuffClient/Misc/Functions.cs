using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using VRC;
using UnityEngine;
using UnityEngine.UI;
using ScuffClient.Patches;
using ScuffClient.Menu;
using Random = System.Random;

namespace ScuffClient.Misc
{
    public class Functions
    {
        /// <summary>
        /// passing a player returns the players id, passing null returns local player id
        /// </summary>
        public static int GetPhotonId(Player p = null) => p? p.GetInstigatorId().GetValueOrDefault() : PlayerManager.GetPlayer(VRC.Core.APIUser.CurrentUser.id).GetInstigatorId().GetValueOrDefault();

        //generate new hwid to avoid hwid bans
        public static string NewDeviceID()
        {
            return KeyedHashAlgorithm.Create().ComputeHash(Encoding.UTF8.GetBytes(string.Format("{0}D-{1}{2}-{3}{4}-{5}{6}-72-5A", new object[]
            {
                new Random().Next(0, 9),
                new Random().Next(0, 9),
                new Random().Next(0, 9),
                new Random().Next(0, 9),
                new Random().Next(0, 9),
                new Random().Next(0, 9),
                new Random().Next(0, 9)
            }))).Select(delegate (byte x)
            {
                byte b = x;
                return b.ToString("x2");
            }).Aggregate((string x, string y) => x + y);
        }

        public static byte[] ExploitData(bool indexExploit)
        {
            return indexExploit ? new byte[0] : new byte[1950];
        }

        public static ColorBlock SetThemeColor(ColorBlock colorBlock)
        {
            colorBlock.normalColor = new Color(1f, 0f, 1f);
            colorBlock.pressedColor = new Color(1f, 0f, 1f);
            colorBlock.highlightedColor = new Color(1f, 0f, 1f, 1f);
            return colorBlock;
        }
    }
}
