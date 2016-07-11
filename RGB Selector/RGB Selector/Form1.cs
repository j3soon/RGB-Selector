using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RGB_Selector
{
    public partial class Form1 : Form
    {
        int r, g, b;  //Current color's RGB.
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            r = g = b = 0;  //Initialize color is Black.
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            r = trackBar1.Value;  //R's TrackBar scroll change r value.
            ReColor();  //Refresh display.
        }
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            g = trackBar2.Value;  //G's TrackBar scroll change g value.
            ReColor();  //Refresh display.
        }
        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            b = trackBar3.Value;  //B's TrackBar scroll change b value.
            ReColor();  //Refresh display.
        }
        //textBox1 LostFocus triggers.
        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            //Input Format: "#RRGGBB" or HTMLColor.
            //ex: "#CCC" or "#CCCCCC" or "blue".
            //  ("#CCC" equals "#CCCCCC")
            try
            {
                Color c = ColorTranslator.FromHtml(textBox1.Text);  //Turn HTMLColor into Color.
                //If textBox1.Text has the wrong format, go to Catch and skip the Change Color part.
                //Change color.
                r = c.R;
                g = c.G;
                b = c.B;
            }
            catch (Exception) { }
            ReColor(); //Refresh display.
        }
        //textBox2 LostFocus triggers.
        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            //Input Format: "R,G,B" or "R G B" or "R, G, B".
            //ex: "204,204,204" or "204 204 204" or "204, 204, 204".
            String[] str = textBox2.Text.Split(new String[] { " ", "," }, StringSplitOptions.RemoveEmptyEntries);
            if (str.Length == 3)  //If not 3 numbers, go to ReColor() and skip the Change Color part.
            {
                try
                {
                    int rr, gg, bb;  //Temporary variables.
                    rr = Int32.Parse(str[0]);
                    gg = Int32.Parse(str[1]);
                    bb = Int32.Parse(str[2]);
                    //If textBox2.Text has the wrong format, go to Catch and skip the Change Color part.
                    if (rr >= 0 && gg >= 0 && bb >= 0 && rr < 256 && gg < 256 && bb < 256)  //between 0 and 255 is the correct value.
                    {
                        //Only change color after confirmed the format.
                        r = rr;
                        g = gg;
                        b = bb;
                    }
                }
                catch (Exception) { }
            }
            ReColor(); //Refresh display.
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //Make Enter key triggers Change Color.
            //Focus trackBar1 is to make textBox1 Lost Focus and triggers textBox1_Validating event.
            if (e.KeyCode == Keys.Enter) trackBar1.Focus();
        }
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            //Make Enter key triggers Change Color.
            //Focus trackBar1 is to make textBox2 Lost Focus and triggers textBox2_Validating event.
            if (e.KeyCode == Keys.Enter) trackBar1.Focus();
        }
        //Triggers when r, g, b changed.
        void ReColor()
        {
            // Change color of pictureBox1.
            pictureBox1.BackColor = Color.FromArgb(r, g, b);

            //int.ToString("X2") Converts a number from Dec to Hex.
            //Output: "#RRGGBB".
            //ex: "#CCCCCC".
            //If enters: "#ccc" force change to "#CCCCCC".
            textBox1.Text = "#" + r.ToString("X2") + g.ToString("X2") + b.ToString("X2");

            //Output: "R, G, B".
            //ex: "204, 204, 204".
            //If enters: "204 204 204" force change to "204, 204, 204".
            textBox2.Text = r + ", " + g + ", " + b;

            //Change Trackbar scroll block position.
            trackBar1.Value = r;
            trackBar2.Value = g;
            trackBar3.Value = b;
        }
    }
}
