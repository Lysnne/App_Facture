# App_Facture


### 1.1 Contexte  
Afin de simplifier la gestion des paiements et des clients dans un salon de coiffure, cette application a été développée pour centraliser la facturation, le suivi des paiements, et les informations des clients dans une interface simple et efficace.


###  Objectif  
- Créer des factures claires.
- Suivre les paiements effectués ou en attente.
- Gérer les informations des clients (ajout, modification).
- Générer un fichier PDF des factures.
- Envoyer les factures par e-mail.


### Portée  
L’application **ne gère pas** :
- Les stocks de produits.
- La prise ou la gestion de rendez-vous.

---

## 2. Technologies utilisées 

| Catégorie               | Outils / Technologies |
|------------------------|------------------------|
| Langage principal       | C# avec .NET 8.0       |
| Interface utilisateur   | WPF (Windows Presentation Foundation) |
| Architecture logicielle | MVVM (via CommunityToolkit.Mvvm) |
| Base de données         | SQLite avec EF Core    |
| ORM                     | Entity Framework Core  |
| PDF                     | QuestPDF pour générer les factures |
| Envoi de mail           | SmtpClient (System.Net.Mail) |
| IDE                     | Visual Studio 2022     |
| Contrôle de version     | GitHub           |

---



### Étapes :
```bash
git clone https://github.com/Ikasaidi/App_Facture.git
cd App_Facture
```


