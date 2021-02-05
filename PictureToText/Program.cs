using System;
using System.Collections.Generic;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Threading;
using System.Linq;
using WeCantSpell.Hunspell;

namespace PictureToText
{
	class Program
	{
		static string endpoint = "COMPUTER_VISION_ENDPOINT";
		static string subscriptionKey = "COMPUTER_VISION_SUBSCRIPTION_KEY";

		private const string ANALYZE_IMAGE = @"C:\Users\Lex Schlee\OneDrive\Documents\Recipes\20210107_194146.jpg";
		private const string ANALYZE_URL_IMAGE = "https://moderatorsampleimages.blob.core.windows.net/samples/sample16.png";

		static void Main(string[] args)
		{
			FileManager fileManager = new FileManager();
			var imageFilesToRead = fileManager.getFilesFromFolder(@"C:\Users\Lex Schlee\OneDrive\Documents\ImageToTextStuff\Recipes");

			ImageReader imageReader = new ImageReader();
			ComputerVisionClient client = imageReader.Authenticate(endpoint, subscriptionKey);

			StringValidator stringValidator = new StringValidator();

			var filesToCategorize = fileManager.getFilesFromFolder(@"C:\Users\Lex Schlee\OneDrive\Documents\ImageToTextStuff\RecipesToCategorize");


			var imageMerger = new ImageMerger();

			//imageMerger.MergeImageFilesVerticallyByFolder(new DirectoryInfo(@"C:\Users\Lex Schlee\OneDrive\Documents\ImageToTextStuff\ImagesToMerge"));

			foreach (var file in filesToCategorize)
			{
				Console.WriteLine("----------New File----------");
				UserStringCategorizer categorizer = new UserStringCategorizer();
				var allLines = fileManager.getAllLinesFromFile(file.FullName);
				var finalFile = categorizer.categorizeRecipeFileToJson(allLines);
				fileManager.saveStringToFile(@"C:\Users\Lex Schlee\OneDrive\Documents\ImageToTextStuff\CategorizedRecipes\" + file.Name, finalFile);
				fileManager.moveFile(file.FullName, @"C:\Users\Lex Schlee\OneDrive\Documents\ImageToTextStuff\Done\" + file.Name);
				Console.Clear();
			}





			/*

						List<string> measurementAbbreviations = new List<string>();


						foreach(var file in imageFilesToRead)
						{
							imageReader.ReadFileLocal(client, file.FullName).Wait();
						}



						Console.WriteLine("----------------------------------------------------------");


						using (StreamReader val = new StreamReader(@"C:\Users\Lex Schlee\source\repos\PictureToText\PictureToText\MeasurementAbbreviations.txt"))
						{
							while (!val.EndOfStream)
							{
								measurementAbbreviations.Add(val.ReadLine());
							}
						}

						var abbreviationDictionary = WordList.CreateFromWords(measurementAbbreviations);
						var dictionary = WordList.CreateFromFiles(@"english.dic");

						Console.WriteLine("----------------------------------------------------------");
						Console.WriteLine(dictionary.Check("ilz"));
						var suggestions = abbreviationDictionary.Suggest("Thep");
						Console.WriteLine("----------------------------------------------------------");
						Console.WriteLine("----------------------------------------------------------");
			*/
		}



	}
}
