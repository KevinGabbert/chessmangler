namespace ChessMangler.Communications.Interfaces
{
    public interface IQueryVersion
    {
        string RequestOpponentCurrentGameVersion(string opponentName);
        string RequestOpponentCMVersion(string opponentName); 
    }
}
