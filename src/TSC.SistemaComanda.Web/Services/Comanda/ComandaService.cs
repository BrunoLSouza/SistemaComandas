using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using TSC.SistemaComanda.Web.DTO;

namespace TSC.SistemaComanda.Web.Services.Comanda
{
    public class ComandaService
    {
        HttpClient client = new HttpClient();
              
        public async Task<string> Token()
        {
            try
            {
                string url = "https://localhost:44399/token";

                var usuario = "InterfaceWeb";
                var pwd = "1234";
                
                var data = $"grant_type=password&username={usuario}&password={pwd}";

                var content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);

                var jsonContent = await response.Content.ReadAsStringAsync();
                Token tokenApp = JsonConvert.DeserializeObject<Token>(jsonContent);

                return tokenApp.AccessToken;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

              

        public async Task<List<ComandaDTO>> ObterComandasAsync(string token)
        {
            try
            {
                string url = "https://localhost:44399/api/Comanda/ObterTodas";
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var response = await client.GetStringAsync(url);
                var comandas = JsonConvert.DeserializeObject<List<ComandaDTO>>(response);
                return comandas;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ProdutoDTO>> ObterProdutosAsync(string token)
        {
            try
            {
                string url = "https://localhost:44399/api/Comanda/ObterTodosProdutos";
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var response = await client.GetStringAsync(url);
                var produtos = JsonConvert.DeserializeObject<List<ProdutoDTO>>(response);
                return produtos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<MensagemDTO> AdicionarProdutoAsync(InserirProdutoDTO inserirProduto, string token)
        {
            try
            {
                MensagemDTO mensagemDTO = new MensagemDTO();

                string url = "https://localhost:44399/api/Comanda/Adicionar";
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var data = JsonConvert.SerializeObject(inserirProduto);

                var content = new StringContent(data, Encoding.UTF8, "application/json");
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

        public async Task<NotaFiscalDTO> FecharComandaAsync(int idComanda, string token)
        {
            try
            {
                string url = "https://localhost:44399/api/Comanda/Fechar?idComanda={0}";
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
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


    internal class Token
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }

}
