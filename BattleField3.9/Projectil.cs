﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;

namespace BattleField3._9
{
    class Projectil
    {
        private Box projectile;
        private float translateX, translateY, translateZ;
        private int id;
        public Boolean Alive{get; set;}

        public Projectil(int id)
        {
            projectile = new Box(2.0f, 0.5f, 1.0f);
            translateX = 6.34f;
            translateY = 0.16f;
            translateZ = -0.88f;
            this.id = id;
        }

        public void Restore()
        {
            Alive = true;
            translateX = 6.34f;
            translateY = 0.16f;
            translateZ = -0.88f;
        }

        public void Draw(bool inAnimation)
        {

            if(inAnimation && Alive)
            {
                //projektil
                Gl.glPushMatrix();
                Gl.glColor3ub(255, 255, 0);
                Gl.glTranslatef(translateX, translateY, translateZ);
                Gl.glRotatef(-10, 0f, 1.0f, 0f);
                Gl.glScalef(0.2f, 0.07f, 0.05f);
                projectile.Draw(id);
                Gl.glPopMatrix();
            }
      
        }

        public void Move()
        {
            translateX -= 0.15f;
        }
    }
}
