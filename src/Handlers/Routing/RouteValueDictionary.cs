// MIT License.

using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace System.Web.Routing;

public class RouteValueDictionary : IDictionary<string, object?>
{
    private readonly IDictionary<string, object?> _other;

    internal RouteValueDictionary(IDictionary<string, object?> other)
    {
        _other = other;
    }

    public object? this[string key] { get => _other[key]; set => _other[key] = value; }

    public ICollection<string> Keys => _other.Keys;

    public ICollection<object?> Values => _other.Values;

    public int Count => _other.Count;

    public bool IsReadOnly => _other.IsReadOnly;

    public void Add(string key, object? value)
    {
        _other.Add(key, value);
    }

    public void Add(KeyValuePair<string, object?> item)
    {
        _other.Add(item);
    }

    public void Clear()
    {
        _other.Clear();
    }

    public bool Contains(KeyValuePair<string, object?> item)
    {
        return _other.Contains(item);
    }

    public bool ContainsKey(string key)
    {
        return _other.ContainsKey(key);
    }

    public void CopyTo(KeyValuePair<string, object?>[] array, int arrayIndex)
    {
        _other.CopyTo(array, arrayIndex);
    }

    public IEnumerator<KeyValuePair<string, object?>> GetEnumerator()
    {
        return _other.GetEnumerator();
    }

    public bool Remove(string key)
    {
        return _other.Remove(key);
    }

    public bool Remove(KeyValuePair<string, object?> item)
    {
        return _other.Remove(item);
    }

    public bool TryGetValue(string key, [MaybeNullWhen(false)] out object? value)
    {
        return _other.TryGetValue(key, out value);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_other).GetEnumerator();
    }
}
