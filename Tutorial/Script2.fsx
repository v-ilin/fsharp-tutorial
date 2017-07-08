#load "Helpers.fs"

type Command = 
    | CreateInvite of firstPlayerId: int * secondPlayerId: int
    | AcceptInvite of tennisInviteId: int
    | DeclineInvite of tennisInviteId: int

type Query =
    | GetInvite of tennisInviteId: int
    | GetInviteStatus of tennisInviteId: int

type Message =
    | Command of Command
    | Query of Query

type Event =
    | InviteCreated
    | InviteAccepted
    | InviteDeclined


type Agent<'T> = MailboxProcessor<'T>

let agent = Agent.Start(fun (inbox:Agent<Message>) ->  
    let rec loop() =
        async {
            let! msg = inbox.Receive()
            
            match msg with

            | Query (GetInvite inviteId) as q -> 
                printfn "Invoked \"%s\" query" (Helpers.GetUnionCaseName q)

            | Command (AcceptInvite inviteId) as c -> 
                printfn "Invoked \"%s\" command" (Helpers.GetUnionCaseName c)

            | _ -> 
                printfn "Message received: %s" (Helpers.GetUnionCaseName msg)
                return! loop() 
        }
    loop() 
)

let post (agent: Agent<'T>) message = agent.Post message
let postAsyncReply (agent: Agent<'T>) messageConstr = agent.PostAndAsyncReply messageConstr

type StreamId = StreamId of int
type StreamVersion = StreamVersion of int

type SaveResult =
    | Ok
    | VersionConflict

type Messages<'T> =
    | GetEvents of StreamId * AsyncReplyChannel<'T list option>
    | SaveEvents of StreamId * StreamVersion * 'T list * AsyncReplyChannel<SaveResult>
    | AddSubscriber of string * (StreamId * 'T list -> unit)
    | RemoveSubscriber  of string

