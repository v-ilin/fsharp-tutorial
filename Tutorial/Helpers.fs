module Helpers

open Microsoft.FSharp.Reflection

let GetUnionCaseName (x:'a) = 
    match FSharpValue.GetUnionFields(x, typeof<'a>) with
    | case, _ -> case.Name  

///Returns the case names of union type 'ty.
let GetUnionCaseNames<'ty> () = 
    FSharpType.GetUnionCases(typeof<'ty>) |> Array.map (fun info -> info.Name)