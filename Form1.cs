using System;
using System.Drawing;
using System.Windows.Forms;

namespace practice
{
    public partial class Form1 : Form
    {
        // --- Переменные (поля класса) ---
        private Timer gameTimer;       // Таймер для анимации
        private PictureBox ball;       // Наш шарик
        private int speedX = 5;        // Скорость по горизонтали
        private int speedY = 5;        // Скорость по вертикали
        private Random rnd = new Random(); // Генератор случайных чисел для цвета

        public Form1()
        {
            // 1. Настройка самой формы
            this.Text = "Практика: Анимация шарика (C#)";
            this.Size = new Size(800, 600);
            this.BackColor = Color.Black;
            this.DoubleBuffered = true; // Чтобы не мерцало

            // 2. Создаем шарик
            CreateBall();

            // 3. Запускаем таймер
            SetupTimer();
        }

        // Метод создания шарика
        private void CreateBall()
        {
            ball = new PictureBox();
            ball.Size = new Size(50, 50);
            ball.BackColor = Color.Transparent;

            // Рисуем кружок
            DrawBall(Color.Cyan);

            // Ставим в центр
            ball.Left = this.ClientSize.Width / 2;
            ball.Top = this.ClientSize.Height / 2;

            // Добавляем на форму
            this.Controls.Add(ball);
        }

        // Метод рисования шарика (чтобы менять цвет)
        private void DrawBall(Color color)
        {
            Bitmap bmp = new Bitmap(ball.Width, ball.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);
                g.FillEllipse(new SolidBrush(color), 0, 0, ball.Width - 1, ball.Height - 1);
            }
            ball.Image = bmp;
        }

        // Настройка таймера
        private void SetupTimer()
        {
            gameTimer = new Timer();
            gameTimer.Interval = 20; // 20 миллисекунд (50 кадров в секунду)
            gameTimer.Tick += GameTimer_Tick; // Что делать при каждом "тике"
            gameTimer.Start();
        }

        // Логика движения (выполняется каждый кадр)
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            // Двигаем шарик
            ball.Left += speedX;
            ball.Top += speedY;

            bool hitWall = false; // Флаг: было ли столкновение

            // Проверка стенок
            if (ball.Right >= this.ClientSize.Width || ball.Left <= 0)
            {
                speedX = -speedX; // Меняем направление по X
                hitWall = true;
            }
            if (ball.Bottom >= this.ClientSize.Height || ball.Top <= 0)
            {
                speedY = -speedY; // Меняем направление по Y
                hitWall = true;
            }

            // Если ударился о стенку — меняем цвет
            if (hitWall)
            {
                Color newColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                DrawBall(newColor);
            }
        }
    }
}