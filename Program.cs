namespace CheckingExistenceEdgeLengthLimitedPaths
{
    internal class Program
    {
        public class Solution
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
                        parent[group2] = parent[group1];
                    }
                    else if (rank[group2] > rank[group1])
                    {
                        parent[group1] = parent[group2];
                    }
                    else
                    {
                        parent[group1] = parent[group2];
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
                Array.Sort(queries, (a, b) => a[2] - b[2]);
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
            Solution solution = new();
            solution.DistanceLimitedPathsExist(3, new int[][] { new int[] { 0, 1, 2 }, new int[] { 1, 2, 4 }, new int[] { 2, 0, 8 }, new int[] { 1, 0, 16 } }, new int[][]
            {
                new int[] { 0, 1, 2 },
                new int[] { 0, 2, 5 }
            });
        }
    }
}