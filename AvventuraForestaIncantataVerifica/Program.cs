using AvventuraForestaIncantataVerifica;

CGioco gioco = new CGioco();
bool auto = true; // di default

Console.Write("Automatico (a) o a step (s)? ");
if (Console.Read() == 's')
    auto = false;

do
{
    Console.WriteLine(gioco.GetRisultato());

    if (!auto)
        Console.ReadKey(true);
} while (gioco.Gioco());

Console.WriteLine(gioco.GetRisultato());