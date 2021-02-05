using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PictureToText
{
	class UserStringCategorizer
	{
		string userInput = "";
		string stringToCategorize = "";
		List<string> recipeName = new List<string>();
		List<string> measurements = new List<string>();
		List<string> directions = new List<string>();
		StringValidator validator = new StringValidator();

		public string categorizeRecipeFileToJson(List<string> lines)
		{
			var dictionary = new Dictionary<string, List<string>>();
			foreach(var line in lines)
			{
				categorizeRecipeString(line);
			}
			dictionary.Add("title", recipeName);
			dictionary.Add("measurements", measurements);
			dictionary.Add("directions", directions);

			return JsonConvert.SerializeObject(dictionary);
		}

		void categorizeRecipeString(string val)
		{
			stringToCategorize = validator.correctRecipeStringMistakes(val);
			outputLine(stringToCategorize);
			getUserInput();
			determineInputAction(userInput);
		}
		void getUserInput()
		{
			userInput = Console.ReadLine();
		}

		void outputLine(string val)
		{
			Console.WriteLine(val);
		}

		void determineInputAction(string val)
		{
			switch(val)
			{
				case "1":
					recipeName.Add(stringToCategorize);
					break;
				case "2":
					measurements.Add(stringToCategorize);
					break;
				case "3":
					directions.Add(stringToCategorize);
					break;
				case "4":
					break;
				case "w":
					getUserInput();
					measurements.Add(userInput);
					break;
				case "e":
					getUserInput();
					directions.Add(userInput);
					break;
				default:
					userInput = val;
					categorizeRecipeString(userInput);
					break;
			}
		}
	}
}
