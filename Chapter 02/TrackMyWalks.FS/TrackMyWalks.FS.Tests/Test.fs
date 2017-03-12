namespace TrackMyWalks.FS.Tests
open System
open NUnit.Framework

open TrackMyWalks.FS

[<TestFixture>]
module WalksPageViewModelTests =

    [<Test>]
    let ``Walk entries initialized correctly`` () =
        let m = new WalksPageViewModel()

        Assert.True(m.Entries.Count = 2)

