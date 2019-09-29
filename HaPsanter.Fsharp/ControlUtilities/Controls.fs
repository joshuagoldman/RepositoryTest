namespace Hapsanter.Fsharp.ControlUtilities

    
    
    type Controls() =
        
        let mutable startAppButton = new ControlAttributes("StartAppButton")
        let mutable startAppImage = new ControlAttributes("StartAppImage")

        // MainPage controls
        member val StartAppButton = startAppButton with get, set
        member val StartAppImage = startAppImage with get, set
                  
