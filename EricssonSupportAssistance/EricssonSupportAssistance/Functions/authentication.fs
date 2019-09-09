namespace EricssonSupportAssistance.Functions 

module Authentication =
    
    type ResOptions =
        | Pass
        | Fail

    type AuthenticationResult =
        {   Result : ResOptions
            Message : string
            }

    type AuthenticationStringPair =
        {   AllowedStrings : seq<string>
            ActualString : string
            }
    
    type AuthenticationInformation =
        {   UserName : AuthenticationStringPair
            Password : AuthenticationStringPair
        }
    
    let doesStrExist (str : string) (comparison : seq<string>)=
    
        comparison
        |> Seq.exists(fun x -> x = str)

    let getAuthentication (authentication : AuthenticationInformation) =
        
        authentication
        |> function
            | _ when doesStrExist authentication.Password.ActualString authentication.Password.AllowedStrings &&
                     doesStrExist authentication.UserName.ActualString authentication.UserName.AllowedStrings ->
                        {Result = ResOptions.Pass ; Message = "Authentication suceeded!"}

            | _ when not(doesStrExist authentication.Password.ActualString authentication.Password.AllowedStrings) ->
                        {Result = ResOptions.Fail ; Message = "Authentication failed. User name does not exist!"}

            | _ when doesStrExist authentication.Password.ActualString authentication.Password.AllowedStrings &&
                     not(doesStrExist authentication.UserName.ActualString authentication.UserName.AllowedStrings) ->
                        {Result = ResOptions.Pass ; Message = "Authentication failed. Invalid Password!"}

            | _ -> {Result = ResOptions.Fail ; Message = "Unforseen error occurred"}      
