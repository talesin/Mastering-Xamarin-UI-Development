//
// Models.fs
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

open Xamarin.Forms
open System
open System.ComponentModel
open System.Runtime.CompilerServices
open System.Collections.ObjectModel

type WalkBaseViewModel () =
    let propertyChanged = Event<PropertyChangedEventHandler, PropertyChangedEventArgs>()

    [<CLIEvent>]
    member this.PropertyChanged = propertyChanged.Publish

    interface INotifyPropertyChanged with
        member this.add_PropertyChanged(handler) = propertyChanged.Publish.AddHandler(handler)
        member this.remove_PropertyChanged(handler) = propertyChanged.Publish.RemoveHandler(handler)

    member this.OnPropertyChanged(name) =
        propertyChanged.Trigger(this, PropertyChangedEventArgs(name))

    member this.SetField(field: 'a option byref, x: 'a, name) =
        field <- optcast x
        this.OnPropertyChanged(name)


type WalkEntryViewModel () as this =
    inherit WalkBaseViewModel ()

    let mutable title: string option = None
    let mutable notes: string option = None
    let mutable latitude: double option = None
    let mutable longitude: double option = None
    let mutable kilometers: double option = None
    let mutable difficulty: string option = None
    let mutable distance: double option = None
    let mutable imageUrl: Uri option = None

    let mutable saveCommand: Command option = None

    do
        this.Title <- "New Walk"
        this.Difficulty <- "Easy"
        this.Distance <- 1.0


    member private this.Save () =
        let item = {
                Title       = this.Title
                Notes       = this.Notes
                Latitude    = this.Latitude
                Longitude   = this.Longitude
                Kilometres  = this.Kilometers
                Difficulty  = this.Difficulty
                Distance    = this.Distance
                ImageUrl    = this.ImageUrl
            }
        ()

    member private this.Validate () =
        not <| String.IsNullOrWhiteSpace(this.Title)


    member this.SaveCommand =
        match saveCommand with
        | Some x -> x
        | None ->
            Command(this.Save, this.Validate)
            |>| fun x -> saveCommand <- Some x

    member this.Title
        with get() = getOrDefault title
        and set(x) = this.SetField(&title, x, "Title")

    member this.Notes
        with get() = getOrDefault notes
        and set(x) = this.SetField(&notes, x, "Notes")

    member this.Latitude
        with get() = getOrDefault latitude
        and set(x) = this.SetField(&latitude, x, "Latitude")

    member this.Longitude
        with get() = getOrDefault longitude
        and set(x) = this.SetField(&longitude, x, "Longitude")

    member this.Kilometers
        with get() = getOrDefault kilometers
        and set(x) = this.SetField(&kilometers, x, "Kilometers")

    member this.Difficulty
        with get() = getOrDefault difficulty
        and set(x) = this.SetField(&difficulty, x, "Difficulty")

    member this.Distance
        with get() = getOrDefault distance
        and set(x) = this.SetField(&distance, x, "Distance")

    member this.ImageUrl
        with get() = getOrDefault imageUrl
        and set(x) = this.SetField(&imageUrl, x, "ImageUrl")


type WalksPageViewModel () =
    inherit WalkBaseViewModel ()

    let mutable entries: ObservableCollection<WalkEntry> option =
        Some <| ObservableCollection<WalkEntry>(
            [{  Title  = "10 Mile Brook Trail, Margaret River"
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
                ImageUrl   = Uri "http://trailswa.com.au/media/cache/media/images/trails/_mid/Ancient_Empire_534_480_c1.jpg"}] )

    member this.WalkEntries
        with get() = getOrDefault entries
        and set(x) = this.SetField(&entries, x, "WalkEntries")


type WalksTrailViewModel (entry') =
    inherit WalkBaseViewModel ()

    let mutable entry: WalkEntry option = Some entry'

    member this.WalkEntry
        with get() = getOrDefault entry
        and set(x) = this.SetField(&entry, x, "WalkEntry")

type DistTravelledViewModel (entry') =
    inherit WalkBaseViewModel ()

    let mutable entry: WalkEntry option = Some entry'
    let mutable travelled: double option = None
    let mutable timeTaken: TimeSpan option = None

    member this.WalkEntry
        with get() = getOrDefault entry
        and set(x) = this.SetField(&entry, x, "WalkEntry")

    member this.Travelled
        with get() = getOrDefault travelled
        and set(x) = this.SetField(&travelled, x, "Travelled")

    member this.TimeTaken
        with get() = getOrDefault timeTaken
        and set(x) = this.SetField(&timeTaken, x, "TimeTaken")
