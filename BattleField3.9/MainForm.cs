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

        public MainForm()
        {
            InitializeComponent();

            openglControl.InitializeContexts();

            scene = new MainScene(openglControl.Width, openglControl.Height);

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
                case Keys.A: scene.RotationY -= 5.0f; break;
                case Keys.D: scene.RotationY += 5.0f; break;
            }

            openglControl.Refresh();
        }
    }
}
