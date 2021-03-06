﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Tao.OpenGl;

namespace BattleField3._9
{
    class Tank
    {
        private AssimpSceneOld tankModel;

        private float translateX, translateY, translateZ;
        private float rotateDegree, rotateX, rotateY, rotateZ;

        public Tank()
        {
            tankModel = new AssimpSceneOld(Path.Combine(Path.GetDirectoryName
                  (Assembly.GetExecutingAssembly().Location), "Resources\\T-90"), "T-90.3DS");

            translateX = 1.5f;
            translateY = -0.2f;
            translateZ = -1.2f;

           // rotateDegree = -100;

            rotateX = 0.0f;
            rotateY = 1.0f;
            rotateZ = 0.0f;

        }

        public void Draw(float rotate)
        {

            //iscrtavanje tenka
            Gl.glPushMatrix();
            Gl.glTexEnvi(Gl.GL_TEXTURE_ENV, Gl.GL_TEXTURE_ENV_MODE, Gl.GL_ADD);
                Gl.glTranslatef(translateX, translateY, translateZ);
                Gl.glScalef(0.2f, 0.2f, 0.2f);
                Gl.glRotatef(rotateDegree + rotate, rotateX, rotateY, rotateZ);
                tankModel.Draw();
            Gl.glTexEnvi(Gl.GL_TEXTURE_ENV, Gl.GL_TEXTURE_ENV_MODE, Gl.GL_DECAL);
            Gl.glPopMatrix();
        }

        public void MoveBackAnimation()
        {
            translateX -= 0.02f;
        }

        public void FlipAnimation()
        {
            rotateY = 0.0f;
            rotateZ = 1.0f;
            rotateDegree += 15f;
        }

        public void Restore()
        {
            translateX = 1.5f;
            translateY = -0.2f;
            translateZ = -1.2f;

             rotateDegree = 0;

            rotateX = 0.0f;
            rotateY = 1.0f;
            rotateZ = 0.0f;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Tank()
        {
            this.Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            tankModel.Dispose();
        }

    }
}
