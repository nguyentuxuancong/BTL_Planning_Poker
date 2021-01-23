using System;
using System.IO;
using System.Collections.Generic;
using BayesServer;
using BayesServer.Inference.RelevanceTree;

namespace Bayesian_Task_Value
{
    class Net
    {
        private static Network network;
        private static State prHigh, prMedium, prLow;
        private static State exHigh, exMedium, exLow;
        private static State cpHigh, cpMedium, cpLow;
        private static State efHigh, efMedium, efLow;
        private static State tmHigh, tmMedium, tmLow;
        private static State lbHigh, lbLow;
        private static State clHigh, clLow;
        private static State tvHigh, tvLow;
        private static Node priority, experience, complexity, effort, time, labor, challenges, taskValue;
        private static Table tPriority, tExperience, tComplexity, tEffort, tTime, tLabor, tChallenges, tTaskValue;
        private static Dictionary<String, State> dict = new Dictionary<string, State>();
        private static List<Table> tables = new List<Table>();
        static Net()
        {
            network = new Network();
            tables.Add(tPriority);
            tables.Add(tExperience);
            tables.Add(tComplexity);
            tables.Add(tEffort);
            tables.Add(tTime);

            //Priority
            prHigh = new State("High");
            prMedium = new State("Medium");
            prLow = new State("Low");
            priority = new Node("Priority", prLow, prMedium, prHigh);
            dict.Add(priority.Name + "3", prHigh);
            dict.Add(priority.Name + "2", prMedium);
            dict.Add(priority.Name + "1", prLow);

            //Experience
            exHigh = new State("High");
            exMedium = new State("Medium");
            exLow = new State("Low");
            experience = new Node("Experience", exLow, exMedium, exHigh);
            dict.Add(experience.Name + "3", exHigh);
            dict.Add(experience.Name + "2", exMedium);
            dict.Add(experience.Name + "1", exLow);

            //Complexity
            cpHigh = new State("High");
            cpMedium = new State("Medium");
            cpLow = new State("Low");
            complexity = new Node("Complexity", cpLow, cpMedium, cpHigh);
            dict.Add(complexity.Name + "3", cpHigh);
            dict.Add(complexity.Name + "2", cpMedium);
            dict.Add(complexity.Name + "1", cpLow);

            //Effort
            efHigh = new State("High");
            efMedium = new State("Medium");
            efLow = new State("Low");
            effort = new Node("Effort", efLow, efMedium, efHigh);
            dict.Add(effort.Name + "3", efHigh);
            dict.Add(effort.Name + "2", efMedium);
            dict.Add(effort.Name + "1", efLow);

            //Time
            tmHigh = new State("High");
            tmMedium = new State("Medium");
            tmLow = new State("Low");
            time = new Node("Time", tmLow, tmMedium, tmHigh);
            dict.Add(time.Name + "3", tmHigh);
            dict.Add(time.Name + "2", tmMedium);
            dict.Add(time.Name + "1", tmLow);

            //Labor
            lbHigh = new State("High");
            lbLow = new State("Low");
            labor = new Node("Labor", lbLow, lbHigh);
            dict.Add(labor.Name + "2", lbHigh);
            dict.Add(labor.Name + "1", lbLow);

            //Challenges
            clHigh = new State("High");
            clLow = new State("Low");
            challenges = new Node("Challenges", clLow, clHigh);
            dict.Add(challenges.Name + "2", clHigh);
            dict.Add(challenges.Name + "1", clLow);

            //Task value
            tvHigh = new State("High");
            tvLow = new State("Low");
            taskValue = new Node("Task value", tvLow, tvHigh);
            dict.Add(taskValue.Name + "2", tvHigh);
            dict.Add(taskValue.Name + "1", tvLow);

            network.Nodes.Add(priority);
            network.Nodes.Add(experience);
            network.Nodes.Add(complexity);
            network.Nodes.Add(effort);
            network.Nodes.Add(time);
            network.Nodes.Add(labor);
            network.Nodes.Add(challenges);
            network.Nodes.Add(taskValue);

            network.Links.Add(new Link(experience, labor));
            network.Links.Add(new Link(complexity, labor));
            network.Links.Add(new Link(effort, challenges));
            network.Links.Add(new Link(time, challenges));
            network.Links.Add(new Link(priority, taskValue));
            network.Links.Add(new Link(labor, taskValue));
            network.Links.Add(new Link(challenges, taskValue));

            tLabor = labor.NewDistribution().Table;

            tChallenges = challenges.NewDistribution().Table;

            tTaskValue = taskValue.NewDistribution().Table;

            using (StreamReader sr = new StreamReader(@"D:\document\KinhTeCongNghePM\BTL\Code\distribution.csv"))
            {
                while (true)
                {
                    if (sr.EndOfStream)
                        break;
                    var line = sr.ReadLine();
                    if (line == null || line.Length < 10)
                        break;
                    var values = line.Split(',');
                    if (values.Length < 5)
                        break;
                    int[] numbers = new int[10];
                    int id = int.Parse(values[0]);
                    if (id == 1)
                    {
                        for (int i = 1; i < 3; ++i)
                        {
                            numbers[i] = int.Parse(values[i]);
                        }
                        var v1 = float.Parse(values[3]);
                        var v2 = float.Parse(values[4]);
                        tLabor[dict[experience.Name + numbers[1]], dict[complexity.Name + numbers[2]], dict[labor.Name + 1]] = v1;
                        tLabor[dict[experience.Name + numbers[1]], dict[complexity.Name + numbers[2]], dict[labor.Name + 2]] = v2;
                    }
                    else if (id == 2)
                    {
                        for (int i = 1; i < 3; ++i)
                        {
                            numbers[i] = int.Parse(values[i]);
                        }
                        var v1 = float.Parse(values[3]);
                        var v2 = float.Parse(values[4]);
                        tChallenges[dict[effort.Name + numbers[1]], dict[time.Name + numbers[2]], dict[challenges.Name + 1]] = v1;
                        tChallenges[dict[effort.Name + numbers[1]], dict[time.Name + numbers[2]], dict[challenges.Name + 2]] = v2;
                    }
                    if (id == 3)
                    {
                        for (int i = 1; i < 4; ++i)
                        {
                            numbers[i] = int.Parse(values[i]);
                        }
                        var v1 = float.Parse(values[4]);
                        var v2 = float.Parse(values[5]);
                        tTaskValue[dict[priority.Name + numbers[1]], dict[labor.Name + numbers[2]], dict[challenges.Name + numbers[3]], dict[taskValue.Name + 1]] = v1;
                        tTaskValue[dict[priority.Name + numbers[1]], dict[labor.Name + numbers[2]], dict[challenges.Name + numbers[3]], dict[taskValue.Name + 2]] = v2;
                    }
                }

                labor.Distribution = tLabor;
                challenges.Distribution = tChallenges;
                taskValue.Distribution = tTaskValue;

            }
        }
        public static void caculateTaskValue(Task task)
        {
            tPriority = priority.NewDistribution().Table;
            tExperience = experience.NewDistribution().Table;
            tComplexity = complexity.NewDistribution().Table;
            tEffort = effort.NewDistribution().Table;
            tTime = time.NewDistribution().Table;
            for (int i = 0; i < 3; ++i)
            {
                tPriority[i] = task.attribute.priority.getValue(i);
                tExperience[i] = task.attribute.experience.getValue(i);
                tComplexity[i] = task.attribute.complexity.getValue(i);
                tEffort[i] = task.attribute.effort.getValue(i);
                tTime[i] = task.attribute.time.getValue(i);

            }
            priority.Distribution = tPriority;
            experience.Distribution = tExperience;
            complexity.Distribution = tComplexity;
            effort.Distribution = tEffort;
            time.Distribution = tTime;

            var factory = new RelevanceTreeInferenceFactory();
            var inference = factory.CreateInferenceEngine(network);
            var queryOptions = factory.CreateQueryOptions();
            var queryOutput = factory.CreateQueryOutput();

            var qValue = new Table(taskValue);
            var qLabor = new Table(labor);
            var qChlgs = new Table(challenges);
            inference.QueryDistributions.Add(qValue);
            inference.QueryDistributions.Add(qLabor);
            inference.QueryDistributions.Add(qChlgs);
            inference.Query(queryOptions, queryOutput); // note that this can raise an exception (see help for details)

            task.attribute.labor.setValue(new double[] { qLabor[lbLow], qLabor[lbHigh] });
            task.attribute.challenges.setValue(new double[] { qChlgs[clLow], qChlgs[clHigh] });
            task.attribute.taskValue.setValue(new double[] { qValue[tvLow], qValue[tvHigh] });
            Console.WriteLine(task);
        }
    }
}
