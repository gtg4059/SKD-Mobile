using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZUMOAPPNAME
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    
    public partial class TabbedPage1 : TabbedPage
    {
        TodoItemManager manager;
        List<dWorker> ldWorker = new List<dWorker>();
        ObservableCollection<WholeWorker> Lwhole = new ObservableCollection<WholeWorker>();
        ObservableCollection<Worker> Lworker = new ObservableCollection<Worker>();
        List<TodoItem> LTodoItem = new List<TodoItem>();
        public TabbedPage1(List<TodoItem> GridTodoItems)
        {
            InitializeComponent();
            manager = TodoItemManager.DefaultManager;
            LTodoItem.Clear();
            Lwhole.Clear();
            Lworker.Clear();
            ldWorker.Clear();
            DateTime today = DateTime.Now;
            //today = DateTime.ParseExact(today, "yyyy-MM-dd-HH:mm:ss", CultureInfo.InvariantCulture);
            List<TodoItem> GridTodoItem = GridTodoItems.OrderBy(x => x.row).ToList();
            List<TodoItem> GridDateTodoItem = new List<TodoItem>();
            //List<TodoItem> GateTodoItem = new List<TodoItem>();
            WholeList.ItemsSource = Lwhole;


            foreach (TodoItem a in GridTodoItem)
            {
                DateTime TodoItemday = DateTime.ParseExact(a.dates, "yyyy-MM-dd-HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
                if (today.Year == TodoItemday.Year && today.Month == TodoItemday.Month && today.Day == TodoItemday.Day)
                {
                    GridDateTodoItem.Add(a);
                    dWorker dw = new dWorker(a.names, a.tags, a.works, 0, 0);
                    ldWorker.Add(dw);
                }
            }
            for (int i = ldWorker.Count - 1; i >= 0; i--)
            {
                for (int j = i - 1; j >= 0; j--)
                {
                    if (ldWorker[i].tag == ldWorker[j].tag)
                    {
                        ldWorker.RemoveAt(j);
                        i--;
                    }
                }
            }

            List<string> GateList = GridDateTodoItem.Select(x => x.gates).Distinct().ToList();
            List<string> JobList = GridDateTodoItem.Select(x => x.works).Distinct().ToList();

            int cnt = GridDateTodoItem.Count;
            string[] pday = new string[cnt];

            for (int i = 0; i < GridDateTodoItem.Count; i++)
            {

                string day = null, time = null, time2 = null;
                string[] gateSplit = GridDateTodoItem[i].gates.Split('-');
                int dt = 0;

                if (GridDateTodoItem[i].inout.Substring(0, 1) == "입" && gateSplit.Length == 1) //MG  들어오는거 발견
                {
                    //Dname = listWorker[i].name;
                    //if (listWorker[i].name != Dname) pday = null;

                    day = GridDateTodoItem[i].dates.Substring(0, 10);
                    time = GridDateTodoItem[i].dates.Substring(11, 5); //출근했으면



                    for (int j = i + 1; j < GridDateTodoItem.Count; j++)
                    {
                        string[] gateSplit2 = GridDateTodoItem[j].gates.Split('-'); //메인게이트
                        if (GridDateTodoItem[i].dates.Substring(0, 10) == GridDateTodoItem[j].dates.Substring(0, 10))
                        {
                            if (GridDateTodoItem[i].names == GridDateTodoItem[j].names && GridDateTodoItem[j].inout.Substring(0, 1) == "출"
                                && gateSplit2.Length == 1)
                            {
                                time2 = GridDateTodoItem[j].dates.Substring(11, 5);

                                foreach (dWorker dw in ldWorker)
                                {
                                    if (dw.tag == GridDateTodoItem[i].tags && cnt >= 1) //&& listWorker[i].date.Substring(0, 10) == listWorker[j].date.Substring(0, 10))
                                    {
                                        if (day != pday[cnt - 1])
                                        {
                                            dw.cnt++;
                                            cnt--;
                                            break;
                                        }

                                    }

                                }
                                break;
                            }
                        }
                        else
                        {
                            time2 = "18:00";
                            foreach (dWorker dw in ldWorker)
                            {
                                if (dw.tag == GridDateTodoItem[i].tags)
                                {
                                    dw.cnt++;
                                    break;
                                }
                            }
                            break;
                        }

                    }
                    if (cnt >= 0 && cnt < ldWorker.Count) pday[cnt] = day;

                }

                if (time != null && time2 != null)
                {
                    DateTime itime = Convert.ToDateTime(time);
                    DateTime ftime = Convert.ToDateTime(time2);
                    if (itime == ftime)
                    {
                        dt = 0;
                    }
                    else
                    {
                        TimeSpan dtime = ftime - itime;
                        dt = dtime.Hours * 60 + dtime.Minutes;
                    }
                    //foundEnd = false;
                }
                foreach (dWorker dw in ldWorker)
                {
                    if (dw.tag == GridDateTodoItem[i].tags)
                    {
                        dw.time += dt;
                        break;
                    }
                }

            }
            int GatesumIn = 0, GatesumOut = 0;

            for (int i = 0; i < GateList.Count; i++)
            {
                //  string comp = formMain.listCompany[i];
                int cntIn = 0, cntOut = 0;
                foreach (TodoItem TodoItems in GridDateTodoItem)
                {
                    if (TodoItems.gates == GridDateTodoItem[i].gates && TodoItems.inout == "입") cntIn++;
                    else if (TodoItems.gates == GridDateTodoItem[i].gates && TodoItems.inout == "출") cntOut++;
                }
                Lwhole.Add(new WholeWorker { WhGate = GridDateTodoItem[i].gates, WhIn = cntIn.ToString(), WhOut = cntOut.ToString(), Whleft = (cntIn - cntOut).ToString() });

                GatesumIn += cntIn;
                GatesumOut += cntOut;

            }
            Allin.Text = GatesumIn.ToString();
            Allout.Text = GatesumOut.ToString();
            Manleft.Text = (GatesumIn - GatesumOut).ToString();
            WholeList.ItemsSource = Lwhole;

            int JobsumIn = 0, JobsumOut = 0;
            for (int i = 0; i < JobList.Count; i++)
            {
                string zob = JobList[i];
                int cntIn = 0, cntOut = 0;
                foreach (TodoItem TodoItems in GridDateTodoItem)
                {
                    if (TodoItems.works == zob && TodoItems.inout == "입") cntIn++;
                    else if (TodoItems.works == zob && TodoItems.inout == "출") cntOut++;
                }
                Lworker.Add(new Worker { WGate = JobList[i], WIn = cntIn.ToString(), WOut = cntOut.ToString(), Wleft = (cntIn - cntOut).ToString() });
                //Worker a = new  Worker(JobList[i], cntIn, cntOut);
                //Lworker.Add(a);
                JobsumIn += cntIn;
                JobsumOut += cntOut;
            }
            Workin.Text = JobsumIn.ToString();
            Workout.Text = JobsumOut.ToString();
            Workleft.Text = (JobsumIn - JobsumOut).ToString();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Set syncItems to true in order to synchronize the data on startup when running in offline mode
            WholeList.ItemsSource = Lwhole;
            Workerlist.ItemsSource = Lworker;
            //await RefreshItems(true, syncItems: true);
        }

        public async void Reset(object sender, EventArgs e)
        {
            var list = (ListView)sender;
            Exception error = null;
            try
            {
                await RefreshItems();
            }
            catch (Exception ex)
            {
                error = ex;
            }
            finally
            {
                list.EndRefresh();
            }
        }

        private async Task RefreshItems()
        {

            LTodoItem = new List<TodoItem>(await manager.GetTodoItemsAsync());
            Application.Current.MainPage = new TabbedPage1(LTodoItem);
        }

        /*public async void Reset(object sender, EventArgs e)
        {

            LTodoItem = new List<TodoItem>(await manager.GetTodoItemsAsync());
            Application.Current.MainPage = new TabbedPage1(LTodoItem);


        }*/


        private class ActivityIndicatorScope : IDisposable
        {
            private bool showIndicator;
            private ActivityIndicator indicator;
            private Task indicatorDelay;

            public ActivityIndicatorScope(ActivityIndicator indicator, bool showIndicator)
            {
                this.indicator = indicator;
                this.showIndicator = showIndicator;

                if (showIndicator)
                {
                    indicatorDelay = Task.Delay(2000);
                    SetIndicatorActivity(true);
                }
                else
                {
                    indicatorDelay = Task.FromResult(0);
                }
            }

            private void SetIndicatorActivity(bool isActive)
            {
                this.indicator.IsVisible = isActive;
                this.indicator.IsRunning = isActive;
            }

            public void Dispose()
            {
                if (showIndicator)
                {
                    indicatorDelay.ContinueWith(t => SetIndicatorActivity(false), TaskScheduler.FromCurrentSynchronizationContext());
                }
            }
        }
    }
}