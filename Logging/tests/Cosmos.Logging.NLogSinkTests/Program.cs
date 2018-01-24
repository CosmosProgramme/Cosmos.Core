﻿using System;
using Cosmos.Logging.Events;
using Cosmos.Logging.Sinks.NLog;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace Cosmos.Logging.NLogSinkTests {
    class Program {
        static void Main(string[] args) {

            try {
                //var config = GetNLogConfig();

                LOGGER.Initialize().RunsOnConsole()
                    //.WriteToNLog(s => s.OriginConfiguration = config)
                    .UseNLog(s => s.EnableUsingDefaultConfig())
                    .AllDone();

                var logger = LOGGER.GetLogger(mode: LogEventSendMode.Manually);

                logger.LogInformation("hello");
                logger.LogError("world");
                logger.SubmitLogger();

                Console.WriteLine("Hello World!");
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.Source);
                Console.WriteLine(e.StackTrace);
            }

            Console.ReadLine();
        }

        private static NLog.Config.LoggingConfiguration GetNLogConfig() {
            var config = new NLog.Config.LoggingConfiguration();
            var consoleTarget = new ColoredConsoleTarget();
            config.AddTarget("console", consoleTarget);
            consoleTarget.Layout = @"${date:format=HH\:mm\:ss} ${logger} ${message}";
            var rule1 = new LoggingRule("a", LogLevel.Debug, consoleTarget);
            config.LoggingRules.Add(rule1);

            return config;
        }
    }
}