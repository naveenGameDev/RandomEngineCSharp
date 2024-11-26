# RandomEngineCSharp
Random engine with seed written in c#, given seed will generate same pattern of random numbers

Choosing the best parameters for a, c, and m in a Linear Congruential Generator (LCG) is essential for ensuring good randomness, a long period, and avoidance of statistical artifacts. Here are some of the most effective and well-studied parameters:
Purpose: General-purpose random number generation.


LCG method pseudo random formula for next : Xn+1 = (a * Xn + c) % m

Parameters: well tested for distribution
- m = 2^32
- a = 1664525
- c = 1013904223

Properties:
- Period: 2^32
- Simple and fast for 32-bit systems.
