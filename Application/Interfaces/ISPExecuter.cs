using Domain.Entities;

namespace Application.Interfaces;

public interface ISPExecuter<T> where T : class
{
	Task<List<T>> ExecuteStoredProcedureAsync<T>(string storedProcName, params object[] parameters);
}
