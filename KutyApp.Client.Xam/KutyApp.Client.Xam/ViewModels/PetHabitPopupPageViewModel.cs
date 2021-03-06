﻿using KutyApp.Client.Services.ClientConsumer.Dtos;
using KutyApp.Client.Services.ServiceCollector.Interfaces;
using KutyApp.Client.Xam.Navigation;
using Prism.Navigation;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace KutyApp.Client.Xam.ViewModels
{
    public class PetHabitPopupPageViewModel : ViewModelBase
    {
        private IKutyAppClientContext KutyAppClientContext { get; }

        public PetHabitPopupPageViewModel(INavigationService navigationService, IKutyAppClientContext kutyAppClientContext)
            : base(navigationService)
        {
            KutyAppClientContext = kutyAppClientContext;
        }

        private HabitDto originalHabit;
        private string habitTitle;
        private string description;
        private TimeSpan startTime;
        private TimeSpan endTime;
        private double amount;
        private string unit;
        private bool inputTransparent;
        private bool allowDelete;

        private ICommand editCommand;
        private ICommand saveCommand;
        private ICommand deleteCommand;

        public bool AllowDelete
        {
            get => allowDelete;
            set => SetProperty(ref allowDelete, value);
        }
        public string HabitTitle
        {
            get => habitTitle;
            set => SetProperty(ref habitTitle, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public TimeSpan StartTime
        {
            get => startTime;
            set => SetProperty(ref startTime, value);
        }

        public TimeSpan EndTime
        {
            get => endTime;
            set => SetProperty(ref endTime, value);
        }

        public double Amount
        {
            get => amount;
            set => SetProperty(ref amount, value);
        }

        public string Unit
        {
            get => unit;
            set => SetProperty(ref unit, value);
        }

        public bool InputTransparent
        {
            get => inputTransparent;
            set { SetProperty(ref inputTransparent, value); /*RaisePropertyChanged(nameof(SaveOrEdit)); */}
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey(nameof(NavigationHelper)))
            {
                var param = (NavigationHelper)parameters[nameof(NavigationHelper)];

                switch (param.Action)
                {
                    case Common.Enums.NavigationAction.Add:
                        InputTransparent = false;
                        AllowDelete = false;
                        break;
                    case Common.Enums.NavigationAction.Edit:
                        originalHabit = param.Parameter as HabitDto;
                        AllowDelete = true;
                        SetUIValues(originalHabit);
                        InputTransparent = true;
                        break;
                    default:
                        InputTransparent = true;
                        break;
                }

                //originalHabit = param.Parameter as HabitDto;
                //SetUIValues(originalHabit);
            }

            base.OnNavigatedTo(parameters);
        }

        public ICommand EditCommand =>
            editCommand ?? (editCommand = new Command(
                () => InputTransparent = !InputTransparent));

        public ICommand SaveCommand =>
            saveCommand ?? (saveCommand = new Command(
                async () =>
                {
                    if (originalHabit == null)
                        await NavigationService.ClearPopupStackAsync(new NavigationParameters
                        {
                            {
                                nameof(NavigationHelper), new NavigationHelper
                                {
                                    Action = Common.Enums.NavigationAction.Add,
                                    Parameter = new HabitDto
                                        {
                                           Title = HabitTitle, Description = Description, StartTime = StartTime, EndTime = EndTime, Amount = Amount, Unit = Unit
                                        },
                                    ParameterTypeName = nameof(HabitDto)
                                }
                            }
                        });

                    else
                        await NavigationService.ClearPopupStackAsync(new NavigationParameters
                        {
                            {
                                nameof(NavigationHelper), new NavigationHelper
                                {
                                    Action = Common.Enums.NavigationAction.Edit,
                                    ParameterTypeName = nameof(HabitDto),
                                    Parameter = new HabitDto
                                    {
                                        Id = originalHabit.Id, PetId = originalHabit.PetId, Title = HabitTitle, Description = Description, StartTime = StartTime, EndTime = EndTime, Amount = Amount, Unit = Unit
                                    }
                                }
                            }
                        });
                }
                   
                ));

        public ICommand DeleteCommand =>
            deleteCommand ?? (deleteCommand = new Command(
                async () => await NavigationService.ClearPopupStackAsync(new NavigationParameters
                {
                    {
                        nameof(NavigationHelper), new NavigationHelper
                        {
                            Action = Common.Enums.NavigationAction.Delete,
                            ParameterTypeName = nameof(HabitDto),
                            Parameter = new HabitDto { Id = originalHabit.Id },
                        }
                    }
                })));

        private void SetUIValues(HabitDto habit)
        {
            if (habit == null)
                return;

            HabitTitle = habit.Title;
            Description = habit.Description;
            StartTime = habit.StartTime ?? new TimeSpan();
            EndTime = habit.EndTime ?? new TimeSpan();
            Amount = habit.Amount;
            Unit = habit.Unit;
        }
    }
}
