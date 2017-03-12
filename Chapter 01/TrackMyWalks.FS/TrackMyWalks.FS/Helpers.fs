//
// Helpers.fs
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

open Xamarin.Forms
open System
open System.ComponentModel
open System.Runtime.CompilerServices;

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