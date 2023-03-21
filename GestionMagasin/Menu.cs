using System.Security.Cryptography.X509Certificates;

namespace GestionMagasin;

public class Menu
{
    // Liste permettant de stocker des objets de type article créer.
    private static List<Article> StockArticle = new List<Article>();
    
    public static void Main(String[] args)
    {
        // int permettant de manipuler le menu
        int retourMenu = -1;

        while (retourMenu != 8)
        {
            Console.WriteLine("------ Menu ------");
            Console.WriteLine("-- 1) Rechercher un article par référence --");
            Console.WriteLine("-- 2) Ajouter un article au stock en vérifiant que la référence est unique --");
            Console.WriteLine("-- 3) Supprimer un article par référence --");
            Console.WriteLine("-- 4) Modifier un article par référence --");
            Console.WriteLine("-- 5) Rechercher un article par nom --");
            Console.WriteLine("-- 6) Rechercher un article par intervalle de prix de vente--");
            Console.WriteLine("-- 7) Afficher tous les articles --");
            Console.WriteLine("-- 8) Quitter --");

            retourMenu = int.Parse(Console.ReadLine());

            switch (retourMenu)
            {
                //permettre l'accès à la méthode affichant l'article en utilisant la référence de l'article
                case 1:
                    RechercherArticleReference(SaisirReference());
                    break;
                //permettre l'accès à la méthode qui ajoute un article et vérifie que sa reférence n'existe pas déjà
                case 2:
                    AjouterArticleUniqueReference(SaisirReference());
                    break;
                // permettre l'accès à la methode qui supprime un article en utilisant la reference comme point d'accès a l'objet article.
                case 3:
                    SupprimerArticleReference(SaisirReference());
                    break;
                // permettre l'accès à la méthode qui modifie un article en utilisant la reference
                case 4:
                    ModifierArticleReference(SaisirReference());
                    break;
                // permettre l'accès à la méthode affichant l'article en utilisant le nom de l'article
                case 5:
                    RechercherArticleNom(SaisirNom());
                    break;
                // permettre l'accès à la methode affichant les articles compris dans un intervalle que l'on renseigne.
                case 6:
                    RechercherArticleIntervalPrix(SaisirIntervalePrix());
                    break;
                //permettre l'affichage de tous les articles compris dans la liste stockArticles.
                case 7:
                    AfficherTousArticles();
                    break;
                //Permet de gérer la mauvaise saisie utilisateur dans le menu.
                default:
                    Console.WriteLine("La saisie n'est pas valide merci de saisir un chiffre en 1 et 8");
                    break;
            }
        }
    }

