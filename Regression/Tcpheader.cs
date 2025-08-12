using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace Regression
{
    public class Tcpheader
    {
        private ushort sourceport;
        private ushort destport;

        private  uint  seqno=555;
        private uint ackno=555 ;
        private ushort flagoff=555;
        private ushort windsize=555;
        private short checksum=555;

        private ushort urgptr;
        private byte HeadLength;           
        private ushort MessageLength;

        private byte[] TCPData = new byte[4096];
        public Tcpheader(byte[] bbuffer, int receive)
        {
            try
            {
                MemoryStream mstream = new MemoryStream(bbuffer, 0, receive);
                BinaryReader br = new BinaryReader(mstream);

                sourceport = (ushort)IPAddress.NetworkToHostOrder(br.ReadInt16());
                destport = (ushort)IPAddress.NetworkToHostOrder(br.ReadInt16());
                seqno = (uint)IPAddress.NetworkToHostOrder(br.ReadInt32());
                ackno = (uint)IPAddress.NetworkToHostOrder(br.ReadInt32());
                flagoff = (ushort)IPAddress.NetworkToHostOrder(br.ReadInt16());
                windsize = (ushort)IPAddress.NetworkToHostOrder(br.ReadInt16());
                checksum = (short)IPAddress.NetworkToHostOrder(br.ReadInt16());
                urgptr = (ushort)IPAddress.NetworkToHostOrder(br.ReadInt16());
                HeadLength = (byte)(flagoff >> 12);
                HeadLength *= 4;
                MessageLength = (ushort)(receive - HeadLength);
                Array.Copy(bbuffer, HeadLength, TCPData, 0, receive - HeadLength);
            }
            catch (Exception e1)
            {
                MessageBox.Show("error" + e1);
            }
       }
        public string Sourceport
        {
            get
            {
                return sourceport.ToString();
            }
        }
        public string Destinationport
        {
            get
            {
                return destport.ToString();
            }
        }
        public string SequenceNo
        {
            get
            {
                return seqno.ToString();
            }
        }
        public string AckNo
        {
            get
            {
                if ((flagoff & 0x10) != 0)
                    return flagoff.ToString();
                else
                    return "";
            }
        }
        public string HeaderLength
        {
            get
            {
                return HeadLength.ToString();
            }
        }
        public string Windsize
        {
            get
            {
                return windsize.ToString();

            }
        }
        public string UrgentPtr
        {
            get
            {
                if ((flagoff & 0x20) != 0)
                    return urgptr.ToString();
                else
                    return "";

            }
        }
        public string Flags
        {
            get
            {
                
                int nFlags = flagoff & 0x3F;

                string stFlags = string.Format("0x{0:x2} (", nFlags);

                
                if ((nFlags & 0x01) != 0)
                {
                    stFlags += "FIN, ";
                }
                if ((nFlags & 0x02) != 0)
                {
                    stFlags += "SYN, ";
                }
                if ((nFlags & 0x04) != 0)
                {
                    stFlags += "RST, ";
                }
                if ((nFlags & 0x08) != 0)
                {
                    stFlags += "PSH, ";
                }
                if ((nFlags & 0x10) != 0)
                {
                    stFlags += "ACK, ";
                }
                if ((nFlags & 0x20) != 0)
                {
                    stFlags += "URG";
                }
                stFlags += ")";

                if (stFlags.Contains("()"))
                {
                    stFlags = stFlags.Remove(stFlags.Length - 3);
                }
                else if (stFlags.Contains(", )"))
                {
                    stFlags = stFlags.Remove(stFlags.Length - 3, 2);
                }

                return stFlags;
            }
        }
        public string Checksum
        {
            get
            {
                return string.Format("0x{0:x2}", checksum);
            }
        }
        public byte[] Data
        {
            get
            {
                return TCPData;
            }
        }
        public ushort MsgLength
        {
            get
            {
                return MessageLength;
            }
        }
    }
}
