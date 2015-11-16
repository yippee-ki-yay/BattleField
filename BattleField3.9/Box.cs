// -----------------------------------------------------------------------
// <file>Box.cs</file>
// <copyright>Grupa za Grafiku, Interakciju i Multimediju 2012.</copyright>
// <author>Srdjan Mihic</author>
// <summary>Klasa koja enkapsulira OpenGL programski kod za iscrtavanje kvadra sa tezistem u koord.pocetku.</summary>
// -----------------------------------------------------------------------
namespace BattleField3._9
{
    using Tao.OpenGl;

    /// <summary>
    ///  Klasa enkapsulira OpenGL kod za iscrtavanje kvadra.
    /// </summary>
    public class Box
    {
        #region Atributi

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
        public Box()
        {
        }

        /// <summary>
        ///		Konstruktor sa parametrima.
        /// </summary>
        /// <param name="width">Sirina kvadra.</param>
        /// <param name="height">Visina kvadra.</param>
        /// <param name="depth"></param>
        public Box(double width, double height, double depth)
        {
            this.m_width = width;
            this.m_height = height;
            this.m_depth = depth;
        }

        #endregion Konstruktori

        #region Metode

        public void Draw()
        {
            Gl.glBegin(Gl.GL_QUADS);

            // Zadnja
            Gl.glVertex3d(-m_width / 2, -m_height / 2, -m_depth / 2);
            Gl.glVertex3d(-m_width / 2, m_height / 2, -m_depth / 2);
            Gl.glVertex3d(m_width / 2, m_height / 2, -m_depth / 2);
            Gl.glVertex3d(m_width / 2, -m_height / 2, -m_depth / 2);

            // Desna
            Gl.glVertex3d(m_width / 2, -m_height / 2, -m_depth / 2);
            Gl.glVertex3d(m_width / 2, m_height / 2, -m_depth / 2);
            Gl.glVertex3d(m_width / 2, m_height / 2, m_depth / 2);
            Gl.glVertex3d(m_width / 2, -m_height / 2, m_depth / 2);

            // Prednja
            Gl.glVertex3d(m_width / 2, -m_height / 2, m_depth / 2);
            Gl.glVertex3d(m_width / 2, m_height / 2, m_depth / 2);
            Gl.glVertex3d(-m_width / 2, m_height / 2, m_depth / 2);
            Gl.glVertex3d(-m_width / 2, -m_height / 2, m_depth / 2);

            // Leva
            Gl.glVertex3d(-m_width / 2, -m_height / 2, m_depth / 2);
            Gl.glVertex3d(-m_width / 2, m_height / 2, m_depth / 2);
            Gl.glVertex3d(-m_width / 2, m_height / 2, -m_depth / 2);
            Gl.glVertex3d(-m_width / 2, -m_height / 2, -m_depth / 2);

            // Donja
            Gl.glVertex3d(-m_width / 2, -m_height / 2, -m_depth / 2);
            Gl.glVertex3d(m_width / 2, -m_height / 2, -m_depth / 2);
            Gl.glVertex3d(m_width / 2, -m_height / 2, m_depth / 2);
            Gl.glVertex3d(-m_width / 2, -m_height / 2, m_depth / 2);

            // Gornja
            Gl.glVertex3d(-m_width / 2, m_height / 2, -m_depth / 2);
            Gl.glVertex3d(-m_width / 2, m_height / 2, m_depth / 2);
            Gl.glVertex3d(m_width / 2, m_height / 2, m_depth / 2);
            Gl.glVertex3d(m_width / 2, m_height / 2, -m_depth / 2);

            Gl.glEnd();
        }

        public void SetSize(double width, double height, double depth)
        {
            m_depth = depth;
            m_height = height;
            m_width = width;
        }

        #endregion Metode
    }
}
