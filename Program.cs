using System;

namespace Hangman
{
    class Handman
    {
        public static void Main(string[] args)
        {
            //Select random word to start game with
            var word = GetWord();

            //Start the game
            PlayGame(word);
        }
        /// <summary>
        /// Asks the user for guesses to a 
        /// </summary>
        /// <param name="word">The word the player is trying to guess</param>
        public static void PlayGame(string word)
        {
            //Create the "empty" word with underscores replacing the letters
            var wordUnderscores = InstantiateUnderscoreArray(word);

            //Create a list to keep track of the player's guesses
            List<string> guesses = new List<string>();

            //Indexing the amount of turns that have been taken
            var i = 0;

            //While the player hasn't guessed the right word...
            while(i <= 8)
            {
                //Draw the hangman board
                DrawHangman(i);
                //Spacer
                Console.WriteLine();
                //Write the letters/underscores based on how much the player has guessed correctly
                Console.WriteLine(wordUnderscores);

                //Ask the player for input
                Console.WriteLine("What is your guess? (If you guess a word with a number" +
                                     " in it or a number, that will count)");
                //Read the user input
                var userInput = Console.ReadLine();

                //Spacer
                Console.WriteLine();

                //If the user has guessed this letter/word before...
                if(guesses.Contains(userInput))
                {
                    //Let them know they have guessed it before and keep going
                    Console.WriteLine($"You have already guessed {userInput}");
                    continue;
                }
                // Check whether they guess a letter or a word
                if(char.TryParse(userInput, out var parsedInput))
                {
                    //Check if the letter is in the word
                    if(word.Contains(userInput))
                    {
                        //Let the user know they guessed a correct letter
                        Console.WriteLine($"The word does have {userInput}(s) in it");
                        //Update the guess list to have the letter the user guessed
                        wordUnderscores = UpdateUnderscoreArray(wordUnderscores, word, userInput);
                    }
                    else
                    {
                        //If the word doesn't contain the letter, let the user know and iterate the board by 1 appendage
                        Console.WriteLine($"The word does not contain any {userInput}s");
                        i+=1;
                    }
                }
                //If the guess isn't a character and isn't the word, we are assuming that they guessed a wrong word
                else if(userInput != word)
                {
                    //Let the player know that they guessed the word incorrectly
                    Console.WriteLine($"{userInput} was not the word.");
                    //Increase the board forward by one appendage
                    i+=1;

                } else if(userInput == word)
                {
                    //If the player guessed the correct word, they win!
                    Console.WriteLine($"You win!! The word was {word}");
                    break;
                }
                //The end of the game
                if(i == 9)
                {
                    //If we get to the end of the available boards, the player lost the game
                    Console.WriteLine($"You lost :/ the word was {word}");
                }
                //Spacer
                Console.WriteLine();

                //Add the guess so that a player can't repeat guesses
                guesses.Add(userInput);

            }
        }

        /// <summary>
        /// Gives a random word from a list of ~20 options
        /// </summary>
        /// <returns>A randomly selected word from the available options</returns>
        public static string GetWord()
        {
            //Create the word list
            List<string> wordList = new List<string>();

            //Add words to the list
            wordList.Add("crank");
            wordList.Add("break");
            wordList.Add("play");
            wordList.Add("treat");
            wordList.Add("pants");
            wordList.Add("harder");
            wordList.Add("lights");
            wordList.Add("streaks");
            wordList.Add("jam");
            wordList.Add("hippocampus");
            wordList.Add("common");
            wordList.Add("rocks");
            wordList.Add("chair");
            wordList.Add("allowed");
            wordList.Add("dreaded");
            wordList.Add("supply");
            wordList.Add("tried");
            wordList.Add("tread");
            wordList.Add("upwards");

            //Create a new random object from System.Random;
            Random rand = new Random();

            //Find a random index from zero to the length of the word list
            int random = rand.Next(0, wordList.Count -1);

            //Return the word given at the random index
            return wordList[random];
        }

