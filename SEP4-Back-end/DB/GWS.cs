using System;

namespace SEP4_Back_end.DB
{
    public class GWS
    {
        public Int64? snnr { get; set; }
        public Int64? rssi { get; set; }
        public Int64? ts { get; set; }
        public String? gweui { get; set; }
        public double? lat { get; set; }
        public double? lon { get; set; }
        public Int64? tmms { get; set; }
        public double? ant { get; set; }
        public DateTime? time { get; set; }
        public GWS () {

        }
    }
}