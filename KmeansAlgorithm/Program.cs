using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;

namespace KmeansAlgorithm
{
    internal class Program
    {
        public static void Main(string[] args)
        {
//            var x = new List<int>();
//            x.Add(6);
//            x.Add(1);
//            x.Add(7);
//            x.Add(1);
//            x.Add(4);
//            x.Add(4);
//            x.Add(1);
//            x.Add(1);
//            x.Add(6);
//            x.Add(1);
//            x.Add(4);
//            x.Add(3);
//            x.Add(6);
//            x.Add(7);
//            x.Add(8);
//            x.Add(5);
//
//            x = x.OrderByDescending(v => v).ToList();
//
//            foreach (var i in x)
//            {
//                Console.Write(i);
//            }


            //genericvector ophalen en extra var geven voor welke cluster hij zit.
            //aantal clusters bepalen
            //aantal iterations neerzetten
            //dataset maken -> lijst met genericvector
            //bovenstaande items doogeven aan kmeans
            //kmeans starten


            //KMEANS
            //random x aantal clusters maken //generate cluster
            //items aan cluster toevoegen    //assing vectors to clusters
            //bovenstaand itereren totdat of iteraties opzijn of de clusters veranderen niet meer. //check iteration


/*
VARIABLES
int clusters
int iterations
set dataset
list centroids

FUNCTIONS
generate random centroids
run functie
recalc centroids
setchanged(datasetold, datasetnew)
getnearestcluster(vector)

*/
            var dataDictionary = new Dictionary<int, GenericVector>();

            var lines = File.ReadAllLines("WineData.csv").Select(v => v.Split(',').Select(float.Parse).ToList()).ToList();

            for (var i = 0; i < lines.Count; i++)
            {
                for (var j = 0; j < lines[i].Count; j++)
                {
                    if (!dataDictionary.ContainsKey(j))
                        dataDictionary[j] = new GenericVector();
                    dataDictionary[j].Add(lines[i][j]);
                }
            }

            var kMeans = new KMeans
            {
                Iterations = 100,
                Dataset = dataDictionary.Values.ToList(),
                Clusters = 4
            };

            kMeans.Run();
            kMeans.PrintClusters();

        }
    }
}