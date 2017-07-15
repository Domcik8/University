namespace KodavimoPraktika.Logic
{
    class Coder
    {
        //Užkoduoja vektorių
        //Parametrai: vector - vektorius kuri užkoduojame, m - kodo parametras m
        //Gražina: užkoduotą vektorių
        public int[] Encode(int[] vector, int m)
        {
            int[] encodedVector = new int[Math.ToPower(2, m)];
            int[,] generatorMatrix = Matrix.CreateGeneratorMatrix(m); //Kodą generuojanti matrica
            int[,] temporaryMatrix = new int[1, vector.Length];
            
            //Idėdame vektoriu i laikiną matricą
            for (int i = 0; i < vector.Length; i++)
                temporaryMatrix[0, i] = vector[i];
            
            temporaryMatrix = Matrix.Multiply(temporaryMatrix, generatorMatrix);
        
            //Iš laikinos matricos išimame užkoduota vektoriu moduliu 2
            for (int i = 0; i < encodedVector.Length; i++)
                encodedVector[i] = temporaryMatrix[0, i] % 2;
            return encodedVector;
        }

        //Užkoduoja vektoriu pagal paskaitų pavyzdžius ALGORITMAS NENAUDOJAMAS
        //Parametrai: vector - vektorius kuri užkoduojame, m - kodo parametras m
        //Gražina: užkoduotą vektorių
        [System.Obsolete("Use Encode instead of EncodeOLD")]
        public int[] EncodeOLD(int[] vector, int m)
        {
            int CLength = Math.ToPower(2, m); //Užkoduoto vektoriaus ilgis
            int[] temporaryNaturalFunction = new int[m]; //Laikinas vektorius saugantis naturalias funkcijas
            int[] encodedVector = new int[CLength];
            for (int i = 0; i < CLength; i++)
            {
                //Kuriam naturalias funkcijas
                temporaryNaturalFunction = Vector.ConvertIntToVector(i, m);

                //Apskaičiuojame i-taji užkodoto vektoriaus elementą
                for (int j = 0; j < m; j++)
                {
                    encodedVector[i] += vector[j] * temporaryNaturalFunction[j];
                }
                encodedVector[i] = (encodedVector[i] + vector[m]) % 2;
            }
            return encodedVector;
        }
    }
}
