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

### Reminder To Self.
Had a issue in Services where Map had redline its beacuse it wasent created also learned some people create it at the end or in the beginning it doesnt matter.
It was fixed with method on line 81.

Had a issue in Program.cs on line 17. error was it had no correct using on Npgsql i thought i had installed the right Packaged
turned out it was the postgress package i needed not hust the NpgSql.

Had a error i mistook EF.Mvc but its is AspNetCore.Mvc in controllers so in the future if i dont find Mvc in list its probably aspnetcore