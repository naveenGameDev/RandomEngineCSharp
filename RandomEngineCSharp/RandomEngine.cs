using System;
using System.Collections.Generic;


public class RandomEngine
{
    private long current;
    private readonly long a = 1664525;  // Multiplier
    private readonly long c = 1013904223; // Increment
    private readonly long m = (long)Math.Pow(2, 32); // Modulus (32-bit)

    public RandomEngine(int seed)
    {
        current = seed;
    }

    /// <summary>
    /// generates a random number in between min and max, both inclusive
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public int Next(int min, int max)
    {
        current = (a * current + c) % m;
        return (int)(min + (current % (max - min + 1)));
    }

    /// <summary>
    /// Get a random item from a list.
    /// </summary>
    public T RandomElement<T>(List<T> list)
    {
        if (list == null || list.Count == 0)
            throw new ArgumentException("The list cannot be null or empty.");

        int index = Next(0, list.Count - 1);
        return list[index];
    }

    /// <summary>
    /// Generate a random floating-point number in the range [min, max].
    /// </summary>
    public double NextDouble(double min, double max)
    {
        current = (a * current + c) % m; // Update the current state
        double normalized = (double)current / m; // Normalize to [0, 1]
        return min + (normalized * (max - min));
    }

    /// <summary>
    /// Generate a random boolean value.
    /// </summary>
    public bool NextBool()
    {
        return Next(0, 1) == 1;
    }

    /// <summary>
    /// shuffles the provided list, changes the original list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    public void Shuffle<T>(List<T> list)
    {
        if (list == null || list.Count < 2) return;

        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Next(0, i);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }

    /// <summary>
    /// Select a random key from a dictionary where the value is the weight.
    /// Items with a weight of 0 are never selected.
    /// </summary>
    public T WeightedRandom<T>(Dictionary<T, int> weightedItems)
    {
        if (weightedItems == null || weightedItems.Count == 0)
            throw new ArgumentException("The dictionary cannot be null or empty.");

        // Calculate total weight, ignoring items with weight 0
        int totalWeight = 0;
        foreach (var weight in weightedItems.Values)
        {
            if (weight < 0)
                throw new ArgumentException("Weights must be non-negative.");
            totalWeight += weight;
        }

        if (totalWeight == 0)
            throw new InvalidOperationException("Total weight must be greater than 0.");

        // Generate a random number in the range [0, totalWeight - 1]
        int randomValue = Next(0, totalWeight - 1);

        // Find the corresponding item
        foreach (var kvp in weightedItems)
        {
            if (kvp.Value > 0) // Skip items with weight 0
            {
                if (randomValue < kvp.Value)
                    return kvp.Key;
                randomValue -= kvp.Value;
            }
        }

        throw new InvalidOperationException("Weighted random selection failed.");
    }

}



