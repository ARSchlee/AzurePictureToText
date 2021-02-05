using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using WeCantSpell.Hunspell;

namespace PictureToText
{
	class StringValidator
	{
		WordList dictionary = WordList.CreateFromFiles(@"english.dic");

		public string correctRecipeStringMistakes(string val)
		{
			var returnString = val;

			if (checkCookTempMistake(val))
				returnString = correctCookTempMistake(val);

			if (checkFractionMistake(val))
				returnString = correctFractionMistake(val);

			if (checkOuncesMistake(val))
				returnString = correctOunceMistake(val);

			return returnString;
		}

		bool checkCookTempMistake(string val)
		{
			Regex regexIfDegreeIsZero = new Regex(@"[0-9]{3}[0]");
			return (regexIfDegreeIsZero.IsMatch(val));
		}

		bool checkFractionMistake(string val)
		{
			Regex regexIfNumShouldBeFraction = new Regex(@"[ilI1-9]{2}[z2-58]");
			return (regexIfNumShouldBeFraction.IsMatch(val));
		}

		bool checkOuncesMistake(string val)
		{
			Regex regexIfOuncesIs02 = new Regex(@"02$");
			return (regexIfOuncesIs02.IsMatch(val));
		}

		string checkSpellingMistake(string val)
		{
			Regex regexIfContainsNum = new Regex(@"[0-9]+");

			var words = val.Split(" ");

			for(int i = 0; i < words.Length; i++)
			{
				if (!dictionary.Check(words[i]))
					words[i] = correctSpellingMistake(words[i]);
			}

			return String.Join(" ",words);
		}

		string correctCookTempMistake(string val)
		{
			return val.Substring(0, 3) + "°";
		}

		string correctFractionMistake(string val)
		{
			var returnString = val.ToCharArray();

			if (isOneLookAlike(returnString[0]))
				returnString[0] = '1';

			returnString[1] = '/';

			if (isTwoLookAlike(returnString[2]))
				returnString[2] = '2';

			return new string(returnString);
		}

		string correctOunceMistake(string val)
		{
			return val.Substring(0, val.Length - 2) + " oz";
		}

		string correctSpellingMistake(string val)
		{
			return dictionary.Suggest(val).ToString();
		}

		bool isOneLookAlike(char val)
		{
			if (val == 'i' || val == 'l' || val == 'I')
				return true;
			else
				return false;
		}

		bool isTwoLookAlike(char val)
		{
			if (val == 'z' || val == '5')
				return true;
			else
				return false;
		}
	}
}
