namespace checkpoint01.Controllers
{
    using checkpoint01.Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

        [ApiController]
        [Route("api/[controller]")]
        public class UsuarioController : ControllerBase
        {
            private readonly UsuarioService _usuarioService;

            public UsuarioController(UsuarioService usuarioService)
            {
                _usuarioService = usuarioService;
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetUsuario(string id)
            {
                var usuario = await _usuarioService.ObterUsuarioAsync(id);

                if (usuario == null)
                    return NotFound();

                return Ok(usuario);
            }
        }
    }

