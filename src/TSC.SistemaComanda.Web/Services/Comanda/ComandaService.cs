using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TSC.SistemaComanda.Web.DTO;

namespace TSC.SistemaComanda.Web.Services.Comanda
{
    public class ComandaService
    {
        HttpClient client = new HttpClient();

        public async Task<List<ComandaDTO>> ObterComandasAsync()
        {
            try
            {
                string url = "https://localhost:44399/api/Comanda/ObterTodas";
                var response = await client.GetStringAsync(url);
                var comandas = JsonConvert.DeserializeObject<List<ComandaDTO>>(response);
                return comandas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ProdutoDTO>> ObterProdutosAsync()
        {
            try
            {
                string url = "https://localhost:44399/api/Comanda/ObterTodosProdutos";
                var response = await client.GetStringAsync(url);
                var produtos = JsonConvert.DeserializeObject<List<ProdutoDTO>>(response);
                return produtos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<MensagemDTO> AdicionarProdutoAsync(InserirProdutoDTO inserirProduto)
        {
            try
            {
                MensagemDTO mensagemDTO = new MensagemDTO();

                string url = "https://localhost:44399/api/Comanda/Adicionar";
                
                var data = JsonConvert.SerializeObject(inserirProduto);

                //var formData = new MultipartFormDataContent();
                //formData.Add(new StringContent(JsonConvert.SerializeObject(produtos)), "produtos");
                //var response = await client.PostAsync(uri, formData);

                var content = new StringContent(data, Encoding.UTF8, "application/json");
                //HttpResponseMessage response = null;
                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    mensagemDTO.Mensagem = "Erro ao adicionar produto na comanda";
                    mensagemDTO.Tipo = "Erro";                   
                }
                else
                {
                    mensagemDTO.Mensagem = "Produto adicionado com sucesso";
                    mensagemDTO.Tipo = "Sucesso";
                }

                return mensagemDTO;

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro inesperado.");
            }

        }

        public async Task<NotaFiscalDTO> FecharComandaAsync(int idComanda)
        {
            try
            {
                string url = "https://localhost:44399/api/Comanda/Fechar?idComanda={0}";
                var uri = new Uri(string.Format(url, idComanda));
                var response = await client.GetStringAsync(uri);
                NotaFiscalDTO notafiscalDto = JsonConvert.DeserializeObject<NotaFiscalDTO>(response);

                               
                return notafiscalDto;
                
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro inesperado.");
            }
        }
    }

}
