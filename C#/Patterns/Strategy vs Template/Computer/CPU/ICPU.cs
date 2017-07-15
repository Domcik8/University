namespace Computer.CPU
{
    public interface ICPU
    {
        void Start();

        string Calculate(float a, float b);

        void ShutDown();
    }
}
