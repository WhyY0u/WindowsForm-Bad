using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorldSkils2
{
    public partial class Login : Form
    {
        public static int WindowRounded = 30;
        Panel panel = new RoundedPanel(WindowRounded);
        Timer timer = new Timer();
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
        public Login()
        {
            InitializeComponent();
            panel.BackColor = ColorTranslator.FromHtml("#111111");
            panel.Location = new Point(1, 1);
            panel.Size = new Size(Width - 2, Height - 2);

            DraggPanel.Size = new Size(Width - 11, Height - 11);
            DraggPanel.Location = new Point(5, 5);
            DraggPanel.BackColor = panel.BackColor;

            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, WindowRounded, WindowRounded));
            Controls.Add(panel);

            Error.ForeColor = panel.BackColor;
            timer.Interval = 200;
            timer.Tick += onTick;
            timer.Start();
            
        }
        int mouseX, mouseY;
        bool dragg;


        private void DraggPanel_MouseMove(object sender, MouseEventArgs e)
        {
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (LoginBox.TextLength <= 0)
            {
                Error.Text = "Ошибка: Поля 'Логин' не может быть пустым";
                Error.ForeColor = Color.Red;

            }
            if (PasswordBox.TextLength <= 0)
            {
                Error.Text = "Ошибка: Поля 'Пароль' не может быть пустым";
                Error.ForeColor = Color.Red;
            }
            if (PasswordBox.TextLength <= 0 && LoginBox.TextLength <= 0)
            {
                Error.Text = "Ошибка: Поля 'Пароль' и 'Логин' не могут быть пустыми";
                Error.ForeColor = Color.Red;
            }
            Error.Location = new Point((Width / 2) - (TextRenderer.MeasureText(Error.Text, Error.Font).Width / 2) , Height - 120);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void onTick(object sender, EventArgs e)
        {
           

            if(PasswordBox.TextLength > 0 && LoginBox.TextLength > 0)
            {
                Error.ForeColor = panel.BackColor;
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(ShowPassword.Checked) {
                PasswordBox.PasswordChar = '\0';
            } else {
                PasswordBox.PasswordChar = '*';

            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label6_MouseHover(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void label6_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void label6_MouseClick(object sender, MouseEventArgs e)
        {

            this.Hide();

            Form register = new Register();
            register.ShowDialog();

 
        }

        private void DraggPanel_MouseUp(object sender, MouseEventArgs e)
        {
            dragg = false;
        }
        private void DraggPanel_MouseDown(object sender, MouseEventArgs e)
        {
            dragg = true;
            mouseX = MousePosition.X;
            mouseY = MousePosition.Y;
        }
    }
}
