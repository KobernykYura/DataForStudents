using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

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
		const string fileUrl = "C:\\Users\\kober\\OneDrive\\Дакументы\\{0}\\{1}\\WritableFile.txt";

		/// <summary>
		/// Полное чтение файла, возвращеие полного текста файла.
		/// </summary>
		public async Task<string> Read(string discipline = "BigData_R", string task = "Lab1")
		{
			string filePath = string.Format(fileUrl, discipline, task);
			if (!File.Exists(filePath))
				throw new FileNotFoundException($"Файл -- {filePath} -- НЕНАЙДЕН!");

			return await File.ReadAllTextAsync(fileUrl);
		}

		/// <summary>
		/// Возвращение коллекции строк файла.
		/// </summary>
		public async Task<ICollection<string>> ReadLines(string discipline = "BigData_R", string task = "Lab1")
		{
			string filePath = string.Format(fileUrl, discipline, task);
			if (!File.Exists(filePath))
				throw new FileNotFoundException($"Файл -- {filePath} -- НЕНАЙДЕН!");

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
