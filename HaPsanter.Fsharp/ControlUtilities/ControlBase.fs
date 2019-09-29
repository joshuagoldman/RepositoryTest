namespace Hapsanter.Fsharp.ControlUtilities


open System.Windows
open System
open System.Reflection


type ControlBase() as this =
    
    let mutable sender = new Controls()
    let mutable dataContextUpdateEv = new Event<ObjectToPassEventArgs>()
    let mutable sequenceOfControls = [||]
    let mutable sequenceOfCtrlBase = [|this|]

    member this.Sender 
        with get() = sender
        and set(value) = 
            if value <> sender then sender <- value

    member this.SequenceOfControls 
        with get() = sequenceOfControls
        and set(value) =
            if value <> sequenceOfControls then sequenceOfControls <- value
    
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

            let fillCtrls (infoArr : PropertyInfo[]) (o : obj) =
                
                infoArr
                |> Array.filter(fun prop -> prop.PropertyType = typeof<Controls>)
                |> Array.map(fun prop -> prop.GetValue(o) :?> Controls)
                |> fun x -> this.SequenceOfControls <- Array.append this.SequenceOfControls x

            let fillCtrlsBase (infoArr : PropertyInfo[]) (o : obj) =
                
                infoArr
                |> Array.filter(fun prop -> prop.PropertyType.IsSubclassOf(typeof<ControlBase>))
                |> Array.map(fun prop -> prop.GetValue(o) :?> ControlBase)
                |> fun x -> this.SequenceOfCtrlBase <- Array.append this.SequenceOfCtrlBase x          


            let allCtrlBaseInstancesFirstLevel =
                
                this.GetType().GetProperties(BindingFlags.Public ||| BindingFlags.Instance)
                |> fun props -> fillCtrlsBase props this
                                |> fun _ -> props
                |> Array.filter(fun prop ->  prop.PropertyType.IsSubclassOf(typeof<ControlBase>))
                |> Array.map(fun prop -> prop.GetValue(this))


            allCtrlBaseInstancesFirstLevel
            |> Array.iter(fun prop -> prop.GetType().GetProperties(BindingFlags.Public ||| BindingFlags.Instance)
                                      |> fun props -> fillCtrlsBase props prop
                                                      |> fun _ -> props
                                      |> Array.filter(fun subProp -> subProp.PropertyType = typeof<Controls> ||
                                                                     subProp.PropertyType.IsSubclassOf(typeof<ControlBase>))
                                      |> fun subProps -> (fillCtrls subProps prop)
                                                         |> fun _ -> subProps
                                                                     |> Array.map(fun subProp -> subProp.GetValue(prop))
                                                         |> fun subPropsObj -> subPropsObj
                                                                               |> Array.iter(fun subPropObj -> subPropObj.GetType().GetProperties(BindingFlags.Public ||| BindingFlags.Instance)
                                                                                                               |> fun subSubProps -> fillCtrls subSubProps subPropObj))
            

            let ctrlAttrsAll =
                
                this.SequenceOfControls
                |> Array.collect(fun ctrl -> ctrl.GetType().GetProperties(BindingFlags.Public ||| BindingFlags.Instance)
                                             |> Array.filter(fun subProp -> subProp.PropertyType = typeof<ControlAttributes>)
                                             |> Array.map(fun subProp -> subProp.GetValue(ctrl) :?> ControlAttributes))

            ctrlAttrsAll
            |> Array.iter(fun ctrlAttr -> ctrlAttr.UpdateDataContext.Add(fun evArgs -> this.UpdateDatacontext evArgs))