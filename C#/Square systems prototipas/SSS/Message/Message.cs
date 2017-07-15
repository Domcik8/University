using System;

namespace Common
{   
    [Serializable]
    public class Message
    {   
        public bool? Isclient = null;
        public bool? IsLogin = null;
        public string CarNmb = null;
        public string Username = null;     
        public string Password = null;    
        public bool? EngineState = null;
        public bool? AlarmState = null;
        public bool? LightState = null;
        public bool? Locked = null;
        public string ComputerMsg = null;
        public bool? OpenWindows = null;

        public bool? SOSSkambutis = null;
        public string krituliuKiekis = null;
        public float? variklioTemp = null;
        public float? rida = null;
        public float? vidGreitis = null;
        public float? degaluSanaudos = null;
        public float? vidausTemp = null;
        public float? laukoTemp = null;
        public float? degalai = null;
	    public float? momentinisGreitis = null;
    }
}