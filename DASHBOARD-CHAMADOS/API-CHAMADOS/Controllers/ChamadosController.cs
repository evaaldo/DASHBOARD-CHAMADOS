using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

[ApiController]
[Route("api/chamados")]
public class ChamadosController : ControllerBase
{
    private readonly string _connectionString;

    public ChamadosController(string connectionString)
    {
        _connectionString = connectionString;
    }

    [HttpGet]
    public async Task<IEnumerable<Chamado>> ListarChamados()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sqlList = @"
                USE Chamados_DEV
                SELECT *
                FROM
                CHAMADOS;
             ";

            var chamados = await connection.QueryAsync<Chamado>(sqlList);

            return chamados;
        }
    }

    [HttpPost]
    public async Task CriarChamado(Chamado chamado)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sqlInsert = @"
                            USE Chamados_DEV
                            INSERT INTO CHAMADOS
                            (ID, Titulo, Area, DataAbertura, Situacao)
                            VALUES
                            (@ID, @Titulo, @Area, @DataAbertura, @Situacao)
                           ";

            var parameters = new
            {
                ID = chamado.ID,
                Titulo = chamado.Titulo,
                Area = chamado.Area,
                DataAbertura = chamado.DataAbertura,
                Situacao = chamado.Situacao
            };

            await connection.QueryAsync(sqlInsert, parameters);
        }
    }

    [HttpPut("{id}")]
    public async Task AtualizarChamado(int id, Chamado chamado)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sqlUpdate = @"USE Chamados_DEV UPDATE CHAMADOS SET Titulo = @Titulo, Area = @Area, DataAbertura = @DataAbertura, Situacao = @Situacao WHERE ID = @ID";

            var parameters = new 
            {
                ID = id,
                Titulo = chamado.Titulo,
                Area = chamado.Area,
                DataAbertura = chamado.DataAbertura,
                Situacao = chamado.Situacao
            };         

            await connection.ExecuteAsync(sqlUpdate, parameters);
        }
    }
    
}