    /// <summary>
    /// Affichage d'un article par sa reference
    /// </summary>
    /// <param name="saisie">reference à afficher</param>
    public static void RechercherArticleReference(string saisie)
    {
        try
        {
            if (StockArticle.Count == 0)
            {
                Console.WriteLine(TableauVide());
            }
            else
            {
                foreach (Article article in StockArticle)
                {
                    if (article.Reference == saisie)
                    {
                        Console.WriteLine("------ Article ------");
                        Console.WriteLine($"-- Nom : {article.NomArticle} --\n" +
                                          $"-- Reference: {article.Reference} -- \n" +
                                          $"-- Prix : {article.PrixVente} --"); 
                    }
                }
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Le format saisie n'est pas le bon.");
        }
        catch (OverflowException)
        {
            Console.WriteLine("La valeur renseignée est trop grande.");
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("La valeur n'appartient pas à la plage de valeur attendue");
        }
    }    
    
    /// <summary>
    /// Création d'un article et ajout de l'article au stock(liste) en verifiant que la reference n'existe pas.
    /// </summary>
    /// <param name="saisie">La reférence de l'article à ajouter</param>
    public static void AjouterArticleUniqueReference(string saisie)
    {
        Article article = new Article();

        try
        {
            if (ArticleReference(saisie))
            {
                Console.WriteLine("La reférence existe déja");
            }
            else
            {
                article.Reference = saisie;
                article.NomArticle = SaisirNom();
                article.PrixVente = SaisirPrix();

                StockArticle.Add(article);
                
            }

        }
        catch (FormatException)
        {
            Console.WriteLine("Le format saisie n'est pas le bon.");
        }
        catch (OverflowException)
        {
            Console.WriteLine("La valeur renseignée est trop grande.");
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("La valeur n'appartient pas à la plage de valeur attendue");
        }
    }
    
    /// <summary>
    /// Suppression d'un article par le biais de sa reference
    /// </summary>
    /// <param name="saisie">Reference de l'article à supprimer</param>
    public static void SupprimerArticleReference(string saisie)
    {
        int indexSupprimmer = 0;

        try
        {
            if (StockArticle.Count == 0)
            {
                Console.WriteLine(TableauVide());
            }
            else if (ArticleReference(saisie))
            {
                for(int index = 0; index < StockArticle.Count -1; index++)
                {
                    if (ArticleReference(saisie))
                    {
                        indexSupprimmer = index;
                    }
                }
            }
            else
            {
                Console.WriteLine("La référence n'est pas connue");
            }
            StockArticle.RemoveAt(indexSupprimmer);
        }
        catch (FormatException)
        {
            Console.WriteLine("Le format saisie n'est pas le bon.");
        }
        catch (OverflowException)
        {
            Console.WriteLine("La valeur renseignée est trop grande.");
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("La valeur n'appartient pas à la plage de valeur attendue");
        }
    }
    
    /// <summary>
    /// Modification d'un article présent dans la liste en utilisant la reférence comme point d'accès à cet article
    /// </summary>
    /// <param name="saisie">Reference de l'article à modifier</param>
    public static void ModifierArticleReference(string saisie)
    {
        try
        {
            if (StockArticle.Count == 0)
            {
                Console.WriteLine(TableauVide());
            }
            else{
                for (int index = 0; index <= StockArticle.Count - 1; index++)
                {
                    if (ArticleReference(saisie))
                    {    
                        StockArticle[index].NomArticle = SaisirNom();
                        StockArticle[index].PrixVente = SaisirPrix();
                        break;
                    }
                }
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Le format saisie n'est pas le bon.");
        }
        catch (OverflowException)
        {
            Console.WriteLine("La valeur renseignée est trop grande.");
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("La valeur n'appartient pas à la plage de valeur attendue");
        }
        
    }
    /// <summary>
    /// Permettre l'affichage d'un article en utilisant le nom de l'article
    /// </summary>
    /// <param name="saisie">Nom de l'article a afficher</param>
    public static void RechercherArticleNom(string saisie)
    {
        try
        {
            if (StockArticle.Count == 0)
            {
                Console.WriteLine(TableauVide());
            }
            else
            {
                foreach (Article article in StockArticle)
                {
                    if (article.NomArticle == saisie)
                    {
                        Console.WriteLine("------ Article ------");
                        Console.WriteLine($"-- Nom : {article.NomArticle} --\n" +
                                          $"-- Reference: {article.Reference} -- \n" +
                                          $"-- Prix : {article.PrixVente} --"); 
                    }
                }
            }
        }catch (FormatException)
        {
            Console.WriteLine("Le format saisie n'est pas le bon.");
        }
    }
    
    /// <summary>
    /// Affichage du/des article(s) compris dans un interval de prix
    /// </summary>
    /// <param name="saisie">Fonction permettant de renseigner le prix min et max de l'interval</param>
    public static void RechercherArticleIntervalPrix(int[] saisie)
    {
        try
        {
            if (StockArticle.Count == 0)
            {
                Console.WriteLine(TableauVide());
            }
            else
            {
                Console.WriteLine($"------ Article compris entre {saisie[0]} et {saisie[1]} ------");
                
                foreach (Article article in StockArticle)
                {
                    if (article.PrixVente >= saisie[0] && article.PrixVente <= saisie[1])
                    {
                        Console.WriteLine("------ Article ------");
                        Console.WriteLine($"-- Nom : {article.NomArticle} --\n" +
                                          $"-- Reference: {article.Reference} -- \n" +
                                          $"-- Prix : {article.PrixVente} --"); 
                    }
                }      
                
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Le format saisie n'est pas le bon.");
        }
        catch (OverflowException)
        {
            Console.WriteLine("La valeur renseignée est trop grande.");
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("La valeur n'appartient pas à la plage de valeur attendue");
        }
    }
    /// <summary>
    /// Affichage du/des articles de la liste
    /// </summary>
    public static void AfficherTousArticles()
    {
        if (StockArticle.Count == 0)
        {
            Console.WriteLine(TableauVide());
        }
        else
        {
            foreach (Article article in StockArticle)
            {
                Console.WriteLine("------ Article ------");
                Console.WriteLine($"-- Nom : {article.NomArticle} --\n" +
                                  $"-- Reference: {article.Reference} -- \n" +
                                  $"-- Prix : {article.PrixVente} --");
            }       
        }
    }
    
    /// <summary>
    /// vérifie si une référence existe dans le stock.
    /// </summary>
    /// <param name="saisie"></param>
    /// <returns>Fonction permettant de renseigner la reference</returns>
    public static bool ArticleReference(string saisie)
    {
        if (StockArticle.Count == 0)
        {
            Console.WriteLine(TableauVide());
            return false;
        }
        else
        {
            
            foreach (Article articleList in StockArticle)
            {
                if (articleList.Reference == saisie)
                {
                    return true;
                }
            }
        }

        return false;
       
    }
    
    /// <summary>
    /// Permet la saisie de la référence
    /// </summary>
    /// <returns>récupère la saisie d'une référence</returns>
    public static string SaisirReference()
    {
        Console.WriteLine("-- Saisir la reference --");
        return Console.ReadLine();

    }
    
    /// <summary>
    /// permet la saisie d'un nom
    /// </summary>
    /// <returns>récupère la saisie d'un nom</returns>
    public static string SaisirNom()
    {
        Console.WriteLine("-- Saisir un Nom --");
        return Console.ReadLine();
    }
    
    /// <summary>
    /// permet de saisir le prix de vente d'un article
    /// </summary>
    /// <returns>Récupère la saisie</returns>
    public static int SaisirPrix()
    {
        Console.WriteLine("-- Saisir le prix de vente --");
        return int.Parse(Console.ReadLine());
    }
    
    /// <summary>
    /// permet de saisir un interval de prix
    /// </summary>
    /// <returns>un tableau comprenant la valeur du prix min et du prix max</returns>
    public static int[] SaisirIntervalePrix()
    {
        int[] result = new int[2];
        Console.WriteLine("-- Saisir prix min --");
        result[0] = int.Parse(Console.ReadLine());
        Console.WriteLine("-- Saisir le prix max --");
        result[1] = int.Parse(Console.ReadLine());

        return result;
    }
    
    /// <summary>
    /// Permet l'affichage d'une phrase
    /// </summary>
    /// <returns>string comme quoi la liste ne contient rien</returns>
    public static string TableauVide()
    {
        return "Le stock est vide d'article";
    }
    
}
