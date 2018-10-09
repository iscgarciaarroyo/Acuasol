namespace Acuasol.VistaModelos
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Modelos;
    using Servicios;
    using Xamarin.Forms;

    public class HomeVistaModelo : BaseVistaModelo
    {
        #region Servicios
        private ApiServicio apiServicio;
        #endregion

        #region Atributos
        private ObservableCollection<Noticias> noticias;
        #endregion

        #region Propiedades
        public ObservableCollection<Noticias> Noticias
        {
            get { return this.noticias; }
            set { AjusteValor(ref this.noticias, value); }
        }
        #endregion

        #region Constructor
        public HomeVistaModelo() {
            this.apiServicio = new ApiServicio();
            this.CargaNoticias();
        }
        #endregion

        #region Metodos
        private async void CargaNoticias()
        {
            var revisaconexion = await this.apiServicio.CheckConnection();

            if (!revisaconexion.Exito)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    revisaconexion.Mensaje,
                    "Aceptar");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            var Respuesta = await this.apiServicio.GetList<Noticias>(
                ""
                ,""
                ,"");
        }
        #endregion

    }
}
