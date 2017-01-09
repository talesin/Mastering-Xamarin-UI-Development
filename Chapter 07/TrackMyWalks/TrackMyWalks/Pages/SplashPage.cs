﻿//
//  SplashPage.cs
//  TrackMyWalks
//
//  Created by Steven F. Daniel on 04/08/2016.
//  Copyright © 2016 GENIESOFT STUDIOS. All rights reserved.
//
using System.Threading.Tasks;
using TrackMyWalks.Services;
using TrackMyWalks.ViewModels;
using Xamarin.Forms;

namespace TrackMyWalks
{
	public class SplashPage : ContentPage
	{
		public SplashPage()
		{
			AbsoluteLayout splashLayout = new AbsoluteLayout
			{
				HeightRequest = 600
			};

			var image = new Image()
			{
				Source = ImageSource.FromFile("icon.png"),
				Aspect = Aspect.AspectFill,
			};

			AbsoluteLayout.SetLayoutFlags(image, AbsoluteLayoutFlags.All);
			AbsoluteLayout.SetLayoutBounds(image, new Rectangle(0f, 0f, 1f, 1f));

			splashLayout.Children.Add(image);

			Content = new StackLayout()
			{
				Children = { splashLayout }
			};
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			// Delay for a few seconds on the splash screen
			await Task.Delay(3000);

			// Instantiate a NavigationPage with the MainPage
			var navPage = new NavigationPage(new WalksPage()
			{
				Title = "Track My Walks - Android"
			});

			navPage.BarBackgroundColor = Color.FromHex("#4C5678");
			navPage.BarTextColor = Color.White;

			// Declare our DependencyService Interface
			var navService = DependencyService.Get<IWalkNavService>() as WalkNavService;
			navService.navigation = navPage.Navigation;

			// Register our View Model Mappings between our ViewModels and Views (Pages)
			navService.RegisterViewMapping(typeof(WalksPageViewModel), typeof(WalksPage));
			navService.RegisterViewMapping(typeof(WalkEntryViewModel), typeof(WalkEntryPage));
			navService.RegisterViewMapping(typeof(WalksTrailViewModel), typeof(WalkTrailPage));
			navService.RegisterViewMapping(typeof(DistTravelledViewModel), typeof(DistanceTravelledPage));

			// Set the MainPage to be our Walks Navigation Page
			Application.Current.MainPage = navPage;
		}
	}
}