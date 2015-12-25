// -----------------------------------------------------------------------
// <file>World.cs</file>
// <copyright>Grupa za Grafiku, Interakciju i Multimediju 2013.</copyright>
// <author>Zoran Milicevic</author>
// <summary>Klasa koja enkapsulira OpenGL programski kod.</summary>
// -----------------------------------------------------------------------
namespace RacunarskaGrafika.Vezbe.AssimpNetSample
{
    using System;
    using Tao.OpenGl;
    using Assimp;
    using System.IO;
    using System.Reflection;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Windows.Forms;

    /// <summary>
    ///  Klasa enkapsulira OpenGL kod i omogucava njegovo iscrtavanje i azuriranje.
    /// </summary>
    public class World : IDisposable
    {
        #region Atributi

        /// <summary>
        ///	 Scena koja se prikazuje.
        /// </summary>
        private AssimpScene m_scene;

        /// <summary>
        ///	 Ugao rotacije sveta oko X ose.
        /// </summary>
        private float m_xRotation = 0.0f;

        private float m_XTranslation = 0.0f;
        private float m_YTranslation = 0.0f;

        private float m_ZTranslation = 0.0f;
        /// <summary>
        ///	 Ugao rotacije sveta oko Y ose.
        /// </summary>
        private float m_yRotation = 0.0f;

        /// <summary>
        ///	 Udaljenost scene od kamere.
        /// </summary>
        private float m_sceneDistance = 150.0f;

        /// <summary>
        ///	 Sirina OpenGL kontrole u pikselima.
        /// </summary>
        private int m_width;

        /// <summary>
        ///	 Visina OpenGL kontrole u pikselima.
        /// </summary>
        private int m_height;

        private BitmapFont m_font = null;

        /// <summary>
        ///	 Poruka koja se ispisuje u izabranom fontu.
        /// </summary>
        private String m_message1 = "Predmet: Racunarska grafika";
        private String m_message2 = "Sk.god: 2015/16.";
        private String m_message3 = "Ime: Vladan";
        private String m_message4 = "Prezime: Desnica";
        private String m_message5 = "Sifra zad: 8.4";

        private bool errorInfoShown = false;

        private Hangar hangar = null;

        #endregion Atributi

        #region Properties

        /// <summary>
        ///  Poruka koja se ispisuje.
        /// </summary>
      /*  public String Message
        {
            get { return m_message; }
            set { m_message = value; }
        }
       */
        public float XTranslation
        {
            get { return m_XTranslation; }
            set { m_XTranslation = value; }
        }

        public float YTranslation
        {
            get { return m_YTranslation; }
            set { m_YTranslation = value; }
        }

        public float ZTranslation
        {
            get { return m_ZTranslation; }
            set { m_ZTranslation = value; }
        }


        /// <summary>
        ///	 Scena koja se prikazuje.
        /// </summary>
        public AssimpScene Scene
        {
            get { return m_scene; }
            set { m_scene = value; }
        }

        /// <summary>
        ///	 Hangar koja se prikazuje.
        /// </summary>
        public Hangar Hangar{
            get { return this.hangar; }
            set { this.hangar = value; }
        }
        

        /// <summary>
        ///	 Ugao rotacije sveta oko X ose.
        /// </summary>
        public float RotationX
        {
            get { return m_xRotation; }
            set { m_xRotation = value; }
        }

        /// <summary>
        ///	 Ugao rotacije sveta oko Y ose.
        /// </summary>
        public float RotationY
        {
            get { return m_yRotation; }
            set { m_yRotation = value; }
        }

        /// <summary>
        ///	 Udaljenost scene od kamere.
        /// </summary>
        public float SceneDistance
        {
            get { return m_sceneDistance; }
            set { m_sceneDistance = value; }
        }

        /// <summary>
        ///	 Sirina OpenGL kontrole u pikselima.
        /// </summary>
        public int Width
        {
            get { return m_width; }
            set { m_width = value; }
        }

        /// <summary>
        ///	 Visina OpenGL kontrole u pikselima.
        /// </summary>
        public int Height
        {
            get { return m_height; }
            set { m_height = value; }
        }

        #endregion Properties

        #region Konstruktori

