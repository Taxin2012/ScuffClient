using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using VRC;

namespace ScuffClient.Misc
{
    public class Functions
    {
        public static int GetPhotonId(Player p) => p.GetInstigatorId().GetValueOrDefault();

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
    }
}
