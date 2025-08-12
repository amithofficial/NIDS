using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Regression
{
    class Udpheader
    {
        private ushort sourceport;
        private ushort destport;
        private ushort udplen;
        private short udpchecksum;
        
        private byte[] UdpData = new byte[4096];  

        public Udpheader(byte [] bbuffer, int receive)
        {
            MemoryStream mstream = new MemoryStream(bbuffer, 0, receive);
            BinaryReader br= new BinaryReader(mstream);
            sourceport = (ushort)IPAddress.NetworkToHostOrder(br.ReadInt16());
            destport = (ushort)IPAddress.NetworkToHostOrder(br.ReadInt16());
            udplen = (ushort)IPAddress.NetworkToHostOrder(br.ReadInt16());
            udpchecksum = IPAddress.NetworkToHostOrder(br.ReadInt16());            
            Array.Copy(bbuffer,8,UdpData,0,receive - 8);
        }
        public string SourcePort
        {
            get
            {
                return sourceport.ToString();
            }
        }

        public string DestinationPort
        {
            get
            {
                return destport.ToString();
            }
        }
        public string Length
        {
            get
            {
                return udplen.ToString();
            }
        }
        public string Checksum
        {
            get
            {
                
                return string.Format("0x{0:x2}",udpchecksum);
            }
        }

        public byte[] Data
        {
            get
            {
                return UdpData;
            }
        }
    }
}
