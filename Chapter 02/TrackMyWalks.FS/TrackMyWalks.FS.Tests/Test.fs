namespace TrackMyWalks.FS.Tests
open System
open NUnit.Framework

open TrackMyWalks.FS

[<TestFixture>]
module WalksPageViewModelTests =

    [<Test>]
    let ``Walk entries initialized correctly`` () =
        let m = new WalksPageViewModel()

        Assert.True(m.WalkEntries.Count = 2)


     

[<TestFixture>]
module WalkEntryViewModel =

    [<Test>]
    let ``Walk entry title set as expected`` () =
        let we = WalkEntryViewModel()

        let expected = "walk entry title"

        we.Title <- expected

        Assert.AreEqual(expected, we.Title)