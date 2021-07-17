﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IpAddressRange.IpV4
{
   public static class IpV4
    {
        public static uint ToUint32(this IPAddress ip)
        {
            var bytes = ip.GetAddressBytes();
            Array.Reverse(bytes);
            return BitConverter.ToUInt32(bytes, 0);
        }
        public static IPAddress ToIpV4(this uint ipNumber)
        {
            var bytes = BitConverter.GetBytes(ipNumber);
            Array.Reverse(bytes);
            return new IPAddress(bytes);
        }
    }
}
