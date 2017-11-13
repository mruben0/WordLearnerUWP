using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WordLearner.ViewModels
{
    //public class NotificationBase : INotifyPropertyChanged
    //{
    //    public event PropertyChangedEventHandler PropertyChanged;

    //    // SetField (Name, value); // where there is a data member
    //    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] String property
    //       = null)
    //    {
    //        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
    //        field = value;
    //        RaisePropertyChanged(property);
    //        return true;
    //    }

    //    // SetField(()=> somewhere.Name = value; somewhere.Name, value)
    //    // Advanced case where you rely on another property
    //    protected bool SetProperty<T>(T currentValue, T newValue, Action DoSet,
    //        [CallerMemberName] String property = null)
    //    {
    //        if (EqualityComparer<T>.Default.Equals(currentValue, newValue)) return false;
    //        DoSet.Invoke();
    //        RaisePropertyChanged(property);
    //        return true;
    //    }

    //    protected void RaisePropertyChanged(string property)
    //    {
    //        if (PropertyChanged != null)
    //        {
    //            PropertyChanged(this, new PropertyChangedEventArgs(property));
    //        }
    //    }
    //}

    //public class NotificationBase<T> : NotificationBase where T : class, new()
    //{
    //    protected T This;

    //    public static implicit operator T(NotificationBase<T> thing)
    //    {
    //        return thing.This;
    //    }

    //    public NotificationBase(T thing = null)
    //    {
    //        This = (thing == null) ? new T() : thing;
    //    }
    //}

    public class BaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(storage, value)) return false;
            storage = value;
            this.OnPropertyChaned(propertyName);
            return true;
        }

        public void OnPropertyChaned(string propertyName)
        {
            var eventHandler = this.PropertyChanged;
            if (eventHandler != null)
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}