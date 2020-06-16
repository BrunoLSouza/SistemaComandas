using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSC.SistemaComanda.Web.DTO;

namespace TSC.SistemaComanda.Web.Services.Comanda
{
    public interface IComandaService
    {
        Task<string> Token();
        Task<List<ComandaDTO>> ObterComandasAsync(string token);
        Task<List<ProdutoDTO>> ObterProdutosAsync(string token);
        Task<MensagemDTO> AdicionarProdutoAsync(InserirProdutoDTO inserirProduto, string token);
        Task<NotaFiscalDTO> FecharComandaAsync(int idComanda, string token);

    }
}
