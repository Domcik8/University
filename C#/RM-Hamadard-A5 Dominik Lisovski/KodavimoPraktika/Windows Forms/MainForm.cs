using System;
using System.Drawing;
using System.Windows.Forms;
using KodavimoPraktika.Logic;

namespace Messenger
{
    public partial class MainForm : Form
    {
        MessageManager messageManager = new MessageManager();
        Decoder decoder = new Decoder();
        Canal canal = new Canal();
        Coder coder = new Coder();

        public string message;
        public float mistakeChance; //Klaidos tikimybė
        public int m; //Parametras m


        //Inicializuoja formos elementus
        //Parametrai: nėra
        //Gražina: Void
        public MainForm()
        {
            InitializeComponent();
        }

        //Leidžia į pranešimo lauką įvesti tik binarį pranešimą
        //Parametrai: standartiniai įvykio parametrai
        //Gražina: Void
        private void Message_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar.Equals('0') || e.KeyChar.Equals('1') || e.KeyChar.Equals('\b')))
            {
                e.KeyChar = '\0';
            }
        }

        //Leidžia į gauto užkoduoto pranešimo lauką įvesti tik binarį pranešimą
        //Parametrai: standartiniai įvykio parametrai
        //Gražina: Void
        private void ReceivedEncodedMessageBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar.Equals('0') || e.KeyChar.Equals('1') || e.KeyChar.Equals('\b')))
            {
                e.KeyChar = '\0';
            }
        }

        //Procedūra atsakinga už vektoriaus išsiuntimą kanalu
        //Parametrai: standartiniai įvykio parametrai
        //Gražina: Void
        private void Siusti_Click(object sender, EventArgs e)
        {
            //Užpildo parametrų laukus bei juos patikrina
            if (1 == CheckParameters())
                return;

            //Konvertuojame pranešimą į vektorių
            messageManager.message = Vector.CreateVector(message);

            //Užkoduojame pranešimą
            messageManager.encodedMessage = coder.Encode(messageManager.message, m);
            EncodedMessageBox.Text = Vector.ToString(messageManager.encodedMessage);

            //Išsiunčiame kanalu paprastą ir užkoduotą pranešimą
            canal.SendThroughCanal(messageManager, mistakeChance);
            ReceivedMessageBox.Text = Vector.ToString(messageManager.receivedMessage);
            ReceivedEncodedMessageBox.Text = Vector.ToString(messageManager.reveivedEncodedMessage);

            //Pariškiname kur buvo padarytos klaidos
            ShowMistakes(ReceivedMessageBox, messageManager.messageMistakeVector, Color.Red);
            ShowMistakes(ReceivedEncodedMessageBox, messageManager.encodedMessageMistakeVector, Color.Red);

            //Aktivuojame mygtuką dekoduoti
            DekoduotiButton.Enabled = true;
        }

        //Patikrina parametrus reikalingus užkodavimui ir siuntimui
        //Parametrai: nėra
        //Gražina: 1 parametrai nėra geri, 0 priešingu atvieju
        private int CheckParameters()
        {
            //Patikrina parametrą m
            int.TryParse(mBox.Text, out m);
            if (m > 25 || m < 0)
            {
                System.Windows.Forms.MessageBox.Show("Parametras m turi būti sveikas skaičius tarp 0 ir 25 imtinai!", "Klaida!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1;
            }

            //Patikrina klaidos tikimybę
            float.TryParse(mistakeChanceBox.Text, out mistakeChance);
            if (mistakeChance > 1 || mistakeChance < 0)
            {
                System.Windows.Forms.MessageBox.Show("Tikimybė turi būti tarp 0 ir 1 imtinai!", "Klaida!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1;
            }

            //Patikrina pranešimo ilgį
            message = MessageBox.Text;
            if (MessageBox.Text.Length != m + 1)
            {
                System.Windows.Forms.MessageBox.Show("Vektoriaus ilgis turi būti m + 1 ilgio!", "Klaida!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1;
            }
            return 0;
        }

        //Pariškina padarytas klaidas
        //Parametrai: textBox - pariškinamo teksto laukas, mistakeVector - klaidų vektorius, color - spalva
        //Gražina: void
        private void ShowMistakes(RichTextBox textBox, int[] mistakeVector, Color color)
        {
            for (int i = 0; i < mistakeVector.Length; i++)
            {
                if (mistakeVector[i] == 1)
                {
                    textBox.Select(i, 1);
                    textBox.SelectionColor = color;
                    textBox.DeselectAll();
                }
                else
                {
                    textBox.Select(i, 1);
                    textBox.SelectionColor = Color.Empty;
                    textBox.DeselectAll();
                }
            }
        }

        //Dekoduojame užkoduota pranešimą išėjusi iš kanalo
        //Parametrai: standartiniai įvykio parametrai
        //Gražina: void
        private void DekoduotiButton_Click(object sender, EventArgs e)
        {
            //Paimame gautą užkoduotą vektoriu
            messageManager.reveivedEncodedMessage = GetReceivedEncodedMessage(m);
            if (messageManager.reveivedEncodedMessage == null)
                return;

            //Dekoduojame pranešimą
            messageManager.decodedMessage = decoder.Decode(messageManager.reveivedEncodedMessage, m);
            DecodedMessageBox.Text = Vector.ToString(messageManager.decodedMessage);


            //Pariškiname dekodavimo klaidas
            int[] decodingMistakes = new int[m + 1];
            for (int i = 0; i < m + 1; i++)
            {
                if (messageManager.message[i] != messageManager.decodedMessage[i])
                    decodingMistakes[i] = 1;
            }

            ShowMistakes(DecodedMessageBox, decodingMistakes, Color.Red);
        }

        //Gražina gauta užkoduotą pranešimą
        //Parametrai: m - kodavimo parametras m
        //Gražina: gautas užkoduotas vektorius
        private int[] GetReceivedEncodedMessage(int m)
        {
            if (ReceivedEncodedMessageBox.Text.Length == KodavimoPraktika.Logic.Math.ToPower(2, m))
                return Vector.CreateVector(ReceivedEncodedMessageBox.Text);
            System.Windows.Forms.MessageBox.Show("Užkoduoto vektoriaus ilgis turi būtu 2 laipsniu m!", "Klaida!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }

        //Deaktivuoja mygtuką dekoduoti pakeitus parametrą m
        //Parametrai: standartiniai įvykio parametrai
        //Gražina: void
        private void mBox_TextChanged(object sender, EventArgs e)
        {
            DekoduotiButton.Enabled = false;
        }
    }
}