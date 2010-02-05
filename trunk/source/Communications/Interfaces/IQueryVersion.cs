namespace ChessMangler.Communications.Interfaces
{
    public interface IQueryVersion
    {
        string RequestOpponentCurrentGameVersion(string opponentJabberName, string myJabberName);
        string RequestOpponentCMVersion(string opponentName); 
    }
}
