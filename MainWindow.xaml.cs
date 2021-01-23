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
using Microsoft.Win32;
using System.IO;
namespace Bayesian_Task_Value
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private String taskListFilePath = null;
        private String attributesFilePath = null;
        private List<Task> tasks;
        public MainWindow()
        {
            InitializeComponent();
            btSubmit.IsEnabled = false;
        }

        private void btSubmit_Click(object sender, RoutedEventArgs e)
        {
            Task task;
            tasks = new List<Task>();
            Random rand = new Random();
            using (StreamReader tlt = new StreamReader(taskListFilePath))
            using (StreamReader ats = new StreamReader(attributesFilePath))
            {
                while (!tlt.EndOfStream || !ats.EndOfStream)
                {
                    String[] line1 = tlt.ReadLine().Split(';');
                    String[] line2 = ats.ReadLine().Split(',');
                    if (line1.Length != 2 || line2.Length != 5)
                        return;

                    int id = int.Parse(line1[0]);
                    String name = line1[1];
                    if (name[0] == '\"')
                        name = name.Substring(1);
                    if (name[name.Length - 1] == '\"')
                        name = name.Substring(0, name.Length - 1);

                    double[] attr = new double[15];
                    for (int i = 0; i < 5; ++i)
                    {
                        int vl = int.Parse(line2[i]);
                        if (vl > 1 && vl < 4)
                            vl = 2;
                        else if (vl >= 4)
                            vl = 3;
                        double sum = 0;
                        for(int j = 0; j < 3; ++j)
                        {
                            attr[i * 3 + j] = rand.NextDouble();
                            sum += attr[i * 3 + j];
                        }
                        for (int j = 0; j < 3; ++j)
                        {
                            attr[i * 3 + j] /= sum ;
                            attr[i * 3 + j] *= 0.4 ;
                        }
                        attr[i * 3 + vl - 1] += 0.6;
                    }

                    task = new Task(id, name);
                    task.attribute.priority.setValue(new double[] { attr[0], attr[1], attr[2] });
                    task.attribute.experience.setValue(new double[] { attr[3], attr[4], attr[5] });
                    task.attribute.complexity.setValue(new double[] { attr[6], attr[7], attr[8] });
                    task.attribute.effort.setValue(new double[] { attr[9], attr[10], attr[11] });
                    task.attribute.time.setValue(new double[] { attr[12], attr[13], attr[14] });
                    task.caculateValue();
                    tasks.Add(task);
                }
                ResultDlg resultDlg = new ResultDlg();
                resultDlg.lvResult.ItemsSource = tasks;
                resultDlg.SizeToContent = SizeToContent.Width;
                resultDlg.ShowDialog();

            }
        }

        private void btTaskLit_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV files (*.csv)|*.csv";
            if (openFileDialog.ShowDialog() == true)
                lbTaskListFileName.Content = openFileDialog.SafeFileName;
            taskListFilePath = openFileDialog.FileName;
            if (taskListFilePath != null && attributesFilePath != null)
                btSubmit.IsEnabled = true;
        }

        private void btAttributes_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV files (*.csv)|*.csv";
            if (openFileDialog.ShowDialog() == true)
                lbAttributesFileName.Content = openFileDialog.SafeFileName;
            attributesFilePath = openFileDialog.FileName;
            if (taskListFilePath != null && attributesFilePath != null)
                btSubmit.IsEnabled = true;
        }
    }
}
