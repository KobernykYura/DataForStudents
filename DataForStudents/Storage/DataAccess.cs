using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;

namespace DataForStudents.Storage
{
	/// <summary>
	/// Класс для доступа к данным.
	/// </summary>
	public class DataAccess : IDataStore
	{
		/// <summary>
		/// Шаблон пути к файлу с данными.
		/// </summary>
		const string fileUri = @"\StatisticData\{0}\{1}.csv";

		private readonly string currentDirectory;

		public DataAccess()
		{
			this.currentDirectory = Directory.GetCurrentDirectory();
		}

		/// <summary>
		/// Полное чтение файла, возвращеие полного текста файла.
		/// </summary>
		public async Task<string> Read(string discipline = "Lab1", string task = "001")
		{
			StringBuilder builder = new StringBuilder(this.currentDirectory);
			string filePath = builder.AppendFormat(fileUri, discipline, task).ToString();

			if (!File.Exists(filePath))
				throw new FileNotFoundException($"File -- {filePath} -- NOT FOUND!");

			return await File.ReadAllTextAsync(filePath);
		}

		/// <summary>
		/// Возвращение коллекции строк файла.
		/// </summary>
		public async Task<ICollection<string>> ReadLines(string discipline = "Lab1", string task = "001")
		{
			StringBuilder builder = new StringBuilder(this.currentDirectory);
			string filePath = builder.AppendFormat(fileUri, discipline, task).ToString();

			if (!File.Exists(filePath))
				throw new FileNotFoundException($"File -- {filePath} -- NOT FOUND!");

			return await File.ReadAllLinesAsync(filePath);
		}
	}

	/// <summary>
	/// Интерфейс хранилища.
	/// </summary>
	public interface IDataStore
	{
		/// <summary>
		/// Полное чтение файла, возвращеие полного текста файла.
		/// </summary>
		Task<string> Read(string discipline, string task);

		/// <summary>
		/// Возвращение коллекции строк файла.
		/// </summary>
		Task<ICollection<string>> ReadLines(string discipline, string task);
	}
}
