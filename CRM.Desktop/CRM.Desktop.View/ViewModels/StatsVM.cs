using CRM.Desktop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Media;

namespace CRM.Desktop.View.ViewModels
{
    public class StatsVM : BaseVM
    {
        private SeriesCollection opportunityCountCollection;
        public SeriesCollection OpportunityCountCollection
        {
            get
            {
                return opportunityCountCollection;
            }
            set
            {
                opportunityCountCollection = value;
                OnPropertyChanged();
            }
        }

        private SeriesCollection thisYearCountCollection;
        public SeriesCollection ThisYearCountCollection
        {
            get
            {
                return thisYearCountCollection;
            }
            set
            {
                thisYearCountCollection = value;
                OnPropertyChanged();
            }
        }

        private SeriesCollection contactsCollection;
        public SeriesCollection ContactsCollection
        {
            get
            {
                return contactsCollection;
            }
            set
            {
                contactsCollection = value;
                OnPropertyChanged();
            }
        }

        private SeriesCollection thisMonthCountCollection;
        public SeriesCollection ThisMonthCountCollection
        {
            get
            {
                return thisMonthCountCollection;
            }
            set
            {
                thisMonthCountCollection = value;
                OnPropertyChanged();
            }
        }

        public StatsVM(CompanyStatisticsDto data)
        {
            var series = new SeriesCollection();
            series.Add(new ColumnSeries
            {
                Values = new ChartValues<int>(data.Opportunities)
            });
            OpportunityCountCollection = series;

            series = new SeriesCollection();
            series.Add(new ColumnSeries
            {
                Values = new ChartValues<double> { data.ThisMonthNet, data.ThisMonthGross, data.ThisMonthMarkup }
            });
            ThisMonthCountCollection = series;

            series = new SeriesCollection();
            series.Add(new ColumnSeries
            {
                Values = new ChartValues<double> { data.ThisYearNet, data.ThisYearGross, data.ThisYearMarkup }
            });
            ThisYearCountCollection = series;

            series = new SeriesCollection
            {
                new PieSeries
                {
                    Title="Inna aktywność",
                    Values=new ChartValues<int>{data.Activities[0]},
                    Fill= Brushes.Red,
                    Stroke=Brushes.Red
                },
                new PieSeries
                {
                    Title="Rozmowa telefoniczna",
                    Values=new ChartValues<int>{data.Activities[1]},
                    Fill= Brushes.Lime,
                    Stroke=Brushes.Lime
                },
                new PieSeries
                {
                    Title="Wiadomość Email",
                    Values=new ChartValues<int>{data.Activities[2]},
                    Fill= Brushes.Blue,
                    Stroke=Brushes.Blue
                },
                new PieSeries
                {
                    Title="Rozmowa online",
                    Values=new ChartValues<int>{data.Activities[3]},
                    Fill= Brushes.Gray,
                    Stroke=Brushes.Gray
                },
                new PieSeries
                {
                    Title="Spotkanie z klientem",
                    Values=new ChartValues<int>{data.Activities[4]},
                    Fill= Brushes.LightGreen,
                    Stroke=Brushes.LightGreen
                },
                new PieSeries
                {
                    Title="Wysłanie oferty",
                    Values=new ChartValues<int>{data.Activities[5]},
                    Fill= Brushes.Aqua,
                    Stroke=Brushes.Aqua
                }
            };
            ContactsCollection = series;
        }
    }
}
