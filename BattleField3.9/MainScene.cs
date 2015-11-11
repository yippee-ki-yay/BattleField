using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tao.OpenGl;

namespace BattleField3._9
{
    class MainScene
    {

        public MainScene()
        {
            Init();
        }

        public void Draw()
        {
            //ocistimo bafer sa bojom
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            float[] pointSizeRange = new float[2];
            Gl.glEnable(Gl.GL_POINT_SMOOTH);
            Gl.glGetFloatv(Gl.GL_POINT_SIZE_RANGE, pointSizeRange);
            Gl.glPointSize(pointSizeRange[1]);

            Gl.glBegin(Gl.GL_POINTS);
            Gl.glVertex2f(0f,-0.5f);
            Gl.glEnd();

            Gl.glFlush();
        }

        private void Init()
        {
            //postavimo pozadinu belu
            Gl.glClearColor(1.0f, 1.0f, 1.0f, 1.0f);

            //zelena boja za iscrtavanje
            Gl.glColor3f(0.0f, 1.0f, 0.0f);

            //Ukljucivanje testiranje dubine
            Gl.glEnable(Gl.GL_DEPTH_TEST);

            //ukljucimo cull_face ne crta ono nazad
            Gl.glEnable(Gl.GL_CULL_FACE);
        }
    }
}
