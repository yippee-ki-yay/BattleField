// -----------------------------------------------------------------------
// <file>OutlineFont.cs</file>
// <copyright>Grupa za Grafiku, Interakciju i Multimediju 2012.</copyright>
// <author>Srdjan Mihic</author>
// <summary>Klasa koja omogucava rad sa outline fontovima u OpenGL-u.</summary>
// -----------------------------------------------------------------------
namespace BattleField3._9
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using Tao.OpenGl;  // Imenski prostor za OpenGL/GLU
    using Tao.Platform.Windows; // GDI

    /// <summary>
    /// Klasa koja enkapsulara programski kod za rad za vektorskim fontovima u OpenGL
    /// </summary>
    public class OutlineFont : IDisposable
    {
        #region Atributi

        /// <summary>
        ///  Windows font.
        /// </summary>
        private Font m_font = null;

        /// <summary>
        ///	 Debljina fonta.
        /// </summary>
        private float m_depth = 0;

        /// <summary>
        ///	 Identifikator fonta.
        /// </summary>
        private int m_ID = -1;

        /// <summary>
        ///	 Bafer za smestanje karaktera.
        /// </summary>
        /// <remarks>
        ///  Windows specifican!
        /// </remarks>
        private Gdi.GLYPHMETRICSFLOAT[] m_gmf = null;

        #endregion Atributi

        #region Properties

        /// <summary>
        ///  Naziv familije fonta.
        /// </summary>
        public String Family
        {
            get { return m_font.Name; }
        }

        /// <summary>
        ///	 Bold atribut.
        /// </summary>
        public bool Bold
        {
            get { return m_font.Bold; }
        }

        /// <summary>
        ///	 Italic atribut.
        /// </summary>
        public bool Italic
        {
            get { return m_font.Italic; }
        }

        /// <summary>
        ///	 Strikeout atribut.
        /// </summary>
        public bool Strikeout
        {
            get { return m_font.Strikeout; }
        }

        /// <summary>
        ///	 Underline atribut.
        /// </summary>
        public bool Underline
        {
            get { return m_font.Underline; }
        }

        /// <summary>
        ///	 Visina fonta u tackama.
        /// </summary>
        public float Size
        {
            get { return m_font.Size; }
        }

        /// <summary>
        ///	 Dubina fonta - 3D font.
        /// </summary>
        public float Depth
        {
            get { return m_depth; }
        }

        #endregion Properties

        #region Konstruktori

        /// <summary>
        ///  Konstruktor outline OpenGL fonta.
        /// </summary>
        /// <param name="familyName">Naziv familije fonta.</param> 
        /// <param name="size">Velicina fonta.</param>
        /// <param name="bold">Bold atribut.</param> 
        /// <param name="italic">Italic atribut.</param>
        /// <param name="underline">Underline atribut.</param> 
        /// <param name="strikeout">Strikeout atribut.</param> 
        public OutlineFont(String familyName, float size, float depth, bool bold, bool italic, bool underline, bool strikeout)
        {
            FontStyle style = (
                (bold ? FontStyle.Bold : FontStyle.Regular) |
                (italic ? FontStyle.Italic : 0) |
                (underline ? FontStyle.Underline : 0) |
                (strikeout ? FontStyle.Strikeout : 0)
              );

            m_depth = depth;

            // Kreiranje Windows font
            try
            {
                m_font = new Font(familyName, size, style);
            }
            catch (Exception)
            {
                MessageBox.Show("Neuspesno kreirana instanca windows fonta", "GRESKA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Kreiranje Windows font
            try
            {
                m_gmf = new Gdi.GLYPHMETRICSFLOAT[256];
            }
            catch (Exception)
            {
                MessageBox.Show("Neuspesno kreirana interna komponenenta outline windows fonta", "GRESKA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Kreiraj OpenGL vektorski font
            CreateFont();
        }

        #endregion Konstruktori

        #region Metode

        /// <summary>
        ///  Iscrtavanje OpenGL teksta u definisanom fontu.
        /// </summary>
        /// <param name="text">Tekst koji ce biti ispisan u izabranom fontu.</param>   
        public void DrawText(String text)
        {
            if (text.Length != 0)
            {
                Gl.glPushAttrib(Gl.GL_LIST_BIT);     // sacuvamo stanje DL steka
                Gl.glListBase(m_ID);  		           // pozicioniraj se na pocetak DL
                Gl.glCallLists(text.Length,
                               Gl.GL_UNSIGNED_SHORT, // STRING JE UNICODE, pa mora 2 bajta!
                                text);	             // ispis DL teksta
                // alternativno
                //byte[] textbytes = new byte[text.Length];
                //for (int i = 0; i < text.Length; i++) textbytes[i] = (byte)text[i];
                //Gl.glCallLists(text.Length,
                //               Gl.GL_UNSIGNED_BYTE,  // STRING JE UNICODE, pa mora 2 bajta
                //               textbytes);	         // ispis DL teksta
                Gl.glPopAttrib();                    // sacuvamo stanje DL steka
            }
        }

        /// <summary>
        ///  Odredjivanje visine teksta u izabranom fontu.
        /// </summary>
        /// <param name="text">Tekst cija visina ce biti odredjena u izabranom fontu.</param>   
        public float CalculateTextHeight(String text)
        {
            float height = -1000;

            foreach (char i in text)
                if (height < m_gmf[i].gmfBlackBoxY)
                    height = m_gmf[i].gmfBlackBoxY; // najveca visina karaktera odredjuje visinu teksta

            return height;
        }

        /// <summary>
        ///  Odredjivanje sirine teksta u izabranom fontu.
        /// </summary>
        /// <param name="text">Tekst cija sirina ce biti odredjena u izabranom fontu.</param>   
        public float CalculateTextWidth(String text)
        {
            float width = 0;

            foreach (char i in text)
                width += m_gmf[i].gmfCellIncX; // povecaj sirinu za sirinu karaktera

            return width;
        }

        /// <summary>
        ///  Kreiranje fonta kao displej lisete koristeci wgl metode.
        /// </summary>
        private bool CreateFont()
        {
            // Generisi DL za 256 karaktera
            m_ID = Gl.glGenLists(256);

            // Selektuj kreirani font kao aktivni
            Gdi.SelectObject(Wgl.wglGetCurrentDC(), m_font.ToHfont());

            bool success = Wgl.wglUseFontOutlinesW(Wgl.wglGetCurrentDC(), // selektuje aktivni DC
                                                  0,								      // pocetni karakter
                                                  255,							      // broj DL koji se kreiraju
                                                  m_ID,      	    	      // identifikator DLa
                                                  0.0f,							      // koliko se razlikuju od originalnog fonta
                                                  m_depth,  						  // debljina fonta (po Z osi)
                                                  Wgl.WGL_FONT_POLYGONS,  // koristi poligone, a ne linije
                                                  m_gmf);						      // bafer u koji se smestaju podaci

            // Deselektuj aktivni font
            Gdi.SelectObject(Wgl.wglGetCurrentDC(), IntPtr.Zero);

            return success;
        }

        /// <summary>
        ///  Dispose metoda.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///  Destruktor.
        /// </summary>
        ~OutlineFont()
        {
            this.Dispose(false);
        }

        #endregion Metode

        #region IDisposable metode

        /// <summary>
        ///  Implementacija IDisposable interfejsa.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            //if (disposing)
            //{
            //  // Oslodi managed resurse
            //}

            // Oslobodi unmanaged resurse
            Terminate();
        }

        /// <summary>
        ///  Korisnicko oslobadjanje OpenGL resursa.
        /// </summary>
        private void Terminate()
        {
            m_font.Dispose();
            //Gl.glDeleteLists(m_ID, 256);
        }

        #endregion IDisposable metode
    }
}
