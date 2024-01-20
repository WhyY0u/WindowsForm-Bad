using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorldSkils2
{
    public enum Error {
        NullName,
        NullLogin,
        NullGender,
        NotReadRules,
        NullPassword,
        LengthPassword,
        NotAcceptRules,
        NULL,
    }
    public partial class Register : Form
    {
        public static int WindowRounded = 30;
        Error error = Error.NULL;
        bool readRules = false;
        private Panel panel = new RoundedPanel(WindowRounded);
        Timer timer = new Timer();
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
        public Register()
        {
            InitializeComponent();
            panel.BackColor = ColorTranslator.FromHtml("#111111");
            panel.Location = new Point(1, 1);
            panel.Size = new Size(Width - 2, Height - 2);

            DraggPanel.Size = new Size(Width - 11, Height - 11);
            DraggPanel.Location = new Point(5, 5);

            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, WindowRounded, WindowRounded));
            Controls.Add(panel);
            BirthDay.CalendarForeColor = Color.Red;

            ErrorPassword.ForeColor = panel.BackColor;
            ErrorBox.ForeColor = panel.BackColor;
            timer.Interval = 300;
            timer.Tick += TimerTick;
            label10.ForeColor = panel.BackColor;

            timer.Start();

        }

        private void TimerTick(object sender, EventArgs e)
        {

            switch (error)
            {
                case Error.NullName:
                     if(textBox1.Text.Length > 1) error = Error.NULL;
                    break;
                case Error.NullPassword:
                    if (Password.Text.Length > 1 && !Password.Focused)  error = Error.NULL;
                    break;
                case Error.NullLogin:
                    if (textBox2.Text.Length > 1) error = Error.NULL;
                    break;
                case Error.NullGender:
                    if (radioButton2.Checked || radioButton1.Checked) error = Error.NULL;
                    break;
                case Error.NotReadRules:
                    if (readRules) error = Error.NULL;
                    break;
                case Error.LengthPassword:
                    if (Password.Text.Length >= 5) error = Error.NULL;
                    break;
                case Error.NotAcceptRules:
                    if (checkBox1.Checked) error = Error.NULL;
                    break;
                case Error.NULL:
                    ErrorBox.Text = "";
                    ErrorBox.ForeColor = panel.BackColor;
                    break;
            }

            if (error == Error.NULL) {
                ErrorBox.ForeColor = panel.BackColor;
            } else
            {
                ErrorBox.ForeColor = Color.Red;
            }
             if (!Password.Text.Equals(Password2.Text) && Password.Text.Length > 0) {
                ErrorPassword.ForeColor = Color.FromArgb(255, 255, 0, 0);
            } else {
                ErrorPassword.ForeColor = panel.BackColor;

            }

            if (Base.CheckLogin(textBox2.Text)) {
                label10.ForeColor = panel.BackColor;
            } else {
                label10.ForeColor = Color.Red;
            }

        }


        int mouseX, mouseY;
        bool dragg;
        private void DraggPanel_MouseMove(object sender, MouseEventArgs e) {

            if (dragg)
            {

                int newMouseX = MousePosition.X;
                int newMouseY = MousePosition.Y;

                int moveX = newMouseX - mouseX;
                int moveY = newMouseY - mouseY;

                Left += moveX;
                Top += moveY;

                mouseX = newMouseX;
                mouseY = newMouseY;
            }
        }

        private void DraggPanel_MouseDown(object sender, MouseEventArgs e) {
            dragg = true;
            mouseX = MousePosition.X;
            mouseY = MousePosition.Y;
        }

  

        private void label7_MouseHover(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void label7_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void label7_MouseClick(object sender, MouseEventArgs e)
        {
            readRules = true;
            string filePath = "C:/Users/User/source/repos/WorldSkils2/Rules.txt";
            Process.Start(filePath);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            error = Error.NULL;
            if (Password.Text.Length < 5) error = Error.LengthPassword;
            if (!readRules) error = Error.NotReadRules;
            if (!checkBox1.Checked) error = Error.NotAcceptRules;
            if (!radioButton2.Checked && !radioButton1.Checked) error = Error.NullGender;
            if (textBox1.Text.Length <= 0) error = Error.NullName;
            if (Password.Text.Length <= 0) error = Error.NullPassword;
            if (textBox2.Text.Length <= 0) error = Error.NullLogin;
           
          

            switch (error)
            {
                case Error.NullName:
                    ErrorBox.Text = "Ошибка: Поля '" + label3.Text + "' не может быть пустым";

                    break;
                case Error.NullPassword:
                    ErrorBox.Text = "Ошибка: Поля '" + label4.Text + "' не может быть пустым";

                    break;
                case Error.NullLogin:
                    ErrorBox.Text = "Ошибка: Поля '" + label2.Text + "' не может быть пустым";

                    break;
                case Error.NullGender:
                    ErrorBox.Text = "Ошибка: Вы не выбрали свой Пол";

                    break;
                case Error.NotReadRules:
                    ErrorBox.Text = "Ошибка: Вы не прочли правила";

                    break;
                case Error.LengthPassword:
                    ErrorBox.Text = "Ошибка: Длина пароля должна составлять не менее пяти символов";

                    break;
                case Error.NotAcceptRules:
                    ErrorBox.Text = "Ошибка: Вы не согласились с условиями использвания";
                    break;
                case Error.NULL:
                    ErrorBox.Text = "";
                    ErrorBox.ForeColor = panel.BackColor;
                    break;
            }
            ErrorBox.Location = new Point((Width / 2) - (TextRenderer.MeasureText(ErrorBox.Text, ErrorBox.Font).Width / 2), Height - 90);

            Console.WriteLine(Password.Text.Equals(Password2.Text));
            Console.WriteLine(Password.TextLength >= 5);
            Console.WriteLine(error == Error.NULL) ;
            Console.WriteLine(error);

            if (Password.Text.Equals(Password2.Text) && Password.TextLength >= 5 && error == Error.NULL)
            {
                DateTime birthDate = DateTime.Parse(BirthDay.Text);
                if (Base.Register(textBox1.Text, textBox2.Text, Password.Text, birthDate.ToString("yyyy-MM-dd"), radioButton1.Checked ? "Male" : "Female", ((int)numericUpDown1.Value)))
                {
                    this.Hide();

                    Login login = new Login();
                    login.ShowDialog();
             
                }
                
            }
        }

        private void label12_MouseClick(object sender, MouseEventArgs e)
        {

            this.Hide();

            Form login = new Login();
            login.ShowDialog();
        }

        private void label12_MouseHover(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;

        }

        private void label12_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;

        }

        private void DraggPanel_MouseClick(object sender, MouseEventArgs e) {
            dragg = false;

        }
    }
}
