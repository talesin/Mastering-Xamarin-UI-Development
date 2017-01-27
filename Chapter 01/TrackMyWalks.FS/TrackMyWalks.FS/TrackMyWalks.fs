﻿//
// TrackMyWalks.FS.fs
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

    let addHandler (event:'a -> IEvent<EventHandler,EventArgs>) fn (element:'a) =
        (event element).AddHandler(fun _ a -> fn a)
        element

    let addHandler' (event:'a -> IEvent<EventHandler<'b>,'b>) fn (element:'a) =
        (event element).AddHandler(fun _ a -> fn a)
        element

    let (|>->) element fn =
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
        Title: string
        Notes: string
        Latitude: double
        Longitude: double
        Kilometres: double
        Difficulty: string
        Distance: double
        ImageUrl: string
    }

type DistanceTravelledPage(walk: WalkEntry) as this =
    inherit ContentPage()

    do
        ()

type WalksTrailPage(walk: WalkEntry option) as this =
    inherit ContentPage()

    do
        this.Title <- "Walks Trail"

        let button =
            Button(
                BackgroundColor = Color.FromHex("#008080"),
                TextColor = Color.White,
                Text = "Begin this Trail")
            |> addHandler
                (fun x -> x.Clicked)
                (fun _ -> match walk with
                            | None -> ()
                            | Some x ->
                                this.Navigation.AsyncPush(DistanceTravelledPage(x))
                                this.Navigation.RemovePage(this))
                        
        ()

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
        |> addHandler (fun x -> x.Clicked) (fun _ -> this.Navigation.AsyncPopToRoot(true))
        |> this.ToolbarItems.Add


type WalksPage() as this =
    inherit ContentPage()

    do
        ToolbarItem(Text = "Add Walk")
        |> addHandler (fun x -> x.Clicked) (fun _ -> this.Navigation.AsyncPush(WalksEntryPage()))
        |> this.ToolbarItems.Add

        let walks = [{  Title  = "10 Mile Brook Trail, Margaret River"
                        Notes  = "The 10 Mile Brook Trail starts in the Rotary Park near Old Kate, a preserved steam engine at the northern edge of Margaret River. "
                        Latitude    = -33.9727604
                        Longitude   = 115.0861599
                        Kilometres  = 7.5
                        Difficulty  = "Medium"
                        Distance    = 0.0
                        ImageUrl    = "http://trailswa.com.au/media/cache/media/images/trails/_mid/FullSizeRender1_600_480_c1.jpg"};
                    
                    {   Title  = "Ancient Empire Walk, Valley of the Giants"
                        Notes  = "The Ancient Empire is a 450 metre walk trail that takes you around and through some of the giant tingle trees including the most popular of the gnarled veterans, known as Grandma Tingle."
                        Latitude  = -34.9749188
                        Longitude   = 117.3560796
                        Kilometres = 450.0
                        Distance   = 0.0
                        Difficulty = "Hard"
                        ImageUrl   = "http://trailswa.com.au/media/cache/media/images/trails/_mid/Ancient_Empire_534_480_c1.jpg"}]

        let tmpl =
            DataTemplate(typeof<ImageCell>)
            |>-> fun x -> x.SetBinding(TextCell.TextProperty, "Title")
            |>-> fun x -> x.SetBinding(TextCell.DetailProperty, "Notes")
            |>-> fun x -> x.SetBinding(ImageCell.ImageSourceProperty, "ImageUrl")


        let list =
            ListView(
                HasUnevenRows = true,
                ItemTemplate = tmpl,
                ItemsSource = walks,
                SeparatorColor = Color.FromHex("#ddd"))
            |> addHandler'
                (fun x -> x.ItemTapped)
                (fun a -> if a.Item <> null then this.Navigation.AsyncPush(WalksTrailPage(a.Item)))

        
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
            base.MainPage <- new NavigationPage(WalksPage(), Title = "Track My Walks")