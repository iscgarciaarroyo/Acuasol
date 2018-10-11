namespace Acuasol.VistaModelos
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Vistas;
    using Servicios;

    public class LoginVistaModelo : BaseVistaModelo
    {
        #region Servicios
        private ApiServicio apiServicio;
        #endregion

        #region Attributo
        private string password;
        private string email;
        private bool run;
        private bool habilitado;
        #endregion  

        #region Propiedades
        public string Email
        {
            get { return this.email; }
            set { AjusteValor(ref this.email, value); }
        }

        public string Password
        {
            get { return this.password; }
            set { AjusteValor(ref this.password, value);}
        }

        public bool Recordar
        {
            get;
            set;
        }

        public bool Run
        {
            get { return this.run; }
            set { AjusteValor(ref this.run, value); }
        }

        public bool Habilitado
        {
            get { return this.habilitado; }
            set { AjusteValor(ref this.habilitado, value); }
        }

        #endregion

        #region Constructor
        public LoginVistaModelo()
        {
            this.apiServicio = new ApiServicio();

            this.Recordar = true;
            this.habilitado = true;

        }
        #endregion

        #region Comando

        public ICommand Ingresar
        {
            get
            {
                return new RelayCommand(Login);
            }
        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert("" +
                    "Error"
                    , "Es necesario ingresar su E-Mail"
                    , "Aceptar"
                    );
                return;
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert("" +
                    "Error"
                    , "Es necesario ingresar su contraseña"
                    , "Aceptar"
                    );
                return;
            }

            this.Run = true;
            this.Habilitado = false;

            var checaConect = await this.apiServicio.CheckConnection();

            if (!checaConect.Exito)
            {
                this.Run = false;
                this.Habilitado = true;

                await Application.Current.MainPage.DisplayAlert("" +
                    "Error"
                    , checaConect.Mensaje
                    , "Aceptar"
                    );
                return;
            }

            var token = await this.apiServicio.GetToken(
                "https://acuasolapi.azurewebsites.net"
                , this.Email
                , this.Password
                );

            if (token == null)
            {
                this.Run = false;
                this.Habilitado = true;

                await Application.Current.MainPage.DisplayAlert("" +
                    "Error"
                    , "Ocurrio un error al intentar acceder, favor de intentar mas tarde"
                    , "Aceptar"
                    );
                return;
            }

            if (string.IsNullOrEmpty(token.AccessToken))
            {
                this.Run = false;
                this.Habilitado = true;

                await Application.Current.MainPage.DisplayAlert("" +
                    "Error"
                    , token.ErrorDescription
                    , "Aceptar"
                    );
                this.Password = string.Empty;
                return;
            }

            var mainVistaModelo = MainVistaModelo.ObtenerInstancia();
            mainVistaModelo.Token = token;
            mainVistaModelo.Home = new HomeVistaModelo();
            await Application.Current.MainPage.Navigation.PushAsync(new HomePage());

            this.Run = false;
            this.Habilitado = true;

            this.Email = string.Empty;
            this.Password = string.Empty;
        }

        public ICommand registrar
        {
            get;
            set;
        }

        #endregion
    }
}
