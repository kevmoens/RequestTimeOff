﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RequestTimeOff.Specflow.zEnd
{
    public class zGenerateLivingDoc
    {
        /// <summary>
        /// Make sure your machine has this tool installed:
        /// dotnet tool install --global SpecFlow.Plus.LivingDoc.CLI
        /// </summary>
        [Fact]
        public void GenerateLivingDoc()
        {           
            Thread.Sleep(2_000);
            var startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.FileName = "livingdoc";
            startInfo.Arguments = "test-assembly RequestTimeOff.Specflow.dll -t TestExecution.json";
            startInfo.WorkingDirectory = System.IO.Directory.GetCurrentDirectory();
            var process = new System.Diagnostics.Process();
            process.StartInfo = startInfo;
            process.Start(); 
            Assert.True(true);
        }
    }
}
