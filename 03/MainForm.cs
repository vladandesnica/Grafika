// -----------------------------------------------------------------------
// <file>MainForm.cs</file>
// <copyright>Grupa za Grafiku, Interakciju i Multimediju 2013.</copyright>
// <author>Zoran Milicevic</author>
// <summary>Demonstracija ucitavanja modela pomocu AssimpNet biblioteke i koriscenja u OpenGL-u.</summary>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Assimp;
using System.IO;
using System.Reflection;

namespace RacunarskaGrafika.Vezbe.AssimpNetSample
{
    public partial class MainForm : Form
    {
        #region Atributi

        /// <summary>
        ///	 Instanca OpenGL "sveta" - klase koja je zaduzena za iscrtavanje koriscenjem OpenGL-a.
        /// </summary>
        World m_world = null;
        BitmapFont m_font = null;

        #endregion Atributi

        #region Konstruktori

        public MainForm()
        {
            // Inicijalizacija komponenti
            InitializeComponent();

            // Inicijalizacija OpenGL konteksta
            openglControl.InitializeContexts();

            // Kreiranje OpenGL sveta
            try
            {
                m_world = new World(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "3D Models\\Touareg"), "11817_Zepplin_V2_L3_v4.3ds", openglControl.Width, openglControl.Height);
            }
            catch (Exception e)
            {
                MessageBox.Show("Neuspesno kreirana instanca OpenGL sveta. Poruka greške: " + e.Message, "GRESKA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        #endregion Konstruktori

        #region Rukovaoci dogadjajima OpenGL kontrole

        /// <summary>
        /// Rukovalac dogadja izmene dimenzija OpenGL kontrole
        /// </summary>
        private void OpenGlControlResize(object sender, EventArgs e)
        {
            m_world.Height = openglControl.Height;
            m_world.Width = openglControl.Width;

            m_world.Resize();
        }

        /// <summary>
        /// Rukovalac dogadjaja iscrtavanja OpenGL kontrole
        /// </summary>
        private void OpenGlControlPaint(object sender, PaintEventArgs e)
        {
            // Iscrtaj svet
            m_world.Draw();
        }

        /// <summary>
        /// Rukovalac dogadjaja: obrada tastera nad formom
        /// </summary>
        private void OpenGlControlKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F10: this.Close(); break;
                case Keys.W: m_world.RotationX = (m_world.RotationX >= 7.0f) ? m_world.RotationX - 7.0f : 0.0f; break;
                case Keys.S: m_world.RotationX = (m_world.RotationX <= 83.0f) ? m_world.RotationX + 7.0f : 90.0f; break;
                case Keys.A: m_world.RotationY -= 7.0f; break;
                case Keys.D: m_world.RotationY += 7.0f; break;
                case Keys.Z: m_world.Hangar.Door_height -= 1.0f; m_world.Draw(); break;
                case Keys.Q: m_world.Hangar.Door_height += 1.0f; m_world.Draw(); break;
                case Keys.U: m_world.YTranslation += 10.0f; break;
                case Keys.J: m_world.YTranslation -= 10.0f; break;
                case Keys.H: m_world.XTranslation -= 10.0f; break;
                case Keys.K:m_world.XTranslation += 10.0f; break;
                
                case Keys.D8 :  m_world.ZTranslation-=10.0f;break;
                case Keys.D2: m_world.ZTranslation += 10.0f; break;

                case Keys.Add: m_world.SceneDistance -= 10.0f; m_world.Resize(); break;
                case Keys.Subtract: m_world.SceneDistance += 10.0f; m_world.Resize(); break;
                case Keys.F2:
                    OpenFileDialog opfModel = new OpenFileDialog();
                    if (opfModel.ShowDialog() == DialogResult.OK)
                    {

                        try
                        {
                            World newWorld = new World(Directory.GetParent(opfModel.FileName).ToString(), Path.GetFileName(opfModel.FileName), openglControl.Width, openglControl.Height);
                            m_world.Dispose();
                            m_world = newWorld;
                            openglControl.Invalidate();
                        }
                        catch (Exception exp)
                        {
                            MessageBox.Show("Neuspesno kreirana instanca OpenGL sveta:\n" + exp.Message, "GRESKA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    break;
            }

            openglControl.Refresh();
        }

        #endregion Rukovaoci dogadjajima OpenGL kontrole
    }
}
