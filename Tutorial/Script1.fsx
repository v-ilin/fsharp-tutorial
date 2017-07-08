#load "Helpers.fs"
#load "DomainTypes.fs"
#load "TennisInviteRepository.fs"

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

type MessageResult<'a> = { Content:'a }

let fireEvent (e: Event) = 
    let eventString = Helpers.GetUnionCaseName(e)
    printfn "\"%s\" event fired" eventString

type Error =
    | InviteCreationFailed
    | InviteNotFound

//type QueryResult =
//    | Success of _
//    | Failture of _

let executeQuery (q: Query): QueryResult<> =
    match q with
    
    | GetInvite inviteId ->
        let invite = TennisInviteRepository.GetInvite inviteId
        Success invite

    | GetInviteStatus inviteId ->
        let status = TennisInviteRepository.GetInviteStatus inviteId
        Success status

let doCommand (c: Command) =
    let commandString = Helpers.GetUnionCaseName(c)
    printfn "Executing command \"%s\"" commandString

    match c with
    
    | Command.CreateInvite (firstPlayesId, secondPlayerId) -> 
        TennisInviteRepository.CreateInvite (firstPlayesId, secondPlayerId)
        fireEvent(InviteCreated)

    | Command.AcceptInvite inviteId ->
        TennisInviteRepository.ChangeInviteStatus (inviteId, TennisInviteStatus.Accepted)
        fireEvent(InviteAccepted)

    | Command.DeclineInvite inviteId ->
        TennisInviteRepository.ChangeInviteStatus (inviteId, TennisInviteStatus.Declined)
        fireEvent(InviteDeclined)

let handleMessage (m: Message) =
    match m with
    | Command c -> doCommand
    | Query q -> executeQuery

