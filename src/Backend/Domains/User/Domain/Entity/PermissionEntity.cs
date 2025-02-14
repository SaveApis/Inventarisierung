using Backend.Domains.Common.Domain.VO;
using SaveApis.Core.Common.Domains.Common.Domain.VO;

namespace Backend.Domains.User.Domain.Entity;

public class PermissionEntity
{
    private PermissionEntity(Id id, Key key)
    {
        Id = id;
        Key = key;
    }

    public Id Id { get; }
    public Key Key { get; }

    public virtual List<PermissionNameEntity> LocalizedNames { get; set; } = [];
    public virtual List<PermissionDescriptionEntity> LocalizedDescriptions { get; set; } = [];

    public virtual List<UserEntity> Users { get; set; } = [];

    public static PermissionEntity Create(Key key)
    {
        return new PermissionEntity(Id.From(Guid.NewGuid()), key);
    }
}
