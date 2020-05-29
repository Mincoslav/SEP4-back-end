using System;

namespace SEP4_Back_end.DB
{
    public class Packet {
        public string? cmd { get; set; }
        public string? EUI { get; set; }
        public Int64? ts { get; set; }
        public bool? ack { get; set; }
        public int? bat { get; set; }
        public int? fcnt { get; set; }
        public int? port { get; set; }
        public String? encdata { get; set; }
        public bool? confirmed { get; set; }
        public String? data { get; set; }
        public int? freq { get; set; }
        public String? dr { get; set; }
        public GWS?[] gws { get; set; }
        public int? rssi { get; set; }
        public int? snr { get; set; }
        public int? toa { get; set; }
        public int? seqno { get; set; }


        /*
        public Packet(string cmd, string EUI, int port, String data) {
            this.confirmed = false;
            this.cmd = cmd;
            this.EUI = EUI;
            this.port = port;
            this.data = data;
        }

        public Packet(string cmd = null, string eui = null, int ts = default, bool ack = default, int bat = default, int fcnt = default, int port = default, string encdata = null, bool confirmed = default, String data = null, int freq = default, string dr = null, byte[] gws = null)
        {
            this.cmd = cmd;
            EUI = eui;
            this.ts = ts;
            this.ack = ack;
            this.bat = bat;
            this.fcnt = fcnt;
            this.port = port;
            this.encdata = encdata;
            this.confirmed = confirmed;
            this.data = data;
            this.freq = freq;
            this.dr = dr;
            this.gws = gws;
        }
        */

        public Packet()
        {
        }
    }
}