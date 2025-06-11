using MusicalInstruments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    internal class MyObservableCollection<T> : MyCollection<T>
        where T: IInit, ICloneable, new()
    {

    }
}
