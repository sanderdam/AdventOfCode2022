using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2
{
    internal class Part1
    {
        internal async Task Start()
        {
            string rawInput = await ReadInputFileAsync("./input.txt");
            var game = ParseText(rawInput);

            int totalScoreOpponentB = game.Rounds.Sum(r => r.ScoreOpponentB);
            Console.WriteLine($"Part 1: The total score of opponent B: {totalScoreOpponentB}");

        }
        Game ParseText(string rawInput)
        {
            Game game = new Game(Rounds: Enumerable.Empty<Round>().ToList());
            ReadOnlySpan<char> rawInputAsSpan = rawInput.AsSpan().Trim();

            while (rawInputAsSpan.Length > 0)
            {
                rawInputAsSpan = GetNextLine(rawInputAsSpan, out var line);
                var round = ParseRound(line);
                game.Rounds.Add(round);
            }

            return game;
        }

        async Task<string> ReadInputFileAsync(string path)
        {
            string rawInput = await File.ReadAllTextAsync(path);
            return rawInput;
        }

        ReadOnlySpan<char> GetNextLine(ReadOnlySpan<char> input, out ReadOnlySpan<char> line)
        {
            int indexOfNextNewLine = input.IndexOf(Environment.NewLine);

            if (indexOfNextNewLine != -1)
            {
                line = input.Slice(0, indexOfNextNewLine);
                return input.Slice(indexOfNextNewLine).Trim();
            }
            else
            {

                line = input;
                return ReadOnlySpan<char>.Empty;
            }
        }

        Round ParseRound(ReadOnlySpan<char> line)
        {
            Round round = new Round();
            ReadOnlySpan<char> inputOpponentA = line.Slice(0, 1);
            ReadOnlySpan<char> inputOpponentB = line.Slice(2, 1);

            round.ChoiceOpponentA = inputOpponentA switch
            {
                "A" => TurnChoice.Rock,
                "B" => TurnChoice.Paper,
                "C" => TurnChoice.Scissor
            };
            round.ChoiceOpponentB = inputOpponentB switch
            {
                "X" => TurnChoice.Rock,
                "Y" => TurnChoice.Paper,
                "Z" => TurnChoice.Scissor
            };

            round.CalculateScores();

            return round;
        }

        record Game(List<Round> Rounds);

        class Round
        {
            public TurnChoice ChoiceOpponentA { get; set; }
            public TurnChoice ChoiceOpponentB { get; set; }

            public int ScoreOpponentA { get; set; }
            public int ScoreOpponentB { get; set; }

            public void CalculateScores()
            {
                ScoreOpponentA = CalculateScoreForPlayer(ChoiceOpponentA, ChoiceOpponentB);
                ScoreOpponentB = CalculateScoreForPlayer(ChoiceOpponentB, ChoiceOpponentA);
            }

            private int CalculateScoreForPlayer(TurnChoice playerToCalculateScore, TurnChoice opponent)
            {
                int score = 0;
                if (playerToCalculateScore == TurnChoice.Rock && opponent == TurnChoice.Scissor) { score = score + 6; }
                if (playerToCalculateScore == TurnChoice.Paper && opponent == TurnChoice.Rock) { score = score + 6; ; }
                if (playerToCalculateScore == TurnChoice.Scissor && opponent == TurnChoice.Paper) { score = score + 6; ; }
                if (playerToCalculateScore == opponent) { score = score + 3; }

                score = score + (int)playerToCalculateScore;
                return score;
            }
        }

        enum TurnChoice
        {
            Rock = 1,
            Paper = 2,
            Scissor = 3
        }
    }
}
