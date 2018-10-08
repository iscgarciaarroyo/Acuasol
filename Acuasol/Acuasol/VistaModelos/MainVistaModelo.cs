namespace Acuasol.VistaModelos
{
    public class MainVistaModelo
    {
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
