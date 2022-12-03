namespace Day3
{
    internal class Part1
    {
        internal async Task Start()
        {
            string rawInput = await ReadInputFileAsync("./input.txt");
            string[] lines = rawInput.Split('\n');

            int score = 0;

            foreach (var line in lines)
            {
                string left = line.Substring(0, line.Length / 2);
                string right = line.Substring(line.Length / 2, line.Length / 2);

                foreach (char letter in left)
                {
                    if (right.Contains(letter, StringComparison.CurrentCulture))
                    {
                        score += getScoreForLetter(letter);
                        break;
                    }
                }
            }


            Console.WriteLine($"Solution day 3 part 1: {score}");
        }

        int getScoreForLetter(char letter)
        {
            const string lettersOnCorrectPosition = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int indexInArray = lettersOnCorrectPosition.IndexOf(letter);
            return indexInArray + 1;
        }


        async Task<string> ReadInputFileAsync(string path)
        {
            string rawInput = await File.ReadAllTextAsync(path);
            return rawInput;
        }
    }
}
