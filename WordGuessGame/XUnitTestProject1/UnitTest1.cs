using System;
using Xunit;
using static WordGuessGame.Program;
using System.IO;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        string path = "../../../../../docs/words.txt";
        [Fact]
        public void TestIfFileReadingInFunctionIsCorrect()
        {
            Assert.Equal(File.ReadAllText(path), ViewWords(path));
        }

        [Fact]
        public void TestIfWordIsAddedToTextFile()
        {
            string word = "flamingo";
            string[] currentFile = File.ReadAllLines(path);
            string[] testOutput = new string[currentFile.Length + 1];
            for (int i = 0; i < testOutput.Length; i++)
            {
                if (i + 1 == testOutput.Length)
                {
                    testOutput[i] = word;
                }
                else
                {
                    testOutput[i] = currentFile[i];
                }
            }
            
            Assert.Equal(testOutput, AddWord(path, word));
        }

        [Fact]
        public void TestIfWordIsRemoved()
        {
            string[] currentFile = File.ReadAllLines(path);
            string word = currentFile[0];

            Assert.NotEqual(currentFile, RemoveWords(path, word));
        }

        [Theory]
        [InlineData('h', new char[] {'H','o','l','a' }, 
            new char[] { '_', '_', '_', '_' }, new char[] { 'H', '_', '_', '_' })]
        [InlineData('e', new char[] {'H','e','l','l','o' }, 
            new char[] { '_', '_', '_', '_', '_' }, new char[] { '_', 'E', '_', '_', '_' })]
        public void TestIfCharacterIsInWord(char guess, char[] currentAnswerArray, 
            char[] currentDisplayArray, char[] answerArray)
        {
            Assert.Equal(answerArray, CheckWordForChar(guess, currentDisplayArray, currentAnswerArray));
        }

    }
}
