namespace MiniProyectoBanking.Core.Application.ViewModels.Transacciones
{
    public class SaveTransaccionViewModel
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public decimal Monto { get; set; }
        public DateTime? Fecha { get; set; }
        public string CuentaOrigenId { get; set; }
        public string CuentaDestinoId { get; set; }
    }
}
