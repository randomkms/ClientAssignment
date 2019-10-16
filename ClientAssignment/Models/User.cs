using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientAssignment.Models
{
    public class User
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public ObservableCollection<Signature> Signatures { get; set; }
    }
}
