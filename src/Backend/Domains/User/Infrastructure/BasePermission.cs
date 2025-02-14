using System.Collections.ObjectModel;
using Backend.Domains.Common.Domain.Types;
using Backend.Domains.Common.Domain.VO;
using SaveApis.Core.Common.Domains.Common.Domain.VO;

namespace Backend.Domains.User.Infrastructure;

public abstract class BasePermission : IPermission
{
    protected readonly ICollection<Tuple<Locale, Name>> Names = [];
    protected readonly ICollection<Tuple<Locale, Description>> Descriptions = [];

    public IReadOnlyCollection<Tuple<Locale, Name>> LocalizedNames => new ReadOnlyCollection<Tuple<Locale, Name>>(Names.ToList());
    public IReadOnlyCollection<Tuple<Locale, Description>> LocalizedDescriptions => new ReadOnlyCollection<Tuple<Locale, Description>>(Descriptions.ToList());

    public abstract Key Key { get; }

    protected void AddName(Locale locale, string name)
    {
        Names.Add(new Tuple<Locale, Name>(locale, Name.From(name)));
    }

    protected void AddDescription(Locale locale, string description)
    {
        Descriptions.Add(new Tuple<Locale, Description>(locale, Description.From(description)));
    }
}