        /// <summary>
        /// Creates a new string that is underscores representing letters in a word seperated by a space
        /// </summary>
        /// <param name="word">The word that the underscores will represent</param>
        /// <returns>String of underscores the length of the word</returns>
        public static string InstantiateUnderscoreArray(string word)
        {
            //Create list to populate with underscores
            List<string> wordList = new List<string>();

            //Populate list with underscores the same length as the word
            while(wordList.Count < word.Length)
            {
                wordList.Add("_");
            }

            //Return the list as a string with spaces seperating each underscore
            return string.Join(" ", wordList);
        }
        /// <summary>
        /// Updates the undescore array to show the correct guesses the player has made.
        /// This function assumes that there is at least one instance of the letter in the word.
        /// </summary>
        /// <param name="underscores">Original underscores string that will be updated</param>
        /// <param name="word">The word the player is guessing</param>
        /// <param name="letter">The letter the player guessed</param>
        /// <returns></returns>
        public static string UpdateUnderscoreArray(string underscores, string word, string letter)
        {
            //Convert the underscores string to a list
            var underscoresList = underscores.Split(" ");

            //Loop through the word at each instance of the guessed letter until it gets to the end
            for (int i = word.IndexOf(letter); i > -1; i = word.IndexOf(letter, i + 1))
            {
                    // At the index of the underscores list matching the instance of the letter from the word, swithc _ to letter
                    underscoresList[i] = letter;
            }
            //Return the list converted to a string seperated by spaces
            return string.Join(" ", underscoresList);
        }

        /// <summary>
        /// Based on how many guesses have been made, draw the corresponding hangman
        /// </summary>
        /// <param name="i">The amount of guesses that a player has made</param>
        /// <returns></returns>    
        public static void DrawHangman(int i)
        {
            //If we have reached a certain instance, draw the respective board
            if(i == 0)
            {
                Console.WriteLine();
                Console.WriteLine("      |----- ");
                Console.WriteLine("      |      ");
                Console.WriteLine("      |      ");
                Console.WriteLine("      |      ");
                Console.WriteLine("      |      ");
                Console.WriteLine("______|______");
            }
            else if(i == 1)
            {
                Console.WriteLine();
                Console.WriteLine("      |----- ");
                Console.WriteLine("      |    O ");
                Console.WriteLine("      |      ");
                Console.WriteLine("      |      ");
                Console.WriteLine("      |      ");
                Console.WriteLine("______|______");
            }
            else if(i == 2)
            {
                Console.WriteLine();
                Console.WriteLine("      |----- ");
                Console.WriteLine("      |    O ");
                Console.WriteLine("      |    | ");
                Console.WriteLine("      |      ");
                Console.WriteLine("      |      ");
                Console.WriteLine("______|______");
            }
            else if(i == 3)
            {
                Console.WriteLine();
                Console.WriteLine("      |----- ");
                Console.WriteLine("      |    O ");
                Console.WriteLine("      |   /| ");
                Console.WriteLine("      |      ");
                Console.WriteLine("      |      ");
                Console.WriteLine("______|______");
            }
            else if(i == 4)
            {
                Console.WriteLine();
                Console.WriteLine("      |----- ");
                Console.WriteLine("      |    O ");
                Console.WriteLine("      |   /|\\");
                Console.WriteLine("      |      ");
                Console.WriteLine("      |      ");
                Console.WriteLine("______|______");
            }
            else if(i == 5)
            {
                Console.WriteLine();
                Console.WriteLine("      |----- ");
                Console.WriteLine("      |    O ");
                Console.WriteLine("      |   /|\\");
                Console.WriteLine("      |    | ");
                Console.WriteLine("      |      ");
                Console.WriteLine("______|______");
            }
            else if(i == 6)
            {
                Console.WriteLine();
                Console.WriteLine("      |----- ");
                Console.WriteLine("      |    O ");
                Console.WriteLine("      |   /|\\");
                Console.WriteLine("      |    | ");
                Console.WriteLine("      |   /  ");
                Console.WriteLine("______|______");
            }
            else if(i == 7)
            {
                Console.WriteLine();
                Console.WriteLine("      |----- ");
                Console.WriteLine("      |    O ");
                Console.WriteLine("      |   /|\\");
                Console.WriteLine("      |    | ");
                Console.WriteLine("      |   / \\");
                Console.WriteLine("______|______");         
            }
            else if(i == 8)
            {
                Console.WriteLine();
                Console.WriteLine("      |----- ");
                Console.WriteLine("      |    O ");
                Console.WriteLine("      |   /|\\");
                Console.WriteLine("      |    | ");
                Console.WriteLine("      |   / \\");
                Console.WriteLine("______|______");         
            }
        }
    }
}