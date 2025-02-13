﻿using Backend.Domains.User.Domain.Types;
using Backend.Domains.User.Domain.VO;
using SaveApis.Core.Common.Domains.Common.Domain.VO;
using SaveApis.Core.Common.Infrastructure.Persistence.Sql.Entity;

namespace Backend.Domains.User.Domain.Entity;

public class UserEntity : ITrackedEntity
{
    private readonly ICollection<Tuple<string, string?, string?>> _changes = [];

    private UserEntity(Id id, Name firstName, Name lastName, Email email, Name userName, Hash? hash, UserState state, bool isInitialUser)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        UserName = userName;
        Hash = hash;
        State = state;
        IsInitialUser = isInitialUser;
    }

    public Id Id { get; }

    public Name FirstName { get; }
    public Name LastName { get; }
    public Email Email { get; }

    public Name UserName { get; }
    public Hash? Hash { get; }
    public UserState State { get; }
    public bool IsInitialUser { get; }

    public static UserEntity Create(Id id, Name firstName, Name lastName, Email email, Name userName, Hash? hash = null, UserState state = UserState.Active, bool isInitialUser = false)
    {
        return new UserEntity(id, firstName, lastName, email, userName, hash, state, isInitialUser);
    }

    public ICollection<Tuple<string, string?, string?>> GetChanges()
    {
        return _changes;
    }
}
