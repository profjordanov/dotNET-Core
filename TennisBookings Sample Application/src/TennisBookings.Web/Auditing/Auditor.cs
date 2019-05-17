namespace TennisBookings.Web.Auditing
{
    public class Auditor<T> : IAuditor<T>
    {
        private readonly IAuditor _auditor;

        public Auditor()
        {
            _auditor = new ConsoleAuditor(typeof(T).Name);
        }

        public void RecordAction(string message)
        {
            _auditor.RecordAction(message);
        }
    }
}
