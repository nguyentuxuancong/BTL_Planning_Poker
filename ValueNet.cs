using System;
using System.IO;
using System.Collections.Generic;
using BayesServer;
using BayesServer.Inference.RelevanceTree;

namespace Bayesian_Task_Value
{
    class ValueNet
    {

        public Network Net { get; private set; }
        public List<Node> Nodes { get; private set; }

        private static List<double[]> ListDistribution = new List<double[]>();
        static ValueNet()
        {

            using (StreamReader reader = new StreamReader(@"D:\document\KinhTeCongNghePM\BTL\Code\distribution.csv"))
            {
                String line = null;

                line = reader.ReadLine();
                ListDistribution.Add(convertDouble(line));

                line = reader.ReadLine();
                ListDistribution.Add(convertDouble(line));

                line = reader.ReadLine();
                ListDistribution.Add(convertDouble(line));
            }
        }

        public ValueNet(String id)
        {
            Net = new Network("value" + id);
            Nodes = new List<Node>();

            String[] arrStateString_2 = { "Low", "High" };
            String[] arrStateString_3 = { "Low", "Medium", "High" };

            //Priority
            Node priority = new Node("Priority",arrStateString_3);
            Net.Nodes.Add(priority);
            Nodes.Add(priority);

            //Experience
            Node experience = new Node("Experience", arrStateString_3);
            Net.Nodes.Add(experience);
            Nodes.Add(experience);


            //Complexity
            Node complexity = new Node("Complexity", arrStateString_3);
            Net.Nodes.Add(complexity);
            Nodes.Add(complexity);

            //Effort
            Node effort = new Node("Effort", arrStateString_3);
            Net.Nodes.Add(effort);
            Nodes.Add(effort);
            
            //Time
            Node time = new Node("Time", arrStateString_3);
            Net.Nodes.Add(time);
            Nodes.Add(time);

            //Labor
            Node labor = new Node("Labor", arrStateString_2);
            Net.Nodes.Add(labor);
            Nodes.Add(labor);

            //Challenges
            Node challenges = new Node("Challenges", arrStateString_2);
            Net.Nodes.Add(challenges);
            Nodes.Add(challenges);

            //Task value
            Node value = new Node("Value", arrStateString_2);
            Net.Nodes.Add(value);
            Nodes.Add(value);

            //Add links
            Net.Links.Add(new Link(priority, value));
            Net.Links.Add(new Link(labor, value));
            Net.Links.Add(new Link(challenges, value));
            Net.Links.Add(new Link(experience, labor));
            Net.Links.Add(new Link(complexity, labor));
            Net.Links.Add(new Link(time, challenges));
            Net.Links.Add(new Link(effort, challenges));

            //Distributions
            Table table1 = labor.NewDistribution().Table;
            TableIterator iterator1 = new TableIterator(table1, new Node[] {labor, experience, complexity});
            double[] dist = ListDistribution[0];
            dist = normalize(dist, 2, 9);
            iterator1.CopyFrom(dist);
            labor.Distribution = table1;

            Table table2 = challenges.NewDistribution().Table;
            TableIterator iterator2 = new TableIterator(table2, new Node[] {challenges, time, effort });
            dist = ListDistribution[1];
            dist = normalize(dist, 2, 9);
            iterator2.CopyFrom(dist);
            challenges.Distribution = table2;

            Table table3 = value.NewDistribution().Table;
            TableIterator iterator3 = new TableIterator(table3, new Node[] { value, priority, labor, challenges });
            dist = ListDistribution[2];
            dist = normalize(dist, 2,12);
            iterator3.CopyFrom(dist);
            value.Distribution = table3;

        }

        private static double[] convertDouble(String line)
        {
            String[] list = line.Split(';');
            double[] dist = new double[list.Length];
            int i = 0;
            for (i = 0; i < list.Length; i++)
            {

                if (list[i].Length > 0)
                {
                    dist[i] = Double.Parse(list[i]);
                }
                else
                {
                    break;
                }
            }
            double[] temp = new double[i];
            for (int j = 0; j < i; j++)
            {
                temp[j] = dist[j];
            }
            return temp;
        }

        private double[] normalize(double[] dist, int row, int col)
        {
            int len = dist.Length;
            double[] newDist = new double[row * col];
            if (len != (row * col))
            {
                Console.WriteLine("Error in normalize function!");
                return null;
            }
            double sum;
            for (int j = 0; j < col; j++)
            {
                sum = 0;
                for (int i = 0; i < row; i++)
                    sum = sum + dist[i * col + j];
                // System.out.println("sum: " + sum);
                for (int i = 0; i < row; i++)
                    newDist[i * col + j] = dist[i * col + j] / sum;
            }
            return newDist;
        }
    }
}
