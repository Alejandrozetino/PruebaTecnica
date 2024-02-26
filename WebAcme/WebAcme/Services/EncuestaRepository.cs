using ACME.DBContext;
using ACME.Helper;
using ACME.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using ACME.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ACME.Services;

public class EncuestaRepository : IEncuestaRepository
{
    private readonly AcmeDbContext _context;
    public EncuestaRepository(AcmeDbContext context)
    {
        _context = context;
    }

    #region -Encuesta-
    public async Task<Encuesta> GetSurveyAsync(int id)
	{
		return await _context.Encuestas.Where(x => x.Id == id)
			.FirstOrDefaultAsync();
	}

	public async Task<RecordList<Encuesta>> ListSurveyAsync()
	{
		var recordList = new RecordList<Encuesta>();

		var encuestas = await _context.Encuestas.Include(usuario => usuario.Usuario)
			.OrderBy(order => order.FechaCreación)
			.ToListAsync();

		recordList.Data.AddRange(encuestas);

		return recordList;
	}

    public async Task<Encuesta> GetSurveyTokenAsync(string link)
    {
        return await _context.Encuestas
                .Where(x => x.Link == link)
                .FirstOrDefaultAsync();
    }


    public void AddSurvey(Encuesta encuesta)
	{
		_context.Encuestas.Add(encuesta);
	}

	public void UpdateSurvey(Encuesta encuesta)
	{
		_context.Encuestas.Update(encuesta);
	}

	public async Task DeleteSurvey(Encuesta encuesta)
	{
        var encuestaRemv = await _context.Encuestas
            .Where(x => x.Id == encuesta.Id)
            .FirstOrDefaultAsync();

		_context.Encuestas.Remove(encuestaRemv);
	}
    #endregion

    #region -CamposEncuesta-
    public async Task<CampoEncuesta> GetSurveyFieldAsync(int id)
    {
        return await _context.CamposEncuestas.Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<RecordList<CampoEncuesta>> ListSurveyFieldAsync(int IdEncuesta)
    {
        var recordList = new RecordList<CampoEncuesta>();

        var camposEncuestas = await _context.CamposEncuestas.Include(encuesta => encuesta.Encuesta)
            .Where(x => x.IdEncuesta == IdEncuesta)
            .OrderBy(order => order.Id)
            .ToListAsync();

        recordList.Data.AddRange(camposEncuestas);

        return recordList;
    }

    public async Task<List<CampoEncuesta>> ListSurveyFieldAsync2(int IdEncuesta)
    {
        var camposEncuestas = await _context.CamposEncuestas.Include(encuesta => encuesta.Encuesta)
            .Where(x => x.IdEncuesta == IdEncuesta)
            .OrderBy(order => order.Id)
            .ToListAsync();

        return camposEncuestas;
    }

    public async Task<List<SelectListItem>> GetTypeData()
    {
        var returnList = new List<SelectListItem>();
        returnList.Add(new SelectListItem("Seleccione un tipo de dato", ""));

        var listCombo = TypeUtil._tiposDeDato.Select(tipo => new SelectListItem(tipo.Key, tipo.Value.ToString())).ToList();
        returnList.AddRange(listCombo);

        return returnList;
    }

    public void AddSurveyField(CampoEncuesta campoEncuesta)
    {
        _context.CamposEncuestas.Add(campoEncuesta);
    }

    public void UpdateSurveyField(CampoEncuesta campoEncuesta)
    {
        _context.CamposEncuestas.Update(campoEncuesta);
    }

    public void DeleteSurveyField(CampoEncuesta campoEncuesta)
    {
        _context.CamposEncuestas.Remove(campoEncuesta);
    }
    #endregion

    #region -RespuestasEncuesta-
    public async Task<ResultadoEncuesta> GetSurveyResponseAsync(int id)
    {
        return await _context.ResultadosEncuestas.Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<RecordList<ResultadoEncuesta>> ListSurveyResponseAsync(int IdEncuesta)
    {
        var recordList = new RecordList<ResultadoEncuesta>();

        var resultadosEncuestas = await _context.ResultadosEncuestas.Include(encuesta => encuesta.Encuesta).Include(campoEncuesta => campoEncuesta.CampoEncuesta)
            .Where(x => x.IdEncuesta == IdEncuesta)
            .OrderBy(order => order.Id)
            .ToListAsync();

        recordList.Data.AddRange(resultadosEncuestas);

        return recordList;
    }


    public void AddSurveyResponse(ResultadoEncuesta resultadoEncuesta)
    {
        _context.ResultadosEncuestas.Add(resultadoEncuesta);
    }

    public void UpdateSurveyResponse(ResultadoEncuesta resultadoEncuesta)
    {
        _context.ResultadosEncuestas.Update(resultadoEncuesta);
    }

    public void DeleteSurveyResponse(ResultadoEncuesta resultadoEncuesta)
    {
        _context.ResultadosEncuestas.Remove(resultadoEncuesta);
    }
    #endregion

    public async Task<bool> SaveChangesAsync()
    {
        return (await _context.SaveChangesAsync() >= 0);
    }
}
