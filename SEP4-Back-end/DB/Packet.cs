using System;

namespace SEP4_Back_end.DB
{
    public class Packet {
        public string? cmd { get; set; }
        public string? EUI { get; set; }
        public long? ts { get; set; }
        public bool? ack { get; set; }
        public long? bat { get; set; }
        public long? fcnt { get; set; }
        public long? port { get; set; }
        public String? encdata { get; set; }
        public bool? confirmed { get; set; }
        public String? data { get; set; }
        public long? freq { get; set; }
        public String? dr { get; set; }
        public GWS?[] gws { get; set; }
        public long? rssi { get; set; }
        public long? snr { get; set; }
        public double? toa { get; set; }
        public long? seqno { get; set; }


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