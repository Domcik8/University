using System.Collections.Generic;

namespace KodavimoPraktika.Logic
{
    class Decoder
    {
        //Dekoduoja užkoduotą vektorių
        //Parametrai: encodedMessage - užkoduotas pranešimas, m - dekodavimo parametras m
        //Gražinak: dekoduotas pranešimas
        public int[] Decode(int[] encodedMessage, int m)
        {
            int[] decodedMessage = new int[m + 1];
            int[,] temporaryMatrix = new int[1, encodedMessage.Length];
            List<int[,]> fhmatrixes = Matrix.FastHadamarMatrixes(m);
            int max = 0, index;

            //0 pakeičiame į -1
            for (int i = 0; i < encodedMessage.Length; i++)
            {
                if (encodedMessage[i] == 1)
                    temporaryMatrix[0, i] = 1;
                else temporaryMatrix[0, i] = -1;
            }

            temporaryMatrix = Matrix.FastTransformation(temporaryMatrix, fhmatrixes, 1, m);
            
            //Randame didžiausią vektoriaus komponentą ir jo vietą
            max = Math.Absolute(temporaryMatrix[0, 0]);
            index = 0;

            for (int i = 1; i < temporaryMatrix.Length; i++)
            {
                if (max < Math.Absolute(temporaryMatrix[0, i]))
                {
                    max = Math.Absolute(temporaryMatrix[0, i]);
                    index = i;
                }
            }

            //Apskaičiuojame lamdas
            int[] lamdas = Vector.ConvertIntToVector(index, m);
            lamdas = Vector.Reverse(lamdas);

            //Nustatomia niu
            if (temporaryMatrix[0, index] >= 0)
                decodedMessage[0] = 1;
            else decodedMessage[0] = 0;

            //Sukuriame dekoduotą pranešimą
            for (int i = 0; i < lamdas.Length; i++)
                decodedMessage[i + 1] = lamdas[i];
            return decodedMessage;
        }
    }
}
