using alacnz.server.Data.Repositories.Interfaces;
using alacnz.server.Models;
using Microsoft.AspNetCore.Mvc;

namespace alacnz.server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClienteController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClientes()
        {
            var clientes = await _unitOfWork.Clientes.GetAllAsync();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClienteById(int id)
        {
            var cliente = await _unitOfWork.Clientes.GetByIdAsync(id);
            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCliente(Cliente cliente)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _unitOfWork.Clientes.AddAsync(cliente);
            await _unitOfWork.CompleteAsync();
            return CreatedAtAction(nameof(GetClienteById), new { id = cliente.Id }, cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCliente(int id, Cliente updatedCliente)
        {
            if (id != updatedCliente.Id || !ModelState.IsValid)
                return BadRequest();

            var cliente = await _unitOfWork.Clientes.GetByIdAsync(id);
            if (cliente == null)
                return NotFound();

            cliente.Nombre = updatedCliente.Nombre;
            cliente.Nacionalidad = updatedCliente.Nacionalidad;
            cliente.Genero = updatedCliente.Genero;
            cliente.Edad = updatedCliente.Edad;
            cliente.EstadoMigratorio = updatedCliente.EstadoMigratorio;
            cliente.BeneficiosSeguridadSocial = updatedCliente.BeneficiosSeguridadSocial;
            cliente.TipoVisa = updatedCliente.TipoVisa;

            await _unitOfWork.CompleteAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _unitOfWork.Clientes.GetByIdAsync(id);
            if (cliente == null)
                return NotFound();

            _unitOfWork.Clientes.Remove(cliente);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
    }
}
