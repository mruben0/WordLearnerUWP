using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordLearner.Models
{
    public class HelperTools
    {
        public void CleanList(ObservableCollection<string> observableCollection)
        {
            while (observableCollection.Count > 0)
            {
                observableCollection.RemoveAt(observableCollection.Count - 1);
            }
        }
    }
}