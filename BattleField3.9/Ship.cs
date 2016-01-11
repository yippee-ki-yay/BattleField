﻿using System;
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
        private AssimpScene shipModel;
        private Projectil projectil;

        private float translateX, translateY, translateZ;
        private float rotateDegree, rotateX, rotateY, rotateZ;

        public Ship(int id)
        {
            shipModel = new AssimpScene(Path.Combine(Path.GetDirectoryName(
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
