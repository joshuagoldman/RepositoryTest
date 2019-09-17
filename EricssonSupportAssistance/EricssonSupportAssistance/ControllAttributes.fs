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
    
    member this.Text 
        with get() = text 
        and set(value) =
            text <- value
            ev.Trigger(this, PropertyChangedEventArgs("Text"))
            dataCntxtEv.Trigger(ObjectToPassEventArgs(value, [|propertyName ; "Text"|]))

    member this.ItemsSource 
        with get() = itemsSource 
        and set(value) =
            itemsSource <- value
            ev.Trigger(this, PropertyChangedEventArgs("ItemsSource"))
            dataCntxtEv.Trigger(ObjectToPassEventArgs(value, [|propertyName ; "ItemsSource"|]))

    member this.Color 
        with get() = color 
        and set(value) =
            color <- value
            ev.Trigger(this, PropertyChangedEventArgs("Color"))
            dataCntxtEv.Trigger(ObjectToPassEventArgs(value, [|propertyName ; "Color"|]))

    member this.TbForeground 
        with get() = tbForeground 
        and set(value) =
            tbForeground <- value
            ev.Trigger(this, PropertyChangedEventArgs("TbForeground"))
            dataCntxtEv.Trigger(ObjectToPassEventArgs(value, [|propertyName ; "TbForeground"|]))

    member this.IsEnabled 
        with get() = isEnabled 
        and set(value) =
            isEnabled <- value
            ev.Trigger(this, PropertyChangedEventArgs("IsEnabled"))
            dataCntxtEv.Trigger(ObjectToPassEventArgs(value, [|propertyName ; "IsEnabled"|]))

    member this.DataContext 
        with get() = datacontext 
        and set(value) =
            datacontext <- value
            ev.Trigger(this, PropertyChangedEventArgs("DataContext"))
            dataCntxtEv.Trigger(ObjectToPassEventArgs(value, [|propertyName ; "DataContext"|]))

    member this.Visibility 
        with get() = visibility 
        and set(value) =
            visibility <- value
            ev.Trigger(this, PropertyChangedEventArgs("Visibility"))
            dataCntxtEv.Trigger(ObjectToPassEventArgs(value, [|propertyName ; "Visibility"|]))
    
    interface INotifyPropertyChanged with
        [<CLIEvent>]
        member this.PropertyChanged = ev.Publish

    [<CLIEvent>]
    member this.UpdateDataContext = dataCntxtEv.Publish

