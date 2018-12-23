﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using DecisionSupportSystem.DataAccessLayer.DataCreationModel;
using DecisionSupportSystem.DataAccessLayer.DbModels;
using DecisionSupportSystem.PresentationLayer.Command;

namespace DecisionSupportSystem.PresentationLayer.ViewModel
{
    public class TaskManagingViewModel : ViewModelBase, IPageViewModel
    {
        private readonly IDataBaseProvider _provider;
        private ActionCommand _addTaskCommand;
        private ActionCommand _updateTaskCommand;
        private ActionCommand _deleteTaskCommand;

        public TaskManagingViewModel(IDataBaseProvider provider)
        {
            _provider = provider;
            DisplayName = "TaskManagingViewModel";
        }

        public ICommand AddTaskCommand => _addTaskCommand ?? (_addTaskCommand = new ActionCommand(param =>
        {
            DecisionTasks.Add(new Task());
        }));

        public ICommand UpdateTaskCommand => _updateTaskCommand ?? (_updateTaskCommand = new ActionCommand(param =>
        {
           _provider.SaveChanges();
        }));

        public ICommand DeleteTaskCommand => _deleteTaskCommand ?? (_deleteTaskCommand = new ActionCommand(param =>
        {
            DecisionTasks.Remove(SelectedTask);
            SelectedTask = null;
            _provider.SaveChanges();
        }));

        public Task SelectedTask
        {
            get => _provider.CurrentTask;
            set
            {
                _provider.CurrentTask = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Task> DecisionTasks
        {
            get
            {
                _provider.RefreshData();
                return _provider.ObservableTasks;
            }
        }
    }
}