using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Engine.Models
{
    public class AAbility : INotifyPropertyChanged
    {
        private string _abl_name;
        public string AblName
        {
            get { return _abl_name; }
            set
            {
                _abl_name = value;
                OnPropertyChanged("AblName");
            }
        }
        public AAbility()
        {
            _abl_name = "";
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }

}
