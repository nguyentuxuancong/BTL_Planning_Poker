using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BayesServer;
using BayesServer.Inference;
namespace Bayesian_Task_Value
{
    class RisksNet
    {
        public Network Net { get; private set; }
        public List<Node> Nodes { get; private set; }

        private static List<double[]> ListDistribution = new List<double[]>();

        static RisksNet()
        {
            using (StreamReader reader = new StreamReader(@"D:\document\KinhTeCongNghePM\BTL\Code\distribution1.csv"))
            {
                String line = null;

                line = reader.ReadLine();
                ListDistribution.Add(convertDouble(line));

                line = reader.ReadLine();
                ListDistribution.Add(convertDouble(line));

                line = reader.ReadLine();
                ListDistribution.Add(convertDouble(line));

                line = reader.ReadLine();
                ListDistribution.Add(convertDouble(line));

                line = reader.ReadLine();
                ListDistribution.Add(convertDouble(line));

                line = reader.ReadLine();
                ListDistribution.Add(convertDouble(line));

                line = reader.ReadLine();
                ListDistribution.Add(convertDouble(line));

                line = reader.ReadLine();
                ListDistribution.Add(convertDouble(line));

                line = reader.ReadLine();
                ListDistribution.Add(convertDouble(line));

                line = reader.ReadLine();
                ListDistribution.Add(convertDouble(line));

                line = reader.ReadLine();
                ListDistribution.Add(convertDouble(line));

                line = reader.ReadLine();
                ListDistribution.Add(convertDouble(line));

                line = reader.ReadLine();
                ListDistribution.Add(convertDouble(line));

                line = reader.ReadLine();
                ListDistribution.Add(convertDouble(line));

                line = reader.ReadLine();
                ListDistribution.Add(convertDouble(line));

                line = reader.ReadLine();
                ListDistribution.Add(convertDouble(line));

                line = reader.ReadLine();
                ListDistribution.Add(convertDouble(line));

                line = reader.ReadLine();
                ListDistribution.Add(convertDouble(line));

                line = reader.ReadLine();
                ListDistribution.Add(convertDouble(line));

                line = reader.ReadLine();
                ListDistribution.Add(convertDouble(line));

                line = reader.ReadLine();
                ListDistribution.Add(convertDouble(line));

                line = reader.ReadLine();
                ListDistribution.Add(convertDouble(line));

                line = reader.ReadLine();
                ListDistribution.Add(convertDouble(line));

                line = reader.ReadLine();
                ListDistribution.Add(convertDouble(line));

                line = reader.ReadLine();
                ListDistribution.Add(convertDouble(line));
            }
        }

