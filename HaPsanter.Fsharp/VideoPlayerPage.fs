namespace HaPsanter.Fsharp

open Xamarin.Forms
open ControlDefinitions.ControlUtilities
open Xamarin.Forms.Xaml
open System.ComponentModel

[<DesignTimeVisible(false)>]
type VideoPlayerPage() =
    inherit ContentView()

    let mutable controlBase = new ControlBase()

    do base.BindingContext <- controlBase.Sender

    member this.ControlBase
        with get() = controlBase
        and set(value) =
            if value <> controlBase then controlBase <- value

    member this.ListenToEvents =
        
        let startupButton = base.FindByName<Button>("StartAppButton")

        startupButton.Clicked.Add(fun _ -> this.OnAppStartButtonClicked)

    member this.AllOtherPages = 
        
        seq[
            this :> obj ;
            new VideoPlayerPage() :> obj
        ]
    
    member this.OnAppStartButtonClicked =
        "" |> ignore
