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

            scene = new MainScene();

        }

        private void openglControl_Paint(object sender, PaintEventArgs e)
        {
            scene.Draw();
        }
    }
}
