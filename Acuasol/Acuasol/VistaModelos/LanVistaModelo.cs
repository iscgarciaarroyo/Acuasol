namespace Acuasol.VistaModelos
{
    using Modelos;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class LanVistaModelo : BaseVistaModelo
    {
        #region Atributos
        private ObservableCollection<Border> borders;
        private ObservableCollection<Moneda> currencies;
        private ObservableCollection<Lenguas> lenguas;
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
        public ObservableCollection<Moneda> Currencies
        {
            get { return this.currencies; }
            set { this.AjusteValor(ref this.currencies, value); }
        }
        public ObservableCollection<Lenguas> Lenguas
        {
            get { return this.lenguas; }
            set { this.AjusteValor(ref this.lenguas, value); }
        }
        #endregion

        #region Constructor
        public LanVistaModelo(Land lan)
        {
            this.Lan = lan;
            this.CargaBorders();
            this.currencies = new ObservableCollection<Moneda>(this.Lan.Currencies);
            this.lenguas = new ObservableCollection<Lenguas>(this.Lan.Languages);
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
