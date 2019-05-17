namespace TennisBookings.Web.Auditing
{
    public interface IAuditor<out T> : IAuditor
    {
    }

    public interface IAuditor
    {
        void RecordAction(string message);
    }
}
