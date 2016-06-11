﻿namespace Sentinel
{
    using System;
    using System.Net.Sockets;
    using System.Runtime.ExceptionServices;
    using System.Windows;

    using Common.Logging;

    using Sentinel.Properties;
    using Sentinel.Services;
    using Sentinel.Services.Interfaces;

    /// <summary>
    /// Interaction logic for MainApplication.xaml
    /// </summary>
    public partial class MainApplication : Application
    {
        private static readonly ILog Log = LogManager.GetLogger<MainApplication>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MainApplication"/> class.
        /// </summary>
        public MainApplication()
        {
            AppDomain.CurrentDomain.FirstChanceException += FirstChanceExceptionHandler;

            Settings.Default.Upgrade();

            ServiceLocator locator = ServiceLocator.Instance;
            locator.ReportErrors = true;
            locator.Register<ISessionManager>(new SessionManager());

            // Request that the application close on main window close.
            ShutdownMode = ShutdownMode.OnMainWindowClose;
        }

        private static void FirstChanceExceptionHandler(object sender, FirstChanceExceptionEventArgs e)
        {
            if (e.Exception is SocketException)
            {
                return;
            }

            if (!string.IsNullOrWhiteSpace(e.Exception.Source) && e.Exception.Source.ToLower() == "mscorlib")
            {
                return;
            }

            // ReSharper disable once UseStringInterpolation
            var errorString =
                string.Format(
                    "Sender: {0} FirstChanceException raised in {1} : Message -- {2} :: InnerException -- {3} :: TargetSite -- {4} :: StackTrace -- {5} :: HelpLink -- {6} ",
                    sender,
                    AppDomain.CurrentDomain.FriendlyName,
                    e.Exception.Message,
                    e.Exception.InnerException?.Message ?? string.Empty,
                    e.Exception.TargetSite?.Name ?? string.Empty,
                    e.Exception.StackTrace ?? string.Empty,
                    e.Exception.HelpLink ?? string.Empty);

            Log.Error(errorString, e.Exception);
        }
    }
}