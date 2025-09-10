using checkpoint01.Data;
using checkpoint01.Services;

class Program
{
    static async Task Main(string[] args)
    {
        var sqlService = new SqlService("Server=localhost;Database=fiap;User=root;Password=123");
        var redisService = new RedisService("localhost");

        var usuarioService = new UsuarioService(sqlService, redisService);

        var usuario = await usuarioService.ObterUsuarioAsync("123");

        if (usuario != null)
        {
            Console.WriteLine($"Usu�rio encontrado: {usuario.Nome}, {usuario.Email}");
        }
        else
        {
            Console.WriteLine("Usu�rio n�o encontrado.");
        }
    }
}
