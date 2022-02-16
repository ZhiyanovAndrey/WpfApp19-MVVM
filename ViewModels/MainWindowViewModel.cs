using System;
using System.Collections.Generic;
using System.ComponentModel; //необходимо подключить для использования делегата
using System.Linq;
using System.Runtime.CompilerServices; //для использования [CallerMemberName] позволяет не указывать название аргумента, которое подставится из вызывающего св-ва
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input; //для использования ICommand

namespace WpfApp19__MVVM.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged; //Событие нужно для уведомления об изменениях


        void OnPropertyChanged([CallerMemberName] string PropertyName=null) //что бы заставить событие сработать и не вызывать в каждом свойстве Invoke пропишем отдельным методом
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        private double num;

        public double Num
        {
            get { return num; } //в уроке лямбда выражение
            set 
            {
                num = value;
                OnPropertyChanged(); //Можем оставить скобки пустыми название Num само подставится, произойдет уведомление подписчиков о смене названия
            }
            
        }
        private string res;

        public string Res
        {
            get { return res; }
            set
            { 
                res = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCommand { get; } //доступно только для чтения
        private void OnAddCommandExecute(object p) 
        {
            Res = (2 * Num * Math.PI).ToString("N2");
           
        }
        private bool CanAddCommandExecuted (object p)
        {
            if (Num != 0) return true; //команда доступна если не равно нулю, но обычно пишется return true
            else return false;
        }
        public MainWindowViewModel()
        {
            AddCommand = new RelayCommand(OnAddCommandExecute, CanAddCommandExecuted); //действия которые мы определим в методах
                                                                                       //через конструктор передадутся в команду
                                                                                       //получится полноценная команда с 2-мя методами и событием

        }
    }
}
