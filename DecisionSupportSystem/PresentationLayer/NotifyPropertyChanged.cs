﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DecisionSupportSystem.PresentationLayer
{
    /// <summary>
    /// Class, who implement INotifyPropertyChanged interface.
    /// </summary>
    [Serializable]
    public class NotifyPropertyChanged : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Property changed event
        /// </summary>
        /// <param name="propertyName">AlternativeName of called property</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}