namespace Day3
{
    internal class Part2
    {
        internal async Task Start()
        {
            string rawInput = await ReadInputFileAsync("./input.txt");
            string[] lines = rawInput.Split('\n');

            int score = 0;

            for (int i = 0; i < lines.Length; i += 3)
            {
                string line1 = lines[i];
                string line2 = lines[i + 1];
                string line3 = lines[i + 2];

                foreach (char letter in line1)
                {
                    if (line2.Contains(letter, StringComparison.CurrentCulture) && line3.Contains(letter, StringComparison.CurrentCulture))
                    {
                        int letterScore = getScoreForLetter(letter);
                        score += letterScore;
                        break;
                    }
                }
            }
            Console.WriteLine($"Solution day 3 part 2: {score}");
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
