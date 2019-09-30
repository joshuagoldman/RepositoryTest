namespace HaPsanter.Fsharp

open Xamarin.Forms
open ControlDefinitions.ControlUtilities
open Xamarin.Forms.Xaml
open System.ComponentModel
open System.Xml.Linq
open System.Xml.XPath
open System.Reflection
open System.IO
open System

[<DesignTimeVisible(false)>]
type GameStart() as this =
    inherit ContentPage()

    let mutable controlBase = new ControlBase()

    do this.Initialization

    member this.ControlBase
        with get() = controlBase
        and set(value) =
            if value <> controlBase then controlBase <- value

    member this.Initialization =
        
        base.BindingContext <- this.ControlBase.Sender
        this.ControlBase.ObjToWorkWith <-
            seq[    this :> obj ;
                    new VideoPlayerPage() :> obj
                    ]
        this.ControlBase.listenToAllCtrlAttr

    member this.ListenToEvents =
        
        let startupButton = base.FindByName<Button>("StartAppButton")

        startupButton.Clicked.Add(fun _ -> this.OnAppStartButtonClicked)
    
    member this.OnAppStartButtonClicked =
        this.ControlBase.Sender.IntroVideoPlayer.IsVisible <- true
        this.ControlBase.Sender.IntroVideoPlayerPage.IsVisible <- true
        this.ControlBase.Sender.StartAppButton.IsVisible <- false
        this.ControlBase.Sender.StartAppImage.IsVisible <- false
        let assembly = Assembly.GetExecutingAssembly()
        let stream = assembly.GetManifestResourceStream("HaPsanter.Fsharp.VideosEmbeddedResource.xml")
        let mutable xDoc = XDocument.Load(stream)
        let file = File.ReadAllBytes("C:\\Users\\Public\\Videos\\Sample Videos\\Naturliv.wmv")
        
        let filInstringForm = Convert.ToBase64String file

        xDoc.XPathSelectElement("*//Video[@Name = 'Nature']").XPathSelectElement("*//Source").FirstAttribute.SetValue(filInstringForm)
        |> fun _ -> let embeddedCritPath = assembly.GetManifestResourceNames()
                    xDoc.Save(embeddedCritPath.[1]
                                            |> fun x -> x.Replace(".", "\\")
                                            |> fun x -> x.Replace("\\xml", ".xml")
                                            |> fun x -> "..\\..\\..\\" + x)
        this.ControlBase.Sender.IntroVideoPlayer.VideoSource <- xDoc.XPathSelectElement("*//Video[@Name = 'Nature']").XPathSelectElement("*//Source").FirstAttribute.Value