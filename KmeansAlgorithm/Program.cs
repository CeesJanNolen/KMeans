using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KmeansAlgorithm
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var dataDictionary = new Dictionary<int, GenericVector>();

            //read csv file
            var lines = File.ReadAllLines("WineData.csv")
                .Select(v => v.Split(',').Select(float.Parse).ToList())
                .ToList();

            //flip data and add to genericvector list
            for (var i = 0; i < lines.Count; i++)
            {
                for (var j = 0; j < lines[i].Count; j++)
                {
                    if (!dataDictionary.ContainsKey(j))
                        dataDictionary[j] = new GenericVector();
                    dataDictionary[j].Add(lines[i][j]);
                }
            }

            var kMeanses = new List<KMeans>();
            //init kmeans ad run it.
            for (var i = 0; i < 1; i++)
            {
                var kMeans = new KMeans
                {
                    Iterations = 100,
                    Dataset = dataDictionary.Values.ToList(),
                    Clusters = 4
                };
                kMeans.Run();
//                kMeans.PrintClustersInLine();
                kMeanses.Add(kMeans);
            }


            var lowestKmeans = kMeanses.Aggregate(
                (minItem, nextItem) => minItem.SSE < nextItem.SSE ? minItem : nextItem);
            lowestKmeans.PrintClusterInfo();


        }
    }
}