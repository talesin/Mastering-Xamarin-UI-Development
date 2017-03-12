//
// TrackMyWalks.FS.fs
//
// Author: Jeremy Clough <jeremy@talesin.net>
//
// Copyright (c) 2017 Jeremy Clough
//
// You may not use, modify or distribute this file, or any part of it, without prior consent and offers of beer.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

namespace TrackMyWalks.FS

open System
open System.Threading.Tasks
open Xamarin.Forms
open Xamarin.Forms.Maps



type DistanceTravelledPage(walk: WalkEntry) as this =
    inherit ContentPage()

    do
        this.BindingContext <- DistTravelledViewModel(walk)

        let views: View list = [
                Maps.Map()
                |>| fun x -> x.Pins.Add(Pin(    Type = PinType.Place,
                                                Label = walk.Title,
                                                Position = Position(this.ViewModel.WalkEntry.Latitude, this.ViewModel.WalkEntry.Longitude)))
                |>| fun x -> x.MoveToRegion(MapSpan.FromCenterAndRadius(Position(this.ViewModel.WalkEntry.Latitude, this.ViewModel.WalkEntry.Longitude), Distance.FromKilometers(1.0)))

                Label(  FontSize = 18.0,
                        FontAttributes = FontAttributes.Bold,
                        TextColor = Color.Black)
                |>| fun x -> x.SetBinding(Label.TextProperty, "WalkEntry.Title")

                Label(  FontAttributes = FontAttributes.Bold,
                        FontSize = 20.0,
                        TextColor = Color.Black,
                        Text = "Distance Travelled",
                        HorizontalTextAlignment = TextAlignment.Center)

                Label(  FontAttributes = FontAttributes.Bold,
                        FontSize = 20.0,
                        TextColor = Color.Black,
                        HorizontalTextAlignment = TextAlignment.Center)
                |>| fun x -> x.SetBinding(Label.TextProperty, "Travelled", BindingMode.Default, stringFormat = "Distance Travelled: {0} km")

                Label(  FontAttributes = FontAttributes.Bold,
                        FontSize = 20.0,
                        TextColor = Color.Black,
                        Text = "Time Taken:",
                        HorizontalTextAlignment = TextAlignment.Center)

                Label(  FontAttributes = FontAttributes.Bold,
                        FontSize = 20.0,
                        TextColor = Color.Black,
                        HorizontalTextAlignment = TextAlignment.Center)
                |>| fun x -> x.SetBinding(Label.TextProperty, "TimeTaken", BindingMode.Default, stringFormat = "Time Taken: {0}")

                Button( BackgroundColor = Color.FromHex("#008080"),
                        TextColor = Color.White,
                        Text = "End this Trail")
                |>| fun x -> x.Clicked.AddHandler(fun _ _ -> this.Navigation.AsyncPopToRoot(true))
            ]

        this.Title <- "Distance Travelled"

        this.Content <-
            ScrollView( Padding = Thickness 10.0,
                        Content =
                            (StackLayout(   Orientation = StackOrientation.Vertical,
                                            HorizontalOptions = LayoutOptions.FillAndExpand)
                                            |> addChildren (views)))


    member this.ViewModel: DistTravelledViewModel = this.BindingContext :?> DistTravelledViewModel



type WalkTrailPage(walk: WalkEntry) as this =
    inherit ContentPage()

    do
        this.BindingContext <- WalksTrailViewModel(walk)

        let (views:View list) =
            [   Image(Aspect = Aspect.AspectFill)
                |>| fun x -> x.SetBinding(Image.SourceProperty, "WalkEntry.ImageUrl")

                Label(  FontSize = 28.0,
                        FontAttributes = FontAttributes.Bold,
                        TextColor = Color.Black)
                |>| fun x -> x.SetBinding(Label.TextProperty, "WalkEntry.Title")

                Label(  FontAttributes = FontAttributes.Bold,
                        FontSize = 12.0,
                        TextColor = Color.Black)
                |>| fun x -> x.SetBinding(Label.TextProperty, "WalkEntry.Kilometres", stringFormat = "Length: {0} km")

                Label(  FontAttributes = FontAttributes.Bold,
                        FontSize = 12.0,
                        TextColor = Color.Black)
                |>| fun x -> x.SetBinding(Label.TextProperty, "WalkEntry.Difficulty", stringFormat = "Difficulty: {0}")

                Label(  FontSize = 11.0,
                        TextColor = Color.Black,
                        HorizontalOptions = LayoutOptions.FillAndExpand)
                |>| fun x -> x.SetBinding(Label.TextProperty, "WalkEntry.Notes")

                Button( BackgroundColor = Color.FromHex("#008080"),
                        TextColor = Color.White,
                        Text = "Begin this Trail")
                |>| fun x -> x.Clicked.AddHandler(fun _ _ ->
                    this.Navigation.AsyncPush(DistanceTravelledPage(walk))
                    this.Navigation.RemovePage(this))]


        this.Title <- "Walks Trail"

        this.Content <-
            ScrollView(
                Padding = Thickness 10.0,
                Content =
                    (StackLayout(
                        Orientation = StackOrientation.Vertical,
                        HorizontalOptions = LayoutOptions.FillAndExpand)
                        |> addChildren views))

    member val ViewModel = this.BindingContext :?> WalksTrailViewModel



