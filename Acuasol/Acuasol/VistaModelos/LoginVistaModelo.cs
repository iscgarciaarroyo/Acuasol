﻿namespace Acuasol.VistaModelos
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LoginVistaModelo : BaseVistaModelo
    {
        #region Attributo
        private string password;
        private bool run;
        private bool habilitado;
        #endregion  

        #region Propiedades
        public string Email
        {
            get;
            set;
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

            if (this.Email != "agarcia@bism.com.mx" || this.Password != "agarcia")
            {
                this.Run = false;
                this.Habilitado = true;
                await Application.Current.MainPage.DisplayAlert("" +
                    "Error"
                    , "E-Mail o Password son incorrectos"
                    , "Aceptar"
                    );
                this.Password = string.Empty;
                return;
            }

            this.Run = false;
            this.Habilitado = true;

            await Application.Current.MainPage.DisplayAlert("" +
                "OK"
                , "Bienvenido"
                , "Aceptar"
                );
            return;
        }

        public ICommand registrar
        {
            get;
            set;
        }

        #endregion
    }
}
