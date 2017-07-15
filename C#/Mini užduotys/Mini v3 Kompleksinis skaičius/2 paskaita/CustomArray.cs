using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomArray
{
    class CustomArray<T>
    {
        T[] array;
        int size;

        public CustomArray(int size)
        {
            array = new T[size];
            this.size = size;
        }

        public T this[int i]
        {
            get {return array[i];}
            set {array[i] = value;}
        }

        public int getSize()
        {
            return size;
        }

        public void Swap(int i, int j)
        {
            T temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}
