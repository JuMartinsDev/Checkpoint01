using checkpoint01.Data;
using checkpoint01.Model;
using Newtonsoft.Json;

namespace checkpoint01.Services
{
        public class UsuarioService
        {
            private readonly SqlService _sqlService;
            private readonly RedisService _redisService;

            public UsuarioService(SqlService sqlService, RedisService redisService)
            {
                _sqlService = sqlService;
                _redisService = redisService;
            }

            public async Task<Usuario> ObterUsuarioAsync(string id)
            {
                try
                {
                    var usuarioCache = await _redisService.ObterCacheAsync(id);
                    if (!string.IsNullOrEmpty(usuarioCache))
                    {
                        return JsonConvert.DeserializeObject<Usuario>(usuarioCache);
                    }

                    var usuario = await _sqlService.ObterUsuarioPorIdAsync(id);
                    if (usuario != null)
                    {
                        await _redisService.SetCacheAsync(id, JsonConvert.SerializeObject(usuario), TimeSpan.FromMinutes(15));
                    }

                    return usuario;
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Erro ao buscar usuário: {ex.Message}");
                    return null;
                }
            }
        }
    }