type WalksEntryPage() as this =
    inherit ContentPage()

    let newWalkItem = ToolbarItem(Text = "Add Walk")

    do
        this.BindingContext <- WalkEntryViewModel()

        this.Title <- "Walks Page"

        let root = TableRoot()
                |> add [
                    (TableSection())
                    |> add [
                        EntryCell(Label = "Title:", Placeholder = "Trail Title")
                        |>| fun x -> x.SetBinding(EntryCell.TextProperty, "Title", BindingMode.TwoWay)

                        EntryCell(Label = "Notes:", Placeholder = "Description")
                        |>| fun x -> x.SetBinding(EntryCell.TextProperty, "Notes", BindingMode.TwoWay)

                        EntryCell(Label = "Latitude:", Placeholder = "Latitude", Keyboard = Keyboard.Numeric)
                        |>| fun x -> x.SetBinding(EntryCell.TextProperty, "Latitude", BindingMode.TwoWay)

                        EntryCell(Label = "Longitude:", Placeholder = "Longitude", Keyboard = Keyboard.Numeric)
                        |>| fun x -> x.SetBinding(EntryCell.TextProperty, "Longitude", BindingMode.TwoWay)

                        EntryCell(Label = "Kilometres", Placeholder = "Kilometres", Keyboard = Keyboard.Numeric)
                        |>| fun x -> x.SetBinding(EntryCell.TextProperty, "Kilometres", BindingMode.TwoWay)

                        EntryCell(Label = "Diffulty Level:", Placeholder = "Walk Difficulty Title")
                        |>| fun x -> x.SetBinding(EntryCell.TextProperty, "Difficulty", BindingMode.TwoWay)

                        EntryCell(Label = "ImageUrl:", Placeholder = "Image URL")
                        |>| fun x -> x.SetBinding(EntryCell.TextProperty, "ImageUrl", BindingMode.TwoWay)
                    ]
                ]

        this.Content <-
            TableView(
                Intent = TableIntent.Form,
                Root = root)

        ToolbarItem(Text = "Save")
        |>| fun x -> x.SetBinding(MenuItem.CommandProperty, "SaveCommand")
        |>| fun x -> x.Clicked.AddHandler(fun _ _ -> this.Navigation.AsyncPopToRoot(true))
        |> this.ToolbarItems.Add

    member val ViewModel = this.BindingContext :?> WalkEntryViewModel


type WalksPage() as this =
    inherit ContentPage()

    do
        this.BindingContext <- WalksPageViewModel()

        ToolbarItem(Text = "Add Walk")
        |>| fun x -> x.Clicked.AddHandler(fun _ _ -> this.Navigation.AsyncPush(WalksEntryPage()))
        |> this.ToolbarItems.Add

 
        let tmpl =
            DataTemplate(typeof<ImageCell>)
            |>| fun x -> x.SetBinding(TextCell.TextProperty, "Title")
            |>| fun x -> x.SetBinding(TextCell.DetailProperty, "Notes")
            |>| fun x -> x.SetBinding(ImageCell.ImageSourceProperty, "ImageUrl")


        let list =
            ListView(
                HasUnevenRows = true,
                ItemTemplate = tmpl,
                SeparatorColor = Color.FromHex("#ddd"))
            |>| fun x -> x.SetBinding(ItemsView<Cell>.ItemsSourceProperty, "WalkEntries")
            |>| fun x -> x.ItemTapped.AddHandler(fun _ a ->
                    match optcast<WalkEntry> a.Item with
                    | Some w -> this.Navigation.AsyncPush(WalkTrailPage(w))
                    | None -> ())

        
        this.Content <- list

    member val ViewModel = this.BindingContext :?> WalksPageViewModel


type SplashPage() as this =
    inherit ContentPage()

    let image =
        Image(
            Source = ImageSource.FromFile("icon.png"),
            Aspect = Aspect.AspectFill)

    let splashLayout =
        AbsoluteLayout(HeightRequest = 600.0)
        |> addChildren [image]

    do
        AbsoluteLayout.SetLayoutFlags(image, AbsoluteLayoutFlags.All)
        AbsoluteLayout.SetLayoutBounds(image, Rectangle(0.0, 0.0, 1.0, 1.0))

        this.Content <-
            (StackLayout()
            |> addChildren [splashLayout])


    override this.OnAppearing() =
        Async.Sleep(3000) |> Async.StartImmediate

        let page = NavigationPage(WalksPage(Title = "Track My Walks"))
        Application.Current.MainPage <- page




type App() =
    inherit Application()

    do
        if Device.OS = TargetPlatform.Android then
            base.MainPage <- SplashPage()
        else
            base.MainPage <- new NavigationPage(WalksPage(Title = "Track My Walks (F#)"))