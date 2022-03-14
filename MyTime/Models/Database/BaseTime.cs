﻿using MyTime.Helpers;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTime.Models.Database
{
    public class BaseTime : Observerable
    {
        private int _id;
        private DateTime _start = DateTime.Now;
        private DateTime _end = DateTime.Now;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public DateTime Start
        {
            get => _start;
            set
            {
                _start = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Duration));
            }
        }

        public DateTime End
        {
            get => _end;
            set
            {
                _end = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Duration));
            }
        }

        [NotMapped]
        public TimeSpan Duration
        {
            get => End - Start;
        }
    }
}
