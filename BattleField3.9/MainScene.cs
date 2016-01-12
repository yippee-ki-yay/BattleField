using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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

        private Tank tank;
        private Ship shipModel;

        private Box wall = null;
        private Box wall2 = null;
        private Box wall3 = null;

        private OutlineFont font = null;

        private int width, height;

        private float rotationX = 0.0f;
        private float rotationY = 0.0f;
        private float sceneDistance = 0.0f;

        private float tankRotation = 0.0f;
        private float shipScale = 0.0f;

        private bool startAnimation = false;

        private enum TextureObjects { Brick = 0, Sea, Sand, Metal};
        private readonly int textureCount = Enum.GetNames(typeof(TextureObjects)).Length;

        private int[] textureId = null;

        private string[] textureFiles = { "..//..//..//brick.jpg", "..//..//..//sea.jpg", "..//..//..//sand.jpg", "..//..//..//rusted_metal.jpg" };

        private String[] text = {"Predmet: Racunarska grafika",
                                 "Sk.god: 2015/2016",
                                 "Ime: Nenad",
                                 "Prezime: Palinkasevic",
                                 "Sifra zad: 3.9"
                                };
        private Box b = new Box();
       
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

        public bool StartAnimiation
        {
            get { return startAnimation; }
            set { startAnimation = value; }
        }

        public float TankRotation
        {
            get { return tankRotation; }
            set { tankRotation = value; }
        }

        public float ShipScale
        {
            get { return shipScale; }
            set { shipScale = value; }
        }

        public float SceneDistance
        {
            get { return sceneDistance; }
            set { sceneDistance = value; }
        }

        public MainScene(int width, int height)
        {

            this.width = width;
            this.height = height;

            tank = new Tank();

            font = new OutlineFont("Verdana", 14, 0.2f, true, false, false, false);

            wall = new Box(16.0f, 4.0f, 1.0f);
            wall2 = new Box(16.0f, 4.0f, 1.0f);
            wall3 = new Box(16.0f, 4.0f, 1.0f);

            textureId = new int[textureCount];

            this.Init(.2f, 0.2f, 0.2f, 1.0f);

            shipModel = new Ship(textureId[(int)TextureObjects.Metal]);



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


            Glu.gluLookAt(1.0f, 5.0f, sceneDistance, 1.0f, 3.0f, -4.0f, 0.0f, 4.0f, 0.0f);

            // Pomeraj objekat po z-osi
            Gl.glTranslatef(0.0f, 0.0f, -5.0f);
            Gl.glRotatef(RotationX, 1.0f, 0.0f, 0.0f);
            Gl.glRotatef(RotationY, 0.0f, 1.0f, 0.0f);

            //skaliranje texture zemlje
            Gl.glMatrixMode(Gl.GL_TEXTURE);
            Gl.glLoadIdentity();
            Gl.glScalef(-10.0f, -10.0f, -10.0f);

            Gl.glMatrixMode(Gl.GL_MODELVIEW);

           //Zemlja
            Gl.glPushMatrix();
                Gl.glColor3ub(0, 145, 45); // tamno zeleno
                Gl.glScalef(0.4f, 0.4f, 0.4f);
                Gl.glBindTexture(Gl.GL_TEXTURE_2D, textureId[(int)TextureObjects.Sand]);
                Gl.glNormal3f(0.0f, 1f, 0.0f);
                Gl.glBegin(Gl.GL_QUADS);

                Gl.glTexCoord2f(0.0f, 0.0f);
                Gl.glVertex3f(10.0f, -0.5f, 10.0f);

                Gl.glTexCoord2f(0.0f, 1.0f);
                Gl.glVertex3f(-10.0f, -0.5f, 10.0f);

                Gl.glTexCoord2f(1.0f, 1.0f);
                Gl.glVertex3f(-10.0f, -0.5f, -10.0f);

                Gl.glTexCoord2f(1.0f, 0.0f);
                Gl.glVertex3f(10.0f, -0.5f, -10.0f);
           
                Gl.glEnd();
            Gl.glPopMatrix();

             //vracanje na pocetnu matricu za teksture
            Gl.glMatrixMode(Gl.GL_TEXTURE);
            Gl.glLoadIdentity();
            Gl.glMatrixMode(Gl.GL_MODELVIEW);


            //skaliranje texture voda
            Gl.glMatrixMode(Gl.GL_TEXTURE);
            Gl.glLoadIdentity();
            Gl.glScalef(-10.0f, -10.0f, -10.0f);

            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            //Voda
            Gl.glPushMatrix();
                Gl.glColor3ub(0, 0, 45);
                Gl.glTranslatef(8.0f, 0f, 0.0f);
                Gl.glScalef(0.4f, 0.4f, 0.4f);

                Gl.glBindTexture(Gl.GL_TEXTURE_2D, textureId[(int)TextureObjects.Sea]);
                Gl.glNormal3f(0.0f, 1f, 0.0f);
                Gl.glBegin(Gl.GL_QUADS);
                    Gl.glTexCoord2f(0.0f, 0.0f);
                    Gl.glVertex3f(10.0f, -0.5f, 10.0f);

                    Gl.glTexCoord2f(0.0f, 1.0f);
                    Gl.glVertex3f(-10.0f, -0.5f, 10.0f);

                    Gl.glTexCoord2f(1.0f, 1.0f);
                    Gl.glVertex3f(-10.0f, -0.5f, -10.0f);

                    Gl.glTexCoord2f(1.0f, 0.0f);
                    Gl.glVertex3f(10.0f, -0.5f, -10.0f);
                Gl.glEnd();
            Gl.glPopMatrix();

            //vracanje na pocetnu matricu za teksture
            Gl.glMatrixMode(Gl.GL_TEXTURE);
            Gl.glLoadIdentity();
            Gl.glMatrixMode(Gl.GL_MODELVIEW);


            Gl.glTexEnvi(Gl.GL_TEXTURE_ENV, Gl.GL_TEXTURE_ENV_MODE, Gl.GL_ADD);

            //Zid 1

            Gl.glPushMatrix();
            Gl.glColor3ub(100, 5, 0);
            Gl.glTranslatef(0.1f, 0.3f, 4.4f);
            Gl.glScalef(0.5f, 0.3f, 0.6f);
            wall.Draw(textureId[(int)TextureObjects.Brick]);
            Gl.glPopMatrix();

            //Zid 2
            Gl.glPushMatrix();
            Gl.glColor3ub(100, 5, 0);
            Gl.glTranslatef(-4.1f, 0.3f, 0.0f);
            Gl.glScalef(0.5f, 0.3f, 0.6f);
            Gl.glRotatef(90, 0f, 1.0f, 0f);
            wall2.Draw(textureId[(int)TextureObjects.Brick]);
            Gl.glPopMatrix();

            //Zid 3
            Gl.glPushMatrix();
            Gl.glColor3ub(100, 5, 0);
            Gl.glTranslatef(0.1f, 0.3f, -4.4f);
            Gl.glScalef(0.5f, 0.3f, 0.6f);
            wall3.Draw(textureId[(int)TextureObjects.Brick]);
            Gl.glPopMatrix();

            tank.Draw(tankRotation);

            shipModel.Draw(shipScale, startAnimation);

            Gl.glTexEnvi(Gl.GL_TEXTURE_ENV, Gl.GL_TEXTURE_ENV_MODE, Gl.GL_DECAL);

            Gl.glPopMatrix();

            Gl.glPushMatrix();
            this.drawTextInfo();
            Gl.glPopMatrix();

            Gl.glFlush();

        }

        private void drawTextInfo()
        {
            //Iscrtavanje teksta

            Gl.glViewport(0, 0, width, height);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluOrtho2D(0, 0, width, height);
            Gl.glColor3f(224.0f, 224.0f, 224.0f);
            Gl.glScalef(0.06f, 0.06f, 0.06f);
            Gl.glTranslatef(2.0f, -16.0f, 0.0f);

            //predmet
            Gl.glPushMatrix();
            Gl.glTranslatef(0.0f, 4.0f, 0.0f);
            //font.DrawText(text[0]);
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

           

            Gl.glLoadIdentity();
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
           
            Resize();

        }

        public void Init(float r, float g, float b, float a)
        {

            float[] ambLight = { r, g, b, a };
            //float[] ambLight = { 0, 255, 0, a };
            
            float[] difLight = { r, g, b, a };
            //float[] spcLight = { 0.95f, 0.95f, 0.95f, 1.0f };
            //zadnja vrednost je 1 pa je reflektorski izvor svetlosti
            float[] lightPos = { 0.0f, 3.0f, 0.0f, 1.0f };
             

            //Ukljucivanje testiranje dubine
            Gl.glEnable(Gl.GL_DEPTH_TEST);

            //ukljucimo cull_face ne crta ono nazad
            Gl.glEnable(Gl.GL_CULL_FACE);

            
            Gl.glTexEnvi(Gl.GL_TEXTURE_ENV, Gl.GL_TEXTURE_ENV_MODE, Gl.GL_DECAL);
            Gl.glEnable(Gl.GL_TEXTURE_2D);

            loadTextures();

            //Ukljucimo color tracking mehanizam
            Gl.glEnable(Gl.GL_COLOR_MATERIAL);

            //definisemo ambijetalnu i difuznu komponentu materijala
            Gl.glColorMaterial(Gl.GL_FRONT_AND_BACK, Gl.GL_AMBIENT_AND_DIFFUSE);

            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glEnable(Gl.GL_NORMALIZE);

            // Podesi osvetljenje
            Gl.glLightModelfv(Gl.GL_LIGHT_MODEL_AMBIENT, ambLight);

            Gl.glLightf(Gl.GL_LIGHT0, Gl.GL_SPOT_CUTOFF, 180.0f);
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, lightPos);

            //Gl.glLightf(Gl.GL_LIGHT1, Gl.GL_SPOT_CUTOFF, 180.0f);
           // Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_POSITION, lightPosShip);


            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_AMBIENT, ambLight);
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_DIFFUSE, difLight);
            //Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_SPECULAR, spcLight);
            Gl.glEnable(Gl.GL_LIGHT0);
           // Gl.glEnable(Gl.GL_LIGHT1);
             
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

        public void Animation(int duration)
        {
            if(startAnimation == true)
            {
                if (duration > 40)
                {
                    shipModel.ShootAnimation();
                }
                else if (duration < 40 && duration > 10)
                {
                    shipModel.removeProjectil();
                    tank.MoveBackAnimation();
                }
                else if (duration < 10 && duration > 0)
                {
                    tank.FlipAnimation();   
                }
                else if(duration == 0)
                {
                    startAnimation = false;
                    tank.Restore();
                    shipModel.Restore();
                }
            }
     
        }

        private void loadTextures()
        {
            Gl.glGenTextures(textureCount, textureId);
            for (int i = 0; i < textureCount; ++i)
            {
                Gl.glBindTexture(Gl.GL_TEXTURE_2D, textureId[i]);

                // Ucitaj sliku i podesi parametre teksture
                Bitmap image = new Bitmap(textureFiles[i]);
 
                image.RotateFlip(RotateFlipType.RotateNoneFlipY);
                Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);

                BitmapData imageData = image.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly,
                                                      System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                Glu.gluBuild2DMipmaps(Gl.GL_TEXTURE_2D, (int)Gl.GL_RGBA8, image.Width, image.Height, Gl.GL_BGRA, Gl.GL_UNSIGNED_BYTE, imageData.Scan0);

                // Podesi parametre teksture
                Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
                Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
                Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_S, Gl.GL_REPEAT);
                Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_T, Gl.GL_REPEAT);

                image.UnlockBits(imageData);
                image.Dispose();
            }

        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Oslodi managed resurse
            }

            // Oslobodi unmanaged resurse
            tank.Dispose();
            shipModel.Dispose();
            font.Dispose();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
