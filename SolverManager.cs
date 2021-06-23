using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SchedulingProblemGrobel
{
    public static class SolverManager
    {
        public static (int, int) Solve(int n, int max)
        {
            var stopWatchBF = new Stopwatch();
            var stopWatchHR = new Stopwatch();        

            var tasks = Helpers.GenerateTasks(n, max);

            var solver = new BruteForceSolver(tasks, 2, 2);
            var heuristicSolver = new HeuristicSolver(tasks, 2, 2);

            stopWatchBF.Start();
            var exactResult = solver.Solve();
            stopWatchBF.Stop();

            stopWatchHR.Start();
            var heuristicResult = heuristicSolver.Solve();
            stopWatchHR.Stop();

            Console.WriteLine($"Rozwiązanie dokładne wynosi: {exactResult} i zajęło: {stopWatchBF.Elapsed}");
            Console.WriteLine($"Rozwiązanie heurystyczne wynosi: {heuristicResult} i zajęło: {stopWatchHR.Elapsed}");

            return (exactResult, heuristicResult);
        }

        public static void DoLab()
        {
            var exactResults = new List<int>();
            var heuristicResults = new List<int>();

            var firstResults = Solve(5, 30);
            var secondResults = Solve(100, 30);
            var thirdResults = Solve(100, 60);
            var fourthResults = Solve(2000, 60);
            double variance;
            double standardDeviation;

            exactResults.Add(firstResults.Item1);
            heuristicResults.Add(firstResults.Item2);
            variance = QualityComparer.Variance(exactResults, heuristicResults);
            standardDeviation = QualityComparer.StandardDeviation(exactResults, heuristicResults);

            // Dokladnosc
            Console.WriteLine(
                $"Wariancja po jednej próbie wynosi: {variance}"
            );
            Console.WriteLine(
                $"Odchylenie standardowe po jednej próbie wynosi: {standardDeviation}"
            );

            exactResults.Add(secondResults.Item1);
            heuristicResults.Add(secondResults.Item2);
            variance = QualityComparer.Variance(exactResults, heuristicResults);
            standardDeviation = QualityComparer.StandardDeviation(exactResults, heuristicResults);

            // Dokladnosc 2
            Console.WriteLine(
                $"Wariancja po dwóch próbach wynosi: {variance}"
            );
            Console.WriteLine(
                $"Odchylenie standardowe po dwóch próbache wynosi: {standardDeviation}"
            );

            exactResults.Add(thirdResults.Item1);
            heuristicResults.Add(thirdResults.Item2);
            variance = QualityComparer.Variance(exactResults, heuristicResults);
            standardDeviation = QualityComparer.StandardDeviation(exactResults, heuristicResults);

            // Dokladnosc 3
            Console.WriteLine(
                $"Wariancja po trzech próbach wynosi: {variance}"
            );
            Console.WriteLine(
                $"Odchylenie standardowe po trzech próbache wynosi: {standardDeviation}"
            );

            exactResults.Add(fourthResults.Item1);
            heuristicResults.Add(fourthResults.Item2);
            variance = QualityComparer.Variance(exactResults, heuristicResults);
            standardDeviation = QualityComparer.StandardDeviation(exactResults, heuristicResults);

            // Dokladnosc 4
            Console.WriteLine(
                $"Wariancja po czterech próbach wynosi: {variance}"
            );
            Console.WriteLine(
                $"Odchylenie standardowe po czterech próbache wynosi: {standardDeviation}"
            );
        }
    }

    
}