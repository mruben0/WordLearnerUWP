using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WordLearner.ViewModels
{
    public class Test 
    {
        
        public string Testing = "Done";
    }
    public class HomeViewModel : BaseModel
    {
        public ICommand TestCommand
        {

            get => new RelayCommand(() =>
            {
                testing = "Wow, it's worked";
            });
        }

        private Test test = new Test();

        public HomeViewModel()
        {
            
        }
       
        public string testing
        {
            get => test.Testing;
            set => SetProperty(ref test.Testing, value);            
        }
    }
}
