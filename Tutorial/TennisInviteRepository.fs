module TennisInviteRepository

open DomainTypes

let CreateInvite (firstPlayerId: int, secondPlayerId: int) =
    // creating tennis invite
    // ...
    // ...
    ()

let GetInvite (inviteId: int): TennisInvite =
    // perform select query to data source
    // ...
    // ...
    {
        Id = inviteId;
        FirstPlayerId = 2;
        SecondPlayerId = 3;
        Status = TennisInviteStatus.Created
    }

let GetInviteStatus (inviteId: int): TennisInviteStatus =
    // perform select query to data source
    // ...
    // ...
    TennisInviteStatus.Created

let ChangeInviteStatus (inviteId: int, status: TennisInviteStatus) =
    // perform update query to data source
    // ...
    // ...
    ()