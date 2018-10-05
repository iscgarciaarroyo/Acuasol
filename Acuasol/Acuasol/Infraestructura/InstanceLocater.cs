namespace Acuasol.Infraestructura
{
    using VistaModelos;
    public class InstanceLocater
    {
        #region Propiedades
        public MainVistaModelo Main
        {
            get;
            set;
        }
        #endregion

        #region Constructor
        public InstanceLocater()
        {
            this.Main = new MainVistaModelo();
        }
        #endregion
    }
}
