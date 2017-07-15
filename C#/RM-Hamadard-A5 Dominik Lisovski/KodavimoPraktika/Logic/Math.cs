namespace KodavimoPraktika.Logic
{
    static class Math
    {
        //Pakelia skačių nurodytu laipsniu
        //Parametrai: number - skaičius kuri kelsime laipsniu, power - laipsnis
        //Gražina: skaičius pakeltas nurodytu laipsniu
        public static int ToPower(int number, int power)
        {
            int result = 1;
            for (int i = 0; i < power; i++)
            {
                result *= number;
            }
            return result;
        }

        //Gražina skaičių modulyje
        //Parametrai: number - skaičius
        //Gražina: skaičiaus number modulį
        public static int Absolute(int number)
        {
            if(number >= 0)
                return number;
            return -number;
        }
    }
}
