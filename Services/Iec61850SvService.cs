using System;
using System.Threading;
using System.Threading.Tasks;


// 인터페이스는 딱 한 번만 정의되어야 합니다.
public interface ISvSubscriber
{
    Task StartSubscribingAsync();
    void StopSubscribing();
}

// 클래스도 딱 한 번만 정의되어야 합니다.
public class Iec61850SvService : ISvSubscriber
{
    private CancellationTokenSource? _cts;

    public async Task StartSubscribingAsync()
    {
        // 중복 실행 방지
        if (_cts != null) return;
        
        _cts = new CancellationTokenSource();
        
        await Task.Run(() =>
        {
            try
            {
                while (!_cts.Token.IsCancellationRequested)
                {
                    // [TODO] 실제 0x88ba EtherType 패킷 수신 로직
                    Thread.Sleep(10); 
                }
            }
            catch (OperationCanceledException) { }
        }, _cts.Token);
    }

    public void StopSubscribing()
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = null;
    }
}