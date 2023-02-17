using System.Text.RegularExpressions;
using AudioSwitcher.AudioApi.CoreAudio;

namespace ComandInheritance.Comandos;

public class ComandoMidia : Comando
{
    public override Task<bool> Executar(IServiceProvider pServiceProvider)
    {
        var xDefaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;
        
        var xObteveVolume = int.TryParse(Regex.Match(Texto, @"\d+").Value, out var xVolume);

        if (xObteveVolume)
        {
            xDefaultPlaybackDevice.Volume = xVolume;
        }

        return Task.FromResult(xObteveVolume);
    }
}