using System;
using System.Threading;
using System.Threading.Tasks;

namespace OCTOVT_ETHERNET_GUI.Services;

public interface ISvSubscriber
{
    Task StartSubscribingAsync();
    void StopSubscribing();
}

public class Iec61850SvService : ISvSubscriber
{
    private CancellationTokenSource? _cts;

    public async Task StartSubscribingAsync()
    {
        _cts = new CancellationTokenSource();
        
        // 실제 구현 시 SharpPcap 등의 라이브러리를 사용하여 0x88ba 패킷 필터링
        await Task.Run(() =>
        {
            try
            {
                while (!_cts.Token.IsCancellationRequested)
                {
                    // [TODO] Raw Socket 수신 루프
                    Thread.Sleep(10); 
                }
            }
            catch (OperationCanceledException) { }
        }, _cts.Token);
    }

    public void StopSubscribing()
    {
        _cts?.Cancel();
    }
}