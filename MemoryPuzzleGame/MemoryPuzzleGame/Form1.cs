using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryPuzzleGame
{
    public partial class Form1 : Form
    {
        Random random = new Random();

        List<string> icons = new List<string>() // global variable
        {
            "!", "!", "n", "n", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        };

        Label firstClicked, secondClicked;
        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();
        }

        private void label_Click(object sender, EventArgs e)
        {
            if (firstClicked != null && secondClicked != null)
                return;

            Label clickedLabel = sender as Label; // as keyword is trying to convert sender into a label but if not possible clickedlabel will be null

            if (clickedLabel == null)
                return;

            if (clickedLabel.ForeColor == Color.Black)
                return;

            if(firstClicked == null)
            {
                firstClicked = clickedLabel;
                firstClicked.ForeColor = Color.Black; // to see what was pressed
                return;
            }

            secondClicked = clickedLabel;
            secondClicked.ForeColor = Color.Black;

            CheckForWinner(); // if game wins program exits

            if(firstClicked.Text == secondClicked.Text)
            {
                firstClicked = null;
                secondClicked = null;
            }
            else
            timer1.Start();
        }

        private void CheckForWinner() // function to end game
        {
            Label label;
            for(int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
                label = tableLayoutPanel1.Controls[i] as Label; // iterating through all labels

                if (label != null && label.ForeColor == label.BackColor)
                    return;
            }

            MessageBox.Show("Congratulations You Matched All The Icons! :-)");
            Close();
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            firstClicked = null;
            secondClicked = null;
        }

        private void AssignIconsToSquares() // function
        {
            Label label;
            int randomNumber; // random index into our list

            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++) // iterating through every single control that is in our tablelayoutpanel
            {
                if (tableLayoutPanel1.Controls[i] is Label)
                    label = (Label)tableLayoutPanel1.Controls[i];
                else
                    continue;

                randomNumber = random.Next(0, icons.Count); // choose random number
                label.Text = icons[randomNumber]; // use random number to choose a random icon in our list
                // set text of that label to the icon^
                
                icons.RemoveAt(randomNumber); // remove random number from our icon list
            }
        }
    }
}
