namespace SEP4Lora
{
    public class Packet
    {
        public string cmd {get;set;}
        public string EUI{get;set;}
        public int port{get;set;}
        public bool confirmed {get;set;}
        public string data{get;set;}

    public Packet(string cmd, string EUI, int port, string data) {
        this.confirmed = false;
        this.cmd = cmd;
        this.EUI = EUI;
        this.port = port;
        this.data = data;
    }
    }
}