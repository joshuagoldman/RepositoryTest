namespace EricssonSupportAssistance

open System.Windows.Media
open System.Windows.Controls
open System
open System.Xaml
open System.ComponentModel
open System.Windows

type ControlAtributes() =
    
    let mutable text = ""
    let mutable itemsSource = [|""|]
    let mutable color = Colors.White
    let mutable tbForeground = Brushes.Black
    let mutable datacontext = new obj()
    let mutable isEnabled = false
    let mutable visibility = Visibility.Hidden
    let ev = new Event<_,_>()

    member this.Text 
        with get() = text 
        and set(value) =
            text <- value
            ev.Trigger(this, PropertyChangedEventArgs("Text"))

    member this.ItemsSource 
        with get() = itemsSource 
        and set(value) =
            itemsSource <- value
            ev.Trigger(this, PropertyChangedEventArgs("ItemsSource"))

    member this.Color 
        with get() = color 
        and set(value) =
            color <- value
            ev.Trigger(this, PropertyChangedEventArgs("Color"))

    member this.TbForeground 
        with get() = tbForeground 
        and set(value) =
            tbForeground <- value
            ev.Trigger(this, PropertyChangedEventArgs("TbForeground"))

    member this.IsEnabled 
        with get() = isEnabled 
        and set(value) =
            isEnabled <- value
            ev.Trigger(this, PropertyChangedEventArgs("IsEnabled"))

    member this.DataContext 
        with get() = datacontext 
        and set(value) =
            datacontext <- value
            ev.Trigger(this, PropertyChangedEventArgs("DataContext"))

    member this.Visibility 
        with get() = visibility 
        and set(value) =
            visibility <- value
            ev.Trigger(this, PropertyChangedEventArgs("Visibility"))
    
    interface INotifyPropertyChanged with
        [<CLIEvent>]
        member this.PropertyChanged = ev.Publish

