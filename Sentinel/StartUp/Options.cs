﻿namespace Sentinel.Startup
{
    using CommandLine;

    public class Options
    {
        // ReSharper disable once UnusedMember.Global
        [VerbOption("log4net", HelpText = "Use log4net listener")]
        public Log4NetOptions Log4NetVerb { get; set; }

        // ReSharper disable once UnusedMember.Global
        [VerbOption("nlog", HelpText = "Use nlog listener")]
        public NLogOptions NLogVerb { get; set; }

        // ReSharper disable once UnusedMember.Global
        [VerbOption("file", HelpText = "Use file monitor")]
        public FileMonitorOptions FileMonitorVerb { get; set; }
    }
}
