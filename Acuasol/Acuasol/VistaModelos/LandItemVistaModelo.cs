namespace Acuasol.VistaModelos
{
    using GalaSoft.MvvmLight.Command;
    using Modelos;
    using System.Windows.Input;
    using Vistas;
    using Xamarin.Forms;

    public class LandItemVistaModelo : Land
    {
        #region Comandos
        public ICommand SelectLandCommand
        {
            get
            {
                return new RelayCommand(SelectLand);
            }
        }

        private async void SelectLand()
        {
            MainVistaModelo.ObtenerInstancia().Lan = new LanVistaModelo(this);
            await Application.Current.MainPage.Navigation.PushAsync(new LandTabbedPage());
        }
        #endregion
    }
}
