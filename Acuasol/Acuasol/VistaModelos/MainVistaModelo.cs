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
        #endregion

        #region Constructor
        public MainVistaModelo()
        {
            this.Login = new LoginVistaModelo();
        }
        #endregion
    }
}
