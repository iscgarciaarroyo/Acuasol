namespace Acuasol.VistaModelos
{
    using Modelos;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class LanVistaModelo : BaseVistaModelo
    {
        #region Atributos
        private ObservableCollection<Border> borders;
        #endregion

        #region Propiedades
        public Land Lan
        {
            get;
            set;
        }
        public ObservableCollection<Border> Borders
        {
            get { return this.borders; }
            set { this.AjusteValor(ref this.borders, value); }
        }
        #endregion

        #region Constructor
        public LanVistaModelo(Land lan)
        {
            this.Lan = lan;
            this.CargaBorders();
        }
        #endregion

        #region Metodos
        private void CargaBorders()
        {
            this.Borders = new ObservableCollection<Border>();
            foreach (var border in this.Lan.Borders)
            {
                var land = MainVistaModelo.ObtenerInstancia().listaPaises
                    .Where(p => p.Alpha3Code == border).
                    FirstOrDefault();

                if (land != null)
                {
                    this.Borders.Add(new Border
                    {
                        Code = land.Alpha3Code,
                        Name = land.Name,
                    });
                }
            }
        }
        #endregion
    }
}
