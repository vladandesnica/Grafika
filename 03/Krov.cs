using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;
using System.Drawing;
using System.Drawing.Imaging;

namespace RacunarskaGrafika.Vezbe.AssimpNetSample
{
    class Krov
    {
         #region Atributi

        private int[] m_textures = null;

        /// <summary>
        ///	 Putanje do slika koje se koriste za teksture
        /// </summary>
        //  private string m_textureFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Textures\\wood_texture33.jpg");

        private string m_textureFile = @"C:\Users\Vale\Documents\GitHub\Grafika-Proj\03\Textures\metal-texture.jpg";
        /// <summary>
        ///	 Visina kvadra.
        /// </summary>
        double m_height = 1.0;

        /// <summary>
        ///	 Sirina kvadra.
        /// </summary>
        double m_width = 1.0;

        /// <summary>
        ///	 Dubina kvadra.
        /// </summary>
        double m_depth = 1.0;

        #endregion Atributi

        #region Properties

        /// <summary>
        ///	 Visina kvadra.
        /// </summary>
        public double Height
        {
            get { return m_height; }
            set { m_height = value; }
        }

        /// <summary>
        ///	 Sirina kvadra.
        /// </summary>
        public double Width
        {
            get { return m_width; }
            set { m_width = value; }
        }

        /// <summary>
        ///	 Dubina kvadra.
        /// </summary>
        public double Depth
        {
            get { return m_depth; }
            set { m_depth = value; }
        }

        #endregion Properties

        #region Konstruktori

        /// <summary>
        ///		Konstruktor.
        /// </summary>
        public Krov()
        {
        }

        /// <summary>
        ///		Konstruktor sa parametrima.
        /// </summary>
        /// <param name="width">Sirina kvadra.</param>
        /// <param name="height">Visina kvadra.</param>
        /// <param name="depth"></param>
        public Krov(double width, double height, double depth)
        {
            Init();
            this.m_width = width;
            this.m_height = height;
            this.m_depth = depth;
        }

        #endregion Konstruktori

        #region Metode

        public void Draw()
        {
            Gl.glBegin(Gl.GL_TRIANGLES);
            Gl.glColor3ub(255, 0, 0);
            // Zadnja
            Gl.glVertex3d(-m_width / 2, 0, -m_depth / 2);
            Gl.glVertex3d(0, m_height / 4, -m_depth / 2);
            Gl.glVertex3d(m_width / 2, 0, -m_depth / 2);
           // Gl.glVertex3d(m_width / 2, -m_height / 2, -m_depth / 2);
            Gl.glEnd();
            
            // Desna

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(m_width / 2, 0, -m_depth / 2);
            Gl.glVertex3d(0, m_height / 4, -m_depth / 2);
            Gl.glVertex3d(0, m_height / 4, m_depth / 2);
            Gl.glVertex3d(m_width / 2, 0, m_depth / 2);
            Gl.glEnd();
           
            // Prednja
            Gl.glBegin(Gl.GL_TRIANGLES);
            Gl.glVertex3d(m_width / 2, 0, m_depth / 2);
            Gl.glVertex3d(0, m_height / 4, m_depth / 2);
            Gl.glVertex3d(-m_width / 2, 0, m_depth / 2);
            Gl.glEnd();
            // Leva
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(-m_width / 2, 0, m_depth / 2);
            Gl.glVertex3d(0, m_height / 4, m_depth / 2);
            Gl.glVertex3d(0, m_height / 4, -m_depth / 2);
            Gl.glVertex3d(-m_width / 2, 0, -m_depth / 2);
            Gl.glEnd();

            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, m_textures[0]);

            Gl.glColor3ub(255, 255, 255);
            Glu.GLUquadric gluObject = Glu.gluNewQuadric(); // U stvari kupa, jer je jedan poluprecnik = 0 
            Glu.gluQuadricTexture(gluObject, Gl.GL_TRUE);
            Gl.glPushMatrix();
         //  Gl.glRotatef(-13.0f, 0.0f, 1.0f, 1.0f);
          

