using ACME.Helper;
using Microsoft.AspNetCore.Mvc.Rendering;
using ACME.Entities;

namespace ACME.Services.Interfaces;

public interface IEncuestaRepository
{
    #region -Encuesta-
    void AddSurvey(Encuesta encuesta);
    void UpdateSurvey(Encuesta encuesta);
    Task DeleteSurvey(Encuesta encuesta);
    Task<Encuesta> GetSurveyAsync(int id);
    Task<Encuesta> GetSurveyTokenAsync(string link);
    Task<RecordList<Encuesta>> ListSurveyAsync();
    #endregion

    #region -CamposEncuesta-
    void AddSurveyField(CampoEncuesta campoEncuesta);
    void UpdateSurveyField(CampoEncuesta campoEncuesta);
    void DeleteSurveyField(CampoEncuesta campoEncuesta);
    Task<CampoEncuesta> GetSurveyFieldAsync(int id);
    Task<RecordList<CampoEncuesta>> ListSurveyFieldAsync(int IdEncuesta);
    Task<List<CampoEncuesta>> ListSurveyFieldAsync2(int IdEncuesta);
    Task<List<SelectListItem>> GetTypeData();
    #endregion

    #region -RespuestasEncuesta-
    void AddSurveyResponse(ResultadoEncuesta resultadoEncuesta);
    void UpdateSurveyResponse(ResultadoEncuesta resultadoEncuesta);
    void DeleteSurveyResponse(ResultadoEncuesta resultadoEncuesta);
    Task<ResultadoEncuesta> GetSurveyResponseAsync(int id);
    Task<RecordList<ResultadoEncuesta>> ListSurveyResponseAsync(int IdEncuesta);
    #endregion

    Task<bool> SaveChangesAsync();
}
