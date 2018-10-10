namespace Acuasol.VistaModelos
{
    using GalaSoft.MvvmLight.Command;
    using Modelos;
    using Servicios;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class HomeVistaModelo : BaseVistaModelo
    {
        #region Servicios
        private ApiServicio apiServicio;
        #endregion

        #region Atributos
        private ObservableCollection<LandItemVistaModelo> land;
        private bool isRefreshing;
        private string filtro;
        #endregion

        #region Propiedades
        public ObservableCollection<LandItemVistaModelo> Land
        {
            get { return this.land; }
            set { AjusteValor(ref this.land, value); }
        }
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { AjusteValor(ref this.isRefreshing, value); }
        }
        public string Filtro
        {
            get { return this.filtro; }
            set {
                AjusteValor(ref this.filtro, value);
                this.Buscar();
            }
        }
        #endregion

        #region Constructor
        public HomeVistaModelo() {
            this.apiServicio = new ApiServicio();
            this.CargaLand();
        }
        #endregion

        #region Metodos
        private async void CargaLand()
        {
            this.IsRefreshing = true;

            var revisaconexion = await this.apiServicio.CheckConnection();

            if (!revisaconexion.Exito)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    revisaconexion.Mensaje,
                    "Aceptar");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            var respuesta = await this.apiServicio.GetList<Land>(
                "http://restcountries.eu"
                , "/rest"
                , "/v2/all");

            if (!respuesta.Exito)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    respuesta.Mensaje,
                    "Aceptar");
                return;
            }

            MainVistaModelo.ObtenerInstancia().listaPaises = (List<Land>)respuesta.Resultado;
            this.Land = new ObservableCollection<LandItemVistaModelo>(
                this.ToLandItem());
            this.IsRefreshing = false;
        }
        #endregion

        #region Metodo
        private IEnumerable<LandItemVistaModelo> ToLandItem()
        {
            return MainVistaModelo.ObtenerInstancia().listaPaises.Select(l => new LandItemVistaModelo
            {
                Alpha2Code = l.Alpha2Code,
                Alpha3Code = l.Alpha3Code,
                AltSpellings = l.AltSpellings,
                Area = l.Area,
                Borders = l.Borders,
                CallingCodes = l.CallingCodes,
                Capital = l.Capital,
                Cioc = l.Cioc,
                //Currencies = l.Currencies,
                Demonym = l.Demonym,
                Flag = l.Flag,
                Gini = l.Gini,
                //Languages = l.Languages,
                Latlng = l.Latlng,
                Name = l.Name,
                NativeName = l.NativeName,
                NumericCode = l.NumericCode,
                Population = l.Population,
                Region = l.Region,
                //RegionalBlocs = l.RegionalBlocs,
                Subregion = l.Subregion,
                Timezones = l.Timezones,
                TopLevelDomain = l.TopLevelDomain,
                //Translations = l.Translations,
            });
        } 
        #endregion

        #region Comandos
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(CargaLand);
            }
        }
        public ICommand ComandoBusqueda
        {
            get
            {
                return new RelayCommand(Buscar);
            }
        }

        private void Buscar()
        {
            if (string.IsNullOrEmpty(this.Filtro))
            {
                this.Land = new ObservableCollection<LandItemVistaModelo>(
                    this.ToLandItem());
            }
            else
            {
                this.Land = new ObservableCollection<LandItemVistaModelo>(
                    this.ToLandItem().Where(
                        p => p.Name.ToLower().Contains(this.Filtro.ToLower()) ||
                             p.Capital.ToLower().Contains(this.Filtro.ToLower())));
            }
        }
        #endregion
    }
}
