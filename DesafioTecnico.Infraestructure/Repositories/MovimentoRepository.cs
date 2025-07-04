using Dapper;
using DesafioTecnico.Domain.Entities;
using DesafioTecnico.Domain.Interfaces;
using System.Data;

namespace DesafioTecnico.Infraestructure.Repositories
{
    public class MovimentoRepository(IDbConnection connection) : IMovimentoRepository
    {
        public async Task<string> AdicionarAsync(Movimento movimento)
        {
            const string sql = @"
                INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor)
                VALUES (@IdMovimento, @IdContaCorrente, @DataMovimento, @TipoMovimento, @Valor)";

            await connection.ExecuteAsync(sql, new
            {
                movimento.IdMovimento,
                movimento.IdContaCorrente,
                DataMovimento = movimento.DataMovimento.ToString("dd/MM/yyyy"),
                TipoMovimento = ((char)movimento.TipoMovimento).ToString(),
                movimento.Valor
            });

            return movimento.IdMovimento;
        }

        public async Task<decimal> CalcularSaldoContaAsync(string idContaCorrente)
        {
            const string sql = @"
                SELECT 
                    COALESCE(SUM(CASE WHEN tipomovimento = 'C' THEN valor ELSE 0 END), 0) -
                    COALESCE(SUM(CASE WHEN tipomovimento = 'D' THEN valor ELSE 0 END), 0) AS Saldo
                FROM movimento 
                WHERE idcontacorrente = @IdContaCorrente";

            var saldo = await connection.QueryFirstOrDefaultAsync<decimal>(sql, new { IdContaCorrente = idContaCorrente });
            return saldo;
        }
    }
}
