using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTime.Models.Database
{
    public class PauseTime : BaseTime
    {
        public virtual WorkTime WorkTime { get; set; }
    }
}