        public RisksNet(String id)
        {
            Net = new Network("risks" + id);
            Nodes = new List<Node>();

            // Add the nodes (variables)
            String[] arrStateBool = { "False", "True" };
            String[] arrStateLevel = { "1", "2", "3", "4", "5" };
            String[] arrSateNodeYesNo = { "no", "yes" };

            // tạo các node với các trạng thái tương ứng
            Node node1 = new Node("node1", arrStateLevel);
            Net.Nodes.Add(node1);
            Nodes.Add(node1);

            Node node2 = new Node("node2", arrStateBool);
            Net.Nodes.Add(node2);
            Nodes.Add(node2);

            Node node3 = new Node("node3", arrSateNodeYesNo);
            Net.Nodes.Add(node3);
            Nodes.Add(node3);

            Node node4 = new Node("node4", arrStateLevel);
            Net.Nodes.Add(node4);
            Nodes.Add(node4);

            Node node5 = new Node("node5", arrSateNodeYesNo);
            Net.Nodes.Add(node5);
            Nodes.Add(node5);

            Node node6 = new Node("node6", arrStateBool);
            Net.Nodes.Add(node6);
            Nodes.Add(node6);

            Node node7 = new Node("node7", arrStateBool);
            Net.Nodes.Add(node7);
            Nodes.Add(node7);

            Node node8 = new Node("node8", arrStateBool);
            Net.Nodes.Add(node8);
            Nodes.Add(node8);

            Node node9 = new Node("node9", arrStateBool);
            Net.Nodes.Add(node9);
            Nodes.Add(node9);

            Node node10 = new Node("node10", arrStateBool);
            Net.Nodes.Add(node10);
            Nodes.Add(node10);

            Node node11 = new Node("node11", arrStateLevel);
            Net.Nodes.Add(node11);
            Nodes.Add(node11);

            Node node12 = new Node("node12", arrStateBool);
            Net.Nodes.Add(node12);
            Nodes.Add(node12);

            Node node13 = new Node("node13", arrStateBool);
            Net.Nodes.Add(node13);
            Nodes.Add(node13);

            Node node14 = new Node("node14", arrSateNodeYesNo);
            Net.Nodes.Add(node14);
            Nodes.Add(node14);

            Node node15 = new Node("node15", arrStateBool);
            Net.Nodes.Add(node15);
            Nodes.Add(node15);

            Node node16 = new Node("node16", arrStateBool);
            Net.Nodes.Add(node16);
            Nodes.Add(node16);

            Node node17 = new Node("node17", arrStateBool);
            Net.Nodes.Add(node17);
            Nodes.Add(node17);

            Node node18 = new Node("node18", arrStateBool);
            Net.Nodes.Add(node18);
            Nodes.Add(node18);

            Node node19 = new Node("node19", arrStateBool);
            Net.Nodes.Add(node19);
            Nodes.Add(node19);

            Node node20 = new Node("node20", arrStateBool);
            Net.Nodes.Add(node20);
            Nodes.Add(node20);

            Node node21 = new Node("node21", arrStateBool);
            Net.Nodes.Add(node21);
            Nodes.Add(node21);

            Node node22 = new Node("node22", arrStateBool);
            Net.Nodes.Add(node22);
            Nodes.Add(node22);

            Node node23 = new Node("node23", arrStateBool);
            Net.Nodes.Add(node23);
            Nodes.Add(node23);

            Node node24 = new Node("node24", arrStateBool);
            Net.Nodes.Add(node24);
            Nodes.Add(node23);


            Node risk = new Node("risk", arrStateBool);
            Net.Nodes.Add(risk);
            Nodes.Add(risk);

            // Add các link cho các node trong mạng
            Net.Links.Add(new Link(node1, risk));
            Net.Links.Add(new Link(node3, node7));
            Net.Links.Add(new Link(node3, node9));
            Net.Links.Add(new Link(node3, node12));
            Net.Links.Add(new Link(node3, node17));
            Net.Links.Add(new Link(node3, node23));
            Net.Links.Add(new Link(node3, node24));
            Net.Links.Add(new Link(node3, risk));
            Net.Links.Add(new Link(node4, node2));
            Net.Links.Add(new Link(node4, node3));
            Net.Links.Add(new Link(node4, node5));
            Net.Links.Add(new Link(node5, node1));
            Net.Links.Add(new Link(node9, node6));
            Net.Links.Add(new Link(node9, node8));
            Net.Links.Add(new Link(node9, node11));
            Net.Links.Add(new Link(node9, node16));
            Net.Links.Add(new Link(node9, node17));
            Net.Links.Add(new Link(node9, node22));
            Net.Links.Add(new Link(node9, risk));
            Net.Links.Add(new Link(node10, node11));
            Net.Links.Add(new Link(node11, risk));
            Net.Links.Add(new Link(node12, node13));
            Net.Links.Add(new Link(node13, risk));
            Net.Links.Add(new Link(node14, node3));
            Net.Links.Add(new Link(node14, node15));
            Net.Links.Add(new Link(node14, risk));
            Net.Links.Add(new Link(node18, node1));
            Net.Links.Add(new Link(node18, risk));
            Net.Links.Add(new Link(node19, node1));
            Net.Links.Add(new Link(node20, node1));
            Net.Links.Add(new Link(node21, node16));

            // thêm bảng phân phối xác suất cho các node
            int index = 0;
            Table table1 = node1.NewDistribution().Table;
            TableIterator iterator1 = new TableIterator(table1, new Node[] { node1, node5, node18, node19, node20 });
            double[] dist = ListDistribution[index++];
            dist = normalize(dist, 5, dist.Length / 5);
            iterator1.CopyFrom(dist);
            node1.Distribution = table1;

            Table table2 = node2.NewDistribution().Table;
            TableIterator iterator2 = new TableIterator(table2, new Node[] { node2, node4 });
            dist = ListDistribution[index++];
            dist = normalize(dist, 2, dist.Length / 2);
            iterator2.CopyFrom(dist);
            node2.Distribution = table2;

            Table table3 = node3.NewDistribution().Table;
            TableIterator iterator3 = new TableIterator(table3, new Node[] { node3, node4, node14 });
            dist = ListDistribution[index++];
            dist = normalize(dist, 2, dist.Length / 2);
            iterator3.CopyFrom(dist);
            node3.Distribution = table3;

            Table table4 = node4.NewDistribution().Table;
            TableIterator iterator4 = new TableIterator(table4, new Node[] { node4 });
            dist = ListDistribution[index++];
            dist = normalize(dist, 5, dist.Length / 5);
            iterator4.CopyFrom(dist);
            node4.Distribution = table4;

            Table table5 = node5.NewDistribution().Table;
            TableIterator iterator5 = new TableIterator(table5, new Node[] { node5, node4 });
            dist = ListDistribution[index++];
            dist = normalize(dist, 2, dist.Length / 2);
            iterator5.CopyFrom(dist);
            node5.Distribution = table5;

            Table table6 = node6.NewDistribution().Table;
            TableIterator iterator6 = new TableIterator(table6, new Node[] { node6, node9 });
            dist = ListDistribution[index++];
            dist = normalize(dist, 2, dist.Length / 2);
            iterator6.CopyFrom(dist);
            node6.Distribution = table6;

            Table table7 = node7.NewDistribution().Table;
            TableIterator iterator7 = new TableIterator(table7, new Node[] { node7, node3 });
            dist = ListDistribution[index++];
            dist = normalize(dist, 2, dist.Length / 2);
            iterator7.CopyFrom(dist);
            node7.Distribution = table7;

            Table table8 = node8.NewDistribution().Table;
            TableIterator iterator8 = new TableIterator(table8, new Node[] { node8, node9 });
            dist = ListDistribution[index++];
            dist = normalize(dist, 2, dist.Length / 2);
            iterator8.CopyFrom(dist);
            node8.Distribution = table8;

            Table table9 = node9.NewDistribution().Table;
            TableIterator iterator9 = new TableIterator(table9, new Node[] { node9, node3 });
            dist = ListDistribution[index++];
            dist = normalize(dist, 2, dist.Length / 2);
            iterator9.CopyFrom(dist);
            node9.Distribution = table9;

            Table table10 = node10.NewDistribution().Table;
            TableIterator iterator10 = new TableIterator(table10, new Node[] { node10 });
            dist = ListDistribution[index++];
            dist = normalize(dist, 2, dist.Length / 2);
            iterator10.CopyFrom(dist);
            node10.Distribution = table10;

            Table table11 = node11.NewDistribution().Table;
            TableIterator iterator11 = new TableIterator(table11, new Node[] { node11, node9, node10 });
            dist = ListDistribution[index++];
            dist = normalize(dist, 5, dist.Length / 5);
            iterator11.CopyFrom(dist);
            node11.Distribution = table11;

            Table table12 = node12.NewDistribution().Table;
            TableIterator iterator12 = new TableIterator(table12, new Node[] { node12, node3 });
            dist = ListDistribution[index++];
            dist = normalize(dist, 2, dist.Length / 2);
            iterator12.CopyFrom(dist);
            node12.Distribution = table12;

            Table table13 = node13.NewDistribution().Table;
            TableIterator iterator13 = new TableIterator(table13, new Node[] { node13, node12 });
            dist = ListDistribution[index++];
            dist = normalize(dist, 2, dist.Length / 2);
            iterator13.CopyFrom(dist);
            node13.Distribution = table13;

            Table table14 = node14.NewDistribution().Table;
            TableIterator iterator14 = new TableIterator(table14, new Node[] { node14 });
            dist = ListDistribution[index++];
            dist = normalize(dist, 2, dist.Length / 2);
            iterator14.CopyFrom(dist);
            node14.Distribution = table14;

            Table table15 = node15.NewDistribution().Table;
            TableIterator iterator15 = new TableIterator(table15, new Node[] { node15, node14 });
            dist = ListDistribution[index++];
            dist = normalize(dist, 2, dist.Length / 2);
            iterator15.CopyFrom(dist);
            node15.Distribution = table15;


            Table table16 = node16.NewDistribution().Table;
            TableIterator iterator16 = new TableIterator(table16, new Node[] { node16, node9, node21 });
            dist = ListDistribution[index++];
            dist = normalize(dist, 2, dist.Length / 2);
            iterator16.CopyFrom(dist);
            node16.Distribution = table16;



            Table table17 = node17.NewDistribution().Table;
            TableIterator iterator17 = new TableIterator(table17, new Node[] { node17, node3, node9 });
            dist = ListDistribution[index++];
            dist = normalize(dist, 2, dist.Length / 2);
            iterator17.CopyFrom(dist);
            node17.Distribution = table17;

            Table table18 = node18.NewDistribution().Table;
            TableIterator iterator18 = new TableIterator(table18, new Node[] { node18 });
            dist = ListDistribution[index++];
            dist = normalize(dist, 2, dist.Length / 2);
            iterator18.CopyFrom(dist);
            node18.Distribution = table18;

            Table table19 = node19.NewDistribution().Table;
            TableIterator iterator19 = new TableIterator(table19, new Node[] { node19 });
            dist = ListDistribution[index++];
            dist = normalize(dist, 2, dist.Length / 2);
            iterator19.CopyFrom(dist);
            node19.Distribution = table19;

            Table table20 = node20.NewDistribution().Table;
            TableIterator iterator20 = new TableIterator(table20, new Node[] { node20 });
            dist = ListDistribution[index++];
            dist = normalize(dist, 2, dist.Length / 2);
            iterator20.CopyFrom(dist);
            node20.Distribution = table20;

            Table table21 = node21.NewDistribution().Table;
            TableIterator iterator21 = new TableIterator(table21, new Node[] { node21 });
            dist = ListDistribution[index++];
            dist = normalize(dist, 2, dist.Length / 2);
            iterator21.CopyFrom(dist);
            node21.Distribution = table21;

            Table table22 = node22.NewDistribution().Table;
            TableIterator iterator22 = new TableIterator(table22, new Node[] { node22, node9 });
            dist = ListDistribution[index++];
            dist = normalize(dist, 2, dist.Length / 2);
            iterator22.CopyFrom(dist);
            node22.Distribution = table22;

            Table table23 = node23.NewDistribution().Table;
            TableIterator iterator23 = new TableIterator(table23, new Node[] { node23, node3 });
            dist = ListDistribution[index++];
            dist = normalize(dist, 2, dist.Length / 2);
            iterator23.CopyFrom(dist);
            node23.Distribution = table23;

            Table table24 = node24.NewDistribution().Table;
            TableIterator iterator24 = new TableIterator(table24, new Node[] { node24, node3 });
            dist = ListDistribution[index++];
            dist = normalize(dist, 2, dist.Length / 2);
            iterator24.CopyFrom(dist);
            node24.Distribution = table24;


            Table tableRisk = risk.NewDistribution().Table;
            TableIterator iteratorRisk = new TableIterator(tableRisk, new Node[] { risk, node1, node3, node9, node11, node13, node14, node18 });
            dist = ListDistribution[index++];
            dist = normalize(dist, 2, dist.Length / 2);

            iteratorRisk.CopyFrom(dist);
            risk.Distribution = tableRisk;
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
