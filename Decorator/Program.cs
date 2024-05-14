Console.OutputEncoding = System.Text.Encoding.UTF8;

// See https://aka.ms/new-console-template for more information

//Implementare un sistema di consegna per un negozio online che supporti diverse opzioni di consegna,
//come consegna standard (costo di 5,99€), consegna espressa (costo aggiuntivo di 9,99€),
//consegna con imballaggio regalo (costo aggiuntivo di 3,99€) e ritiro in negozio (gratuito). 
//Il costo totale della consegna deve essere calcolato in base alle opzioni selezionate dal cliente.


//Console.WriteLine("Hello, World!");

//Consegna consegnaStandard = new ConsegnaStandard();
//Console.WriteLine("Costo consegna standard: " + consegnaStandard.CalcolaCosto());

//Consegna consegnaEspressa = new ConsegnaEspressa(consegnaStandard);
//Console.WriteLine("Costo consegna espressa: " + consegnaEspressa.CalcolaCosto());

//Consegna consegnaImballaggioRegalo = new ConsegnaImballaggioRegalo(consegnaStandard);
//Console.WriteLine("Costo consegna con imballaggio regalo: " + consegnaImballaggioRegalo.CalcolaCosto());

//Consegna ritiroInNegozio = new RitiroInNegozio(consegnaStandard);
//Console.WriteLine("Costo ritiro in negozio: " + ritiroInNegozio.CalcolaCosto());


//Consegna consegnaEspressaRegalo = new ConsegnaImballaggioRegalo(consegnaEspressa);
//Console.WriteLine("Costo consegna espressa con imballaggio regalo: " + consegnaEspressaRegalo.CalcolaCosto());


//Console.WriteLine("///////////////////////////////////");

Consegna consegna = null;


Console.WriteLine("Seleziona l'opzione di consegna:");
Console.WriteLine("1. Ritiro in negozio (gratuito)");
Console.WriteLine("2. Consegna standard (5,99€)");
Console.WriteLine("3. Consegna espressa (+9,99€)");
Console.WriteLine("4. Consegna con imballaggio regalo (+3,99€)");
Console.WriteLine("5. Termina e calcola il costo totale");
Console.Write("Inserisci la tua scelta: ");

int scelta = int.Parse(Console.ReadLine());

switch (scelta)
{
    case 1:
        consegna = new RitiroInNegozio();
        break;
    case 2:
        consegna = new ConsegnaStandard(new RitiroInNegozio());
        break;
    case 3:
        consegna = new ConsegnaEspressa(new RitiroInNegozio());
        break;
    case 4:
        consegna = new ConsegnaImballaggioRegalo(new ConsegnaEspressa(new RitiroInNegozio()));
        break;
    default:
        Console.WriteLine("Scelta non valida. Riprova.");
        break;
};
Console.WriteLine($"La consegna selezionata è {consegna.Description} ed il costo è {consegna.CalcolaCosto()}.");



// classe abs
public abstract class Consegna
{
    public abstract double CalcolaCosto();
    public abstract string Description { get; }
}

// classe base 
public class RitiroInNegozio : Consegna
{
    public override string Description => "ritiro in negozio";

    public override double CalcolaCosto()
    {
        return 0;
    }
}
//decorator
public abstract class ConsegnaDecorator : Consegna
{
    private Consegna _consegna;

    public ConsegnaDecorator(Consegna consegna)
    {
        _consegna = consegna;
    }
    public override double CalcolaCosto()
    {
        return _consegna.CalcolaCosto();
    }
}

//consegne specifiche

public class ConsegnaStandard : ConsegnaDecorator
{
    public ConsegnaStandard(Consegna consegna) : base(consegna) { }

    public override string Description => "consegna standard";

    public override double CalcolaCosto()
    {
        return base.CalcolaCosto() + 5.99;
    }
}
public class ConsegnaEspressa : ConsegnaDecorator
{
    public ConsegnaEspressa(Consegna consegna) : base(consegna) { }

    public override string Description => "consegna espressa";

    public override double CalcolaCosto()
    {
        return base.CalcolaCosto() + 9.99;
    }
}


public class ConsegnaImballaggioRegalo : ConsegnaDecorator
{
    public ConsegnaImballaggioRegalo(Consegna consegna) : base(consegna) { }

    public override string Description => "consegna imballaggio regalo";

    public override double CalcolaCosto()
    {
        return base.CalcolaCosto() + 3.99;
    }
}







//consegna standard(costo di 5,99€), 
//consegna espressa(costo aggiuntivo di 9,99€),
//consegna con imballaggio regalo (costo aggiuntivo di 3,99€)
//e ritiro in negozio (gratuito). 


//Il costo totale della consegna deve essere calcolato in base alle opzioni selezionate dal cliente.
