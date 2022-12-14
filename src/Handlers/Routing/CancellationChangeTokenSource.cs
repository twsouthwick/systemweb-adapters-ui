// MIT License.

using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Primitives;

namespace System.Web.Routing;

internal sealed class CancellationChangeTokenSource : IDisposable
{
    private readonly ReaderWriterLockSlim _lock;
    private readonly IDisposable _exitReadLock;
    private readonly IDisposable _exitWriteLock;

    private CancellationChangeToken _token;
    private CancellationTokenSource _cts;
    private State _state;

    public CancellationChangeTokenSource()
    {
        Init();

        _lock = new ReaderWriterLockSlim();
        _exitReadLock = new Disposable(() => _lock.ExitReadLock());
        _exitWriteLock = new Disposable(() =>
        {
            try
            {
                if (_state is State.PausedChanges)
                {
                    ResetInternal();
                }

                _state = State.None;
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        });
    }

    [MemberNotNull(nameof(_token), nameof(_cts))]
    private void Init()
    {
        _cts = new();
        _token = new(_cts.Token);
    }

    public IChangeToken GetChangeToken() => _token;

    public IDisposable GetWriteLock()
    {
        _state = State.Paused;
        _lock.EnterWriteLock();
        return _exitWriteLock;
    }

    public IDisposable GetReadLock()
    {
        _lock.EnterReadLock();
        return _exitReadLock;
    }

    public void OnChange()
    {
        if (_state is State.Paused)
        {
            _state = State.PausedChanges;
            return;
        }

        if (_state is State.PausedChanges)
        {
            return;
        }

        ResetInternal();
    }

    private void ResetInternal()
    {
        var previous = _cts;

        Init();

        previous.Cancel();
        previous.Dispose();
    }

    public void Dispose()
    {
        _cts.Dispose();
        _lock.Dispose();
    }

    private enum State
    {
        None,
        Paused,
        PausedChanges,
    }

    private sealed class Disposable : IDisposable
    {
        private static IDisposable? _empty;

        private readonly Action _action;

        public static IDisposable Empty => _empty ??= new Disposable(() => { });

        public Disposable(Action action) => _action = action;

        public void Dispose() => _action();
    }
}
