using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ЛР1
{
    internal class Tetrads : NotifyPropertyChanged
    {
        private string _op;
        private string _arg1;
        private string _arg2;
        private string _result;

        public Tetrads(string op, string arg1, string arg2, string result)
        {
            Op = op;
            Arg1 = arg1;
            Arg2 = arg2;
            Result = result;
        }

        public string Op
        {
            get { return _op; }
            set
            {
                if (_op != value)
                {
                    _op = value;
                    OnPropertyChanged(nameof(Op));
                }
            }
        }

        public string Arg1
        {
            get { return _arg1; }
            set
            {
                if (_arg1 != value)
                {
                    _arg1 = value;
                    OnPropertyChanged(nameof(Arg1));
                }
            }
        }

        public string Arg2
        {
            get { return _arg2; }
            set
            {
                if (_arg2 != value)
                {
                    _arg2 = value;
                    OnPropertyChanged(nameof(Arg2));
                }
            }
        }

        public string Result
        {
            get { return _result; }
            set
            {
                if (_result != value)
                {
                    _result = value;
                    OnPropertyChanged(nameof(Result));
                }
            }
        }
    }
}
