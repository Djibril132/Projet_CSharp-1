using System;

class PierrePapierCiseaux
{
    static void Main(string[] args)
    {
        Random random = new Random();
        string[] options = { "Pierre", "Papier", "Ciseaux" };

        Console.WriteLine("Bienvenue dans le jeu Pierre-Papier-Ciseaux !");
        
        while (true)
        {
            Console.WriteLine("\nChoisissez entre Pierre, Papier ou Ciseaux (ou 'exit' pour quitter) : ");
            string choixUtilisateur = Console.ReadLine().Trim();

            
            if (choixUtilisateur.ToLower() == "exit")
            {
                Console.WriteLine("Merci d'avoir joué ! À bientôt.");
                break;
            }

            
            if (Array.Exists(options, option => option.ToLower() == choixUtilisateur.ToLower()))
            {
                int choixOrdinateur = random.Next(0, 3); 
                string choixOrdinateurStr = options[choixOrdinateur];
                Console.WriteLine($"L'ordinateur a choisi : {choixOrdinateurStr}");

                
                if (choixUtilisateur.ToLower() == choixOrdinateurStr.ToLower())
                {
                    Console.WriteLine("C'est une égalité !");
                }
                else if (
                    (choixUtilisateur.ToLower() == "pierre" && choixOrdinateurStr == "Ciseaux") ||
                    (choixUtilisateur.ToLower() == "ciseaux" && choixOrdinateurStr == "Papier") ||
                    (choixUtilisateur.ToLower() == "papier" && choixOrdinateurStr == "Pierre"))
                {
                    Console.WriteLine("Vous avez gagné !");
                }
                else
                {
                    Console.WriteLine("L'ordinateur a gagné !");
                }
            }
            else
            {
                Console.WriteLine("Choix invalide. Essayez de nouveau !");
            }
        }
    }
}