            Gl.glTranslatef( (float)m_width / 6, (float)m_height / 8, (float)m_depth/6);
            Gl.glRotatef(-90.0f, 0.0f, 1.0f, 1.0f);
              Gl.glRotatef(-55.0f, 0.0f, 1.0f, 0.0f);
              Gl.glRotatef(-32.0f, 1.0f, 0.0f, 0.0f);
            //  Gl.glRotatef(-20.0f, 0.0f, 0.0f, 1.0f);
            Glu.gluCylinder(gluObject, 2.5f, 2.5f, 12.0f, 300, 300); 
            Gl.glPopMatrix();

            Glu.gluDeleteQuadric(gluObject);

          
            Glu.GLUquadric gluObject2 = Glu.gluNewQuadric();
            Glu.gluQuadricTexture(gluObject2, Gl.GL_TRUE);
            Gl.glPushMatrix();
          
          Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glTranslatef((float)m_width / 6, (float)m_height / 8 + 11, (float)m_depth / 6);
            Gl.glRotatef(60.0f, 1.0f, 0.0f, 0.0f);
            Glu.gluDisk(gluObject2, 2.5f, 8.5f, 300, 300);
            Gl.glPopMatrix();
           // Gl.glDisable(Gl.GL_TEXTURE_2D);
            Glu.gluDeleteQuadric(gluObject2);

           // Gl.glColor3ub(0, 0, 255);
            Glu.GLUquadric gluObject3 = Glu.gluNewQuadric();
            Glu.gluQuadricTexture(gluObject3, Gl.GL_TRUE);
            Gl.glPushMatrix();
           // Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glTranslatef((float)m_width / 6, (float)m_height / 8 + 11, (float)m_depth / 6);
            Gl.glRotatef(-120.0f, 1.0f, 0.0f, 0.0f);
            Glu.gluDisk(gluObject3, 2.5f, 8.5f, 300, 300);
            Gl.glPopMatrix();
            Glu.gluDeleteQuadric(gluObject3);

            Gl.glDisable(Gl.GL_TEXTURE_2D);
            
        }

        public void SetSize(double width, double height, double depth)
        {
            m_depth = depth;
            m_height = height;
            m_width = width;
        }

        public void Init()
        {
            // Gl.glEnable(Gl.GL_TEXTURE_2D);
            
            m_textures = new int[1];
            // Ucitaj slike i kreiraj teksture

            Gl.glGenTextures(1, m_textures);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, m_textures[0]);

            // Ucitaj sliku i podesi parametre teksture
            Bitmap image = new Bitmap(m_textureFile);
            // rotiramo sliku zbog koordinantog sistema opengl-a
            image.RotateFlip(RotateFlipType.RotateNoneFlipY);
            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
            // RGBA format (dozvoljena providnost slike tj. alfa kanal)
            BitmapData imageData = image.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly,
                                                  System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            Glu.gluBuild2DMipmaps(Gl.GL_TEXTURE_2D, (int)Gl.GL_RGBA8, image.Width, image.Height, Gl.GL_BGRA, Gl.GL_UNSIGNED_BYTE, imageData.Scan0);


            Gl.glTexParameteri((int)Gl.GL_TEXTURE_2D, (int)Gl.GL_TEXTURE_MIN_FILTER, (int)Gl.GL_LINEAR);		// Linear Filtering
            Gl.glTexParameteri((int)Gl.GL_TEXTURE_2D, (int)Gl.GL_TEXTURE_MAG_FILTER, (int)Gl.GL_LINEAR);		// Linear Filtering
            Gl.glTexParameterf(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_S, Gl.GL_REPEAT);
            Gl.glTexParameterf(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_T, Gl.GL_REPEAT);
            Gl.glTexEnvi(Gl.GL_TEXTURE_ENV, Gl.GL_TEXTURE_ENV_MODE, Gl.GL_MODULATE);

            image.UnlockBits(imageData);
            image.Dispose();


        }
        #endregion Metode
    }
}
    

