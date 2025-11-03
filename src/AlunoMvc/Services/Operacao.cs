namespace AlunoMvc.Services
{
    public class Operacao : IOperacao
    {
        public Guid OperacaoID {get; private set;}

        public Operacao()
        {
            OperacaoID = Guid.NewGuid();
        }
    }

    public interface IOperacao
    {
        Guid OperacaoID { get; }
    }
}
