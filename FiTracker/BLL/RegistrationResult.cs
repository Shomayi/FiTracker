namespace FiTracker.BLL
{
    public class RegistrationResult
    {
        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; } = new();
    }
}
