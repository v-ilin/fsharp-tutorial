#load "DomainTypes.fs"

type CommandResult =
    | Success
    | Failture

type QueryResult<'TSuccess, 'TError> =
    | Success of 'TSuccess
    | Failture of 'TError

type TennisQuerySuccess =
    | TennisInvite of DomainTypes.TennisInvite
    | TennisInviteStatus of DomainTypes.TennisInviteStatus 

type TopicsQuerySuccess =
    | Topic of DomainTypes.Topic
    | Topics of DomainTypes.Topic list

type QuerySuccess =
    | TennisQuerySuccess of TennisQuerySuccess
    | TopicsQuerySuccess of TopicsQuerySuccess
    
type QueryError =
    | InviteNotFound
    | InviteStateIncompatible

type MessageResult =
    | CommandResult of CommandResult
    | QueryResult of QueryResult<QuerySuccess, QueryError>

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

let handleCommand (c: Command): CommandResult =
    match c with
    | CreateInvite (firstPlayerId, secondPlayerId) -> CommandResult.Success
    | AcceptInvite inviteId -> CommandResult.Failture
    | DeclineInvite inviteId -> CommandResult.Success

let executeQuery (q: Query): QueryResult<QuerySuccess, QueryError> =
    match q with
    | GetInvite inviteId -> Failture InviteNotFound
    | GetInviteStatus inviteId -> Success (TennisQuerySuccess (TennisInviteStatus DomainTypes.TennisInviteStatus.Created))

let handleMessage (m: Message): MessageResult =
    match m with
    | Command c -> CommandResult (handleCommand c)
    | Query q -> QueryResult (executeQuery q)

let result = handleMessage (Query (GetInvite 1))

match result with

| QueryResult queryResult ->

    printf "Query "
    match queryResult with

    | QueryResult.Success s ->
        printf "was executed successfully. "
        match s with

        | QuerySuccess.TennisQuerySuccess tennisSuccess -> 
            printf "Query category - tennis. "
            match tennisSuccess with
            | TennisQuerySuccess.TennisInvite invite ->
                printf "Query name - \"TennisInvite\""
            | TennisQuerySuccess.TennisInviteStatus status ->
                printf "Query name - \"TennisInviteStatus\""

    | QueryResult.Failture f -> printfn "Query failed!"

| CommandResult cr -> 
    printfn "Command was executed"
    match cr with

    | CommandResult.Success -> printfn "Command executed successfully"

    | CommandResult.Failture -> printfn "Command failed!"
