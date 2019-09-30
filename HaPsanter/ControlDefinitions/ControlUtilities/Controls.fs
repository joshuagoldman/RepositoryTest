namespace ControlDefinitions.ControlUtilities

    
    
    type Controls() =
        
        let mutable startAppButton = new ControlAttributes("StartAppButton")
        let mutable startAppImage = new ControlAttributes("StartAppImage")
        let mutable VideoPlayerPage = new ControlAttributes("IntroVideoPlayerPage")
        let mutable VideoPlayer = new ControlAttributes("IntroVideoPlayer")

        // MainPage controls
        member val StartAppButton = startAppButton with get, set
        member val StartAppImage = startAppImage with get, set
        // VideoPage controls
        member val IntroVideoPlayerPage = VideoPlayerPage with get, set
        member val IntroVideoPlayer = VideoPlayer with get, set
                  
