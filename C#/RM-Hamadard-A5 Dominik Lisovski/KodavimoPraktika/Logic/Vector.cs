using System.Text;

namespace KodavimoPraktika.Logic
{
    static class Vector
    {
        //Pranešimas konvertuojamas į vektoriu
        //Parametrai: message - pranešimas
        //Gražina: vektorius
        public static int[] CreateVector(string message)
        {
            int[] vectorMessage = new int[message.Length];
            for (int i = 0; i < message.Length; i++)
                if (message[i] == '0')
                    vectorMessage[i] = 0;
                else vectorMessage[i] = 1;
            return vectorMessage;
        }

        //Sudeda du vektorius
        //Parametrai: Vektoriai kuriuos norime sudėti
        //Gražina: Sudėties rezultatas
        public static int[] Addition(int[] vector1, int[] vector2)
        {
            int[] vector = new int[vector1.Length];
            for (int i = 0; i < vector.Length; i++)
            {
                vector[i] = vector1[i] + vector2[i];
                vector[i] %= 2;
            }
            return vector;
        }

        //Konvertuoja vektoriu į stringą
        //Parametrai: Vektorius kurį norime konvertuoti
        //Gražina: Vektorius stringo pavidalu
        public static string ToString(int[] vector)
        {
            StringBuilder message = new StringBuilder();
            for (int i = 0; i < vector.Length; i++)
                if (vector[i] == 0)
                    message.Append("0");
                else message.Append("1");
            return message.ToString();
        }

        //Konvertuoja skaičiu į vektoriu
        //Parametrai: number - skaičius konvertavimui, m - kuriamo vektoriaus ilgis
        //Gražinas: skaičius vektorinėje išraiškoje
        public static int[] ConvertIntToVector(int number, int m)
        {
            int[] vector = new int[m];
            for (int i = 0; i < m; i++)
            {
                if (number == 0)
                    break;
                vector[i] = number / Math.ToPower(2, m - i - 1);
                number %= Math.ToPower(2, m - i - 1);
            }
            return vector;
        }

        //Apsuka vektoriu
        //Parametrai: vector - vektorius
        //Gražinas: apsuktas vektorius
        public static int[] Reverse(int[] vector)
        {
            int[] result = new int[vector.Length];
            for (int i = 0; i < vector.Length; i++)
            {
                result[result.Length - i - 1] = vector[i];
            }
            return result;
        }
    }
}
