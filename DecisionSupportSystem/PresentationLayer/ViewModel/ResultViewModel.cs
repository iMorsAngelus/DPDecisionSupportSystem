﻿using DecisionSupportSystem.BusinessLogicLayer;
using DecisionSupportSystem.Common;
using DecisionSupportSystem.DataAccessLayer.DataCreationModel;
using DecisionSupportSystem.DataAccessLayer.DbModels;
using DecisionSupportSystem.PresentationLayer.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Task = System.Threading.Tasks.Task;

namespace DecisionSupportSystem.PresentationLayer.ViewModel
{
    class ResultViewModel : ViewModelBase
    {
        private readonly IDataBaseProvider _provider;
        private readonly PriorityVectorSearcher _priorityVectorSearcher;
        private Criteria _selectedCriteria;
        private ActionCommand _includeCriteriaCommand;
        private ActionCommand _excludeCriteriaCommand;
        private ActionCommand _searchResultCommand;
        private double[] _results;
        private string _output;

        public ResultViewModel(IDataBaseProvider provider, PriorityVectorSearcher priorityVectorSearcher)
        {
            _provider = provider;
            _priorityVectorSearcher = priorityVectorSearcher;
            Criterias = new ObservableCollection<Criteria>();
            ExcludedCriterias = new ObservableCollection<Criteria>();
            IncludedCriterias = new ObservableCollection<Criteria>();
            DisplayName = "ResultViewModel";
        }

        public ICommand IncludeCriteriaCommand =>
            _includeCriteriaCommand ?? (_includeCriteriaCommand = new ActionCommand(
                param =>
                {
                    if (ExcludedCriterias.Contains(SelectedCriteria))
                    {
                        IncludedCriterias.Add(SelectedCriteria);
                        ExcludedCriterias.Remove(SelectedCriteria);
                    }
                }));

        public ICommand ExcludeCriteriaCommand =>
            _excludeCriteriaCommand ?? (_excludeCriteriaCommand = new ActionCommand(
                param =>
                {
                    if (IncludedCriterias.Contains(SelectedCriteria))
                    {
                        ExcludedCriterias.Add(SelectedCriteria);
                        IncludedCriterias.Remove(SelectedCriteria);
                    }
                }));

        public ICommand SearchResultCommand =>
            _searchResultCommand ?? (_searchResultCommand = new ActionCommand(param => Calculate()));

        public ObservableCollection<Criteria> Criterias { get; private set; }
        public ObservableCollection<Criteria> ExcludedCriterias { get; private set; }
        public ObservableCollection<Criteria> IncludedCriterias { get; private set; }
        public Criteria SelectedCriteria
        {
            get => _selectedCriteria;
            set
            {
                _selectedCriteria = value;
                OnPropertyChanged();
            }
        }

        public double[] Results
        {
            get => _results;
            private set
            {
                _results = value;
                OnPropertyChanged();
            }
        }

        public string Output
        {
            get => _output;
            set
            {
                _output = value; 
                OnPropertyChanged();
            }
        }

        public override void UpdateDataOnPage()
        {
            SetupViewCriterias();
        }

        private void SetupViewCriterias()
        {
            Criterias = new ObservableCollection<Criteria>(_provider.CurrentTask.Criterias);
            ExcludedCriterias = new ObservableCollection<Criteria>();
            IncludedCriterias = new ObservableCollection<Criteria>(Criterias);
        }

        private List<Task<double[]>> SearchPriorityVectors()
        {
            var priorityVectorsTasks = new List<Task<double[]>>();
            var pairMatrices = BoxingExtension.Unboxing(Convert.FromBase64String(_provider.CurrentTask.PairMatrices)) as List<PairMatrix<double>>;
            pairMatrices?.ForEach(matrix =>
            {
                var task = _priorityVectorSearcher.SearchAsync(matrix.Size, matrix);
                priorityVectorsTasks.Add(task);
            });

            return priorityVectorsTasks;
        }

        private async void Calculate()
        {
            //TODO: [igor.armash]: Extract all logic to a new class
            if (_provider.CurrentTask.AlternativePriorityVector.Count == 0 || _provider.CurrentTask.CriteriaPriorityVector.Count == 0)
            {
                var tasks = SearchPriorityVectors();
                var priorityVectors = tasks.Select(task => task.Result).ToList();

                await Task.WhenAll(tasks);

                SaveVectorsToDataBase(priorityVectors);
            }

            Output = $"Task - {_provider.CurrentTask.Name}\r\n\r\nCriterion's:\r\n";

            Results = new double[_provider.CurrentTask.Alternatives.Count];

            Output += string.Join("\r\n",
                _provider.CurrentTask.Criterias
                    .Where(c => ExcludedCriterias.Contains(c))
                    .Select(c => string.Join(" - ", c.Name, c.Description)));

            var criteriaPriorityVector = _provider
                .CurrentTask
                .Criterias
                .Select(criteria => criteria.CriteriaPriorityVector.FirstOrDefault())
                .ToDictionary(vec => vec.CriteriaId, vec => vec.Value);

            var alternativePriorityVector = _provider
                .CurrentTask
                .Alternatives
                .Select(alternative => alternative.AlternativePriorityVector.ToDictionary(vec => vec.CriteriaId, vec => vec.Value))
                .ToArray();
            var sum = criteriaPriorityVector.Select(vec => ExcludedCriterias.Any(criteria => criteria.ID == vec.Key) ? 0 : vec.Value).Sum();

            foreach (var criteriaPriorityItem in criteriaPriorityVector)
            {
                if (ExcludedCriterias.Any(criteria => criteria.ID == criteriaPriorityItem.Key))
                {
                    continue;
                }

                for (int j = 0; j < _provider.CurrentTask.Alternatives.Count; j++)
                {
                    var criteriaValue = criteriaPriorityVector.Count - ExcludedCriterias.Count > 1
                        ? (criteriaPriorityItem.Value / sum)
                        : 1;

                    Results[j] += Math.Round(criteriaValue * alternativePriorityVector[j][criteriaPriorityItem.Key],
                        int.Parse(ConfigurationManager.AppSettings["CalculationAccuracy"]),
                        MidpointRounding.AwayFromZero);
                }
            }

            Output += "\r\n\r\nAlternatives rating:\r\n" + string.Join("\r\n",
                Results.Select((value, i) =>
                    $"For alternative {_provider.CurrentTask.Alternatives.ElementAt(i).Name} rating is {value * 100}%"));
        }

        private void SaveVectorsToDataBase(IReadOnlyList<double[]> priorityVectors)
        {
            for (var i = 0; i < priorityVectors[0].Length; i++)
            {
                _provider.CurrentTask.CriteriaPriorityVector.Add(new CriteriaPriority()
                {
                    CriteriaId = _provider.CurrentTask.Criterias.ElementAt(i).ID,
                    TaskId = _provider.CurrentTask.ID,
                    Value = priorityVectors[0][i]
                });
            }

            for (var i = 1; i < priorityVectors.Count; i++)
            {
                for (var j = 0; j < priorityVectors[i].Length; j++)
                {
                    _provider.CurrentTask.AlternativePriorityVector.Add(new AlternativePriority()
                    {
                        CriteriaId = _provider.CurrentTask.Criterias.ElementAt(i - 1).ID,
                        AlternativeId = _provider.CurrentTask.Alternatives.ElementAt(j).ID,
                        TaskId = _provider.CurrentTask.ID,
                        Value = priorityVectors[i][j]
                    });
                }
            }

            _provider.SaveChanges();
        }
    }
}