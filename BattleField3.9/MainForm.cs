using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleField3._9
{
    public partial class MainForm : Form
    {
        private MainScene scene = null;
        private int duration = 70;

        public MainForm()
        {
            InitializeComponent();

            openglControl.InitializeContexts();

            scene = new MainScene(openglControl.Width, openglControl.Height);

            scene.Draw();

        }

        ~MainForm()
        {
            scene.Dispose();
        }

        private void openglControl_Paint(object sender, PaintEventArgs e)
        {
            scene.Draw();
         
        }

        private void openglControl_Resize(object sender, EventArgs e)
        {
            scene.Width = openglControl.Width;
            scene.Height = openglControl.Height;

            scene.Resize();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            scene.Dispose();
        }

        private void openglControl_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {

                case Keys.W:
                    {
                        if (scene.RotationX > -30.0f)
                        {
                            scene.RotationX -= 5.0f;
                        }
                      
                    }
                    break;


                case Keys.S: 
                    {
                        if (scene.RotationX < 125.0f)
                        {
                            scene.RotationX += 5.0f;
                        }
                    }
                    break;

                case Keys.T:
                    {
                        if (scene.RotationX > -30.0f)
                        {
                            scene.RotationX -= 5.0f;
                        }

                    }
                    break;


                case Keys.G:
                    {
                        if (scene.RotationX < 125.0f)
                        {
                            scene.RotationX += 5.0f;
                        }
                    }
                    break;
                


                case Keys.A: scene.RotationY -= 5.0f; break;
                case Keys.D: scene.RotationY += 5.0f; break;
                case Keys.F: scene.RotationY -= 5.0f; break;
                case Keys.H: scene.RotationY += 5.0f; break;

                case Keys.Add:
                    {
                        if(scene.SceneDistance < 10.0f)
                        {
                            scene.SceneDistance += 0.5f;
                            scene.Resize();
                        }
                       
                    }
                    break;
                case Keys.Subtract:
                    {
                        if(scene.SceneDistance > 0.0f)
                        {
                            scene.SceneDistance -= 0.5f; scene.Resize();
                        }
                       
                    }
                    break;

                case Keys.F5: this.Close(); break;
                case Keys.C: scene.StartAnimiation = true; duration = 70;
                    break;
            }

            openglControl.Refresh();
        }

        //poziva se metoda 10 puta u sekundi
        private void UpdateScene(object sender, EventArgs e)
        {
            scene.Animation(duration--);

            if(scene.StartAnimiation)
            {
                numericUpDown1.Enabled = false;
                numericUpDown2.Enabled = false;
                button1.Enabled = false;
            }
            else
            {
                numericUpDown1.Enabled = true;
                numericUpDown2.Enabled = true;
                button1.Enabled = true;
            }

            openglControl.Refresh();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

            scene.TankRotation = (float)numericUpDown1.Value;

            openglControl.Refresh();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

            scene.ShipScale = (float)numericUpDown2.Value;

            openglControl.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ColorDialog colorDiag = new ColorDialog();

            if(colorDiag.ShowDialog() == DialogResult.OK)
            {
                Color c = colorDiag.Color;
                scene.Init(c.R, c.G, c.B, 1.0f);
                scene.Draw();
                openglControl.Focus();
                pictureBox1.BackColor = c;
            }
        }
    }
}
