using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.Json;
using Microsoft.Win32;
using System.IO;
using BayesServer;
using BayesServer.Inference.RelevanceTree;
namespace Bayesian_Task_Value
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private String DataFilePath = null;
        private String ResourceFilePath = null;
        private List<Task> Tasks;
        private List<Resource> Resources;
        private Project project;
        public MainWindow()
        {
            InitializeComponent();
            btSubmit.IsEnabled = false;
            Tasks = new List<Task>();
        }

        private void btSubmit_Click(object sender, RoutedEventArgs e)
        {
            foreach (Sprint sprint in project.Items)
            {
                RisksNet risksNet = new RisksNet(sprint.Id.ToString());
                var riskFactory = new RelevanceTreeInferenceFactory();
                var riskInference = riskFactory.CreateInferenceEngine(risksNet.Net);
                var riskQueryOptions = riskFactory.CreateQueryOptions();
                var riskQueryOutput = riskFactory.CreateQueryOutput();

                foreach (Task task in sprint.Items)
                {

                    //Tính giá trị task
                    ValueNet valueNet = new ValueNet(task.Id.ToString());           //mạng bayes

                    var factory = new RelevanceTreeInferenceFactory();
                    var inference = factory.CreateInferenceEngine(valueNet.Net);
                    var queryOptions = factory.CreateQueryOptions();
                    var queryOutput = factory.CreateQueryOutput();

                    //lấy các nodes
                    Node nodePriority = valueNet.Net.Nodes["Priority"];
                    Node nodeExperience = valueNet.Net.Nodes["Experience"];
                    Node nodeComplexity = valueNet.Net.Nodes["Complexity"];
                    Node nodeEffort = valueNet.Net.Nodes["Effort"];
                    Node nodeTime = valueNet.Net.Nodes["Time"];

                    //tạo các bảng xác suất
                    var pTable = nodePriority.NewDistribution().Table;
                    var eTable = nodeExperience.NewDistribution().Table;
                    var cTable = nodeComplexity.NewDistribution().Table;
                    var efTable = nodeEffort.NewDistribution().Table;
                    var tTable = nodeTime.NewDistribution().Table;

                    //lấy giá trị từ các task
                    pTable.CopyFrom(task.Priority);
                    eTable.CopyFrom(task.Experience);
                    cTable.CopyFrom(task.Complexity);
                    efTable.CopyFrom(task.Effort);
                    tTable.CopyFrom(task.Time);

                    //gán giá trị cho các bảng
                    nodePriority.Distribution = pTable;
                    nodeExperience.Distribution = eTable;
                    nodeComplexity.Distribution = cTable;
                    nodeEffort.Distribution = efTable;
                    nodeTime.Distribution = tTable;

                    
                    var qValue = new BayesServer.Table(valueNet.Net.Nodes["Value"]);
                    var qLabor = new BayesServer.Table(valueNet.Net.Nodes["Labor"]);
                    var qChlgs = new BayesServer.Table(valueNet.Net.Nodes["Challenges"]);
                    inference.QueryDistributions.Add(qValue);
                    inference.QueryDistributions.Add(qLabor);
                    inference.QueryDistributions.Add(qChlgs);
                    inference.Query(queryOptions, queryOutput);

                    task.Labor = new double[] { qLabor[0], qLabor[1] };
                    task.Challenges = new double[] { qChlgs[0], qChlgs[1] };
                    task.Value = new double[] { qValue[0], qValue[1] };
                    sprint.TaskValue += task.Value[1];
                }

                sprint.TaskValue /= sprint.Items.Count;
                //Tính xác suất thành công
                //Duyệt toàn bộ nhân lực
                foreach (Resource resource in Resources)
                {
                    //Lấy các nodes
                    Node node14 = risksNet.Net.Nodes["node14"];
                    Node node1 = risksNet.Net.Nodes["node1"];
                    Node node4 = risksNet.Net.Nodes["node4"];
                    Node node3 = risksNet.Net.Nodes["node3"];
                    Node node11 = risksNet.Net.Nodes["node11"];
                    Node node18 = risksNet.Net.Nodes["node18"];

                    //Lấy danh sách các trạng thái
                    StateCollection stateNode14 = node14.Variables[0].States;
                    StateCollection stateNode1 = node1.Variables[0].States;
                    StateCollection stateNode4 = node4.Variables[0].States;
                    StateCollection stateNode3 = node3.Variables[0].States;
                    StateCollection stateNode11 = node11.Variables[0].States;
                    StateCollection stateNode18 = node18.Variables[0].States;

                    //Lấy ra trạng thái
                    State valueNode14 = stateNode14[resource.ManagementExperience];
                    State valueNode1 = stateNode1[resource.HumanSkill.ToString()];
                    State valueNode4 = stateNode4[resource.Productivity.ToString()];
                    State valueNode3 = stateNode3[resource.TimePressure];
                    State valueNode11 = stateNode11[sprint.Speed.ToString()];
                    State valueNode18 = stateNode18[sprint.TaskValue > 0.5 ? "True" : "False"];

                    //Gán các trạng thái
                    riskInference.Evidence.SetState(valueNode14);
                    riskInference.Evidence.SetState(valueNode1);
                    riskInference.Evidence.SetState(valueNode4);
                    riskInference.Evidence.SetState(valueNode3);
                    riskInference.Evidence.SetState(valueNode11);
                    riskInference.Evidence.SetState(valueNode18);

                    //Query
                    BayesServer.Table query = new BayesServer.Table(risksNet.Net.Nodes["risk"]);
                    riskInference.QueryDistributions.Add(query);
                    riskInference.Query(riskQueryOptions, riskQueryOutput);
                    var result = query[risksNet.Net.Nodes["risk"].Variables[0].States["True"]];
                    if (result > sprint.ProbabilityOfSuccess)
                        sprint.ProbabilityOfSuccess = result;
                }
                project.ProbabilityOfSuccess += sprint.ProbabilityOfSuccess / project.Items.Count;
            }
            ResultDlg resultDlg = new ResultDlg();
            resultDlg.treeView.Items.Add(project);
            resultDlg.ShowDialog();


        }

        private void btTaskLit_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json";
            if (openFileDialog.ShowDialog() == true)
            {
                tbDataPath.Text = openFileDialog.SafeFileName;
                btSubmit.IsEnabled = (ResourceFilePath != null);
                DataFilePath = openFileDialog.FileName;
            }
            var jsonString = File.ReadAllText(DataFilePath, Encoding.UTF8);
            project = JsonSerializer.Deserialize<Project>(jsonString);
            int maxDealine = 0;
            foreach (Sprint sprint in project.Items)
            {
                foreach(Task task in sprint.Items)
                {
                    sprint.Deadline += task.Deadline;
                }
                if (sprint.Deadline > maxDealine)
                    maxDealine = sprint.Deadline;
            }

            foreach (Sprint sprint in project.Items)
            {
                sprint.Speed = 5 * sprint.Deadline / maxDealine;
            }
        }

        private void btResource_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "csv files (*.csv)|*.csv";
            if (openFileDialog.ShowDialog() == true)
            {
                tbResourcePath.Text = openFileDialog.SafeFileName;
                btSubmit.IsEnabled = (DataFilePath != null);
                ResourceFilePath = openFileDialog.FileName;
            }
            Resources = new List<Resource>();

            using (StreamReader sr = new StreamReader(ResourceFilePath))
            {
                while (!sr.EndOfStream)
                {
                    string[] lines = sr.ReadLine().Split(',');
                    if (lines.Length == 5)
                    {
                        Resource resource = new Resource();
                        resource.Id = int.Parse(lines[0]);
                        resource.ManagementExperience = (lines[1]);
                        resource.HumanSkill = int.Parse(lines[2]);
                        resource.Productivity = int.Parse(lines[3]);
                        resource.TimePressure = lines[4];
                        Resources.Add(resource);
                    }
                }
            }
        }
    }
}
