//
// TrackMyWalks.fs
//
// Author: Jeremy Clough <jeremy@talesin.net>
//
// Copyright (c) 2017 Jeremy Clough
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


[<AutoOpen>]
module FormsHelper =
    let inline add< ^b, ^a when ^a : (member Add: ^b -> unit)> (items:^b list) element =
        items
        |> List.iter (fun x -> ( ^a : (member Add: ^b -> unit) (element, x)))
        element

    let addChildren items (element:'a when 'a :> Layout<_>) =
        items
        |> List.iter element.Children.Add
        element

    let (|>|) element fn =
        fn element
        element

    let optcast<'a> o =
        try
            if o = null then None
            else Some (unbox<'a> o)
        with
        | _ -> None

    type INavigation with
        member this.AsyncPush(page: Page) =
            this.PushAsync(page) |> Async.AwaitTask |> Async.StartImmediate

        member this.AsyncPopToRoot(animated: bool) =
            this.PopToRootAsync(animated) |> Async.AwaitTask |> Async.StartImmediate


type WalkEntry = {
        Title:      string
        Notes:      string
        Latitude:   double
        Longitude:  double
        Kilometres: double
        Difficulty: string
        Distance:   double
        ImageUrl:   Uri
    }

type DistanceTravelledPage(walk: WalkEntry) as this =
    inherit ContentPage()

    let (views:View list) = [
            Maps.Map()
                |>| fun x -> x.Pins.Add(Pin(    Type = PinType.Place,
                                                Label = walk.Title,
                                                Position = Position(walk.Latitude, walk.Longitude)))
                |>| fun x -> x.MoveToRegion(MapSpan.FromCenterAndRadius(Position(walk.Latitude, walk.Longitude), Distance.FromKilometers(1.0)))

            Label(  FontSize = 18.0,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.Black,
                    Text = walk.Title)

            Label(  FontAttributes = FontAttributes.Bold,
                    FontSize = 20.0,
                    TextColor = Color.Black,
                    Text = "Distance Travelled",
                    HorizontalTextAlignment = TextAlignment.Center)

            Label(  FontAttributes = FontAttributes.Bold,
                    FontSize = 20.0,
                    TextColor = Color.Black,
                    Text = sprintf "%f km" walk.Distance,
                    HorizontalTextAlignment = TextAlignment.Center)

            Label(  FontAttributes = FontAttributes.Bold,
                    FontSize = 20.0,
                    TextColor = Color.Black,
                    Text = "Time Taken:",
                    HorizontalTextAlignment = TextAlignment.Center)

            Label(  FontAttributes = FontAttributes.Bold,
                    FontSize = 20.0,
                    TextColor = Color.Black,
                    Text = "0h 0m 0s",
                    HorizontalTextAlignment = TextAlignment.Center)

            Button( BackgroundColor = Color.FromHex("#008080"),
                    TextColor = Color.White,
                    Text = "End this Trail")
                    |>| fun x -> x.Clicked.AddHandler(fun _ _ -> this.Navigation.AsyncPopToRoot(true))
        ]

    do
        this.Title <- "Distance Travelled"

        this.Content <-
            ScrollView( Padding = Thickness 10.0,
                        Content =
                            (StackLayout(   Orientation = StackOrientation.Vertical,
                                            HorizontalOptions = LayoutOptions.FillAndExpand)
                                            |> addChildren views))

type WalksTrailPage(walk: WalkEntry option) as this =
    inherit ContentPage()

    let (views:View list) =
        match walk with
        | None -> []
        | Some w ->
            [   Image(  Aspect = Aspect.AspectFill,
                        Source = ImageSource.FromUri w.ImageUrl)

                Label(  FontSize = 28.0,
                        FontAttributes = FontAttributes.Bold,
                        TextColor = Color.Black,
                        Text = w.Title)

                Label(  FontAttributes = FontAttributes.Bold,
                        FontSize = 12.0,
                        TextColor = Color.Black,
                        Text = sprintf "Length: %f km" w.Kilometres)

                Label(  FontAttributes = FontAttributes.Bold,
                        FontSize = 12.0,
                        TextColor = Color.Black,
                        Text = sprintf "Difficulty: %s" w.Difficulty)

                Label(  FontSize = 11.0,
                        TextColor = Color.Black,
                        Text = w.Notes,
                        HorizontalOptions = LayoutOptions.FillAndExpand)

                Button( BackgroundColor = Color.FromHex("#008080"),
                        TextColor = Color.White,
                        Text = "Begin this Trail")
                        |>| fun x -> x.Clicked.AddHandler(fun _ _ ->
                            this.Navigation.AsyncPush(DistanceTravelledPage(w))
                            this.Navigation.RemovePage(this))]

    do
        this.Title <- "Walks Trail"

        this.Content <-
            ScrollView(
                Padding = Thickness 10.0,
                Content =
                    (StackLayout(
                        Orientation = StackOrientation.Vertical,
                        HorizontalOptions = LayoutOptions.FillAndExpand)
                        |> addChildren views))

    new(o: obj) =
        WalksTrailPage (optcast<WalkEntry> o)

type WalksEntryPage() as this =
    inherit ContentPage()

    let newWalkItem = ToolbarItem(Text = "Add Walk")

    do
        this.Title <- "Walks Page"

        let root = TableRoot()
                |> add [
                    (TableSection())
                    |> add [
                        (EntryCell(Label = "Title:", Placeholder = "Trail Title"))
                        (EntryCell(Label = "Notes:", Placeholder = "Description"))
                        (EntryCell(Label = "Latitude:", Placeholder = "Latitude", Keyboard = Keyboard.Numeric))
                        (EntryCell(Label = "Longitude:", Placeholder = "Longitude", Keyboard = Keyboard.Numeric))
                        (EntryCell(Label = "Kilometres", Placeholder = "Kilometres", Keyboard = Keyboard.Numeric))
                        (EntryCell(Label = "Diffulty Level:", Placeholder = "Walk Diffculty Title"))
                        (EntryCell(Label = "ImageUrl:", Placeholder = "Image URL"))]]

        this.Content <-
            TableView(
                Intent = TableIntent.Form,
                Root = root)


        ToolbarItem(Text = "Save")
        |>| fun x -> x.Clicked.AddHandler(fun _ _ -> this.Navigation.AsyncPopToRoot(true))
        |> this.ToolbarItems.Add


type WalksPage() as this =
    inherit ContentPage()

    do
        ToolbarItem(Text = "Add Walk")
        |>| fun x -> x.Clicked.AddHandler(fun _ _ -> this.Navigation.AsyncPush(WalksEntryPage()))
        |> this.ToolbarItems.Add

        let walks = [{  Title  = "10 Mile Brook Trail, Margaret River"
                        Notes  = "The 10 Mile Brook Trail starts in the Rotary Park near Old Kate, a preserved steam engine at the northern edge of Margaret River. "
                        Latitude    = -33.9727604
                        Longitude   = 115.0861599
                        Kilometres  = 7.5
                        Difficulty  = "Medium"
                        Distance    = 0.0
                        ImageUrl    = Uri "http://trailswa.com.au/media/cache/media/images/trails/_mid/FullSizeRender1_600_480_c1.jpg"};
                    
                    {   Title  = "Ancient Empire Walk, Valley of the Giants"
                        Notes  = "The Ancient Empire is a 450 metre walk trail that takes you around and through some of the giant tingle trees including the most popular of the gnarled veterans, known as Grandma Tingle."
                        Latitude  = -34.9749188
                        Longitude   = 117.3560796
                        Kilometres = 450.0
                        Distance   = 0.0
                        Difficulty = "Hard"
                        ImageUrl   = Uri "http://trailswa.com.au/media/cache/media/images/trails/_mid/Ancient_Empire_534_480_c1.jpg"}]

        let tmpl =
            DataTemplate(typeof<ImageCell>)
            |>| fun x -> x.SetBinding(TextCell.TextProperty, "Title")
            |>| fun x -> x.SetBinding(TextCell.DetailProperty, "Notes")
            |>| fun x -> x.SetBinding(ImageCell.ImageSourceProperty, "ImageUrl")


        let list =
            ListView(
                HasUnevenRows = true,
                ItemTemplate = tmpl,
                ItemsSource = walks,
                SeparatorColor = Color.FromHex("#ddd"))
            |>| fun x -> x.ItemTapped.AddHandler(fun _ a -> if a.Item <> null then this.Navigation.AsyncPush(WalksTrailPage(a.Item)))

        
        this.Content <- list



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