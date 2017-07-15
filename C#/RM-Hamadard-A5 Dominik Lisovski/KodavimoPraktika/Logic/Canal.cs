using System;

namespace KodavimoPraktika.Logic
{
    class Canal
    {
        Random random = new Random();

        //Siunčia vektorių per kanalą
        //Parametrai: messageManager - klasė laikanti visas pranešimo variacijas, mistakeChance - klaidos tikimybė
        //Gražina: void
        public void SendThroughCanal(MessageManager messageManager, float mistakeChance)
        {
            //Nusiunčiamas kanalu paprastas pranešimas
            messageManager.messageMistakeVector = GetMistakeVector(messageManager.message.Length, mistakeChance);
            messageManager.receivedMessage = Vector.Addition(messageManager.message, messageManager.messageMistakeVector);

            //Nusiunčiamas kanalu užkoduotas pranešimas
            messageManager.encodedMessageMistakeVector = GetMistakeVector(messageManager.encodedMessage.Length, mistakeChance);
            messageManager.reveivedEncodedMessage = Vector.Addition(messageManager.encodedMessage, messageManager.encodedMessageMistakeVector);
        }

        //Gražina klaidų vektoriu imituojanti triukšmą kanale
        //Parametrai: length - klaidų vektoriaus ilgis, mistakeChance - klaidos tikimybė
        //Gražinak: klaidos vektoriu
        private int[] GetMistakeVector(int length, float mistakeChance)
        {
            int[] mistakeVector = new int[length];
            for (int i = 0; i < length; i++)
                if (mistakeChance >= random.NextDouble())
                    mistakeVector[i] = 1;
                else mistakeVector[i] = 0;
            return mistakeVector;
        }
    }
}

