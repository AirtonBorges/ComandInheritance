using System.Text.RegularExpressions;
using AudioSwitcher.AudioApi.CoreAudio;
using ComandInheritance.Services;

namespace ComandInheritance.Comandos;

public class ComandoMidia : Comando, IComando
{
    public override Task<bool> Executar()
    {
        var xDefaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;

        var xObteveVolume = int.TryParse(Regex.Match(Instrucao.Texto, @"\d+").Value, out var xVolume);

        if (xObteveVolume)
        {
            xDefaultPlaybackDevice.Volume = xVolume;
        }

        return Task.FromResult(xObteveVolume);
    }
}