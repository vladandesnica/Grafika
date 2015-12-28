using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;

namespace RacunarskaGrafika.Vezbe.AssimpNetSample
{
    public class Hangar
    {
         #region Atributi

        private int[] m_textures = null;

        /// <summary>
        ///	 Putanje do slika koje se koriste za teksture
        /// </summary>
      //  private string m_textureFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Textures\\wood_texture33.jpg");

        private string m_textureFile = @"C:\Users\Vale\Documents\GitHub\Grafika-Proj\03\Textures\wood_texture33.jpg";

        /// <summary>
        ///	 Visina kvadra.
        /// </summary>
        double m_height = 1.0;

        /// <summary>
        ///	 Sirina kvadra.
        /// </summary>
        double m_width = 1.0;

        double door_height;
        bool door_closed = true;

        /// <summary>
        ///	 Dubina kvadra.
        /// </summary>
        double m_depth = 1.0;

        #endregion Atributi

        #region Properties

        /// <summary>
        ///	 Visina kvadra.
        /// </summary>
        /// 
        public double Door_height
        {
            get { return door_height; }
            set { door_height = value; }
        }


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

        public bool Door_Closed
        {
            get { return door_closed; }
            set { door_closed = value; }
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
        public Hangar()
        {
           
        }

        /// <summary>
        ///		Konstruktor sa parametrima.
        /// </summary>
        /// <param name="width">Sirina kvadra.</param>
        /// <param name="height">Visina kvadra.</param>
        /// <param name="depth"></param>
        public Hangar(double width, double height, double depth)
        {
            Init(); 
            this.m_width = width;
            this.m_height = height;
            this.m_depth = depth;
            this.door_height = -height / 2;
            this.door_closed = true;

            
        }

        #endregion Konstruktori

        #region Metode

        public void Draw()
        {
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, m_textures[0]);
            Gl.glColor3ub(160, 160, 160);
            Gl.glBegin(Gl.GL_QUADS);

            // Zadnja cv
          //  Gl.glColor3ub(0, 255, 255);
            Gl.glTexCoord2f(0.0f, 0.0f);
            Gl.glVertex3d(-m_width / 2, -m_height / 2, -m_depth / 2);
            Gl.glTexCoord2f(0.0f, 1.0f);
            Gl.glVertex3d(-m_width / 2, m_height / 2, -m_depth / 2);
            Gl.glTexCoord2f(1.0f, 1.0f);
            Gl.glVertex3d(m_width / 2, m_height / 2, -m_depth / 2);
            Gl.glTexCoord2f(1.0f, 0.0f);
            Gl.glVertex3d(m_width / 2, -m_height / 2, -m_depth / 2);

           // 
            // Desna ccw
            Gl.glTexCoord2f(0.0f, 0.0f);
            Gl.glVertex3d(m_width / 2, -m_height / 2, -m_depth / 2);
            Gl.glTexCoord2f(0.0f, 1.0f);
            Gl.glVertex3d(m_width / 2, m_height / 2, -m_depth / 2);
            Gl.glTexCoord2f(1.0f, 1.0f);
            Gl.glVertex3d(m_width / 2, m_height / 2, m_depth / 2);
            Gl.glTexCoord2f(1.0f, 0.0f);
            Gl.glVertex3d(m_width / 2, -m_height / 2, m_depth / 2);

            // Prednja cv
            Gl.glTexCoord2f(0.0f, 0.0f);
            Gl.glVertex3d(m_width / 2, -m_height / 2, m_depth / 2);
            Gl.glTexCoord2f(0.0f, 1.0f);
            Gl.glVertex3d(m_width / 2, m_height / 2, m_depth / 2);
            Gl.glTexCoord2f(1.0f, 1.0f);
            Gl.glVertex3d(m_width / 4, m_height / 2, m_depth / 2);
            Gl.glTexCoord2f(1.0f, 0.0f);
            Gl.glVertex3d(m_width / 4, -m_height / 2, m_depth / 2);

            
            //Vrata ccw
           // door_height = m_height - 4;
          //  door_closed = true;
            if(door_height >= m_height/2 -4)
            {
                door_height = m_height/2 -5;
             //   break;
            }

            else if (door_height > -m_height / 2)
            {
                door_closed = false;
            }
            
            else
            {
                door_closed = true;
                door_height = -m_height / 2;
            }

            if (!door_closed)
            {
                //
                Gl.glPushMatrix();
                Gl.glDisable(Gl.GL_CULL_FACE);
                Gl.glEnable(Gl.GL_DEPTH_TEST);
                Gl.glPopMatrix();
            }
            else
            {
                Gl.glPushMatrix();
                Gl.glEnable(Gl.GL_CULL_FACE);
                Gl.glPopMatrix();
            }


            Gl.glColor3ub(255, 0, 0);
            Gl.glVertex3d(m_width / 4, door_height, m_depth / 2);
            Gl.glVertex3d(m_width / 4, m_height / 2 - 4, m_depth / 2);
            Gl.glVertex3d(-m_width / 4, m_height / 2 -4, m_depth / 2);
            Gl.glVertex3d(-m_width / 4, door_height, m_depth / 2);

            Gl.glColor3ub(160, 160, 160);

            
            //Nastavak prednje cw
            Gl.glTexCoord2f(0.0f, 0.0f);
            Gl.glVertex3d(-m_width / 4, -m_height / 2, m_depth / 2);
            Gl.glTexCoord2f(0.0f, 1.0f);
            Gl.glVertex3d(-m_width / 4, m_height / 2, m_depth / 2);
            Gl.glTexCoord2f(1.0f, 1.0f);
            Gl.glVertex3d(-m_width / 2, m_height / 2, m_depth / 2);
            Gl.glTexCoord2f(1.0f, 0.0f);
            Gl.glVertex3d(-m_width / 2, -m_height / 2,m_depth/2);

            Gl.glTexCoord2f(0.0f, 0.0f);
            Gl.glVertex3d(m_width / 4, m_height / 2 - 4, m_depth / 2);
            Gl.glTexCoord2f(0.0f, 1.0f);
            Gl.glVertex3d(m_width / 4, m_height / 2, m_depth / 2);
            Gl.glTexCoord2f(1.0f, 1.0f);
            Gl.glVertex3d(-m_width / 4, m_height / 2, m_depth / 2);
            Gl.glTexCoord2f(1.0f, 0.0f);
            Gl.glVertex3d(-m_width / 4, m_height / 2 - 4, m_depth / 2);

           


            // Leva cw
            Gl.glTexCoord2f(0.0f, 0.0f);
            Gl.glVertex3d(-m_width / 2, -m_height / 2, m_depth / 2);
            Gl.glTexCoord2f(0.0f, 1.0f);
            Gl.glVertex3d(-m_width / 2, m_height / 2, m_depth / 2);
            Gl.glTexCoord2f(1.0f, 1.0f);
            Gl.glVertex3d(-m_width / 2, m_height / 2, -m_depth / 2);
            Gl.glTexCoord2f(1.0f, 0.0f);
            Gl.glVertex3d(-m_width / 2, -m_height / 2, -m_depth / 2);

            //donja
           // Gl.glColor3ub(220, 220, 220);
          //  Gl.glVertex3d(-m_width , -m_height/2 , -m_depth );
         //   Gl.glVertex3d(m_width , -m_height/2 , -m_depth );
         //   Gl.glVertex3d(m_width, -m_height/2 , m_depth );
          //  Gl.glVertex3d(-m_width, -m_height/2 , m_depth );
           
            //Gl.glColor3ub(160, 160, 160);
            //Gornja
            Gl.glTexCoord2f(0.0f, 0.0f);
            Gl.glVertex3d(-m_width / 2, m_height / 2, -m_depth / 2);
            Gl.glTexCoord2f(0.0f, 1.0f);
            Gl.glVertex3d(-m_width / 2, m_height / 2, m_depth / 2);
            Gl.glTexCoord2f(1.0f, 1.0f);
            Gl.glVertex3d(m_width / 2, m_height / 2, m_depth / 2);
            Gl.glTexCoord2f(1.0f, 0.0f);
            Gl.glVertex3d(m_width / 2, m_height / 2, -m_depth / 2);

            Gl.glEnd();


            Gl.glDisable(Gl.GL_TEXTURE_2D);


          
            Gl.glPushMatrix();
            double zaKrov = m_height / 2 ;
            Gl.glTranslatef(0.0f, (float)zaKrov, 0.0f);
            Gl.glEnable(Gl.GL_CULL_FACE);
            Krov krov = new Krov(m_width, m_height, m_depth);
            krov.Draw();
          //  Gl.glRotatef(15.0f, 0.0f, 1.0f, 0.0f);
            
            Gl.glPopMatrix();
             
        }

        public void SetSize(double width, double height, double depth)
        {
            m_depth = depth;
            m_height = height;
            m_width = width;
        }

        public void Init()
        {
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

