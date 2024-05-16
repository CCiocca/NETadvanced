// See https://aka.ms/new-console-template for more information
using System.ComponentModel;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;


Console.WriteLine("Hello, World!");


//## Requirement 1
//Write a method `greet(name)` that interpolates `name` in a simple greeting. For example, when `name` is `"Bob"`, the method should return a string `"Hello, Bob."`.


public abstract class GreetingHandler
{
    protected GreetingHandler? Next;
    public GreetingHandler SetNext(GreetingHandler next)
    {
        Next = next;
        return Next;
    }
    public virtual void Greet(string[] names)
    {
        Next?.Greet(names);
    }
}

public class SimpleGreeting : GreetingHandler 
{
    public override void Greet(string[] names)
    {
        if (names.Length ==1 && !string.IsNullOrEmpty(names[0]) && names[0].ToUpper() != names[0]) 
        {
            Console.WriteLine($"Hello, {names[0]}");
        }
        base.Greet(names);
    }
}

//## Requirement 2
//Handle nulls by introducing a stand-in.
//For example, when `name` is null, then the method should return the string `"Hello, my friend."`
public class NullGreeting : GreetingHandler
{
    public override void Greet(string[] names)
    {
        if (names.Length == 0 || (names.Length == 1 && !string.IsNullOrEmpty(names[0])) || names == null)
        {
            Console.WriteLine("Hello, my friend.");
        }
        base.Greet(names);
    }
}

//## Requirement 3
//Handle shouting. When `name` is all uppercase, then the method should shout back to the user. 
//For example, when `name` is `"JERRY"` then the method should return the string `"HELLO JERRY!"`
public class ShoutingGreeting : GreetingHandler
{
    public override void Greet(string[] names)
    {
        if (names.Length == 1 && (names[0].ToUpper() == names[0]))
        {
            Console.WriteLine($"HELLO {names[0]}!");
        }
        base.Greet(names);
    }
}


//## Requirement 4
//Handle two names of input. When `name` is an array of two names (or, in languages that support it, varargs or a splat), then both names should be printed.
//For example, when `name` is `["Jill", "Jane"]`, then the method should return the string `"Hello, Jill and Jane."`
public class TwoNamesGreeting : GreetingHandler
{
    public override void Greet(string[] names)
    {
        if (names.Length == 2)
        {
            Console.WriteLine($"Hello {names[0]} and {names[1]}!");
        }
        base.Greet(names);
    }
}
//## Requirement 5
//Handle an arbitrary number of names as input. When `name` represents more than two names, separate them with commas and close with an Oxford comma and "and".
//For example, when `name` is `["Amy", "Brian", "Charlotte"]`, then the method should return the string `"Hello, Amy, Brian, and Charlotte."`
public class MultipleNamesGreeting : GreetingHandler
{
    public override void Greet(string[] names)
    {
        if (names.Length > 2)
        {
            string allNamesExceptLast = string.Join(", ", names, 0, names.Length - 1);
            //string lastName = names[names.Length - 1];
            string lastName = names.Last();

            Console.WriteLine($"Hello {allNamesExceptLast} and {lastName}!");
        }
        base.Greet(names);
    }
}


//## Requirement 6
//Allow mixing of normal and shouted names by separating the response into two greetings.
//For example, when `name` is `["Amy", "BRIAN", "Charlotte"]`, then the method should return the string `"Hello, Amy and Charlotte. AND HELLO BRIAN!"`
public class MixedNormalAndShoutingGreeting : GreetingHandler
{
    public override void Greet(string[] names)
    {
        var normalNames = names.Where(name => name.ToUpper() != name).ToArray();
        var shoutedNames = names.Where(name => name.ToUpper() == name).ToArray();

        string normalNamesString = normalNames.Length > 0 ?
            $"Hello, {string.Join(", ", normalNames, 0, normalNames.Length - 1)}{normalNames.Last()}." : string.Empty;

        string shoutedNamesString = shoutedNames.Length > 0 ?
            $"HELLO, {string.Join(", ", normalNames, 0, normalNames.Length - 1)}{normalNames.Last()}." : string.Empty;


        //string greeting = normalNames != null && shoutedNames!= null
        //    ? $"Hello, {normalNamesString}. AND HELLO {shoutedNamesString}" 
        //    : normalNames != null && shoutedNames ==null 
        //        ? $"Hello, {normalNamesString}." 
        //        : normalNames == null && shoutedNames != null
        //            ? $"HELLO, {shoutedNamesString}" :

 
        if(normalNames != null)
            {
                if (shoutedNames != null)
                {
                Console.WriteLine($"Hello, {normalNamesString}. AND HELLO {shoutedNamesString}");
                }
                else
                {
                Console.WriteLine($"Hello, {normalNamesString}.");
                }
            }
        else
        {
            if (shoutedNames != null)
            {
                Console.WriteLine($"HELLO, {normalNamesString}.");
            }
            else
            {
                Console.WriteLine("insert a name");
            }
        }
  

        base.Greet(names);
    }
}

//## Requirement 7
//If any entries in `name` are a string containing a comma, split it as its own input.
//For example, when `name` is `["Bob", "Charlie, Dianne"]`, then the method should return the string `"Hello, Bob, Charlie, and Dianne."`.

//## Requirement 8
//Allow the input to escape intentional commas introduced by Requirement 7.
//These can be escaped in the same manner that CSV is, with double quotes surrounding the entry.
//For example, when `name` is `["Bob", "\"Charlie, Dianne\""]`, then the method should return the string `"Hello, Bob and Charlie, Dianne."`.



