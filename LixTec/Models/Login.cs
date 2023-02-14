using Google.Cloud.Firestore;

namespace LixTec.Models
{
    [FirestoreData]
    public class Login
    {
        [FirestoreProperty]
        public string? Name { get; set; }


        [FirestoreProperty]
        public string? Password { get; set; }

    }
}
