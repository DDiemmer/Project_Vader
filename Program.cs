﻿using Microsoft.Speech.Recognition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;

namespace ProjetoVader
{

    class Program
    {
        private static Vader vader;
       
        
        
        static void Main(string[] args)
        {
        
            vader = new Vader();

        }
    }
}
