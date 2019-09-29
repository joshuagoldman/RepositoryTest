namespace HaPsanter.Fsharp

open Xamarin.Forms.Xaml

module DummyModuleOnWhichToAttachAssemblyAttribute =

    [<assembly: XamlCompilation(XamlCompilationOptions.Compile)>]

    do()