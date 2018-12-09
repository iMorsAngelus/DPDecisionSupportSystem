using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using DecisionSupportSystem.BusinessLogicLayer;
using DecisionSupportSystem.DataAccessLayer;
using DecisionSupportSystem.DataAccessLayer.ApplicationModels;
using DecisionSupportSystem.PresentationLayer.Command;

namespace DecisionSupportSystem.PresentationLayer.ViewModel
{
    class PairMatrixViewModel : ViewModelBase, IPageViewModel
    {
        private readonly List<PairMatrix<double>> _pairMatrices;
        private readonly DecisionTask _decisionTask;
        private string _labelCriteriaTemplate = "Select range that the evaluation interval for criteria {0} compared with criteria {1}.";
        private string _labelAlternativeTemplate = "Select range that the evaluation interval for alternative {0} compared with alternative {1} for criterion {2}.";
        private string _labelText = "";
        private int _currentMatrixIndex = 0;
        private ActionCommand _nextCriteriaCommand;
        private int _minRangeValue;
        private int _maxRangeValue;

        public PairMatrixViewModel(List<PairMatrix<double>> pairMatrices, DecisionTask decisionTask)
        {
            InitializeDescription();
            _pairMatrices = pairMatrices;
            _decisionTask = decisionTask;
            var index = pairMatrices[_currentMatrixIndex].GetUnpopulatedIndex;
            LabelText = string.Format(_labelCriteriaTemplate, _decisionTask.Criterias[index.Column].CriteriaName, _decisionTask.Criterias[index.Row].CriteriaName);
        }

        public EventHandler<List<PairMatrix<double>>> OnPairComplete;

        public ICommand NextCriteriaCommand => _nextCriteriaCommand?? (_nextCriteriaCommand = new ActionCommand(param =>
        {
            var middleValue = ((double) MinRangeValue + MaxRangeValue) / 2;
            _pairMatrices[_currentMatrixIndex].SetFirstUnpopulatedElementInUpperTriangle(new FuzzyNumber<double>(new []
            {
                MinRangeValue, 
                middleValue,
                MaxRangeValue
            }));
            MinRangeValue = 1;
            MaxRangeValue = 3;

            if (_pairMatrices[_currentMatrixIndex].GetUnpopulatedIndex == null)
            {
                _pairMatrices[_currentMatrixIndex].PopulateLowerTriangle();
                ++_currentMatrixIndex;
            }

            if (_pairMatrices.Count <= _currentMatrixIndex)
            {
                OnPairComplete.Invoke(this, _pairMatrices);
                return;
            }

            if (_currentMatrixIndex > 0)
            {
                var index = _pairMatrices[_currentMatrixIndex].GetUnpopulatedIndex;
                LabelText = String.Format(_labelAlternativeTemplate, _decisionTask.Alternatives[index.Row].AlternativeName,
                    _decisionTask.Alternatives[index.Column].AlternativeName, _decisionTask.Criterias[_currentMatrixIndex-1].CriteriaName);
            }
            else
            {
                var index = _pairMatrices[_currentMatrixIndex].GetUnpopulatedIndex;
                LabelText = String.Format(_labelCriteriaTemplate, _decisionTask.Criterias[index.Row],
                    _decisionTask.Criterias[index.Column].CriteriaName);
            }
        }));

        private void InitializeDescription()
        {
            MarkDescriptions = new List<MarkDescription>
            {
                new MarkDescription
                {
                    Comment = "Two alternatives are equally preferable in terms of purpose.",
                    Description = "Equal preference",
                    ValueDegree = "1"
                },
                new MarkDescription
                {
                    Comment = "Intermediate gradation between equal and average preference",
                    Description = "Weak preference",
                    ValueDegree = "2"
                },
                new MarkDescription
                {
                    Comment =
                        "The expert’s experience allows one of the alternatives to be considered a bit preferable to the other.",
                    Description = "Average preference",
                    ValueDegree = "3"
                },
                new MarkDescription
                {
                    Comment = "Intermediate gradation between medium and moderately strong preference",
                    Description = "Above average",
                    ValueDegree = "4"
                },
                new MarkDescription
                {
                    Comment =
                        "The expert’s experience allows one of the alternatives to be considered clearly preferable to the other.",
                    Description = "Moderately strong preference",
                    ValueDegree = "5"
                },
                new MarkDescription
                {
                    Comment = "Intermediate gradation between moderately strong and very strong preference",
                    Description = "Strong preference",
                    ValueDegree = "6"
                },
                new MarkDescription
                {
                    Comment =
                        "The expert’s experience allows one of the alternatives to be considered much more preferable than the other: the domination of the alternative is confirmed by practice.",
                    Description = "Very strong (obvious) preference",
                    ValueDegree = "7"
                },
                new MarkDescription
                {
                    Comment = "Intermediate gradation between very strong and absolute preference",
                    Description = "Very very strong preference",
                    ValueDegree = "8"
                },
                new MarkDescription
                {
                    Comment =
                        "The obviousness of the overwhelming preference of one alternative over the other is indisputable confirmation",
                    Description = "Absolute preference",
                    ValueDegree = "9"
                }
            };
        }

        public string LabelText
        {
            get => _labelText;
            set
            {
                _labelText = value;
                OnPropertyChanged();
            }
        }

        public int MinRangeValue
        {
            get => _minRangeValue;
            set
            {
                _minRangeValue = value;
                OnPropertyChanged();
            }
        }

        public int MaxRangeValue
        {
            get => _maxRangeValue;
            set
            {
                _maxRangeValue = value;
                OnPropertyChanged();
            }
        }

        public List<MarkDescription> MarkDescriptions { get; private set; }
    }
}