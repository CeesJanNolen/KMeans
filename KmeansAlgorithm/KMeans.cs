using System;
using System.Collections.Generic;
using System.Linq;

namespace KmeansAlgorithm
{
    public class KMeans
    {
        public List<GenericVector> Dataset;
        public int Iterations;
        public int Clusters;
        public Dictionary<int, GenericVector> Centroids;
        private readonly Random _random = new Random();

        //the method to run the KMeans algorithm
        public void Run()
        {
            Centroids = GenerateRandomCentroids(Clusters);
            //for loop Iterations
            for (var i = 0; i < Iterations; i++)
            {
                //storing the old cluster to compare it later on.
                var oldCluster = Dataset.Select(v => v.Cluster).ToList();

                //assign data set
                AssignDataset();

                //recalculate clusters
                RecalculateCentroids();

                //check if the cluster is still changing
                if (!ClustersChanged(oldCluster, Dataset.Select(v => v.Cluster).ToList()))
                    break;
            }
        }

        //assign the vectors to the clusters nearby
        private void AssignDataset()
        {
            Dataset.ForEach(vector => vector.Cluster = GetNearestCluster(vector));
        }

        //get nearest cluster
        private int GetNearestCluster(GenericVector vector)
        {
            var clusterid = Centroids
                .OrderBy(v => GenericVector.Distance(vector, v.Value))
                .Select(v => v.Key)
                .FirstOrDefault();
            return clusterid;
        }

        //generate random centroids for the first time
        private Dictionary<int, GenericVector> GenerateRandomCentroids(int clusters)
        {
            var centroids = new Dictionary<int, GenericVector>();
            var index = 0;
            clusters.Times(() => centroids.Add(index++, RandomVector()));
            return centroids;
        }

        //check if the clusters changed
        private static bool ClustersChanged(IEnumerable<int> a, IReadOnlyList<int> b)
        {
            return a.Where((value, index) => value != b[index]).Any();
        }

        //recalculate the new centroids based on the mean
        private void RecalculateCentroids()
        {
            foreach (var centroidkey in Centroids.Keys.ToList())
            {
                var cluster = Dataset.Where(v => v.Cluster == centroidkey).ToList();

                //generating a new genericvector with the mean of the existing vectors
                Centroids[centroidkey] = cluster
                    .Aggregate(new GenericVector(Dataset.First().Size),
                        (current, y) => current.Sum(y))
                    .Divide(cluster.Count);
            }
        }

        //get a random vector
        private GenericVector RandomVector()
        {
            return Dataset[_random.Next(Dataset.Count)];
        }

        //print the clusters to the console.
        public void PrintClusters()
        {
            var clusters = Dataset.GroupBy(v => v.Cluster).ToList().OrderBy(v => v.Key);
            foreach (var cluster in clusters)
            {
                Console.WriteLine("Cluster " + cluster.ElementAt(0).Cluster + " has " + cluster.Count());
            }
        }
        public void PrintClustersInLine()
        {
            var clusters = Dataset.GroupBy(v => v.Cluster).ToList().OrderBy(v => v.Key);
            foreach (var cluster in clusters)
            {
                Console.Write(cluster.ElementAt(0).Cluster + " -> " + cluster.Count() + " || ");
            }
            Console.WriteLine();
        }

        /*
        TODO
        QUESTIONS
        - DO WE ALSO NEED TO IMPLEMENT THE ALGORITHMS WE LEARNED TO CHECK THE KMEANS?
        - CHECK IF STOPPED MOVING OR ALSO THE CHECK IF CENTROIDS ARENT CHANGED?
        - RANDOM: HOW TO DETERMINE THE FIRST RANDOM CENTROID? IS IT OK TO GET A VECTOR FROM THE ACTUAL SET?
        */
    }


    /*
        ASK SSE
        loop every k (cluster)
        loop every item in the cluster
        get the distance between item and clustercenter
        sum all those (first square it??)

    */

}