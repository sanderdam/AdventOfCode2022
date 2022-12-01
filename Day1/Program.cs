using System.Diagnostics;

string rawInput = await ReadInputFileAsync("./d1p1.txt");
List<Elf> elves = SplitStringToElfs(rawInput);

var elfWithMaxCalories = elves.Max(e => e.TotalCalories);
Console.WriteLine($"Solution for day 1: {elfWithMaxCalories}");

var elvesSortedByMostCalories = elves.OrderByDescending(e => e.TotalCalories);
var topThreeElvesWithMostCalories = elvesSortedByMostCalories.Take(3);
int totalAmountOfCaloriesOfTopThreeElves = topThreeElvesWithMostCalories.Sum(e => e.TotalCalories);
Console.WriteLine($"Solution for day 2: {totalAmountOfCaloriesOfTopThreeElves}");


List<Elf> SplitStringToElfs(string rawInput)
{
    List<Elf> elfs = new List<Elf>();

    var rawInputAsSpan = rawInput.AsSpan().Trim();
    const string splitByElf = "\r\n\r\n";

    while (rawInputAsSpan.Length > 0)
    {
        int indexOfNewElf = rawInputAsSpan.IndexOf(splitByElf);
        ReadOnlySpan<char> sliceForElf;
        if (indexOfNewElf != -1)
        {
            sliceForElf = rawInputAsSpan.Slice(0, indexOfNewElf);
            rawInputAsSpan = rawInputAsSpan.Slice(indexOfNewElf).Trim();
        }
        else
        {
            sliceForElf = rawInputAsSpan;
            rawInputAsSpan = ReadOnlySpan<char>.Empty;
        }

        var newElf = new Elf(Calories: new List<int>());

        const string splitByCalorie = "\r\n";
        while (sliceForElf.Length > 0)
        {
            int indexOfNewCalorie = sliceForElf.IndexOf(splitByCalorie);
            ReadOnlySpan<char> sliceForCalorie;
            if (indexOfNewCalorie != -1)
            {
                sliceForCalorie = sliceForElf.Slice(0, indexOfNewCalorie);
                sliceForElf = sliceForElf.Slice(indexOfNewCalorie).Trim();
            }
            else
            {
                sliceForCalorie = sliceForElf;
                sliceForElf = ReadOnlySpan<char>.Empty;
            }

            int calorie = int.Parse(sliceForCalorie);
            newElf.Calories.Add(calorie);            
        }
        elfs.Add(newElf);
    }

    return elfs;
}

async Task<string> ReadInputFileAsync(string path)
{
    string rawInput = await File.ReadAllTextAsync(path);
    return rawInput;
}

[DebuggerDisplay("TotalCalories: {TotalCalories}")]
record Elf(List<int> Calories)
{
    public int TotalCalories => this.Calories.Sum();
}