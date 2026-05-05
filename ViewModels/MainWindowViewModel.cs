using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OCTOVT_ETHERNET_GUI.Services;
using System.Threading.Tasks;

namespace OCTOVT_ETHERNET_GUI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly ISvSubscriber _svService;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ButtonText))]
    private bool _isRunning;

    // IsRunning 상태에 따라 버튼에 바인딩될 텍스트 결정
    public string ButtonText => IsRunning ? "Stop" : "Run";

    public MainWindowViewModel()
    {
        // 서비스 클래스 인스턴스화
        _svService = new Iec61850SvService();
    }

    [RelayCommand]
    private async Task ToggleRun()
    {
        if (!IsRunning)
        {
            IsRunning = true;
            await _svService.StartSubscribingAsync();
        }
        else
        {
            _svService.StopSubscribing();
            IsRunning = false;
        }
    }
}