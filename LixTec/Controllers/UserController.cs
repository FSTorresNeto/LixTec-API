using Google.Cloud.Firestore;
using LixTec.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LixTec.Controllers
{
    public class UserController : Controller
    {
        private string directory = "C:\\Users\\Torres\\OneDrive\\Área de Trabalho\\Meus Documentos\\UFC\\TCC\\LixTec-API\\LixTec\\LixTec\\lixtec-dac2e-b98674e41b82.json";

        private string projectId = "lixtec-dac2e";

        private FirestoreDb _firestoreDb;
        
        public UserController()
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREADENTIALS", directory);
            projectId = "lixtec-dac2e";
            _firestoreDb = FirestoreDb.Create(projectId);
        }

        public async Task<IActionResult> Index()
        {
            Query userQuery = _firestoreDb.Collection("User");

            QuerySnapshot userQuerySnapshot = await userQuery.GetSnapshotAsync();

            List<User> listUser = new List<User>();


            foreach(DocumentSnapshot documentSnapshot in userQuerySnapshot.Documents)
            {
                if (documentSnapshot.Exists)
                {
                    Dictionary<string, object> user = documentSnapshot.ToDictionary();

                    string result = JsonConvert.SerializeObject(user);

                    User newUser = JsonConvert.DeserializeObject<User>(result);

                    newUser.UserId = documentSnapshot.Id;

                    listUser.Add(newUser);
                }
            }

            return View(listUser);// Colocar retorno pro front
        }

        // ---- Create User ---- \\

        [HttpPost]
        public async Task<IActionResult> NewUser(User user)
        {
            CollectionReference collectionRefecence = _firestoreDb.Collection("User");

            await collectionRefecence.AddAsync(user);

            return RedirectToAction(nameof(Index));// Colocar retorno pro front
        }


        // ---- Update User ---- \\

        [HttpGet]
        public async Task<IActionResult> UpdateUser(string userId)
        {
            DocumentReference documentReference = _firestoreDb.Collection("User").Document(userId);

            DocumentSnapshot documentSnapshot = await documentReference.GetSnapshotAsync();

            if (documentSnapshot.Exists)
            {
                User selectedUser = documentSnapshot.ConvertTo<User>();

                return View(selectedUser);// Colocar retorno pro front
            }

            return NotFound();// Colocar retorno pro front
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(User user)
        {
            DocumentReference documentReference = _firestoreDb.Collection("user").Document(user.UserId);

            await documentReference.SetAsync(user, SetOptions.Overwrite);

            return RedirectToAction(nameof(Index));// Colocar retorno pro front
        }
    }
}