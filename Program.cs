namespace CheckingExistenceEdgeLengthLimitedPaths
{
    internal class Program
    {
        public class CheckingExistenceEdgeLengthLimitedPaths
        {
            private class UnionFind
            {
                private readonly int[] parent;
                private readonly int[] rank;

                public UnionFind(int size)
                {
                    parent = new int[size];
                    rank = new int[size];
                    for(int i = 0; i < size; ++i)
                    {
                        parent[i] = i;
                    }
                }

                public int Find(int node)
                {
                    if (parent[node] != node)
                    {
                        parent[node] = Find(parent[node]);
                    }
                    return parent[node];
                }

                public void Join(int node1, int node2)
                {
                    int group1 = Find(node1);
                    int group2 = Find(node2);

                    if (group1 == group2)
                    {
                        return;
                    }

                    if (rank[group1] > rank[group2])
                    {
                        parent[group2] = group1;
                    }
                    else if (rank[group2] > rank[group1])
                    {
                        parent[group1] = group2;
                    }
                    else
                    {
                        parent[group1] = group2;
                        rank[group2] += 1;
                    }
                }

                public bool AreConnected(int node1, int node2)
                {
                    int group1 = Find(node1);
                    int group2 = Find(node2);
                    return group1 == group2;
                }
            }
            public bool[] DistanceLimitedPathsExist(int n, int[][] edgeList, int[][] queries)
            {
                int queryCount = queries.Length;
                bool[] distanceLimitedPathExist = new bool[queryCount];
                UnionFind uf = new(n);
                int[][] queryWithIndex = new int[queryCount][];
                for(int i = 0; i < queryCount; ++i)
                {
                    queryWithIndex[i] = new int[4];
                    queryWithIndex[i][0] = queries[i][0];
                    queryWithIndex[i][1] = queries[i][1];
                    queryWithIndex[i][2] = queries[i][2];
                    queryWithIndex[i][3] = i;
                }
                Array.Sort(edgeList, (a, b) => a[2] - b[2]);
                Array.Sort(queryWithIndex, (a, b) => a[2] - b[2]);
                int edgeIndex = 0;
                for(int queryIndex = 0; queryIndex < queryCount; ++queryIndex)
                {
                    int p = queryWithIndex[queryIndex][0];
                    int q = queryWithIndex[queryIndex][1];
                    int limit = queryWithIndex[queryIndex][2];
                    int queryOriginalIndex = queryWithIndex[queryIndex][3];

                    while(edgeIndex < edgeList.Length && edgeList[edgeIndex][2] < limit)
                    {
                        int node1 = edgeList[edgeIndex][0];
                        int node2 = edgeList[edgeIndex][1];
                        uf.Join(node1, node2);
                        edgeIndex += 1;
                    }

                    distanceLimitedPathExist[queryOriginalIndex] = uf.AreConnected(p, q);
                }
                return distanceLimitedPathExist;
            }
        }

        static void Main(string[] args)
        {
            CheckingExistenceEdgeLengthLimitedPaths solution = new();
            foreach(bool res in solution.DistanceLimitedPathsExist(3, new int[][] { new int[] { 0, 1, 2 }, new int[] { 1, 2, 4 }, new int[] { 2, 0, 8 }, new int[] { 1, 0, 16 } }, new int[][]
            {
                new int[] { 0, 1, 2 },
                new int[] { 0, 2, 5 }
            }))
            {
                Console.Write(res + " ");
            }
            Console.WriteLine();
            foreach (bool res in solution.DistanceLimitedPathsExist(13, new int[][] 
            {
                new int[] {9,1,53},new int[] {3,2,66},new int[] {12,5,99},new int[] {9,7,26},new int[] {1,4,78},new int[] {11,1,62},new int[] {3,10,50},new int[] {12,1,71},new int[] {12,6,63},new int[] {1,10,63},new int[] {9,10,88},new int[] {9,11,59},new int[] {1,4,37},new int[] {4,2,63},new int[] {0,2,26},new int[] {6,12,98},new int[] {9,11,99},new int[] {4,5,40},new int[] {2,8,25},new int[] {4,2,35},new int[] {8,10,9},new int[] {11,9,25},new int[] {10,11,11},new int[] {7,6,89},new int[] {2,4,99},new int[] {10,4,63}
            }, new int[][]
            {
               new int[] {9,7,65},new int[] {9,6,1},new int[] {4,5,34},new int[] {10,8,43},new int[] {3,7,76},new int[] {4,2,15},new int[] {7,6,52},new int[] {2,0,50},new int[] {7,6,62},new int[] {1,0,81},new int[] {4,5,35},new int[] {0,11,86},new int[] {12,5,50},new int[] {11,2,2},new int[] {9,5,6},new int[] {12,0,95},new int[] {10,6,9},new int[] {9,4,73},new int[] {6,10,48},new int[] {12,0,91},new int[] {9,10,58},new int[] {9,8,73},new int[] {2,3,44},new int[] {7,11,83},new int[] {5,3,14},new int[] {6,2,33}
            }))
            {
                Console.Write(res + " ");
            }
            Console.WriteLine();
        }
    }
}