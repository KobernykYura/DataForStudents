using DataForStudents.Storage;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataForStudents.Services
{
	/// <summary>
	/// Сервис по предоставлению данных.
	/// </summary>
	public class DataService : DataLoader.DataLoaderBase
	{
		private readonly ILogger<GreeterService> _logger;
		private readonly IDataStore _data;
		public DataService(ILogger<GreeterService> logger, IDataStore dataStore)
		{
			this._logger = logger;
			this._data = dataStore;
		}

		/// <summary>
		/// Возвращение всех данных.
		/// </summary>
		public async override Task<TaskDataReply> FetchData(TaskDataRequest request, ServerCallContext context)
		{
			try
			{
				string fileData = await this._data.Read(request.AcademicDisciplineId, request.TaskId);

				return new TaskDataReply { Data = fileData, DataFormat = ".txt" };
			}
			catch (System.Exception ex)
			{
				this._logger.LogError(ex, "Что-то не так при чтении файла!");
				throw;
			}
		}

		/// <summary>
		/// Возвращение всех строк в потоке.
		/// </summary>
		public async override Task FetchDataAsStream(TaskDataRequest request, IServerStreamWriter<TaskDataReply> responseStream, ServerCallContext context)
		{
			try
			{
				ICollection<string> fileData = await this._data.ReadLines(request.AcademicDisciplineId, request.TaskId);
				foreach (var fileLine in fileData)
				{
					await responseStream.WriteAsync(new TaskDataReply { Data = fileLine, DataFormat = "string" });
				}
			}
			catch (System.Exception ex)
			{
				this._logger.LogError(ex, "Что-то не так при чтении файла!");
				throw;
			}
		}
	}
}
