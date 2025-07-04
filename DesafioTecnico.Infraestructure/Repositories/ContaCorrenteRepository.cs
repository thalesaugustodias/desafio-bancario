using Dapper;
using DesafioTecnico.Domain.Entities;
using DesafioTecnico.Domain.Interfaces;
using System.Data;

namespace DesafioTecnico.Infraestructure.Repositories
{
    public class ContaCorrenteRepository : IContaCorrenteRepository
    {
        private readonly IDbConnection _connection;

        public ContaCorrenteRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<ContaCorrente> ObterPorIdAsync(string idContaCorrente)
        {
            const string sql = @"
                SELECT idcontacorrente AS IdContaCorrente, 
                       numero AS Numero, 
                       nome AS Nome, 
                       ativo AS Ativo 
                FROM contacorrente 
                WHERE idcontacorrente = @IdContaCorrente";

            var conta = await _connection.QueryFirstOrDefaultAsync<ContaCorrente>(sql, new { IdContaCorrente = idContaCorrente });
            return conta;
        }

        public async Task<ContaCorrente> ObterComMovimentosAsync(string idContaCorrente)
        {
            const string sql = @"
                SELECT cc.idcontacorrente AS IdContaCorrente, 
                       cc.numero AS Numero, 
                       cc.nome AS Nome, 
                       cc.ativo AS Ativo,
                       m.idmovimento AS IdMovimento,
                       m.idcontacorrente AS IdContaCorrente,
                       m.datamovimento AS DataMovimento,
                       m.tipomovimento AS TipoMovimento,
                       m.valor AS Valor
                FROM contacorrente cc
                LEFT JOIN movimento m ON cc.idcontacorrente = m.idcontacorrente
                WHERE cc.idcontacorrente = @IdContaCorrente";

            var contaDict = new Dictionary<string, ContaCorrente>();

            var result = await _connection.QueryAsync<ContaCorrente, Movimento, ContaCorrente>(
                sql,
                (conta, movimento) =>
                {
                    if (!contaDict.TryGetValue(conta.IdContaCorrente, out var contaEntry))
                    {
                        contaEntry = conta;
                        contaDict.Add(conta.IdContaCorrente, contaEntry);
                    }

                    if (movimento != null)
                    {
                        contaEntry.AdicionarMovimento(movimento);
                    }

                    return contaEntry;
                },
                new { IdContaCorrente = idContaCorrente },
                splitOn: "IdMovimento"
            );

            return contaDict.Values.FirstOrDefault();
        }
    }
}
