using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PictureToText
{
	class FileManager
	{
		public List<FileInfo> getFilesFromFolder(string folderPath)
		{
			DirectoryInfo Di = new DirectoryInfo(folderPath);
			return Di.GetFiles().ToList();
		}

		public List<DirectoryInfo> scanForSubFolders(string folderPath)
		{
			DirectoryInfo Di = new DirectoryInfo(folderPath);
			return Di.GetDirectories().ToList();
		}

		public List<string> getAllLinesFromFile(string filePath)
		{
			List<string> allLines = new List<string>();
			using(StreamReader streamReader = new StreamReader(filePath))
			{
				while(!streamReader.EndOfStream)
				{
					allLines.Add(streamReader.ReadLine());
				}
			}
			return allLines;
		}

		public void saveStringToFile(string filePathName, string fileStringToSave)
		{
			File.WriteAllText(filePathName, fileStringToSave);
		}
		public void moveFile(string sourceFilePath, string destinationFilePath)
		{
			File.Move(sourceFilePath, destinationFilePath);
		}
	}
}
