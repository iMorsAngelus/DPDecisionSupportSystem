using DecisionSupportSystem.BusinessLogicLayer;
using DecisionSupportSystem.Common;
using DecisionSupportSystem.DataAccessLayer.ApplicationModels;
using DecisionSupportSystem.DataAccessLayer.DbModels;
using DecisionSupportSystem.PresentationLayer.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace DecisionSupportSystem.PresentationLayer.ViewModel
{
    class PairMatrixViewModel : ViewModelBase
    {
        private readonly IDataBaseProvider _provider;

        private string _labelCriteriaTemplate = "Select range that the evaluation interval for Criterias {0} compared with Criterias {1}.";
        private string _labelAlternativeTemplate = "Select range that the evaluation interval for Alternatives {0} compared with Alternatives {1} for criterion {2}.";
        private string _labelText = "";
        private int _currentMatrixIndex = 0;
        private ActionCommand _nextCriteriaCommand;
        private List<PairMatrix<double>> _pairMatrix;
        private int _minRangeValue = 1;
        private int _maxRangeValue = 1;

        public PairMatrixViewModel(IDataBaseProvider provider)
        {
            _provider = provider;
            InitializeDescription();
            _pairMatrix = new List<PairMatrix<double>>();
            LabelText = "";

            DisplayName = "PairMatrixViewModel";
        }

        public ICommand NextCriteriaCommand => _nextCriteriaCommand ?? (_nextCriteriaCommand = new ActionCommand(param =>
        {
            double minValue = MinRangeValue;
            double maxValue = MaxRangeValue;
            var middleValue = ((double)minValue + maxValue) / 2;
            if (MinRangeValue < 0 && MaxRangeValue < 0)
            {
                minValue = MaxRangeValue;
                maxValue = MinRangeValue;
            }

            
             _pairMatrix[_currentMatrixIndex].SetFirstUnpopulatedElementInUpperTriangle(new FuzzyNumber<double>(new[]
             {
                minValue,
                middleValue,
                maxValue
             }));
             MinRangeValue = 1;
             MaxRangeValue = 3;

             if (_pairMatrix[_currentMatrixIndex].GetUnpopulatedIndex == null)
             {
                 _pairMatrix[_currentMatrixIndex].PopulateLowerTriangle();
                 ++_currentMatrixIndex;
             }

             if (_pairMatrix.Count <= _currentMatrixIndex)
             {
                 _provider.CurrentTask.PairMatrices = Convert.ToBase64String(BoxingExtension.Boxing(_pairMatrix));
                 _provider.SaveChanges();
                 Mediator.Notify("GoHome");
                 return;
             }

             if (_currentMatrixIndex > 0)
             {
                 var index = _pairMatrix[_currentMatrixIndex].GetUnpopulatedIndex;

                 var firstArgument = _provider.CurrentTask.Alternatives.ElementAt(index.Row).Name;
                 var secondArgument = _provider.CurrentTask.Alternatives.ElementAt(index.Column).Name;
                 var thirdArgument = _provider.CurrentTask.Criterias.ElementAt(_currentMatrixIndex - 1).Name;
                 LabelText = String.Format(_labelAlternativeTemplate, firstArgument, secondArgument, thirdArgument);
             }
             else
             {
                 var index = _pairMatrix[_currentMatrixIndex].GetUnpopulatedIndex;
                 var firstArgument = _provider.CurrentTask.Criterias.ElementAt(index.Row).Name;
                 var secondArgument = _provider.CurrentTask.Criterias.ElementAt(index.Column).Name;
                 LabelText = String.Format(_labelCriteriaTemplate, firstArgument, secondArgument);
             }
         }));

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

        public override void UpdateDataOnPage()
        {
            Initialize();
            var index = _pairMatrix[_currentMatrixIndex].GetUnpopulatedIndex;

            var firstArgument = _provider.CurrentTask.Criterias.ElementAt(index.Row).Name;
            var secondArgument = _provider.CurrentTask.Criterias.ElementAt(index.Column).Name;
            LabelText = string.Format(_labelCriteriaTemplate, firstArgument, secondArgument);
        }

        private void Initialize()
        {
            _pairMatrix = new List<PairMatrix<double>> { new PairMatrix<double>(_provider.CurrentTask.Criterias.Count, 1) };
            foreach (var criterias in _provider.CurrentTask.Criterias)
            {
                _pairMatrix.Add(new PairMatrix<double>(_provider.CurrentTask.Alternatives.Count, 1));
            }
        }

        private void InitializeDescription()
        {
            MarkDescriptions = new List<MarkDescription>
            {
                new MarkDescription
                {
                    Comment = "Two Alternativess are equally preferable in terms of purpose.",
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
                        "The expert’s experience allows one of the Alternativess to be considered a bit preferable to the other.",
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
                        "The expert’s experience allows one of the Alternativess to be considered clearly preferable to the other.",
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
                        "The expert’s experience allows one of the Alternativess to be considered much more preferable than the other: the domination of the Alternatives is confirmed by practice.",
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
                        "The obviousness of the overwhelming preference of one Alternatives over the other is indisputable confirmation",
                    Description = "Absolute preference",
                    ValueDegree = "9"
                }
            };
        }
    }
}