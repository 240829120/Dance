using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Dance.UnitTest
{
    [DanceSingleton(typeof(IStudent))]
    [ComVisible(true)]
    public class Student : IStudent
    {
        public string? Name { get; set; }
    }
}
