using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WinForms
{
    public partial class Form3 : Form
    {
        // Переменные для отслеживания кликов
        Label firstClicked = null;
        Label secondClicked = null;

        Random random = new Random();

        // Список иконок
        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        };

        public Form3()
        {
            InitializeComponent();
            AssignIconsToSquares();
        }

        /// <summary>
        /// Метод для назначения иконок на метки
        /// </summary>
        private void AssignIconsToSquares()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;  // Скрываем иконки
                    icons.RemoveAt(randomNumber);
                }
            }
        }

        /// <summary>
        /// Обработчик события клика по меткам
        /// </summary>
        private void label1_Click(object sender, EventArgs e)
        {
            // Если таймер запущен, игнорируем клики
            if (timer1.Enabled == true)
                return;

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                // Игнорируем клики по уже открытым иконкам
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                // Если firstClicked равен null, это первый клик
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }

                // Если дошли сюда, это второй клик
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                // Проверяем на совпадение
                if (firstClicked.Text == secondClicked.Text)
                {
                    // Если иконки совпадают, сбрасываем переменные
                    firstClicked = null;
                    secondClicked = null;

                    // Проверяем, не победил ли игрок
                    CheckForWinner();
                    return;
                }

                // Если иконки не совпадают, запускаем таймер
                timer1.Start();
            }
        }

        /// <summary>
        /// Таймер скрывает иконки, если они не совпали
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Останавливаем таймер
            timer1.Stop();

            // Скрываем иконки
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            // Сбрасываем переменные
            firstClicked = null;
            secondClicked = null;
        }

        /// <summary>
        /// Метод для проверки победы
        /// </summary>
        private void CheckForWinner()
        {
            // Проходим по всем меткам и проверяем их видимость
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    // Если хотя бы одна иконка скрыта, игра продолжается
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            // Если все иконки открыты, игрок победил
            MessageBox.Show("Вы нашли все совпадения!", "Поздравляем!");
            Close();
        }
    }
}
