module Main

open DomainTypes
open TennisInviteAggregate
open Helpers

[<EntryPoint>]
let main args =
    
    let result = handleMessage (Query (Query.GetInviteStatus 1))

    match unbox result with
    | objectResult when match unbox objectResult with
                        | :? TennisInvite as invite -> 
                            printfn "Message result is TennisInvite - Id = %i" invite.Id
                            true
                        | :? TennisInviteStatus as status->
                            GetUnionCaseName(status)
                            |> printfn "Message result is TennisInviteStatus - %s"
                            true
                        | _ -> false
                    -> ()
    | _ -> printfn "Unknown message result"
    0