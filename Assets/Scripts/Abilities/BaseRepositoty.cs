using System.Collections.Generic;
using Tools;

public abstract class BaseRepositoty<Tkey, Tvalue, Tconfig> :BaseController, IRepository<Tkey, Tvalue> where Tconfig:IUnique<Tkey>
{
    public IReadOnlyDictionary<Tkey, Tvalue> Content => _content;
    private Dictionary<Tkey, Tvalue> _content;

    protected BaseRepositoty(List<Tconfig> configs)
    {
        _content = new Dictionary<Tkey, Tvalue>();
        PopulateContent(ref _content, configs);
    }

    private void PopulateContent(ref Dictionary<Tkey, Tvalue> dictionary, List<Tconfig> configs)
    {
        foreach (var config in configs)
        {
            if (_content.ContainsKey(config.Id))
                continue;
            _content.Add(config.Id, CreateValue(config));
        }
    }
    protected override void OnDispose()
    {
        _content.Clear();
        _content = null;
    }
    protected abstract Tvalue CreateValue(Tconfig config);
}
