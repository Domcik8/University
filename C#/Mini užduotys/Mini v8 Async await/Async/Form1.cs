using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Async;

namespace Async
{
    public partial class Form1 : Form
    {
        
        MyList myList = new MyList();
        

        public Form1()
        {
            InitializeComponent();
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //EventListener listener = new EventListener(myList);
            myList.Add(Convert.ToInt64(maskedTextBox1.Text));
            textBox1.Text = maskedTextBox1.Text;
            maskedTextBox1.Text = "0";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            myList.Fill();
            textBox1.Text = (myList.GetPrimeCount()).ToString();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            myList.Fill();
            textBox1.Text = (await myList.GetPrimeCountAsync()).ToString();
        }
    }

    public class MyList : List<long>
    {
        List<long> myList = new List<long>();
        public delegate void ChangedEventHandler(object sender, EventArgs e);
        public event ChangedEventHandler Changed;
        
        
       

        public void Fill()
        {
            this.Add(0);
            this.Add(1);
            this.Add(2);
            this.Add(3);
            this.Add(4);
            this.Add(5);
            this.Add(6);
            this.Add(7);
            this.Add(8);
            this.Add(9);
        }

        public void Add(long i)
        {
            myList.Add(i);
            bool prime = true;
            if (!(i < 2))
            {
                for (int j = 2; j <= i / 2; j++)
                {
                    if (i % j == 0)
                    {
                        prime = false;
                        break;
                    }
                }
                if (prime == true)
                {
                    PrimeAdded(new EventArgs());
                }

            }
        }

        public long GetPrimeCount()
        {
            int n = 0;
            bool prime = true;
            foreach (long numb in myList)
            {
                if (!(numb < 2))
                {
                    prime = true;
                    for (int i = 2; i <= numb / 2; i++)
                    {
                        if (numb % i == 0)
                        {
                            prime = false;
                            break;
                        }
                    }
                    if (prime == true) n++;
                }
            }
            return n;
        }

        public async Task<long> GetPrimeCountAsync()
        {
            long n = 0;
            bool prime = true;
            return await Task<long>.Run(() =>
                {
                    foreach (long numb in myList)
                    {
                        if (!(numb < 2))
                        {
                            prime = true;
                            for (int i = 2; i <= numb / 2; i++)
                            {
                                if (numb % i == 0)
                                {
                                    prime = false;
                                    break;
                                }
                            }
                       
                            if (prime == true) n++;
                        }
                    }
                    return n;
                });
        }

        protected virtual void PrimeAdded(EventArgs e)
        {
            if (Changed != null)
                Changed(this, e);
        }
    }

    class EventListener
    {
        private MyList List;
        public EventListener(MyList list)
        {
            List = list;
            List.Changed += new MyList.ChangedEventHandler(PrimeAdded);
        }

        // This will be called whenever the list changes.
        private void PrimeAdded(object sender, EventArgs e)
        {
            MessageBox.Show("It was a prime");
        }
    }
}
