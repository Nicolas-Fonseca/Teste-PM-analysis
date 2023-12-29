using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PessoasController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PessoasController(ApplicationDbContext context)
    {
        _context = context;
    }

    
    [HttpPost]
    public async Task<ActionResult<Pessoa>> SalvarPessoa(PessoaViewModel pessoaViewModel)
    {
    

        if (pessoaViewModel.Arquivo != null && pessoaViewModel.Arquivo.Length > 0)
        {
            var caminhoArquivo = SalvarArquivo(pessoaViewModel.Arquivo);
            pessoa.CaminhoArquivo = caminhoArquivo;
        }

        _context.Pessoas.Add(pessoa);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPessoa), new { id = pessoa.Id }, pessoa);
    }

    private string SalvarArquivo(IFormFile arquivo)
    {
    
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Pessoa>> GetPessoa(int id)
    {
        var pessoa = await _context.Pessoas.FindAsync(id);

        if (pessoa == null)
        {
            return NotFound();
        }

        return pessoa;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditarPessoa(int id, Pessoa pessoa)
    {
        if (id != pessoa.Id)
        {
            return BadRequest();
        }

        _context.Entry(pessoa).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PessoaExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> ExcluirPessoa(int id)
    {
        var pessoa = await _context.Pessoas.FindAsync(id);
        if (pessoa == null)
        {
            return NotFound();
        }

        _context.Pessoas.Remove(pessoa);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool PessoaExists(int id)
    {
        return _context.Pessoas.Any(e => e.Id == id);
    }
}
