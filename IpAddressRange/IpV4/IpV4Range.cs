using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text.Json.Serialization;

namespace IpAddressRange.IpV4
{
    public struct IpV4Range :IEnumerable<IPAddress>
    {
        public uint Start { get;private init; }
        public uint End { get;private init; }
        [JsonIgnore]
        public uint Lenght { get; private init; }

        public IpV4Range(uint start, uint end)
        {
            this.Start = start;
            this.End = end;
            Lenght = End - Start + 1;
        }
        public IpV4Range(IPAddress start, IPAddress end)
        {
            this.Start = start.ToUint32();
            this.End = end.ToUint32();

            Lenght = (End - Start) + 1;
        }
        public IpV4Range(ReadOnlySpan<char> ipv4Range)
        {
            int index = ipv4Range.IndexOf('-');

            var ip1 = ipv4Range.Slice(0,index);
            var ip2 = ipv4Range.Slice(index+1);

            Start = IPAddress.Parse(ip1).ToUint32();
            End = IPAddress.Parse(ip2).ToUint32();

            Lenght = (End - Start) + 1;
        }

        public IPAddress this[uint index]
        {
            get
            {
                if (index >= Lenght)
                    throw new IndexOutOfRangeException();
                return (Start + index).ToIpV4();
            }
        }
        public bool Contains(IPAddress ip)
        {
            uint ipNumber = ip.ToUint32();
            return (ipNumber >= Start && ipNumber <= End);
        }
        public bool Contains(uint ipNumber)
        {
            return (ipNumber >= Start && ipNumber <= End);
        }

        public override string ToString()
        {
            return $"{Start.ToIpV4()}-{End.ToIpV4()}";
        }

        public IEnumerator<IPAddress> GetEnumerator()
        {
            for (uint i = Start; i <= End; i++)
            {
                yield return i.ToIpV4();
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
