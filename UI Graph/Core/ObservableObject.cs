using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UI_Graph.Core
{
    class ObservableObject : INotifyPropertyChanged//класс для реализации интерфейса, чтобы обрабатывать изменения(по сути нажатия кнопок)
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
