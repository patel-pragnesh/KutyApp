﻿using KutyApp.Client.Common.Constants;
using KutyApp.Client.Services.ClientConsumer.Dtos;
using KutyApp.Client.Services.ClientConsumer.Enums;
using KutyApp.Client.Services.ClientConsumer.Interfaces;
using KutyApp.Client.Services.LocalRepository.Interfaces;
using KutyApp.Client.Services.ServiceCollector.Interfaces;
using KutyApp.Client.Xam.Navigation;
using KutyApp.Client.Xam.Views;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace KutyApp.Client.Xam.ViewModels
{
    public class PetDetailPageViewModel : ViewModelBase
    {
        private IPetRepository PetRepository { get; }
        private IMediaManager MediaManager { get; }
        private IEnvironmentApiService EnvironmentApiService { get; }
        private IKutyAppClientContext KutyAppClientContext { get; }
        private PetDto Pet { get; set; }

        public PetDetailPageViewModel(INavigationService navigationService, IPetRepository petRepository, IMediaManager mediaManager, IEnvironmentApiService environmentApiService, IKutyAppClientContext kutyAppClientContext) : base(navigationService)
        {
            PetRepository = petRepository;
            MediaManager = mediaManager;
            EnvironmentApiService = environmentApiService;
            KutyAppClientContext = kutyAppClientContext;

            Title = "petdetail";
            SwipeEnabled = true;
            CarouselViews = new ObservableCollection<View>(new List<View> { new PetDetailView(), new PetHabitsView()});
        }

        private string name;
        private string chipNumber;
        private Gender gender;
        public IEnumerable<Gender> GenderValues => Enum.GetValues(typeof(Gender)).Cast<Gender>();
        private DateTime birthDate;
        private int? age;
        private int id;
        private string imagePath;
        private ImageSource petImageSource;
        private ImageSource petBigImageSource;
        private bool isOnline;
        private ObservableCollection<View> carouselViews;
        private ObservableCollection<HabitDto> habits = new ObservableCollection<HabitDto>();
        private bool swipeEnabled;

        private ICommand addOrEditPetCommand;
        private ICommand deletePetCommand;
        private ICommand takePhotoCommand;
        private ICommand pickPhotoCommand;
        private ICommand newHabitCommand;

        #region Public Properties
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        public string ChipNumber
        {
            get { return chipNumber; }
            set { SetProperty(ref chipNumber, value); }
        }

        public Gender Gender
        {
            get { return gender; }
            set { SetProperty(ref gender, value); }
        }

        public DateTime BirthDate
        {
            get { return birthDate; }
            set { SetProperty(ref birthDate, value); }
        }

        public int? Age
        {
            get { return age; }
            set { SetProperty(ref age, value); }
        }

        public int Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        public string ImagePath
        {
            get { return imagePath; }
            set { SetProperty(ref imagePath, value); }
        }

        public ImageSource PetImageSource
        {
            get => petImageSource ?? ImageSource.FromUri(new Uri("http://via.placeholder.com/600x500?text=Your+Pet"));
            set => SetProperty(ref petImageSource, value);
        }

        public ImageSource PetBigImageSource
        {
            get => petBigImageSource ?? ImageSource.FromUri(new Uri("http://via.placeholder.com/600x500?text=Your+Pet"));
            set => SetProperty(ref petBigImageSource, value);
        }

        public bool IsOnline
        {
            get => isOnline;
            set => SetProperty(ref isOnline, value);
        }
        
        public ObservableCollection<View> CarouselViews
        {
            get => carouselViews;
            set => SetProperty(ref carouselViews, value);
        }

        public ObservableCollection<HabitDto> Habits
        {
            get => habits;
            set => SetProperty(ref habits, value);
        }

        public bool SwipeEnabled
        {
            get => swipeEnabled;
            set => SetProperty(ref swipeEnabled, value);
        }
        #endregion

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.ContainsKey(ParameterKeys.PetId))
                await LoadPet((int)parameters[ParameterKeys.PetId]);

            IsOnline = KutyAppClientContext.IsLoggedIn;
        }

        private async Task LoadPet(int id)
        {
            IsBusy = true;

            Pet = KutyAppClientContext.IsLoggedIn ? await EnvironmentApiService.GetPetAsync(id) : await PetRepository.GetDetailedPetByIdAsync(id);
            Name = Pet.Name;
            ChipNumber = Pet.ChipNumber;
            Gender = Pet.Gender;
            BirthDate = Pet.BirthDate ?? DateTime.MinValue;
            Age = Pet.Age;
            Id = Pet.Id;
            ImagePath = Pet.ImagePath;

            if (!string.IsNullOrEmpty(ImagePath))
                await LoadImage();

            Habits = new ObservableCollection<HabitDto>(Pet.Habits);

            IsBusy = false;
        }

        private async Task LoadImage(bool pickedPhoto = false)
        {
            try
            {
                if (KutyAppClientContext.IsLoggedIn && !pickedPhoto)
                {
                    var content = await EnvironmentApiService.GetImageAsync(ImagePath);
                    var pictureArray = await content.ReadAsByteArrayAsync();
                    var original = new MemoryStream(pictureArray.ToArray());
                    var copy = new MemoryStream(pictureArray.ToArray());

                    PetImageSource = ImageSource.FromStream(() => original);
                    PetBigImageSource = ImageSource.FromStream(() => copy);
                }
                else
                    PetImageSource = ImageSource.FromFile(ImagePath);
            }
            catch (Exception e)
            {

            }
        }

        public ICommand AddOrEditPetCommand =>
                 addOrEditPetCommand ?? (addOrEditPetCommand = new Command(
                    async () => await AddOrEditPet()
                    ));

        private async Task AddOrEditPet()
        {
            try
            {
            //TODO: habit, medicaltreatment
            var dto = new AddOrEditPetDto
            {
                Id = Pet?.Id,
                BirthDate = BirthDate,
                ChipNumber = ChipNumber,
                //Color = Color,
                Gender = Gender,
                Name = Name,
                //Weight = Weight,
                Habits = new List<AddOrEditHabitDto>(),
                MedicalTreatments = new List<AddOrEditMedicalTreatmentDto>()
            };

            if(ImagePath != Pet?.ImagePath)
            {
                using (FileStream file = new FileStream(ImagePath, FileMode.Open))
                {
                    var res = await EnvironmentApiService.AddOrEditComplexPetAsync(dto,  new Refit.StreamPart(file, Path.GetFileName(ImagePath)));
                    if (res.Id != 0)
                        await NavigationService.NavigateAsync(nameof(Views.PetsPage));
                }
            }
            else
            {
                var res = await EnvironmentApiService.AddOrEditPetAsync(dto);
                if (res.Id != 0)
                    await NavigationService.NavigateAsync(nameof(Views.PetsPage));
            }
            }
            catch (Exception e)
            {

            }
        }

        public ICommand DeletePetCommand =>
                deletePetCommand ?? (deletePetCommand = new Command(
                    async () =>
                    {
                        //TODO: tenyleg akarja e torolni dialogservice
                        await EnvironmentApiService.DeletePetAsync(id);
                        await NavigationService.NavigateAsync(nameof(Views.PetsPage));
                    }));


        public ICommand TakePhotoCommand =>
            takePhotoCommand ?? (takePhotoCommand = new Command(
                async () => await TakePhotoAsync()));

        public ICommand PickPhotoCommand =>
            pickPhotoCommand ?? (pickPhotoCommand = new Command(
                async () => await PickPhotoAsync()));

        public ICommand NewHabitCommand =>
            newHabitCommand ?? (newHabitCommand = new Command(
                async () => await NavigationService.NavigateAsync(nameof(PetHabitPopupPage))));

        private async Task PickPhotoAsync()
        {
            var file = await MediaManager.PickPhotoAsync();
            if(file != null)
            {
                //TODO: save picked image into private library
                ImagePath = file.Path;
                file.Dispose();

                await LoadImage(true);
            }
        }

        private async Task TakePhotoAsync()
        {
            var file = await MediaManager.TakePhotoAsync();

            if (file != null)
            {
                ImagePath = file.Path;
                file.Dispose();

                await LoadImage(true);
            }
        }
    }
}
