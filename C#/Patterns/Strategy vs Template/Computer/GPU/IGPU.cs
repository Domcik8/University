namespace Computer.GPU
{
    public interface IGPU
    {
        void Start();

        void Display(string message);

        void ShutDown();
    }
}
