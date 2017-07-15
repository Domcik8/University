namespace KodavimoPraktika.Logic
{
    class MessageManager
    {
        public int[] message;                      //pranešimas
        public int[] encodedMessage;               //užkoduotas pranešimas
        public int[] receivedMessage;              //gautas iš kanalo pranešimas
        public int[] reveivedEncodedMessage;       //gautas iš kanalo užkoduotas pranešimas
        public int[] decodedMessage;               //dekoduotas pranešimas

        public int[] messageMistakeVector;         //paprasto pranešimo klaidų vektorius
        public int[] encodedMessageMistakeVector;  //užkoduoto pranešimo klaidų vektorius
    }
}
