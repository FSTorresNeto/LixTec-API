using Google.Cloud.Firestore;
using LixTec.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LixTec.Controllers
{
    public class DepositController : Controller
    {
        private string directory = "C:\\Users\\Torres\\OneDrive\\Área de Trabalho\\Meus Documentos\\UFC\\TCC\\LixTec-API\\LixTec\\LixTec\\lixtec-dac2e-b98674e41b82.json";

        private string projectId = "lixtec-dac2e";

        private FirestoreDb _firestoreDb;

        public DepositController()
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREADENTIALS", directory);
            projectId = "lixtec-dac2e";
            _firestoreDb = FirestoreDb.Create(projectId);
        }

        // ---- Post Deposit ---- \\

        [HttpGet]
        public async Task<IActionResult> Login(User userDeposit)
        {
            // get do user

            DocumentReference documentReference = _firestoreDb.Collection("User").Document(userDeposit.UserId);

            DocumentSnapshot documentSnapshot = await documentReference.GetSnapshotAsync();

            if (documentSnapshot.Exists)
            {
                User selectedUser = documentSnapshot.ConvertTo<User>();


                if(userDeposit.DepositsTotalValue > 0)
                {
                    // atualizar o dado de deposits

                    DocumentReference user = _firestoreDb.Collection("user").Document(userDeposit.UserId);

                    await user.SetAsync(user, SetOptions.Overwrite);// Verificar

                    return View(selectedUser);// Colocar retorno pro front
                }
            }

            return NotFound(); // Erro
        }

    }
}
