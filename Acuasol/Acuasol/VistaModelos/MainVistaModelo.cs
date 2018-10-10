namespace Acuasol.VistaModelos
{
    using Modelos;
    using System.Collections.Generic;

    public class MainVistaModelo
    {
        #region Propiedad
        public List<Land> listaPaises
        {
            get;
            set;
        }
        #endregion

        #region vistaModelo
        public LoginVistaModelo Login
        {
            get;
            set;
        }

        public HomeVistaModelo Home
        {
            get;
            set;
        }

        public LanVistaModelo Lan
        {
            get;
            set;
        }

        #endregion

        #region Constructor
        public MainVistaModelo()
        {
            instancia = this;
            this.Login = new LoginVistaModelo();
        }
        #endregion

        #region Singleyton
        private static MainVistaModelo instancia;
        public static MainVistaModelo ObtenerInstancia()
        {
            if (instancia == null)
            {
                return new MainVistaModelo();
            }
            return instancia;
        }
        #endregion
    }
}
