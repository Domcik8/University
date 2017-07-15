using System.Collections.Generic;

namespace KodavimoPraktika.Logic
{
    static class Matrix
    {
        //Sukuria greitajai Hadamardo transformacijai reikalingą matricų rinkinį
        //Parametrai: m - matricų parametras
        //Gražinak: matricų rinkinys
        public static List<int[,]> FastHadamarMatrixes(int m)
        {
            int size;
            int[,] I;
            int[,] H = { { 1, 1 }, { 1, -1 } };
            List<int[,]> fhmatrixes = new List<int[,]>();
            
            for (int i = 1; i <= m; i++)
            {
                //Apskaičiuojamas I laipsniu m - i
                size = Math.ToPower(2, m - i);
                I =  new int[size, size];
                for (int j = 0; j < size; j++)
                    I[j, j] = 1;

                //Apskaičiuojame I laipsniu m - i kart H
                int[,] temp = KronecherProduct(I, H);

                //Apskaičiuojame I 2 laipsniu i - 1
                size = Math.ToPower(2, i - 1);
                I = new int[size, size];
                for (int j = 0; j < size; j++)
                    I[j, j] = 1;

                //Apskaičiuojame I 2 laipsniu m - i kart H kart I 2 laipsniu i - 1
                temp = KronecherProduct(temp, I);

                fhmatrixes.Add(temp);
            }

            return fhmatrixes;
        }

        //Sudaugina dvi matricas, palaiko tik dvimates matricas
        //Parametrai: a, b - matricos
        //Gražinak: matricų andauga
        public static int[,] Multiply(int[,] a, int[,] b)
        {
            int aRowLength = a.GetLength(0);
            int aColumnLength = a.GetLength(1);
            int bRowLength = b.GetLength(0);
            int bColumnLength = b.GetLength(1);

            //Patikriname ar paduotos matricos yra tinkamos sandaugai
            if (aColumnLength != bRowLength)
                return null;
            

            int[,] result = new int[aRowLength, bColumnLength];
            for(int i = 0; i < aRowLength; i++)
                for (int j = 0; j < b.GetLength(1); j++)
                    for (int k = 0; k < aColumnLength; k++)
                        result[i, j] += a[i, k] * b[k, j];
                    
            return result;
        }

        //Gražina dviejų matricų Kronecher produktą, tai yra įstato antrą matricą į pirmą
        //Parametrai: a, b - matricos
        //Gražinak: Kronecher produktas
        private static int[,] KronecherProduct(int[,] a, int[,] b)
        {
            int aRowLength = a.GetLength(0);
            int aColumnLength = a.GetLength(1);
            int bRowLength = b.GetLength(0);
            int bColumnLength = b.GetLength(1);

            int[,] result = new int[aRowLength * bRowLength, aColumnLength * bColumnLength];
            for (int i = 0; i < aRowLength; i++)
                for (int j = 0; j < aColumnLength; j++)
                    for (int k = 0; k < bRowLength; k++)
                        for (int l = 0; l < bColumnLength; l++)
                        {
                            result[i * bRowLength + k, j * bColumnLength + l] = a[i, j] * b[k, l];
                        }
            return result;
        }

        //Nauduojant greitąją Hadamaro transformacija transformuoja matricą.
        //Parametrai: messageMatrix - matrica kuria transformuosime, ghmatrixes - matricos reikalingos greitajai transformacijai, i - rekursijos gylis, m - kodavimo parametras m
        //Gražinak: Kronecher produktas
        public static int[,] FastTransformation(int[,] messageMatrix, List<int[,]> fhmatrixes, int i, int m)
        {
            if (i > m)
                return messageMatrix;
            messageMatrix = Multiply(messageMatrix, fhmatrixes[i - 1]);
            i++;
            return FastTransformation(messageMatrix, fhmatrixes, i, m);
        }

        //Įdedą vektorių į matricos stulpelį, vektoriaus ilgis turi būri vienetu mažesnis už matricos stulpelio ilgį
        //Parametrai: matrix - matrica į kurią įdedame, vector - vektorius, kuria idėdame, n - matricos stulpelio numeris į kuri įdedam vektoriu
        //Gražinak: void
        public static void FillColumn(int[,] matrix, int[] vector, int n)
        {
            for (int i = 0; i < vector.Length; i++)
                matrix[i, n] = vector[i];
        }

        //Apverčia matricą aukštin kojom
        //Parametrai: matrix - matrica kurai norime apversti
        //Gražinak: apversta matrica
        public static int[,] RevertMatrix(int[,] matrix)
        {
            int rowLength = matrix.GetLength(1);
            int columnLength = matrix.GetLength(0);
            int[,] revertedMatrix = new int[columnLength, rowLength];


            for(int i = 0; i < columnLength; i++)
                for (int j = 0; j < rowLength; j++)
                {
                    revertedMatrix[i, j] = matrix[columnLength - i - 1, j];
                }
            return revertedMatrix;
        }

        //Sukuria kodo generuojančią matricą
        //Parametrai: m - kodo parametras m
        //Gražina: kodo generuojanti matrica
        public static int[,] CreateGeneratorMatrix(int m)
        {
            int length = Math.ToPower(2, m);
            int[,] generatorMatrix = new int[m + 1, length];

            for (int i = 0; i < length; i++)
            {
                //Kuriam naturalią funkciją
                int[] temporaryVector = Vector.ConvertIntToVector(i, m);

                //Įdėdame funkciją į stulpelį
                FillColumn(generatorMatrix, temporaryVector, i);
            }
            //Užpildome paskutinę matricos eilutę vienetais
            for (int i = 0; i < length; i++)
                generatorMatrix[m, i] = 1;

            //Apsukam matrica aukštin kojom
            generatorMatrix = Matrix.RevertMatrix(generatorMatrix);

            return generatorMatrix;
        }
    }
}
