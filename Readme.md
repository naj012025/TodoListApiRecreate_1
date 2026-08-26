Todolist practice nr1

Step 1{
Step 1 i will make the folder structure i need.

step 2 i will download package npg and Ef core.

step 3 i will make model with get; private set containing title / DateTimeNow.utc/bool Iscompleted.

step 4 i will make method and constructor in same model file.
}

Step 2{
step1 Make 3 Dto CreateTodoRequest , UpdateTodoRequest and TodoResponse
step2 In Create and update Tood Using System.CombonentModel.DataAnnotiations; and add in the info you need
step3 in TodoRespons AFTER Get do init so data to user cant be change itsalread been initialized. Its a copy of the Model in this case.
}

Step 3{
step1 using Microsofte.EntityframeworkCore; and the Using Project Name.Models at the top. 
step2 Make a public Sealed class Name of cs file : DbContext. it inherits from efcore using here.
step3 Make the method u need.
}

Step 4{
Step 1 Make TodoService.cs and add in Using Models/dto/data/EFCore paths.
step 2 Make the maping etc you need this will be different ish per case but in this Todo its 5 Async task / one sealed private class of the _db and one public of hte db}
step 3 Rember from service into controll the update/delete/post are connected to service.

step 5{
step 1 Make TodoController.cs and add in IMPORTANT Microsoft.AspNetCore.Mvc do not use Efcore here its not correct
step 2 Make the Attributes you need for in this case at top [ApiController] Then Route attribute for [Route("api/todos")] Before the public class that has the 
HttpGet/put etc
Step 3 Make the attributes and body for each thing in CRUD look in Controller for info its done there.}

Step 6 {
step 1 Make the changes in Program.cs you need to activate Swagger and using the controllers etc 
step 2 build and run it might have to change stuff to make it run in swagger update this when you have figure out what.}

### Reminder To Self.
Had a issue in Services where Map had redline its beacuse it wasent created also learned some people create it at the end or in the beginning it doesnt matter.
It was fixed with method on line 81.

Had a issue in Program.cs on line 17. error was it had no correct using on Npgsql i thought i had installed the right Packaged
turned out it was the postgress package i needed not just the NpgSql.

Had a error i mistook EF.Mvc but its is AspNetCore.Mvc in controllers so in the future if i dont find Mvc in list its probably aspnetcore

Tried to use Scalar but wouldt open so i went with swagger.
Had Issues with making swagger because i installed the wrong nuget package noticed and fixed and discovered another error in routing 
Needed to fix ("{id:int}") in a Get that was duplicated name.
Also Importnat if installed a new Nugetpacket that supposed to fix the issue and it doesnt restart VS that fixed it.

