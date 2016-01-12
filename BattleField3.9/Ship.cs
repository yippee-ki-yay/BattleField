using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Tao.OpenGl;

namespace BattleField3._9
{
    class Ship
    {
        private AssimpSceneOld shipModel;
        private Projectil projectil;

        private float translateX, translateY, translateZ;
        private float rotateDegree, rotateX, rotateY, rotateZ;

        private float[] ambLight = { 0.1f, 0.1f, 0.1f, 1.0f};

        public Ship(int id)
        {
            shipModel = new AssimpSceneOld(Path.Combine(Path.GetDirectoryName(
                        Assembly.GetExecutingAssembly().Location), "Resources\\Hamina"), "T-90.obj");

           // shipModel = new AssimpScene(Path.Combine(Path.GetDirectoryName
           //       (Assembly.GetExecutingAssembly().Location), "Resources\\T-90"), "T-90.3DS");

            projectil = new Projectil(id);
            projectil.Alive = true;

            translateX = 6.0f;
            translateY = 0.0f;
            translateZ = -1.2f;

            rotateDegree = -100;

            rotateX = 0.0f;
            rotateY = 1.0f;
            rotateZ = 0.0f;

        }




        public void Draw(float scale, bool inAnimation)
        {

            float[] lightPosShip = { translateX, translateY + 5.0f, translateZ, 1.0f };

            //iscrtavanje svetla
            Gl.glLightf(Gl.GL_LIGHT1, Gl.GL_SPOT_CUTOFF, 180.0f);
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_POSITION, lightPosShip);
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_DIFFUSE, new float[] { 1, 1, 0, 1 });
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_AMBIENT, ambLight);

            Gl.glEnable(Gl.GL_LIGHT1);
            
            //iscrtavanja broda
            Gl.glPushMatrix();
                Gl.glTranslatef(translateX, translateY, translateZ);
                Gl.glScalef(0.1f + scale, 0.1f + scale, 0.1f + scale);
                Gl.glRotatef(rotateDegree, rotateX, rotateY, rotateZ);
                shipModel.Draw();
            Gl.glPopMatrix();

            projectil.Draw(inAnimation);
        }

        public void Restore()
        {
            translateX = 6.0f;
            translateY = 0.0f;
            translateZ = -1.2f;

            rotateDegree = -100;

            rotateX = 0.0f;
            rotateY = 1.0f;
            rotateZ = 0.0f;

            projectil.Restore();
        }

        public void ShootAnimation()
        {
            projectil.Move();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void removeProjectil()
        {
            projectil.Alive = false;
        }

        ~Ship()
        {
            this.Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            shipModel.Dispose();
        }
    }
}
