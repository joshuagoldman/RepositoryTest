namespace EricssonSupportAssistance

open System.Windows.Media
open System.Windows.Controls
open System
open System.Xaml
open System.ComponentModel
open System.Windows

type ObjectToPassEventArgs(value : obj, nameWOwner : string[]) =
    member this.Value = value
    member this.nameWOwner = nameWOwner

type ControlAtributes(propertyName : string) =
    
    let mutable text = ""
    let mutable itemsSource = [|""|]
    let mutable color = Colors.White
    let mutable tbForeground = Brushes.Black
    let mutable datacontext = new obj()
    let mutable isEnabled = false
    let mutable visibility = Visibility.Hidden
    let ev = new Event<_,_>()
    let mutable dataCntxtEv = new Event<ObjectToPassEventArgs>()

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

    member this.DataContext 
        with get() = datacontext 
        and set(value) =
            if (value <> datacontext)
            then datacontext <- value
                 dataCntxtEv.Trigger(ObjectToPassEventArgs(value, [|propertyName ; "DataContext"|]))
                 ev.Trigger(this, PropertyChangedEventArgs("DataContext"))

    member this.Visibility 
        with get() = visibility 
        and set(value) =
            if (value <> visibility)
            then visibility <- value
                 dataCntxtEv.Trigger(ObjectToPassEventArgs(value, [|propertyName ; "Visibility"|]))
                 ev.Trigger(this, PropertyChangedEventArgs("Visibility"))
            
    
    interface INotifyPropertyChanged with
        [<CLIEvent>]
        member this.PropertyChanged = ev.Publish

    [<CLIEvent>]
    member this.UpdateDataContext = dataCntxtEv.Publish

