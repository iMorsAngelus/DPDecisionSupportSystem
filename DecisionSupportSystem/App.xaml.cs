using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using DecisionSupportSystem.DataAccessLayer.ApplicationModels;
using DecisionSupportSystem.DataAccessLayer.DataCreationModel;
using DecisionSupportSystem.DataAccessLayer.DbModels;
using DecisionSupportSystem.PresentationLayer.ViewModel;
using log4net;

namespace DecisionSupportSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(App));

        /// <summary>Raises the <see cref="E:System.Windows.Application.Startup" /> event.</summary>
        /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs" /> that contains the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            var version = typeof(App).Assembly.GetName().Version;
            Log.Info($"Managing app started. Programm version: {version}.");

            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            base.OnStartup(e);

            var context = new DecisionSupportSystemDataBaseModelContainer();
            var dataProvider = new DataBaseProvider(context);

            var viewModelList = new List<IPageViewModel>
            {
                new TaskManagingViewModel(dataProvider),
                new InputViewModel(null, null)
            };

            var mainWindowViewModel = new MainWindowViewModel(viewModelList, dataProvider);
            var mainWindow = new MainWindow { DataContext = mainWindowViewModel };

            Log.Info("Initialize is successful");
            mainWindow.Show();
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = (Exception)e.ExceptionObject;
            Log.Error(e.IsTerminating ? "Application is terminating because of an unhandled exception." : "Unhandled thread exception.", exception);
            if (!e.IsTerminating)
            {
                return;
            }

            var message = $"Unhandled exception has occurred.\n \"{exception.Message}\"\nThe application will be terminated.";
            MessageBox.Show(message, "Unhandled Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            Environment.Exit(0);
        }
    }
}
