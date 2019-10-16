using System.Collections.ObjectModel;

namespace ClientAssignment.Models
{
    public class User
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public ObservableCollection<Signature> Signatures { get; set; }
    }
}
