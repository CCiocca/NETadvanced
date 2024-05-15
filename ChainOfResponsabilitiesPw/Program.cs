using System.Text.RegularExpressions;
//Creiamo una funzione che possa essere utilizzata come validatore per il campo password di un modulodi registrazione utente.
//La funzione di convalida riceve una stringa in input
//e restituisce un risultato di convalida.
//Il risultato della convalida dovrebbe contenere un valore booleano che indica se la password è 
//valida o meno, e anche un campo con i possibili errori di convalida.

//Requisiti

//1) La password deve essere lunga almeno 8 caratteri. Se non lo è, deve essere restituito il seguente messaggio di errore: "La password deve essere lunga almeno 8 caratteri".
//2) La password deve contenere almeno 2 numeri. Se non lo è, deve essere restituito il seguente messaggio di errore: "La password deve contenere almeno 2 numeri".
//3) La funzione di convalida deve gestire più errori di convalida contemporaneamente.
//Ad esempio, per "somepassword" dovrebbe restituire un messaggio di errore: "La password deve essere lunga almeno 8 caratteri\nLa password deve contenere almeno 2 numeri".
//4) La password deve contenere almeno una lettera maiuscola. Se non lo è, deve essere restituito il seguente messaggio di errore: "La password deve contenere almeno una lettera maiuscola".
//5) La password deve contenere almeno un carattere speciale. Se non lo è, deve essere restituito il seguente messaggio di errore: "La password deve contenere almeno un carattere speciale".


var pwLenght = new PwLenght();
var pwNumbers = new PwNumbers();
var pwUpperCase = new PwUpperCase();
var pwSpecialChar = new PwSpecialChar();

pwLenght
    .SetSuccessor(pwNumbers)
    .SetSuccessor(pwUpperCase)
    .SetSuccessor(pwSpecialChar);


var result = new ValidationResult(); //bool, errorList
pwLenght.Validate("ree", result); //if wrong pw, adds an error to the list

result.PrintErrors(); //cw of result (error List) if bool of ValidationResult is False




////validation result: bool e campo con possibili errori
public class ValidationResult
{
    public bool IsValid { get; set; }
    public List<string> Errors { get; set; }

    public ValidationResult()
    {
        IsValid = true;
        Errors = new List<string>();
    }

    //method to add an error to the list
    public void AddError(string error)
    {
        IsValid = false; //since it found an error, means the isValis must turn false
        Errors.Add(error);
    }

    //method to print the error list
    public void PrintErrors()
    {
        Console.WriteLine(IsValid ? "Passwordcorretta" : string.Join("\n", Errors));
    }
}

//generic
public abstract class PwRequirementValidator
{
    protected PwRequirementValidator? Next;
    public PwRequirementValidator SetSuccessor(PwRequirementValidator next)
    {
        Next = next;
        return Next;
    }

    public virtual void Validate(string password, ValidationResult result)
    {
        Next?.Validate(password, result);
    }
}

//specific
//1) La password deve essere lunga almeno 8 caratteri.
//Se non lo è, deve essere restituito il seguente messaggio di errore:
//"La password deve essere lunga almeno 8 caratteri".

public class PwLenght : PwRequirementValidator
{
    public override void Validate(string password, ValidationResult result)
    {
        if (password.Length < 8)
        {
            result.AddError("La password deve essere lunga almeno 8 caratteri");
        }
        base.Validate(password, result);
    }
}

//2) La password deve contenere almeno 2 numeri.
//Se non lo è, deve essere restituito il seguente messaggio di errore:
//"La password deve contenere almeno 2 numeri".
public class PwNumbers : PwRequirementValidator
{
    public override void Validate(string password, ValidationResult result)
    {
        if (password.Count(char.IsDigit) < 2)
        {
            Console.WriteLine("La password deve contenere almeno 2 numeri");
        }
        base.Validate(password, result);
    }
}

//4) La password deve contenere almeno una lettera maiuscola.
//Se non lo è, deve essere restituito il seguente messaggio di errore:
//"La password deve contenere almeno una lettera maiuscola".
public class PwUpperCase : PwRequirementValidator
{
    public override void Validate(string password, ValidationResult result)
    {
        if (!password.Any(char.IsUpper))
        {
            Console.WriteLine("La password deve contenere almeno una lettera maiuscola");
        }
        base.Validate(password, result);
    }
}

//5) La password deve contenere almeno un carattere speciale.
//Se non lo è, deve essere restituito il seguente messaggio di errore:
//"La password deve contenere almeno un carattere speciale".
public class PwSpecialChar : PwRequirementValidator
{
    public override void Validate(string password, ValidationResult result)
    {
        if (!Regex.IsMatch(password, @"[!@#$%^&*(),.?""{}|<>]"))
        {
            Console.WriteLine("La password deve contenere almeno un carattere speciale");
        }
        base.Validate(password, result);
    }
}


//3) La funzione di convalida deve gestire più errori di convalida contemporaneamente.
//Ad esempio, per "somepassword" dovrebbe restituire un messaggio di errore: "La password deve essere lunga almeno 8 caratteri\nLa password deve contenere almeno 2 numeri".
