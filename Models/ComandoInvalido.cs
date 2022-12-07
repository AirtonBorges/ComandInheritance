namespace ComandInheritance.Models
{
    internal class ComandoInvalido : Comando
    {
        public override Task<bool> Executar(IServiceProvider pServiceProvider)
        {
            Console.WriteLine($"O comando {Texto} não existe.");
            return Task.FromResult(false);
        }
    }
}