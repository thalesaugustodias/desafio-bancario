using System.ComponentModel.DataAnnotations;

namespace DesafioTecnico.Application.DTOs
{
    public class MovimentacaoRequest
    {
        [Required(ErrorMessage = "IdRequisicao é obrigatório")]
        public string IdRequisicao { get; set; }

        [Required(ErrorMessage = "IdContaCorrente é obrigatório")]
        public string IdContaCorrente { get; set; }

        [Required(ErrorMessage = "Valor é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Valor deve ser positivo")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "TipoMovimento é obrigatório")]
        [RegularExpression("^[CD]$", ErrorMessage = "TipoMovimento deve ser 'C' ou 'D'")]
        public string TipoMovimento { get; set; }
    }
}
