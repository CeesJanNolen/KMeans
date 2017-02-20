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


        public void Run()
        {
            Centroids = GenerateRandomCentroids(Clusters);
            //for loop Iterations
            for (var i = 0; i < Iterations; i++)
            {
                var oldCluster = Dataset.Select(v => v.Cluster).ToList();

                //assign data set
                AssignDataset();

                //recalculate clusters
                RecalculateCenteroids();

                if (!ClustersChanged(oldCluster, Dataset.Select(v => v.Cluster).ToList()))
                {

                    Console.WriteLine(i);
                    break;
                }
//                    break;
            }
        }

        public void AssignDataset()
        {
            Dataset.ForEach(vector => vector.Cluster = GetNearestCluster(vector));
            foreach (var vector in Dataset)
            {
                vector.Cluster = GetNearestCluster(vector);
            }
        }

        public int GetNearestCluster(GenericVector vector)
        {
            var clusterid = Centroids
                .OrderBy(v => GenericVector.Distance(vector, v.Value))
                .Select(v => v.Key)
                .FirstOrDefault();
            return clusterid;
        }

        public Dictionary<int, GenericVector> GenerateRandomCentroids(int clusters)
        {
            var centroids = new Dictionary<int, GenericVector>();
            var index = 0;
            clusters.Times(() => centroids.Add(index++, RandomVector()));
            return centroids;
        }

        public bool ClustersChanged(List<int> a, List<int> b)
        {
            return a.Where((value, index) => value != b[index]).Any();
        }

        public void RecalculateCenteroids()
        {
            foreach (var centroid in Centroids.Keys.ToList())
            {
                var cluster = Dataset.Where(v => v.Cluster == centroid).ToList();

                Centroids[centroid] = cluster
                    .Aggregate(new GenericVector(Dataset.First().Size),
                        (current, y) => current.Sum(y))
                    .Devide(cluster.Count);
            }
        }

        public GenericVector RandomVector()
        {
            return Dataset[_random.Next(Dataset.Count)];
        }

        public void PrintClusters()
        {
            var clusters = Dataset.GroupBy(v => v.Cluster);
            foreach (var cluster in clusters)
            {
                Console.WriteLine("Cluster " + cluster.ElementAt(0).Cluster + " has " + cluster.Count());
            }
        }
    }
}