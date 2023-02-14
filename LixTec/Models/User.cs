using Google.Cloud.Firestore;

namespace LixTec.Models
{
    [FirestoreData]
    public class User
    {
        public string? UserId {get; set;}

        [FirestoreProperty]
        public string? Name { get; set; }


        [FirestoreProperty]
        public string? Password { get; set; }


        [FirestoreProperty]
        public string? PhoneNumber { get; set; }


        [FirestoreProperty]
        public string? Email { get; set; }

        [FirestoreProperty]
        public long DepositsTotalValue { get; set; }

    }
}
