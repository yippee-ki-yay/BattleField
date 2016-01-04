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
        private int duration = 90;

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
                case Keys.W: scene.RotationX -= 5.0f; break;
                case Keys.S: scene.RotationX += 5.0f; break;
                case Keys.T: scene.RotationX -= 5.0f; break;
                case Keys.G: scene.RotationX += 5.0f; break;
                case Keys.A: scene.RotationY -= 5.0f; break;
                case Keys.D: scene.RotationY += 5.0f; break;
                case Keys.F: scene.RotationY -= 5.0f; break;
                case Keys.H: scene.RotationY += 5.0f; break;
                case Keys.F5: this.Close(); break;
                case Keys.C: scene.StartAnimiation = true; duration = 90;
                    break;
            }

            openglControl.Refresh();
        }

        //poziva se metoda 10 puta u sekundi
        private void UpdateScene(object sender, EventArgs e)
        {
            scene.Animation(duration--);

            openglControl.Refresh();
        }
    }
}
