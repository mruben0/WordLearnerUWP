﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordLearner.ViewModels
{
    public class Test 
    {
        
        public string Testing = "Done";
    }
    public class HomeViewModel : BaseModel
    {
        private Test test = new Test();
        
       
        public string testing
        {
            get => test.Testing;
            set => SetProperty(ref test.Testing, value);            
        }
    }
}
