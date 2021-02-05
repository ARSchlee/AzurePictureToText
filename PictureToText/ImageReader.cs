using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PictureToText
{
	class ImageReader
	{
		public ComputerVisionClient Authenticate(string endpoint, string key)
		{
			ComputerVisionClient client =
				new ComputerVisionClient(new ApiKeyServiceClientCredentials("f327f60e17bb400d93b2ca164420667d"))
				{ Endpoint = "https://alex-schlee-cv-testing.cognitiveservices.azure.com/" };
			return client;
		}

		public async Task ReadFileLocal(ComputerVisionClient client, string imageFile)
		{
			Console.WriteLine("-----------------------------------------------");
			Console.WriteLine("Read Image from file");
			Console.WriteLine();

			var textHeaders = await client.ReadInStreamAsync(File.OpenRead(imageFile), language: "en");

			string operationLocation = textHeaders.OperationLocation;
			Thread.Sleep(2000);

			const int numberOfCharsInOperationId = 36;
			string operationId = operationLocation.Substring(operationLocation.Length - numberOfCharsInOperationId);

			ReadOperationResult results;
			Console.WriteLine($"Reading text from local file {Path.GetFileName(imageFile)}...");
			Console.WriteLine();
			do
			{
				results = await client.GetReadResultAsync(Guid.Parse(operationId));
			} while ((results.Status == OperationStatusCodes.Running || results.Status == OperationStatusCodes.NotStarted));

			SaveOutputToFile(results.AnalyzeResult.ReadResults, imageFile);
		}

		void SaveOutputToFile(IList<ReadResult> results, string sourceFile)
		{
			foreach (ReadResult page in results)
			{
				var outputFileName = @"C:\Users\Lex Schlee\OneDrive\Documents\ImageToTextStuff\ImageReaderOutput\" + page.Lines[0].Text;
				using (StreamWriter outputFile = new StreamWriter(outputFileName + ".txt"))
				{
					SaveInputFile(sourceFile, outputFileName);
					foreach (Line line in page.Lines)
					{
						outputFile.WriteLine(line.Text);
					}
				}
			}
		}

		void SaveInputFile(string sourceFile, string outputFileName)
		{
			File.Copy(sourceFile, outputFileName + ".jpeg");
		}
	}
}
