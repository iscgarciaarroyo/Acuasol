namespace Acuasol.VistaModelos
{
    using System.Windows.Input;

    public class LoginVistaModelo
    {
        #region Propiedades
        public string Email
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public bool recordar
        {
            get;
            set;
        }

        public bool run
        {
            get;
            set;
        }
        #endregion

        #region Constructor
        public LoginVistaModelo()
        {
            this.recordar = true;
        }
        #endregion

        #region Comando

        public ICommand ingresar
        {
            get;
            set;
        }

        public ICommand registrar
        {
            get;
            set;
        }

        #endregion
    }
}
