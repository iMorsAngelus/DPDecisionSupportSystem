using System;
using System.Collections.Generic;
using System.Windows;
using DecisionSupportSystem.BusinessLogicLayer;
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
            Log.Info($"Managing app started. Program version: {version}.");

            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            base.OnStartup(e);

            var prioritySearcher = new PriorityVectorSearcher();
            var context = new DssContext();
            var dataProvider = new DataBaseProvider(context);

            var viewModelList = new List<IPageViewModel>
            {
                new TaskManagingViewModel(dataProvider),
                new InputViewModel(dataProvider),
                new PairMatrixViewModel(dataProvider),
                new ResultViewModel(dataProvider, prioritySearcher)
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
