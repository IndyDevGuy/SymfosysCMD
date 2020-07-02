using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using SymfosysCMD.Console;
using SymfosysCMD.Framework;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Management;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SymfosysCMD.Controls
{
    /// <summary>
    /// Interaction logic for ProjectConsoleTabControl.xaml
    /// </summary>
    public partial class ProjectConsoleTabControl : UserControl, INotifyPropertyChanged
    {
        private string _cpuUsage;
        public string CPUUsage { get { return this._cpuUsage; } set { this._cpuUsage = value; OnPropertyChanged("CPUUsage"); } }
        private string _ramUsage;
        public string RAMUsage { get { return this._ramUsage; } set { this._ramUsage = value; OnPropertyChanged("RAMUsage"); } }
        private bool _callingCommand;

        public SymfosysConsole symfosysConsole;
        public bool CallingCommand { get { return this._callingCommand; } set { this._callingCommand = value; OnPropertyChanged("CallingCommand"); } }

        private SeriesCollection _CPUSeriesCollection;
        public SeriesCollection CPUSeriesCollection { get { return this._CPUSeriesCollection; } set { this._CPUSeriesCollection = value; OnPropertyChanged("CPUSeriesCollection"); } }

        private SeriesCollection _RAMSeriesCollection;
        public SeriesCollection RAMSeriesCollection { get { return this._RAMSeriesCollection; } set { this._RAMSeriesCollection = value; OnPropertyChanged("RAMSeriesCollection"); } }

        public LineSeries cpuLineSeries;

        public LineSeries ramLineSeries;

        private double _CommandHourTime;
        private double _CommandMinuteTime;
        private double _CommandSecondTime;
        public double CommandHourTime { get { return this._CommandHourTime; } set { this._CommandHourTime = value; OnPropertyChanged("CommandHourTime"); } }
        public double CommandMinuteTime { get { return this._CommandMinuteTime; } set { this._CommandMinuteTime = value; OnPropertyChanged("CommandMinuteTime"); } }
        public double CommandSecondTime { get { return this._CommandSecondTime; } set { this._CommandSecondTime = value; OnPropertyChanged("CommandSecondTime"); } }
        public DispatcherTimer timer;
        public double time;
        public ProjectConsoleTabControl()
        {
            this.CallingCommand = false;
            DataContext = this;
            InitializeComponent();
            this.timer = new DispatcherTimer();
            this.timer.Interval = TimeSpan.FromSeconds(1);
            this.timer.Tick += Timer_Tick;
            this.initSeriesCollections();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            time++;
            if (time < 60)
                CommandSecondTime++;
            else if (time > 60)
            {
                if(CommandMinuteTime > 60)
                {
                    CommandHourTime++;
                    CommandMinuteTime = 0;
                }
                CommandSecondTime = 0;
                CommandMinuteTime++;
                time = 0;
            }
        }

        public void initSeriesCollections()
        {
            this.CPUUsage = "0";
            this.RAMUsage = "0";
            this.cpuLineSeries = new LineSeries
            {
                Title = "CPU",
                LineSmoothness = 0,
                PointGeometry = null,
                Values = new ChartValues<ObservableValue>
                {
                    new ObservableValue(0),
                    new ObservableValue(0),
                }
            };
            this.ramLineSeries = new LineSeries
            {
                Title = "Memory",
                LineSmoothness = 0,
                PointGeometry = null,
                Values = new ChartValues<ObservableValue>
                {
                    new ObservableValue(0),
                    new ObservableValue(0),
                }
            };
            //ramLineSeries.Values.Add(2);
            CPUSeriesCollection = new SeriesCollection
            {
                this.cpuLineSeries
            };
            RAMSeriesCollection = new SeriesCollection
            {
                this.ramLineSeries
            };
        }

        public void populateCPUInfo()
        {
            try
            {
                // Creates and returns a CpuUsage instance that can be
                // used to query the CPU time on this operating system.
                //cpu = CpuUsage.Create();
                /// Creating a New Thread 
                Thread thread = new Thread(new ThreadStart(delegate ()
                {
                    try
                    {
                        if (ProcessAsyncHelper.currentProcess != null)
                        {
                            this.time = 00;
                            this.CommandSecondTime = 00;
                            this.CommandMinuteTime = 00;
                            this.CommandHourTime = 00;
                            this.timer.Start();
                            var cpu = new PerformanceCounter("Process", "% Processor Time", "symfosyscmd");
                            var ram = new PerformanceCounter("Process", "Private Bytes", ProcessAsyncHelper.currentProcess.ProcessName);
                            cpu.NextValue();
                            ram.NextValue();
                            //get total memory of ram on system
                            ObjectQuery wql = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
                            ManagementObjectSearcher searcher = new ManagementObjectSearcher(wql);
                            ManagementObjectCollection results = searcher.Get();

                            double res;
                            double ramMb = 0;
                            foreach (ManagementObject result in results)
                            {
                                res = Convert.ToDouble(result["TotalVisibleMemorySize"]);
                                double fres = Math.Round((res / (1024 * 1024)), 2);
                                ramMb = Math.Round((res) / 1024, 2);
                                Debug.WriteLine("Total usable memory size: " + fres + "GB");
                                Debug.WriteLine("Total usable memory size: " + ramMb + "MB");
                                Debug.WriteLine("Total usable memory size: " + res + "KB");
                            }
                            while (CallingCommand)
                            { 
                                // Creating delay to get correct values of CPU usage during next query
                                Thread.Sleep(1000);
                                double cpuCounter = Math.Round(cpu.NextValue() / (float) Environment.ProcessorCount,2);
                                double memCounter = ram.NextValue();
                                
                                this.CPUUsage = cpuCounter.ToString();
                                CPUSeriesCollection[0].Values.Add(new ObservableValue(cpuCounter));
                                //if (CPUSeriesCollection[0].Values.Count > 20)
                                    //clear old data point after Thrad Sleep time * 40
                                    //CPUSeriesCollection[0].Values.RemoveAt(0);

                                double tmpRam = Math.Round((memCounter) / 1024, 2);
                                double RAM = Math.Round((tmpRam / ramMb) * 100,2);
                                this.RAMUsage = RAM.ToString();
                                RAMSeriesCollection[0].Values.Add(new ObservableValue(RAM));
                                //if (RAMSeriesCollection[0].Values.Count > 40)
                                //clear old data point after Thrad Sleep time * 40
                                    //RAMSeriesCollection[0].Values.RemoveAt(0);
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }

                }))
                {
                    Priority = ThreadPriority.Highest,
                    IsBackground = true
                };
                thread.Start();//Start the Thread
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);

            }
        }

        public void CallCommand(object sender, RoutedEventArgs e)
        {
            if (!this.CallingCommand)
            {
                this.symfosysConsole.callCommand();
                this.populateCPUInfo();
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
