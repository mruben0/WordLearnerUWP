using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordLearner.ViewModels
{
    public class Test : NotificationBase
    {
        private string testing;
        public string Testing
        {
            get => "Done";
            set => SetProperty(ref testing, value);
        }
    }
    public class HomeViewModel
    {
        private Test defaultTest = new Test();
        public Test DefaultTest { get => this.defaultTest; }
    }
}
