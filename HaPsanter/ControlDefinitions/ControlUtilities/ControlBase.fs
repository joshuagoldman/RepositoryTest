namespace ControlDefinitions.ControlUtilities


open System.Windows
open System
open System.Reflection


type ControlBase() =
    
    let mutable sender = new Controls()
    let mutable dataContextUpdateEv = new Event<ObjectToPassEventArgs>()
    let mutable sequenceOfCtrlBase = seq[new ControlBase()]
    let mutable objToWorkWith = seq[new obj()]

    member this.ObjToWorkWith 
        with get() = objToWorkWith
        and set(value) = 
            if value <> objToWorkWith then objToWorkWith <- value

    member this.Sender 
        with get() = sender
        and set(value) = 
            if value <> sender then sender <- value
    
    member this.SequenceOfCtrlBase 
        with get() = sequenceOfCtrlBase
        and set(value) =
            if value <> sequenceOfCtrlBase then sequenceOfCtrlBase <- value

    [<CLIEvent>]
    member this.UpdateDataContext = dataContextUpdateEv.Publish

    member this.UpdateDatacontext (o : ObjectToPassEventArgs) =
        
        let controlAttrPropertyToChange = this.Sender.GetType().GetProperties(BindingFlags.Public ||| BindingFlags.Instance)
                                          |> Array.filter(fun prop ->  prop.PropertyType = typeof<ControlAttributes>)
                                          |> Array.find(fun prop -> prop.Name = o.nameWOwner.[0])
                                          |> fun x -> x.GetValue(this.Sender)

        controlAttrPropertyToChange.GetType().GetProperties(BindingFlags.Public ||| BindingFlags.Instance)
        |> Array.find(fun prop -> prop.Name = o.nameWOwner.[1])
        |> fun prop -> prop.SetValue(controlAttrPropertyToChange, o.Value)

        this.SequenceOfCtrlBase
        |> Seq.iter(fun ctrlBase -> ctrlBase.Sender <- this.Sender)


    member this.listenToAllCtrlAttr = 
            
            let allCtrlBase =
                this.ObjToWorkWith
                |> Seq.collect(fun prop -> prop.GetType().GetProperties(BindingFlags.Public ||| BindingFlags.Instance)
                                           |> Seq.filter(fun subProp -> subProp.PropertyType = typeof<ControlBase>)
                                           |> Seq.map(fun subProp -> subProp.GetValue(prop) :?> ControlBase))

            this.SequenceOfCtrlBase <- allCtrlBase
            
            let allCtrlAttrs =
                this.SequenceOfCtrlBase
                |> Seq.collect(fun prop -> prop.GetType().GetProperties(BindingFlags.Public ||| BindingFlags.Instance)
                                           |> Seq.filter(fun subProp -> subProp.PropertyType = typeof<ControlAttributes>)
                                           |> Seq.map(fun subProp -> subProp.GetValue(prop) :?> ControlAttributes))

            allCtrlAttrs
            |> Seq.iter(fun ctrlAttr -> ctrlAttr.UpdateDataContext.Add(fun evArgs -> this.UpdateDatacontext evArgs))