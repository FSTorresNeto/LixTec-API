using Google.Cloud.Firestore;
using LixTec.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LixTec.Controllers
{
    public class AuthController : Controller
    {
        private string directory = "C:\\Users\\Torres\\OneDrive\\Área de Trabalho\\Meus Documentos\\UFC\\TCC\\LixTec-API\\LixTec\\LixTec\\lixtec-dac2e-b98674e41b82.json";

        private string projectId = "lixtec-dac2e";

        private FirestoreDb _firestoreDb;

        public AuthController()
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREADENTIALS", directory);
            projectId = "lixtec-dac2e";
            _firestoreDb = FirestoreDb.Create(projectId);
        }

        // ---- Get User ---- \\

        [HttpGet]
        public async Task<IActionResult> Login(Login login)
        {

            Query userQuery = _firestoreDb.Collection("User");

            QuerySnapshot userQuerySnapshot = await userQuery.GetSnapshotAsync();

            List<User> listUser = new List<User>();


            foreach (DocumentSnapshot documentSnapshot in userQuerySnapshot.Documents)
            {
                if (documentSnapshot.Exists)
                {
                    Dictionary<string, object> user = documentSnapshot.ToDictionary();

                    string result = JsonConvert.SerializeObject(user);

                    User users = JsonConvert.DeserializeObject<User>(result);

                    users.UserId = documentSnapshot.Id;

                    if(login.Name == users.Name && login.Password == users.Password)
                    {

                        return View(listUser); // Retorna o usuário pro front
                    }
                }
            }

            return NotFound(); // Não encontrado ou Erro
        }

    }
}
