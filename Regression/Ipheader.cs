using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;

using System.IO;
namespace Regression
{
   
    class Ipheader
    {
        private byte verandheader;
        private byte diffserv;
        private ushort totallength;
        private ushort identf;
        private ushort flagandoff;
        private byte ttl;
        private byte bprotocol;
        private short checksum;

        private uint ipsource;
        private uint ipdest;

        private byte headlen;
        private byte[] Ipdata = new byte[4096];

        public Ipheader(byte[] bbuffer, int receive)
        {
            try
            {
                MemoryStream mstream = new MemoryStream(bbuffer, 0, receive);
                BinaryReader br = new BinaryReader(mstream);
                verandheader = br.ReadByte();
                diffserv = br.ReadByte();
                totallength = ( ushort )IPAddress.NetworkToHostOrder(br.ReadInt16());
                identf = (ushort)IPAddress.NetworkToHostOrder(br.ReadInt16());
                flagandoff = (ushort)IPAddress.NetworkToHostOrder(br.ReadInt16());
                ttl = br.ReadByte();
                bprotocol = br.ReadByte();
                checksum = IPAddress.NetworkToHostOrder(br.ReadInt16());
                ipsource = (uint)(br.ReadInt32());
                ipdest = (uint)(br.ReadInt32());
                headlen = verandheader;
                headlen <<=4;
                headlen >>=4;
                headlen *=4;
                Array.Copy(bbuffer,headlen,Ipdata,0,totallength-headlen);
                
            }
            catch (Exception e1)
            {
                MessageBox.Show("error" + e1);
            }
        }
        public IPAddress sourceaddr
        {
            get
            {
                return new IPAddress(ipsource);
            }
        }
        public IPAddress destaddr
        {
            get
            {
                return new IPAddress(ipdest);
            }
        }
        public  string Version
        {
            get
            {
                if((verandheader >> 4)==4)
                    return "IP V4";
                else if((verandheader >>6)==6)
                    return "IP V6";
                else
                    return "unknown";
            }
           
        }
        public string Headerlength
        {
            get
            {
                return(headlen.ToString());

            }
        }
        public ushort Messagelength
        {
            get
            {
                return  (ushort)(totallength-headlen);
            }
        }
        public string diffservice
        {
            get
            {
                return(string.Format("0x{0:x2} ({1}) ",diffserv,diffserv));
            }
        }
        public string Flags
        {
            get
            {
                int nflag=flagandoff >> 13;
                if(nflag==2)
                {
                    return "Don't fragment";

                }
                else if(nflag==1)
                {
                    return "More fragments";
                }
                else
                {
                    return(nflag.ToString());
                }
            }
            
        }
        public string Fragmentationoffset
        {
            get
            {
                int nflag=flagandoff << 3;
                nflag >>=3;
                return(nflag.ToString());
            }

        }
        public string TTL
        {
            get
            {
                return ttl.ToString();
            }
        }
        public protocol Protocoltype
        {
            get
            {
                if (bprotocol == 6)
                    return protocol.TCP;
                else if (bprotocol == 17)
                    return protocol.UDP;
                else
                    return protocol.Unknown;

            }
        }
        public string Checksum
        {
            get
            {
                return string.Format("0x:{0:x2} ", checksum); 
            }
        }

       
        public string TotalLength
        {
            get
            {
                return totallength.ToString();


            }
        }
        public string Identification
        {
            get
            {
                return identf.ToString();
            }
        }

        public byte[] Data
        {
            get
            {
                return Ipdata;
            }
        }
    }
}
