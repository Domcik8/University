using System;
using UI;

namespace UIImp
{
    public class UIDialog : IUIDialog
    {
        public string ShowMenu()
        {
            Console.Write("1. Create an institute.\n" +
                              "2. Register a noble to a institute.\n" +
                              "3. List institutes.\n" +
                              "4. List registered nobles.\n" +
                              "5. Teach nobles.\n" +
                              "6. Write works.\n" +
                              "7. Undo last operation.\n" +
                              "0. Exit.\n\n" +
                              "Option: ");
            string answer = Console.ReadLine();
            Console.WriteLine();
            return answer;
        }

        public string ShowDialog(string message)
        {
            Console.Write(message + " ");
            string answer = Console.ReadLine();
            Console.WriteLine();
            return answer;
        }

        public void ShowMonolog(string message)
        {
            Console.WriteLine(message);
            Console.ReadLine();
        }
    }
}
