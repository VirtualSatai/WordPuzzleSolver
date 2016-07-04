using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordPuzzleSolver
{
    class Program
    {
        private static int _count;
        static void Main(string[] args)
        {
            if (args.Length != 2)
                return;

            // Read input puzzle
            var inputPuzzle = System.IO.File.ReadAllLines(args[0]).ToList();

            // Read input wordlist
            var inputWords = System.IO.File.ReadAllLines(args[1]).ToList();

            var wordList = new List<string>();
            inputWords.ForEach(x => wordList.Add(x.Replace(" ", "").ToUpper()));

            // Find Horizontal Words
            FindWords(inputPuzzle, wordList, "Horizontal", false);

            // Find reversed horizontal Words
            FindWords(ReverseList(inputPuzzle), wordList, "Reversed Horizontal", true);

            // Find Vertical Words
            FindWords(MakeVerticalList(inputPuzzle), wordList, "Vertical", false);

            // Find Reversed Vertical words
            FindWords(ReverseList(MakeVerticalList(inputPuzzle)), wordList, "Reversed Vertical", true);

            // Find Crossing Words (-45 deg)
            FindWords(MakeCrossList(inputPuzzle), wordList, "Crossing, top-left to bottom-right (-45 deg)", false);

            // Find Reversed Crossing Words (135 deg)
            FindWords(ReverseList(MakeCrossList(inputPuzzle)), wordList, "Crossing, bottom-right to top-left (135 deg)", true);

            // Find Crossing Words (215 deg) 
            FindWords(MakeCrossList(ReverseList(inputPuzzle)), wordList, "Crossing, top-right to bottom-left (215 deg)", true);

            // Find Crossing Words (45 deg) 
            FindWords(MakeCrossList(ReverseList(MakeVerticalList(inputPuzzle))), wordList, "Crossing, bottom-left to top-right (45 deg)", true);
        }

        public static List<string> MakeVerticalList(List<string> inputPuzzle)
        {
            var output = new List<string>();
            for (int i = 0; i < inputPuzzle[0].Length; i++)
            {
                StringBuilder tmp = new StringBuilder(inputPuzzle.Count);
                for (int j = 0; j < inputPuzzle.Count; j++)
                {
                    tmp.Append(inputPuzzle[j][i]);
                }
                output.Add(tmp.ToString());
            }

            return output;
        }

        public static void FindWords(List<string> inpPuzzle, List<string> wordlist, string direction, bool reversed)
        {
            for (var i = 0; i < inpPuzzle.Count; i++)
            {
                foreach (var word in wordlist.Where(word => inpPuzzle[i].Contains(word)))
                {
                    Console.WriteLine("{4}: {3}: {0},{2} contains  \"{1}\"", i + 1, word, reversed ? inpPuzzle[i].Length - inpPuzzle[i].IndexOf(word) : inpPuzzle[i].IndexOf(word) + 1, direction, ++_count);
                }
            }
        }

        public static List<string> MakeCrossList(List<string> inputPuzzle)
        {
            var result = new List<string>();
            for (var i = 0; i < inputPuzzle[i].Length - 1; i++)
            {
                var tmp = new StringBuilder(inputPuzzle.Count * 2);
                for (var j = 0; j < inputPuzzle.Count - i; j++)
                {
                    var c = inputPuzzle[j + i][j];
                    tmp.Append(c);
                }
                result.Add(tmp.ToString());
            }

            result.Add(inputPuzzle[inputPuzzle.Count - 1][0].ToString());


            for (var i = 0; i < inputPuzzle[i].Length - 1; i++)
            {
                var tmp = new StringBuilder(inputPuzzle.Count * 2);
                for (var j = 0; j < inputPuzzle.Count - i; j++)
                {
                    var c = inputPuzzle[j][j + i];
                    tmp.Append(c);
                }
                result.Add(tmp.ToString());
            }

            result.Add(inputPuzzle[0][inputPuzzle[0].Length - 1].ToString());

            return result;
        }

        public static List<string> ReverseList(List<string> stringList)
        {
            return stringList.Select(Reverse).ToList();
        }

        public static string Reverse(string s)
        {
            var charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
