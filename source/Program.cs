using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearch
{
    class Program
    {
        static char[,] lines;

        static bool[,] solvedIndexes;
        static bool[,] startingIndexes;
        static int[,] colorIndexes;

        static int colorWordIndex = 0;

        static string[] words = { "ADVANTAGEOUS","AMELIORATE","COMMENCE","COMMENSURATE","CONSOLIDATE","DELETERIOUS","DISSEMINATE","ENDEAVOR","ERRONEOUS","EXPEDITIOUS","FACILITATE","INCEPTION","IMPLEMENT","LEVERAGE","PRESCRIBED","PROFICIENCIES","PROMULGATE","PROXIMITY","REGARDING","REMUNERATION","SUBSEQUENTLY"  };

        static int startX = 0;
        static int startY = 0;

        static int[] tempX;
        static int[] tempY;

        static int direction = 0;

        static string[] fillWordList(string filename)
        {
            string line;
            int lineCounter = 0;
            string[] input;

            System.IO.StreamReader file = new System.IO.StreamReader(filename);
            while ((line = file.ReadLine()) != null)
            {
                lineCounter++;
            }

            input = new string[lineCounter];
            lineCounter = 0;

            file = new System.IO.StreamReader(filename);
            while ((line = file.ReadLine()) != null)
            {
                lineCounter++;
                input[lineCounter - 1] = line.ToUpper();
            }

            file.Close();
            return input;
        }

        static char[,] readLinesFromFile(string filename)
        {
            int lineWidth = 0;
            int lineCounter = 0;
            int counter = 0;
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(filename);
            while((line = file.ReadLine()) != null)
            {
                lineWidth = line.Length;
                lineCounter++;
            }
            char[,] lineArray = new char[lineCounter, lineWidth];
            Console.WriteLine(lineCounter + "x" + lineWidth);
            System.IO.StreamReader file2 = new System.IO.StreamReader(filename);
            while((line = file2.ReadLine()) != null)
            {
                for(int i = 0; i < line.Length; i++)
                {
                    lineArray[counter, i] = line[i];
                }
                counter++;
            }

            file.Close();
            file2.Close();
            return lineArray;
        }

        static void printLines(char[,] array, int height, int width)
        {
            Console.Write("Character Array:\n   |  ");
            for (int h = 0; h < width; h++ )
            {
                if ((h + 1).ToString().Length < 2)
                {
                    Console.Write((h + 1) + "  ");
                }
                else
                {
                    Console.Write((h + 1) + " " );
                }
            }
            Console.Write("\n=====");
            for (int p = 0; p < width; p++)
            {
                Console.Write("===");
            }
            Console.WriteLine("");
            int wordIndex = 0;
            for (int i = 0; i < height; i++)
            {
                int nI = i + 1;
                string iString = nI.ToString();
                if(iString.Length < 2)
                {
                    Console.Write(" " + (i + 1) + " | ");
                }
                else
                {
                    Console.Write((i + 1) + " | ");
                }
                for (int j = 0; j < width; j++)
                {
                    if (startingIndexes[i, j] == true)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(" " + array[i, j] + " ");
                        wordIndex++;
                        //Console.Write(wordIndex);
                    }
                    else if(solvedIndexes[i, j] == true)
                    {
                        switch(colorIndexes[i, j])
                        {
                            case 0:
                                Console.BackgroundColor = ConsoleColor.Green;
                                break;
                            case 1:
                                Console.BackgroundColor = ConsoleColor.Green;
                                break;
                            case 2:
                                Console.BackgroundColor = ConsoleColor.Green;
                                break;
                            case 3:
                                Console.BackgroundColor = ConsoleColor.Green;
                                break;
                        }

                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(" " + array[i, j] + " ");
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(" " + array[i, j] + " ");
                    }
                    
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.Write("\n");
            }
        }

        static void searchAdjacentChars(char[,] chars, int x, int y, bool has_direction, int width, int height, string word, int charIndex, bool hasMatch)
        {
            //if (word != "FISH") { return; }
            char currentChar = word[charIndex - 1];
            if(charIndex >= word.Length)
            {
                
                Console.WriteLine(word + " Found!");
                Console.WriteLine("Starting at " + (startX + 1) + ", " + (startY + 1));
                Console.WriteLine("Ending at " + (x + 1) + ", " + (y + 1));
                Console.WriteLine("-----------------\n");
                //Console.ReadKey();
                startingIndexes[tempY[0], tempX[0]] = true;
                for (int c = 1; c < word.Length; c++ )
                {
                    solvedIndexes[tempY[c], tempX[c]] = true;
                    if (colorWordIndex % 2 == 0)
                    {
                        colorIndexes[tempY[c], tempX[c]] = 1;
                    }
                    else if (colorWordIndex % 2 != 0)
                    {
                        colorIndexes[tempY[c], tempX[c]] = 3;
                    }
                }
                colorWordIndex += 1;
                //Console.Write(wordIndex);
                return;
            }
            currentChar = word[charIndex];
            //Console.Write(charIndex);
            if(!has_direction)
            {
                if (x + 1 < width)
                {
                    if(chars[y, x+1] == currentChar)
                    {
                        x += 1;
                        //Console.WriteLine(tempX[charIndex] + " " + tempY[charIndex]);
                        //Console.WriteLine("    Adjacent Match found at " + (x + 1) + ", " + (y + 1));
                        direction = 2;
                        startX = x - 1;
                        startY = y;
                        hasMatch = true;
                    }
                }
                if (y + 1 < height)
                {
                    if (chars[y + 1, x] == currentChar)
                    {
                        y += 1;
                        //Console.WriteLine("    Adjacent Match found at " + (x + 1) + ", " + (y + 1));
                        direction = 4; 
                        startX = x;
                        startY = y - 1;
                        hasMatch = true;
                    }
                }
                if (x - 1 >= 0)
                {
                    if (chars[y, x - 1] == currentChar)
                    {
                        x -= 1;
                        //Console.WriteLine("here 1");
                        //Console.WriteLine("    Adjacent Match found at " + (x + 1) + ", " + (y + 1));
                        direction = 6;
                        startX = x + 1;
                        startY = y;
                        hasMatch = true;
                    }
                }
                if (y - 1 >= 0)
                {
                    if (chars[y - 1, x] == currentChar)
                    {
                        y -= 1;
                        //Console.WriteLine("    Adjacent Match found at " + (x + 1) + ", " + (y + 1));
                        direction = 0;
                        startX = x;
                        startY = y + 1;
                        hasMatch = true;
                    }
                }

                //diagonals
                if (x + 1 < width && y - 1 >= 0)
                {
                    if (chars[y - 1, x + 1] == currentChar)
                    {
                        x += 1;
                        y -= 1;
                        //Console.WriteLine("    Adjacent Match found at " + (x + 1) + ", " + (y + 1));
                        direction = 1;
                        startX = x - 1;
                        startY = y + 1;
                        hasMatch = true;
                    }
                }
                if (y + 1 < height && x + 1 < width)
                {
                    if (chars[y + 1, x + 1] == currentChar)
                    {
                        y += 1;
                        x += 1;
                        //Console.WriteLine("    Adjacent Match found at " + (x + 1) + ", " + (y + 1));
                        direction = 3;
                        startX = x - 1;
                        startY = y - 1;
                        hasMatch = true;
                    }
                }
                if (x - 1 > 0 && y + 1 < height)
                {
                    if (chars[y + 1, x - 1] == currentChar)
                    {
                        x -= 1;
                        y += 1;
                        //Console.WriteLine("    Adjacent Match found at " + (x + 1) + ", " + (y + 1));
                        direction = 5;
                        startX = x + 1;
                        startY = y - 1;
                        hasMatch = true;
                    }
                }
                if (y - 1 > 0 && x - 1 > 0)
                {
                    if (chars[y - 1, x - 1] == currentChar)
                    {
                        y -= 1;
                        x -= 1;
                        //Console.WriteLine("    Adjacent Match found at " + (x + 1) + ", " + (y + 1));
                        direction = 7;
                        startX = x + 1;
                        startY = y + 1;
                        hasMatch = true;
                    }
                }
                if(hasMatch)
                {
                    tempX[charIndex] = x;
                    tempY[charIndex] = y;
                    searchAdjacentChars(chars, x, y, true, width, height, word, charIndex + 1, true);
                    //Console.WriteLine("Has match");
                }
            }
            else
            {
                //Console.WriteLine(direction);
                int newX = x;
                int newY = y;
                //Console.WriteLine(x + ", " + y);
                switch(direction)
                {
                    case 0:
                        newY -= 1;
                        break;
                    case 1:
                        newX += 1;
                        newY -= 1;
                        break;
                    case 2:
                        newX += 1;
                        break;
                    case 3:
                        newX += 1;
                        newY += 1;
                        break;
                    case 4:
                        newY += 1;
                        break;
                    case 5:
                        newX -= 1;
                        newY += 1;
                        break;
                    case 6:
                        newX -= 1;
                        break;
                    case 7:
                        newX -= 1;
                        newY -= 1;
                        break;
                    default:

                        break;
                }
                if(newX >= width || newY >= height || newX < 0 || newY < 0)
                {
                    return;
                }
                else if(chars[newY, newX] == currentChar)
                {
                    x = newX;
                    y = newY;
                    tempX[charIndex] = x;
                    tempY[charIndex] = y;
                    searchAdjacentChars(chars, x, y, true, width, height, word, charIndex + 1, true);
                    //Console.WriteLine("    Adjacent Match found at " + (x + 1) + ", " + (y + 1));
                }
                else
                {
                    return;
                }
            }
            
        }

        static void findWords(char[,] chars, string[] words, int height, int width)
        {
            for (int wordIndex = 0; wordIndex < words.Length; wordIndex++ )
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        string currentWord = words[wordIndex];
                        if (chars[y, x] == currentWord[0])
                        {
                            //Console.WriteLine("Match at " + (x + 1) + ", " + (y + 1));
                            tempX[0] = x;
                            tempY[0] = y;
                            searchAdjacentChars(chars, x, y, false, width, height, currentWord, 1, false);
                        }
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            int height = 20;
            int width = 20;
            Console.Title = "Word Search Solver";
            solvedIndexes = new bool[height, width];
            startingIndexes = new bool[height, width];
            colorIndexes = new int[height, width];
            Console.ForegroundColor = ConsoleColor.Yellow;
            tempX = new int[20];
            tempY = new int[20];
            for (int i = 0; i < width; i ++ )
            {

                for(int j = 0; j < height; j ++)
                {
                    solvedIndexes[i, j] = false;
                }
            }
            words = fillWordList("ToFind.txt");
            lines = readLinesFromFile("CharArray.txt");
            findWords(lines, fillWordList("ToFind.txt"), height, width);
            printLines(lines, height, width);
            for (int t = 0; t < 20; t++)
            {
                for (int s = 0; s < 20; s++)
                {
                    //Console.Write(solvedIndexes[t, s]);
                }
                //solvedIndexes[tempY[t], tempX[t]] = true;
            }
            Console.ReadKey();
            
        }
                
    }
}
