using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyTime.Models
{
    public abstract class Observerable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
