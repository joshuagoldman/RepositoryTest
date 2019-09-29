namespace HaPsanter.Fsharp

open Xamarin.Forms
open HaPsanter.Fsharp.Definitions
open Hapsanter.Fsharp.ControlUtilities
open Xamarin.Forms.Xaml
open System.ComponentModel

[<DesignTimeVisible(false)>]
type GameStart() =
    inherit ContentPage()

    let mutable controlBase = new ControlBase()

    member this.ControlBase
        with get() = controlBase
        and set(value) =
            if value <> controlBase then controlBase <- value

    member this.ListenToEvents =
        
        let startupButton = base.FindByName<Button>("StartAppButton")

        startupButton.Clicked.Add(fun _ -> this.OnAppStartButtonClicked)
    
    member this.OnAppStartButtonClicked =
        "" |> ignore