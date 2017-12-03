using System;
using System.Collections.Generic;
using System.Text;

namespace ESCde.Cars.MobileApp.Model
{
    public abstract class Observable
    {

        private List<IObserver> observerList = new List<IObserver>();

        /// <summary>
        /// Method to register an observer, that will be informed, when something changed.
        /// </summary>
        /// <param name="observer">The observer to register.</param>
        public bool Register(IObserver observer)
        {
            if (observer == null) return false;
            observerList.Add(observer);
            return true;
        }

        /// <summary>
        /// Method to unregister an Observer.
        /// </summary>
        /// <param name="observer">The observer that will be unregistered</param>
        public bool Unregister(IObserver observer)
        {
            if (observer == null) return false;
            observerList.Remove(observer);
            return true;
        }

        /// <summary>
        /// Method that updates all registered Observers.
        /// </summary>
        public void Update()
        {
            foreach (IObserver observer in observerList)
            {
                observer.Update();
            }
        }
    }
}
