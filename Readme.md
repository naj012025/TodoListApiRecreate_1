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
step3 make model
}