namespace Acuasol.VistaModelos
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class BaseVistaModelo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void CambioPropiedad([CallerMemberName] string nombrePropiedad = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombrePropiedad));
        }
        protected void AjusteValor<T>(ref T backingField, T value,[CallerMemberName] string nombrePropiedad = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value))
            {
                return;
            }
            backingField = value;
            CambioPropiedad(nombrePropiedad);
        }
    }
}
