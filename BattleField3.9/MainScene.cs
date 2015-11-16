using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tao.OpenGl;

namespace BattleField3._9
{
    class MainScene : IDisposable
    {

        private AssimpScene tankModel = null;
        private AssimpScene shipModel = null;

        private Box wall = null;
        private Box projectile = null;

        private OutlineFont font = null;

        private int width, height;

        private float rotationX = 0.0f;
        private float rotationY = 0.0f;

        private String[] text = {"Predmet: Racunarska grafika",
                                 "Sk.god: 2015/2016",
                                 "Ime: Nenad",
                                 "Prezime: Palinkasevic",
                                 "Sifra zad: 3.9"
                                };

        public float RotationX 
        {
            get { return rotationX; }
            set { rotationX = value; }
        }

        public float RotationY
        {
            get { return rotationY; }
            set { rotationY = value; }
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        public MainScene(int width, int height)
        {
            this.Init();

            this.width = width;
            this.height = height;

            tankModel = new AssimpScene(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Resources\\T-90"), "T-90.3DS");

            shipModel = new AssimpScene(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Resources\\Hamina"), "Hamina.3DS");

            font = new OutlineFont("Verdana", 14, 0.2f, true, false, false, false);

            wall = new Box(16.0f, 4.0f, 1.0f);

            projectile = new Box(2.0f, 1.0f, 1.0f);

            this.Resize();
        }

        ~MainScene()
        {
            this.Dispose(false);
        }

        public void Draw()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            Gl.glPushMatrix();

            // Pomeraj objekat po z-osi
            Gl.glTranslatef(0.0f, 0.0f, -5.0f);
            Gl.glRotatef(RotationX, 1.0f, 0.0f, 0.0f);
            Gl.glRotatef(RotationY, 0.0f, 1.0f, 0.0f);

           //Zemlja
            Gl.glPushMatrix();
                Gl.glColor3ub(0, 145, 45); // tamno zeleno
                Gl.glScalef(0.4f, 0.4f, 0.4f);
                Gl.glBegin(Gl.GL_QUADS);
                Gl.glVertex3f(10.0f, -0.5f, 10.0f);
                Gl.glVertex3f(-10.0f, -0.5f, 10.0f);
                Gl.glVertex3f(-10.0f, -0.5f, -10.0f);
                Gl.glVertex3f(10.0f, -0.5f, -10.0f);
                Gl.glEnd();
            Gl.glPopMatrix();

            //Voda
            Gl.glPushMatrix();
                Gl.glColor3ub(0, 0, 45);
                Gl.glTranslatef(3.0f, 0f, -0.5f);
                Gl.glScalef(0.4f, 0.4f, 0.4f);
                Gl.glBegin(Gl.GL_QUADS);
                Gl.glVertex3f(10.0f, -0.5f, 10.0f);
                Gl.glVertex3f(-10.0f, -0.5f, 10.0f);
                Gl.glVertex3f(-10.0f, -0.5f, -10.0f);
                Gl.glVertex3f(10.0f, -0.5f, -10.0f);
                Gl.glEnd();
            Gl.glPopMatrix();

            //Zid
            Gl.glPushMatrix();
                Gl.glColor3ub(100, 0, 0);
                Gl.glTranslatef(0.0f, 0.0f, 6.0f);
                Gl.glScalef(0.6f, 0.6f, 0.6f);
                wall.Draw();
            Gl.glPopMatrix();

            //iscrtavanje tenka
            Gl.glPushMatrix();
              Gl.glTranslatef(1.0f, -0.5f, 5.0f);
              Gl.glScalef(0.2f, 0.2f, 0.2f);
              Gl.glRotatef(160, 0.0f, 1.0f, 0.0f);
              tankModel.Draw(); 
            Gl.glPopMatrix();

            //iscrtavanja broda
            Gl.glPushMatrix();
                Gl.glTranslatef(5.0f, -0.5f, 1.0f);
                Gl.glScalef(0.02f, 0.02f, 0.02f);
                Gl.glRotatef(90, 0.0f, 1.0f, 0.0f);
                shipModel.Draw();
            Gl.glPopMatrix();

            Gl.glPopMatrix();

            this.drawTextInfo();

            Gl.glEnable(Gl.GL_DEPTH_TEST);

        }

        private void drawTextInfo()
        {
            //Iscrtavanje teksta

            Gl.glViewport(0, 0, width, height);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluOrtho2D(0, 0, width, height);
            Gl.glColor3f(255.0f, 255.0f, 255.0f);
            Gl.glScalef(0.06f, 0.06f, 0.06f);
            Gl.glTranslatef(2.0f, -16.0f, 0.0f);

            //predmet
            Gl.glPushMatrix();
            Gl.glTranslatef(0.0f, 4.0f, 0.0f);
            font.DrawText(text[0]);
            Gl.glPopMatrix();

            //ime
            Gl.glPushMatrix();
            Gl.glTranslatef(0.0f, 3.0f, 0.0f);
            font.DrawText(text[1]);
            Gl.glPopMatrix();

            //prezime
            Gl.glPushMatrix();
            Gl.glTranslatef(0.0f, 2.0f, 0.0f);
            font.DrawText(text[2]);
            Gl.glPopMatrix();

            //skolska godina
            Gl.glPushMatrix();
            Gl.glTranslatef(0.0f, 1.0f, 0.0f);
            font.DrawText(text[3]);
            Gl.glPopMatrix();

            //sifra
            Gl.glPushMatrix();
            Gl.glTranslatef(0.0f, 0.0f, 0.0f);
            font.DrawText(text[4]);
            Gl.glPopMatrix();

            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Resize();

        }

        private void Init()
        {
            //postavimo pozadinu belu
          //  Gl.glClearColor(1.0f, 1.0f, 1.0f, 1.0f);

            //zelena boja za iscrtavanje
            Gl.glColor3f(0.0f, 1.0f, 0.0f);

            //Ukljucivanje testiranje dubine
            Gl.glEnable(Gl.GL_DEPTH_TEST);

            //ukljucimo cull_face ne crta ono nazad
            Gl.glEnable(Gl.GL_CULL_FACE);
        }

        public void Resize()
        {
            Gl.glViewport(0, 0, width, height); // kreiraj viewport po celom prozoru
            Gl.glMatrixMode(Gl.GL_PROJECTION);      // selektuj Projection Matrix
            Gl.glLoadIdentity();			        // resetuj Projection Matrix

            Glu.gluPerspective(45.0, (double)width / (double)height, 1.0, 20000.0);

            Gl.glMatrixMode(Gl.GL_MODELVIEW);   // selektuj ModelView Matrix
            Gl.glLoadIdentity();  

        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Oslodi managed resurse
            }

            // Oslobodi unmanaged resurse
            tankModel.Dispose();
            shipModel.Dispose();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
