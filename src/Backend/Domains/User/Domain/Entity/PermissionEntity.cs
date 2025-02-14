using Backend.Domains.Common.Domain.Types;
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

    public PermissionEntity AddLocalizedName(Locale locale, Name name)
    {
        LocalizedNames.Add(PermissionNameEntity.Create(name, locale, Id));

        return this;
    }

    public PermissionEntity AddLocalizedDescription(Locale locale, Description description)
    {
        LocalizedDescriptions.Add(PermissionDescriptionEntity.Create(description, locale, Id));

        return this;
    }

    public static PermissionEntity Create(Key key)
    {
        return new PermissionEntity(Id.From(Guid.NewGuid()), key);
    }
}
