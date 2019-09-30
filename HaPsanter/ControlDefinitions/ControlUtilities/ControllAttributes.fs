namespace ControlDefinitions.ControlUtilities

open System
open Xamarin.Essentials
open Xamarin.Forms
open Xamarin.Forms
open System.ComponentModel
open System.Windows

type ObjectToPassEventArgs(value : obj, nameWOwner : string[]) =
    member this.Value = value
    member this.nameWOwner = nameWOwner

type ControlAttributes(propertyName : string) =
    
    let mutable text = ""
    let mutable itemsSource = [|""|]
    let mutable color = Color.White
    let mutable tbForeground = Color.White
    let mutable isEnabled = false
    let mutable isVisible = false
    let mutable toolTip = ""
    let ev = new Event<_,_>()
    let mutable dataCntxtEv = new Event<ObjectToPassEventArgs>()
    let mutable videoSource = ""

    member private this.triggerEvents (result : bool) (value : obj) (propName : string)=
        
        None
        |> function
           | _ when result = false -> 
                
                dataCntxtEv.Trigger(ObjectToPassEventArgs(value, [|propertyName ; propName|]))

           | _ -> None |> ignore

    
    member this.Text 
        with get() = text 
        and set(value) =
            if (value <> text)
            then text <- value
                 dataCntxtEv.Trigger(ObjectToPassEventArgs(value, [|propertyName ; "Text"|]))
                 ev.Trigger(this, PropertyChangedEventArgs("Text"))

    member this.ItemsSource 
        with get() = itemsSource 
        and set(value) =
            if (value <> itemsSource)
            then itemsSource <- value
                 dataCntxtEv.Trigger(ObjectToPassEventArgs(value, [|propertyName ; "ItemsSource"|]))
                 ev.Trigger(this, PropertyChangedEventArgs("ItemsSource"))

    member this.Color 
        with get() = color 
        and set(value) =
            if (value <> color)
            then color <- value
                 dataCntxtEv.Trigger(ObjectToPassEventArgs(value, [|propertyName ; "Color"|]))
                 ev.Trigger(this, PropertyChangedEventArgs("Color"))

    member this.TbForeground 
        with get() = tbForeground 
        and set(value) =
            if (value <> tbForeground)
            then tbForeground <- value
                 dataCntxtEv.Trigger(ObjectToPassEventArgs(value, [|propertyName ; "TbForeground"|]))
                 ev.Trigger(this, PropertyChangedEventArgs("TbForeground"))

    member this.IsEnabled 
        with get() = isEnabled 
        and set(value) =
            if (value <> isEnabled)
            then isEnabled <- value
                 dataCntxtEv.Trigger(ObjectToPassEventArgs(value, [|propertyName ; "IsEnabled"|]))
                 ev.Trigger(this, PropertyChangedEventArgs("IsEnabled"))

    member this.IsVisible 
        with get() = isVisible 
        and set(value) =
            if (value <> isVisible)
            then isVisible <- value
                 dataCntxtEv.Trigger(ObjectToPassEventArgs(value, [|propertyName ; "IsVisible"|]))
                 ev.Trigger(this, PropertyChangedEventArgs("IsVisible"))

    member this.ToolTip 
        with get() = toolTip 
        and set(value) =
            if (value <> toolTip)
            then toolTip <- value
                 dataCntxtEv.Trigger(ObjectToPassEventArgs(value, [|propertyName ; "ToolTip"|]))
                 ev.Trigger(this, PropertyChangedEventArgs("ToolTip"))
            
    member this.VideoSource 
        with get() = videoSource 
        and set(value) =
            if (value <> videoSource)
            then videoSource <- value
                 dataCntxtEv.Trigger(ObjectToPassEventArgs(value, [|propertyName ; "VideoSource"|]))
                 ev.Trigger(this, PropertyChangedEventArgs("VideoSource"))

    interface INotifyPropertyChanged with
        [<CLIEvent>]
        member this.PropertyChanged = ev.Publish

    [<CLIEvent>]
    member this.UpdateDataContext = dataCntxtEv.Publish