        /// <summary>
        ///  Konstruktor klase World.
        /// </summary>
        public World(String scenePath, String sceneFileName,int width, int height)
        {
            //this.m_font = font;
            this.m_scene = new AssimpScene(scenePath, sceneFileName);
            this.m_width = width;
            this.m_height = height;

            try
            {
                m_font = new BitmapFont("Verdana", 12, true, false, true, false);
                this.hangar = new Hangar(65, 36, 75);
            }
            catch (Exception)
            {
                MessageBox.Show("Neuspesno kreirana instanca OpenGL fonta", "GRESKA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Initialize();  // Korisnicka inicijalizacija OpenGL parametara

            this.Resize();      // Podesi projekciju i viewport
        }

        /// <summary>
        ///  Destruktor klase World.
        /// </summary>
        ~World()
        {
            this.Dispose(false);
        }

        #endregion Konstruktori

        #region Metode

        /// <summary>
        ///  Iscrtavanje OpenGL kontrole.
        /// </summary>
        public void Draw()
        {
            

            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            Gl.glViewport(0, 0, m_width, m_height);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluPerspective(45.0, (double)m_width / (double)m_height, 1.0, 20000.0);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);

            Gl.glPushMatrix();
            
            //Transformacije nad citavom scenom
            Gl.glTranslatef(0.0f, 0.0f, -m_sceneDistance);
            Gl.glRotatef(m_xRotation, 1.0f, 0.0f, 0.0f);
            Gl.glRotatef(m_yRotation, 0.0f, 1.0f, 0.0f);

           #region Podloga_Mesec

           Gl.glPushMatrix();

           Gl.glDisable(Gl.GL_CULL_FACE);
    
           Gl.glColor3ub(220,220,220);
           Gl.glBegin(Gl.GL_QUADS);

           Gl.glColor3ub(220, 220, 220);
           Gl.glVertex3d(-100.0f, -36.0f / 2, -100.0f);
           Gl.glVertex3d(100.0f, -36.0f / 2, -100.0f);
           Gl.glVertex3d(100.0f, -36.0f / 2, 20.0f);
           Gl.glVertex3d(-100.0f, -36.0f / 2, 20.0f);
        
           Gl.glEnd();

           Gl.glPopMatrix();

           //Sjaj mjesece
           Gl.glPushMatrix();
           Gl.glTranslatef(100.0f, 75.0f, -60.0f);
           Gl.glScalef(0.5f, 0.5f, 0.5f);
           Gl.glColor3ub(252, 252, 219); // bledo zuta boja
           Glu.GLUquadric  m_gluObj = Glu.gluNewQuadric();
           Glu.gluQuadricNormals(m_gluObj, Glu.GLU_SMOOTH);
           Glu.gluSphere(m_gluObj, 5.0f, 640, 640);
           Gl.glPopMatrix();

           #endregion Podloga_Nebo 

           #region Hangar
           Gl.glPushMatrix();
            
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glEnable(Gl.GL_CULL_FACE);

            Gl.glColor3ub(255, 0, 0);
            Gl.glTranslatef(20.0f, 0.0f, -45.0f);
    
            if (!hangar.Door_Closed)
            {
               Gl.glDisable(Gl.GL_CULL_FACE);
            }           
           
            this.hangar.Draw();
            
            
            Gl.glPopMatrix();

           #endregion Hangar

            #region Model
            // Sacuvaj stanje modelview matrice i primeni transformacije
            
            Gl.glPushMatrix();
            
            if (!hangar.Door_Closed)
            {
                Gl.glDisable(Gl.GL_CULL_FACE);
            }

            Gl.glTranslatef(17.0f, -5.0f, 0.0f);
            Gl.glTranslatef(m_XTranslation, 0.0f, 0.0f);
            Gl.glTranslatef(0.0f, m_YTranslation, 0.0f);
            Gl.glTranslatef(0.0f, 0.0f, m_ZTranslation);

            Gl.glScalef(0.02f, 0.02f, 0.02f);
            Gl.glRotatef(90.0f, 1.0f, 0.0f, 0.0f);

            m_scene.Draw();
            Gl.glPopMatrix();
            
           
          
            Gl.glPopMatrix();

            #endregion Model

            

            #region text
            Gl.glViewport(0, 0, m_width, m_height);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            
            Gl.glLoadIdentity();
            Glu.gluOrtho2D(-m_width / 2.0, m_width / 2.0, -m_height / 2.0, m_height / 2.0);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glColor3ub(0, 191, 255);
            Gl.glRasterPos2f(- m_width/ 2.0f, -m_height / 2.0f + m_font.CalculateTextHeight(m_message5));
            m_font.DrawText(m_message5);
            Gl.glRasterPos2f(-m_width / 2.0f, -m_height / 2.0f + 2*m_font.CalculateTextHeight(m_message4));
            m_font.DrawText(m_message4);
            Gl.glRasterPos2f(-m_width / 2.0f, -m_height / 2.0f + 3*m_font.CalculateTextHeight(m_message3));
            m_font.DrawText(m_message3);
            Gl.glRasterPos2f(-m_width / 2.0f, -m_height / 2.0f + 4*m_font.CalculateTextHeight(m_message2));
            m_font.DrawText(m_message2);
            Gl.glRasterPos2f(-m_width / 2.0f, -m_height / 2.0f + 5*m_font.CalculateTextHeight(m_message1));
            m_font.DrawText(m_message1);

            Gl.glPopMatrix();

            #endregion text

            // Oznaci kraj iscrtavanja
            Gl.glFlush();
        }

        /// <summary>
        ///  Korisnicka inicijalizacija i podesavanje OpenGL parametara.
        /// </summary>
        private void Initialize()
        {
            Gl.glClearColor(0.0f, 0.0f, 0.2588f, 1.0f); // teget boja
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glEnable(Gl.GL_CULL_FACE);
        }

        /// <summary>
        /// Podesava viewport i projekciju za OpenGL kontrolu.
        /// </summary>
        public void Resize()
        {
            //TODO 1.1
            Gl.glViewport(0, 0, m_width, m_height); // kreiraj viewport po celom prozoru
            Gl.glMatrixMode(Gl.GL_PROJECTION);      // selektuj Projection Matrix
      //      Gl.glOrtho(-m_height, m_height, m_width, -m_width, -1, 1);
            Gl.glLoadIdentity();			        // resetuj Projection Matrix

            //TODO 1
            Glu.gluPerspective(45.0, (double)m_width / (double)m_height, 1.0, 20000.0);
           // Gl.glOrtho(-m_height+300, m_height, m_width+300, -m_width, -1, 1);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);   // selektuj ModelView Matrix
            Gl.glLoadIdentity();                // resetuj ModelView Matrix
        }

        /// <summary>
        ///  Implementacija IDisposable interfejsa.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Oslodi managed resurse
            }

            // Oslobodi unmanaged resurse
            m_scene.Dispose();
        }

        #endregion Metode

        #region IDisposable metode

        /// <summary>
        ///  Dispose metoda.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable metode
    }
}
