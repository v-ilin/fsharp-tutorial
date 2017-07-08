module DomainTypes

    type User = { Id: int }

    type TennisInviteStatus =
        | Created
        | Accepted
        | Declined

    type TennisInvite = {
        Id: int;
        FirstPlayerId: int;
        SecondPlayerId: int;
        Status: TennisInviteStatus
    }

    type Topic = {
        Name: string;
    }