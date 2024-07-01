using Bogus;
using Bogus.DataSets;
using Lexicon.Api.Entities;
using Lexicon.Api.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Security.Policy;

namespace Lexicon.Api.Services;

public class DataGeneratorService(IUnitOfWork unitOfWork)
{
    public async Task GenerateDataAsync()
    {
        if (await unitOfWork.Users.CountAsync() == 0)
        {
            await GenerateUsersAsync();
            await GenerateCoursesAsync();
            await GenerateModulesAsync();
            await GenerateActivitiesAsync();
            await GenerateDocumentsAsync();
            await AddExampleUsersToCoursesAsync();
        }
    }

    private async Task GenerateDocumentsAsync()
    {
        var exampleTeacher = await unitOfWork.Users.GetAsync(1);
        foreach (var module in await unitOfWork.Modules.GetAllAsync())
        {
            for (var i = 0; i < 4; i++)
            {
                var document = new Faker<Document>("sv")
                    .RuleFor(d => d.Name, f => f.Hacker.Noun())
                    .RuleFor(d => d.Description, f => f.Hacker.Phrase())
                    .RuleFor(d => d.AddedBy, _ => exampleTeacher)
                    .RuleFor(d => d.Module, _ => module)
                    .RuleFor(d => d.Url, f => f.Internet.Url())
                    .RuleFor(d => d.TimeAdded, f => f.Date.Past())
                    .Generate();
                unitOfWork.Documents.Add(document);
            }
        }

        var Docmuents = new[]
        {
            // C# .net 2024 Course
            new Document
            {
                Name = "C# Lexicon LMS",
                Description = "",
                AddedBy = exampleTeacher,
                Url = "",
                TimeAdded = DateTime.Now
            },
            new Document
            {
                Name = "Industriell maskinpark –\r\nBlazor",
                Description = "",
                AddedBy = exampleTeacher,
                Url = "",
                TimeAdded = DateTime.Now
            },
            // The Complete Python Bootcamp From Zero to Hero in Python
            new Document
            {
                Name = "Läxa: Objektorienterad Programmering i Python",
                Description = "",
                AddedBy = exampleTeacher,
                Url = "",
                TimeAdded = DateTime.Now
            },
            new Document
            {
                Name = "Läxa: Fel och Undantag i Python",
                Description = "",
                AddedBy = exampleTeacher,
                Url = "",
                TimeAdded = DateTime.Now
            }
        };

        await unitOfWork.SaveAsync();
    }

    private async Task GenerateModulesAsync()
    {
        var faker = new Faker("sv");

        foreach (var course in await unitOfWork.Courses.GetAllAsync())
        {
            for (var i = 0; i < 4; i++)
            {
                var module = new Faker<Module>("sv")
                    .RuleFor(m => m.Course, _ => course)
                    .RuleFor(m => m.Name, f => f.Commerce.ProductName())
                    .RuleFor(m => m.Description, f => f.Commerce.ProductDescription())
                    .RuleFor(m => m.StartDate, f => f.Date.Past())
                    .RuleFor(m => m.EndDate, f => f.Date.Future())
                    // .RuleFor(m => m.Documents, f => faker.PickRandom(documents, 2).ToList())
                    .Generate();
                unitOfWork.Modules.Add(module);
            }
        }

        var Models = new[]
        {
            // C# .net 2024 Course
            new Module
            {
                Name = "C# Lexicon LMS",
                Description = "Ett Learning Management System (LMS) är en mjukvaruplattform som används för administration, dokumentation, spårning, rapportering, automatisering och leverans av utbildningskurser, utbildningsprogram eller inlärnings- och utvecklingsprogram. Ett LMS gör det möjligt för utbildningsinstitutioner, företag och organisationer att effektivt hantera och distribuera utbildningsinnehåll till sina användare.",
                StartDate = faker.Date.Past(),
                EndDate = faker.Date.Future(),
                // Documents = null
            },
            new Module
            {
                Name = "Industriell maskinpark –\r\nBlazor",
                Description = "Ett maskinparkshanteringssystem är en mjukvaruplattform som används för att övervaka och hantera en flotta av maskiner och fordon. Systemet gör det möjligt för företag att effektivt spåra, underhålla och optimera användningen av sina maskiner och fordon, vilket resulterar i förbättrad driftseffektivitet och minskade driftkostnader.",
                StartDate = faker.Date.Past(),
                EndDate = faker.Date.Future(),
                // Documents = null
            },
            new Module
            {
                Name = "Blazor och Grundläggande LINQ i C#",
                Description = "Ett maskinparkshanteringssystem är en mjukvaruplattform som används för att övervaka och hantera en flotta av maskiner och fordon. Systemet gör det möjligt för företag att effektivt spåra, underhålla och optimera användningen av sina maskiner och fordon, vilket resulterar i förbättrad driftseffektivitet och minskade driftkostnader. Genom att använda Blazor för frontend-utveckling och LINQ för datamanipulation i C# blir utvecklingen både modern och effektiv.",
                StartDate = faker.Date.Past(),
                EndDate = faker.Date.Future(),
                // Documents = null
            },

            // The Complete Python Bootcamp From Zero to Hero in Python
            new Module
            {
                Name = "Objektorienterad Programmering i Python",
                Description = "Objektorienterad programmering (OOP) är ett programmeringsparadigm som organiserar programvara genom att kombinera data och funktionalitet i enheter som kallas \"objekt\". Python är ett kraftfullt programmeringsspråk som stöder OOP, vilket gör det möjligt för utvecklare att skapa strukturerad och återanvändbar kod. OOP i Python underlättar design och utveckling av komplexa system genom att modulera problem i hanterbara och logiska enheter.",
                StartDate = faker.Date.Past(),
                EndDate = faker.Date.Future(),
                // Documents = null
            },
            new Module
            {
                Name = "Fel och Undantag i Python",
                Description = "Fel och undantag är en viktig del av programmering som hjälper utvecklare att hantera oförutsedda situationer och buggar som kan uppstå under körning av ett program. I Python finns det ett robust system för att fånga och hantera undantag, vilket förbättrar programmets tillförlitlighet och användarupplevelse.",
                StartDate = faker.Date.Past(),
                EndDate = faker.Date.Future(),
                // Documents = null
            },
            new Module
            {
                Name = "Fel och Undantag i Python 2 och Python Selenium",
                Description = "Fel och undantag är viktiga koncept inom programmering, som hjälper utvecklare att hantera oväntade situationer och buggar som kan uppstå under körning av ett program. Python 2 har ett system för att fånga och hantera undantag, vilket förbättrar programmets tillförlitlighet och användarupplevelse.\n" +
                "Selenium är ett kraftfullt verktyg för web automatisering. Det används för att automatisera webbläsare för att simulera en användares interaktioner med webbapplikationer. Selenium kan integreras med Python för att skriva testskript och automatiseringsskript.",
                StartDate = faker.Date.Past(),
                EndDate = faker.Date.Future(),
                // Documents = null
            }
            
        };

        await unitOfWork.SaveAsync();
    }

    private async Task GenerateActivitiesAsync()
    {
        var faker = new Faker("sv");

        foreach (var module in await unitOfWork.Modules.GetAllAsync())
        {
            for (var i = 0; i < 4; i++)
            {
                var activity = new Faker<Activity>("sv")
                    .RuleFor(a => a.Module, _ => module)
                    .RuleFor(a => a.Name, f => f.Name.JobTitle())
                    .RuleFor(a => a.Description, f => f.Name.JobDescriptor())
                    .RuleFor(a => a.Type, f => f.PickRandom<ActivityType>())
                    .RuleFor(a => a.StartDate, f => f.Date.Past())
                    .RuleFor(a => a.EndDate, f => f.Date.Future())
                    // .RuleFor(a => a.Documents, f => faker.PickRandom(documents, 1).ToList())
                    .Generate();
                unitOfWork.Activities.Add(activity);
            }
        }
        var Activities = new[]
        {
            // C# .net 2024 Course
            new Activity
            {
                Name = "Läxa: C# Lexicon LMS",
                Type = ActivityType.Assignment,
                Description = "Lexicon LMS\r\n" +
                "Projektet ni skall arbeta med under den avslutande modulen är en lärplattform, ett så\r\n" +
                "kallat LMS1 (Learning Management System), anpassat för Lexicons\r\npåbyggnadsutbildningar. " +
                "Ett LMS förenklar och centraliserar kommunikationen mellan\r\nlärare, lärosäte och elev genom att samla schema, " +
                "kursmaterial, övrig information,\r\növningsuppgifter och inlämningar på ett och samma ställe.\r\n" +
                "Ni skall från grunden producera systemet med databas, back-end funktionalitet och ett\r\n" +
                "genomtänkt frontend. Detta kallas ett ”full-stack projekt” och syftar till att visa upp er\r\n" +
                "förståelse för samtliga delar av en webbapplikation och nutida system. Projektet ämnar\r\n" +
                "att testa bredden av er förståelse och att ni har en grund att stå på oavsett framtida\r\n" +
                "inriktning inom .NET.\r\nProduktbeskrivning\r\nSystemet vi skall bygga har som främsta uppgift och mål att enkelt " +
                "tillgängliggöra\r\nkursmaterial och schema för elever. Det skall även fungera som en samlingsplats för\r\n" +
                "inlämningsuppgifter. För att detta skall vara möjligt behöver vi även smidig funktionalitet\r\n" +
                "för lärare att enkelt kunna administrera dessa klasser, elever, scheman och dokument. För\r\n" +
                "om det inte är enkelt för läraren att använda verktyget, så kommer eleverna aldrig få\r\n" +
                "chansen att använda det.\r\nDet färdiga systemet är ämnat att framför allt täcka grundläggande funktionalitet, men på\r\n" +
                "ett genomtänkt och genomarbetat sätt. Less is more är ofta sant när det gäller denna\r\n" +
                "typ av applikationer som skall användas dagligen. Tyvärr, för att nå en så bred marknad\r\n" +
                "som möjligt är de flesta LMS som finns tillgängliga idag enormt tunga och överbelamrade av\r\n" +
                "all tänkbar funktionalitet som man sällan har användning för - detta skall ni ändra på! Less is\r\n" +
                "more behöver inte nödvändigtvis syfta till ren funktionalitet, utan snarare om upplevd\r\n" +
                "komplexitet. Det får gärna finnas djup funktionalitet, men användaren skall inte behöva 14\r\n" +
                "alternativ i varje val den gör.\r\nRamverk och tekniker\r\n" +
                "Applikationen skall ha vara byggd i Blazor Web App. Databasen skall byggas med Entity\r\n" +
                "Framework Core enligt code first-metoden. Frontend skall använda Bootstrap 5.\r\n" +
                "1 Mer information: https://sv.wikipedia.org/wiki/Lärplattform\r\n" +
                "Entiteter, relationer och attribut (grundform)\r\n" +
                "Nedan beskrivna entiteter och attribut är ett minimum, inte en absolut beskrivning.\r\n" +
                "Framför allt attributen kommer behöva byggas ut när ni i närmare detalj planerar\r\nsystemet.\r\n" +
                "Användare\r\nApplikationen skall hantera användare i rollerna av elever och lärare, dessa skall alla ha\r\n" +
                "inloggningar och konton i applikationen. Användarna bör sparas med minst ett namn och\r\nen e-postadress.\r\n" +
                "Kurs\r\nAlla elever tillhör en kurs och endast en som i sin tur har ett kursnamn, en beskrivning och\r\n" +
                "ett startdatum. Exempel på kursnamn: “.NET 2024”.\r\nModul\r\n" +
                "Varje kurs läser en eller flera moduler, dessa har modulnamn, en beskrivning, startdatum\r\n" +
                "och slutdatum. Exempel på moduler: “Databasdesign”, “Javascript” osv.\r\n" +
                "Moduler får inte överlappa varandra eller gå utanför kursen.\r\nAktiviteter\r\n" +
                "Modulerna i sin tur har aktiviteter, dessa aktiviteter kan vara e-learning pass, föreläsningar,\r\n" +
                "övningstillfällen, inlämningsuppgifter eller annat. Aktiviteterna har en typ, ett namn, en\r\n" +
                "start/sluttid och en beskrivning.\r\nAktiviteter får inte överlappa varandra eller gå utanför modulen.\r\n" +
                "Dokument\r\nAlla entiteter ovan kan hålla dokument.\r\nExempel på dokument:\r\n" +
                "Inlämningsuppgifter från eleverna, moduldokument, generella\r\n" +
                "informationsdokument för kursen, föreläsningsunderlag eller övningsuppgifter\r\n" +
                "kopplade till aktiviteterna.\r\nDen (dessa) dokumententitet(er) bör ha ett namn, en beskrivning, en tidsstämpel för\r\n" +
                "uppladdning tillfället samt information om vilken användare som laddat upp filen.\r\nUse-cases (minimalt krav)\r\n" +
                "Dessa use-cases är inte heltäckande; beroende på implementation måste mer detaljerade\r\n" +
                "fall tas fram för att täcka in all praktisk funktionalitet.\r\nEn icke inloggad besökare skall kunna:\r\n" +
                "● Logga in\r\nEn elev skall kunna:\r\n● Se vilken kurs denne läser och vilka de andra kursdeltagarna är\r\n" +
                "● Se vilka moduler denne läser\r\n● Se aktiviteterna för en specifik modul (modulschema).\r\nEn lärare skall kunna:\r\n" +
                "● Se alla kurser\r\n● Se alla moduler som ingår i en kurs\r\n● Se alla aktiviteter en modul innehåller\r\n" +
                "● Skapa och redigera användare (lärare och elever)\r\n● Skapa och redigera kurser\r\n● Skapa och redigera moduler\r\n" +
                "● Skapa och redigera aktiviteter\r\nUse-cases (önskvärda)\r\nEn elev skall kunna:\r\n" +
                "● Se om en specifik modul eller aktivitet har några dokument kopplade till sig och i\r\n" +
                "sådant fall ladda ned dessa.\r\n● Se vilka inlämningsuppgifter denne har fått, om den redan är inlämnad, när den\r\n" +
                "senast skall lämnas in och om den är försenad.\r\n● Ladda upp filer som inlämningsuppgifter\r\nEn lärare skall kunna:\r\n" +
                "● Ladda upp dokument för kurser/moduler/aktiviteter\r\n● Ta emot inlämningsuppgifter\r\nUse-cases (extra om tid finns)\r\n" +
                "En icke inloggad besökare skall kunna:\r\n● Begära nytt lösenord\r\nEn elev skall kunna:\r\n● Dela dokument med sin kurs eller modul" +
                "\r\n● Få notifieringar när en lärare lagt upp nya dokument för kursen\r\n● Ta emot feedback på inlämningsuppgifter\r\n" +
                "● Få notifieringar när en lärare har lämnat feedback på en inlämningsuppgift\r\n" +
                "● Registrera sig själv efter ha fått en inbjudan via mejl\r\n" +
                "● Skall kunna ta bort sig från systemet samt radera all information om sig som finns\r\nsparad enligt GDPR\r\n" +
                "En lärare skall kunna:\r\n● Ge feedback på inlämningsuppgifter\r\nAPI\r\n" +
                "För att göra systemet mer flexibelt lägger vi all data access i ett API. Det medför att vi\r\n" +
                "enkelt kan återanvända den implementationen om vi har behov av att skapa andra typer av\r\n" +
                "applikationer som en mobil app eller en byta ut vår frontend mot exempelvis React.\r\n" +
                "Applikationen hämtar sin data från APIet via HttpClient.\r\nFrontend – Blazor\r\n" +
                "Blazor Web App-templatet ska användas, med följande inställningar:\r\n" +
                "Vi lägger även till autentisering i form av “Individual Accounts”.\r\n" +
                "(Ni väljer själva om ni vill köra med eller utan top-level statements)\r\n" +
                "Utöver detta behöver ni lägga till ett API-projekt manuellt så att det ser ut ungefär\r\nså här:\r\n" +
                "Önskemålet är att frontend visuellt har ett enhetligt utseende. Gränssnittet ska vara byggt\r\n" +
                "med Bootstrap. Titta på de komponenter som finns färdiga!\r\n" +
                "Utöver dessa rent estetiska önskemål skall resterande frontend fokus riktas mot\r\n" +
                "användarupplevelsen och att minska användarens kognitiva friktion.\r\n" +
                "Systemet ska vara lättanvänt. Och det ska vara tydligt hur det fungerar.\r\n" +
                "Applikationen skall vara responsiv. Bonuspoäng för en välfungerande mobilversion.\r\n" +
                "Arbetssätt\r\nScrum\r\nProjektet skall utföras i grupp med ett scrum-baserat arbetssätt. Vi kommer att arbeta med två\r\n" +
                "sprintar. En ny sprint inleds med en sprintplanering där ni sätter upp en sprint-backlog, fördelar\r\n" +
                "arbetet och uppdaterar er task-board.\r\nVarje dag inleds med en standup (daily scrum) där ni kort, en och en, avhandlar\r\n" +
                "1. Vad ni gjort sedan förra standup,\r\n2. Vad ni planerar att göra fram till nästa och\r\n" +
                "3. Om det är något som blockerar planerat arbete.\r\nNi håller mötet i Teams appen i gruppens kanal. " +
                "Ni ska ha er task-board framme så ni\r\nvisuellt kan se hur ni ligger till.\r\n" +
                "När sprinten är färdig avslutar ni den med en sprintdemo inför lärarna följt av ett\r\n" +
                "retrospektiv.\r\nGit - Versionshantering\r\nProjektet skall versionshanteras med git och GitHub.\r\n" +
                "Ni ska minst ha varsin personlig branch samt en master och en development. Vi\r\n" +
                "rekommenderar att ni i det här projektet börjar arbeta med feature brancher istället för en\r\n" +
                "personlig branch.\r\nTest (önskvärt)\r\nTesta gärna att skriva test. Inget krav men kan vara otroligt lärorikt.\r\n" +
                "Plus för att skriva testen först enligt TDD.\r\nAvstämningspunkter och leverabler\r\n" +
                "Under projektets gång förväntas ni redovisa vissa moment innan ni fortsätter. Detta för att\r\n" +
                "undvika återvändsgränder och maximera er effektiva utvecklingstid.\r\n" +
                "● Produkt-backlog skall godkännas innan ni startar implementation.\r\n" +
                "● ER-Diagram skall godkännas innan ni startar en implementation.\r\n" +
                "● Wireframes för några nyckelvyer skall godkännas innan ni startar en\r\nimplementation.\r\n" +
                "● Sprint-backlog skall godkännas innan ni startar en ny sprint.\r\n" +
                "● Vid avslutad sprint skall alla leveransklara ändringar demonstreras vid sprintdemo.\r\n" +
                "Planering\r\nUnder projektets uppstart ska ni börja arbeta med att planera arbetet. Ni ska ta fram\r\n" +
                "dokumentation enligt punkterna i avstämningspunkter och leverabler.\r\n" +
                "Fundera på hur systemet ska fungera för en lärare. Vad är det primära en lärare är\r\n" +
                "intresserad av? Hur skapas ett bra flöde för att skapa upp nya kurser med allt en sådan\r\n" +
                "består av?\r\nVad är ni som elever mest intresserade av när ni loggar in?\r\n" +
                "Vilken information är mest relevant att få åtkomst till direkt?\r\n" +
                "Här handlar det enbart om hur ni presenterar den data som finns lagrad i systemet.\r\n" +
                "Hur ska ni navigera till all funktionalitet osv.\r\nRedovisning\r\n" +
                "Detaljerad information om redovisningsmomentet kommer senare\r\nLycka till!",
                // Documents = null
            },
            new Activity
            {
                Name = "Läxa: Industriell maskinpark –\r\nBlazor",
                Type = ActivityType.Assignment,
                Description = "Övning 16 – Industriell maskinpark –\r\nBlazor\r\n" +
                "Du ska i denna laboration skapa en Blazor Web App. Applikation kommer även användas i andra\r\n" +
                "laborationer framöver så det är viktigt att du genomför denna laboration.\r\n" +
                "Sidan kommer vara ett Management System för en fiktiv industriell maskinpark. Den är till för\r\n" +
                "supporttekniker som ska administrera och övervaka maskinparken.\r\n" +
                "Steg 1 – Bygg upp Blazorsidan\r\n" +
                "Bygg själva managementsidan som listar upp alla industriella maskiner samt information om dem.\r\n" +
                "Informationen ska vara maskinens namn, id (GUID), aktuell status (online/offline) samt senaste data\r\n" +
                "som skickats från maskinen. Det ska även finnas en knapp som ska kunna skicka data till maskinen.\r\n" +
                "På valfritt ställe på sidan ska det finnas en komponent som är synlig hela tiden där man visar upp lite\r\n" +
                "statistik om maskinparken. Exempel på data kan vara antal maskiner, antal maskiner som är online,\r\n" +
                "senast editerad maskin etc. Sidan ska också ha en navbar som minst har en företagslogga och länk\r\n" +
                "till listan av maskiner.\r\nExempel på hur detta kan se ut:\r\nSteg 2 – Backendfunktionalitet\r\n" +
                "Annan funktionalitet som ska finnas i programmet (ok att skriva ett Mock Api till en början eller\r\n" +
                "jobba med en in-memory collection):\r\n Hämta och visa upp alla maskiner.\r\n Starta och stäng av maskiner.\r\n" +
                " Uppdatera data i maskinen.\r\n Lägga till och ta bort maskiner.",
                // Documents = null
            },
            new Activity
            {
                Name = "E-learning",
                Type = ActivityType.ELearning,
                Description = "PluralSight"
                // Documents = null
            },
            new Activity
            {
                Name = "Lecture",
                Type = ActivityType.Lecture,
                Description = "Blazor" 
                // Documents = null
            },
             new Activity
            {
                Name = "Läxa: Grundläggande LINQ i C#",
                Type = ActivityType.Assignment,
                Description = "#### Uppgift 1: Skapa en Lista\r\n1. Skapa en ny C#-konsolapplikation i Visual Studio.\r\n2. Skapa en klass `Person` med följande egenskaper:\r\n   - `Name` (string)\r\n   - `Age` (int)\r\n\r\n3. Skapa en lista av `Person`-objekt med minst fem olika personer med olika namn och åldrar.\r\n\r\n#### Uppgift 2: Använd LINQ för att Hämta Data\r\n1. Använd LINQ för att hitta alla personer som är äldre än 25 år. Skriv ut deras namn och åldrar till konsolen.\r\n2. Använd LINQ för att hitta personen med det längsta namnet. Skriv ut namnet och åldern till konsolen.\r\n\r\n#### Uppgift 3: Sortera och Gruppera Data\r\n1. Använd LINQ för att sortera listan av personer efter ålder i stigande ordning. Skriv ut den sorterade listan till konsolen.\r\n2. Använd LINQ för att gruppera personerna efter deras ålder. Skriv ut varje grupp och personerna i varje grupp till konsolen.\r\n\r\n#### Uppgift 4: Använd LINQ för att Transformera Data\r\n1. Använd LINQ för att skapa en ny lista av strängar som innehåller personernas namn i versaler. Skriv ut denna lista till konsolen.\r\n2. Använd LINQ för att skapa en ny lista av anonyma objekt som innehåller `Name` och en boolsk egenskap `IsAdult` (true om `Age` är 18 eller äldre, annars false). Skriv ut denna lista till konsolen.\r\n\r\n#### Bonusuppgift: Avancerad Fråga\r\n1. Använd LINQ för att hitta den genomsnittliga åldern av alla personer i listan. Skriv ut det genomsnittliga värdet till konsolen.\r\n2. Använd LINQ för att hitta alla personer vars namn börjar med bokstaven 'A'. Skriv ut deras namn och åldrar till konsolen.\r\n\r\n---\r\n\r\nLycka till med läxan!"
                // Documents = null
            },
            // The Complete Python Bootcamp From Zero to Hero in Python
            new Activity
            {
                Name = "Läxa: Objektorienterad Programmering i Python",
                Type = ActivityType.Assignment,
                Description =
                "### Läxa: Objektorienterad Programmering i Python\r\n\r\n" +
                "#### Uppgift 1: Skapa en Klass\r\n1. Skapa en klass som representerar en bok. Klassen ska ha följande egenskaper:\r\n" +
                "   - `title` (str): Bokens titel.\r\n   - `author` (str): Författarens namn.\r\n   - `year` (int): Utgivningsår.\r\n\r\n" +
                "2. Lägg till en metod i klassen som heter `book_info` " +
                "som returnerar en sträng med information om boken i formatet: \"Titel: [title], Författare: [author], År: [year]\".\r\n\r\n" +
                "#### Uppgift 2: Arv och Polymorfism\r\n1. Skapa en subklass till `Book` som heter `EBook`. Denna klass ska ha en extra egenskap:" +
                "\r\n   - `file_size` (float): Storleken på filen i MB.\r\n\r\n2. Överskugga metoden `book_info` i `EBook` " +
                "för att även inkludera information om filstorleken.\r\n\r\n#### Uppgift 3: Användning av Klasser\r\n" +
                "1. Skapa en lista med olika bokobjekt (både `Book` och `EBook`).\r\n" +
                "2. Skriv en funktion som tar en lista av böcker och skriver ut informationen för varje bok med hjälp av metoden `book_info`." +
                "\r\n\r\n#### Bonusuppgift: Hantera Undantag\r\n1. Lägg till felhantering i `__init__`-metoden " +
                "för att säkerställa att `year` och `file_size` (om det finns) är av korrekt datatyp och inom rimliga värden." +
                "\r\n\r\n---\r\n\r\nLycka till med läxan!",
                // Documents = null
            },
            new Activity
            {
                Name = "Läxa: Fel och Undantag i Python",
                Type = ActivityType.Assignment,
                Description =
                "### Läxa: Fel och Undantag i Python\r\n\r\n#### " +
                "Uppgift 1: Grundläggande Undantagshantering\r\n1. " +
                "Skriv en funktion `divide` som tar två argument `a` och `b` och returnerar resultatet " +
                "av `a` dividerat med `b`.\r\n2. Lägg till undantagshantering för att hantera fallet där " +
                "`b` är noll. Om detta inträffar ska funktionen returnera en lämplig felmeddelande.\r\n\r\n#### " +
                "Uppgift 2: Hantera Flera Undantag\r\n1. Skriv en funktion `open_file` som tar ett " +
                "filnamn som argument och försöker öppna filen.\r\n2. Lägg till undantagshantering för att " +
                "hantera fallet där filen inte finns (FileNotFoundError) och fallet där det är problem med att " +
                "läsa filen (IOError). Funktionen ska returnera lämpliga felmeddelanden för varje typ av fel.\r\n\r\n" +
                "#### Uppgift 3: Egna Undantag\r\n1. Skapa en egen undantagsklass `NegativeNumberError` som ärvs från `Exception`.\r\n" +
                "2. Skriv en funktion `check_positive` som tar ett tal som argument och kastar `NegativeNumberError` " +
                "om talet är negativt. Annars ska funktionen returnera talet.\r\n\r\n" +
                "#### Uppgift 4: Användning av `try`, `except`, `else` och `finally`\r\n" +
                "1. Skriv en funktion `process_numbers` som tar en lista av tal som argument.\r\n" +
                "2. I funktionen ska du använda `try`, `except`, `else` och `finally` för att:\r\n" +
                "   - Försöka iterera genom listan och skriva ut kvadraten av varje tal.\r\n" +
                "   - Hantera eventuella TypeErrors (t.ex. om listan innehåller en sträng istället för ett tal).\r\n" +
                "   - Om inga undantag inträffar, skriv ut ett meddelande om att alla tal har bearbetats framgångsrikt.\r\n" +
                "   - Slutligen, oavsett om ett undantag inträffade eller inte, skriv ut ett meddelande om att funktionen är klar.\r\n\r\n" +
                "#### Bonusuppgift: Nestade Undantag\r\n1. Skriv en funktion `nested_exception` som tar två listor av tal som argument.\r\n" +
                "2. I funktionen ska du försöka iterera genom båda listorna och för varje par av tal (ett från varje lista) försöka dividera dem." +
                "\r\n3. Lägg till undantagshantering för både ZeroDivisionError och IndexError. " +
                "Om något av dessa undantag inträffar, skriv ut ett lämpligt felmeddelande och fortsätt med nästa par.\r\n\r\n---\r\n\r\n" +
                "Lycka till med läxan!",
                // Documents = null
            },
            new Activity
            {
                Name = "E-Learning",
                Type = ActivityType.ELearning,
                Description = "PluralSight"
                // Documents = null
            },
            new Activity
            {
                Name = "Lecture",
                Type = ActivityType.Lecture,
                Description = "Python Selenium"
                // Documents = null
            },
            new Activity
            {
                Name = "Lecture",
                Type = ActivityType.Lecture,
                Description = "Python object oriented"
                // Documents = null
            },
        };

        await unitOfWork.SaveAsync();
    }

    private async Task GenerateCoursesAsync()
    {
        var faker = new Faker("sv");
        var allUsers = await unitOfWork.Users.GetAllAsync();
        var availableUsers = new List<User>(allUsers);
        var courses = new[]
        {
            new Course
            {
                Name = "C# .net 2024",
                Description = "I en unik blended learning-lösning får du både personlig handledning\r\n" +
                "från professionella IT-utbildare och kommer även att lära dig genom\r\npraktiska laborationer, övningsuppgifter och självstudier. " +
                "Du får kunskaperna och färdigheterna i programmering för att kunna utveckla\r\nIT-system, applikationer med mera." +
                "\r\nDetta kan bli starten på, eller fortsättningen av, en spännande och\r\n" +
                "givande karriär med långsiktigt mycket goda karriärmöjligheter!\r\n" +
                "ÄR DU MORGONDAGENS UTVECKLARE OCH DESIGNER\r\nAV C#/.NET- APPAR, SYSTEM OCH API:er?\r\n" +
                "UTBILDNING\r\n.NET FULLSTACK\r\nSYSTEMUTVECKLARE\r\n" +
                "Att komma igång med kodning är enkelt, men för en professionell roll behöver man kunna bemästra detta på en högre\r\n" +
                "nivå. Denna utbildning låter dig ta steget till bli en professionell systemutvecklare, med de kunskaper och verktyg du\r\n" +
                "behöver.\r\nKONTAKT OCH MER INFORMATION\r\nMäret Wahlbom\r\n08-511 611 00\r\ninfoaf@lexicon.se\r\nÅrstaängsgatan 9, Stockholm\r\n" +
                ".NET-UTBILDNING\r\nINRIKTNING MOT SYSTEMUTVECKLARE FÖR SÖKANDE\r\nMED TIDIGARE ERFARENHET AV SYSTEMUTVECKLING\r\n" +
                "ELLER MOTSVARANDE KUNSKAPER\r\nSÅ GÅR DET TILL\r\nUtbildningen är kostnadsfri och du kvalificeras genom din lokala\r\n" +
                "Arbetsförmedling, som bland annat beaktar dina tidigare kunskaper\r\n" +
                "inom IT, svenska och engelska. Tester kommer att genomföras för att\r\n" +
                "bedöma dina kompetenser. Utbildningen genomförs på heltid i cirka 5\r\n" +
                "månader med flexibelt lärande, där olika utbildningsformer kan kombineras med stor fokus på praktisk övning. " +
                "Utbildningen följs upp med\r\nen 12 veckors praktik hos organisationer som kan ta emot eller är i\r\n" +
                "behov av systemutvecklare.\r\nVEM PASSAR DET FÖR?\r\nUtbildningen och systemutvecklar-yrket passar för personer som\r\n" +
                "antingen har viss bakgrund inom IT eller ett starkt intresse. Eller erfarenhet eller kunskap inom ett IT-område. " +
                "Innehållet är även lämpligt\r\nför dem som redan har en god förståelse för programmering och vill\r\nbli ännu mer kunniga.\r\n" +
                "DET HÄR INNEHÅLLER DEN\r\nInnehåll och upplägg kan variera eftersom utbildningen anpassas efter\r\n" +
                "rekryterande organisationers behov, men detta är en vägledning för\r\n" +
                "vad du kommer att göra under den fem månader långa utbildningen:\r\n" +
                "• C# GRUND: OOP, Generics, Linq\r\n• FRONTEND: HTML5, CSS3, JavaScript, Bootstrap\r\n• " +
                "DATABAS: SQL, Databasmodellering, EntityFrameworkCore, Database-och CodeFirst, No SQL\r\n• " +
                "TEST: TDD, XUnit, Moq\r\n• ASP.NETCORE: Razor, HttpClient, Automapper, Auth\r\n" +
                "• INTEGRATION: Minimal-, Web-, REST-, Open-API,Swagger\r\n" +
                "• PROJEKT: Scrumprojektmetodik, BlazorWASM, Identityserver, Git,\r\nASP .NET Core\r\n" +
                "• AZURE: SQL Database, Web Apps, functions, CI/CD med Azure\r\nDevOps, Cosmos DB.\r\n" +
                "En utbildning via Lexicon, Sveriges största företagsutbildare. På uppdrag av Arbetsförmedlingen.",
                StartDate = faker.Date.Past(),
                EndDate = faker.Date.Future(),
                // Documents = null
            },
            new Course
            {
                Name = "The Complete Python Bootcamp From Zero to Hero in Python",
                Description = "Become a Python Programmer and learn one of employer's most requested skills of 2023!\r\n\r\n" +
                "This is the most comprehensive, yet straight-forward, course for the Python programming language on Lexicon! " +
                "Whether you have never programmed before, already know basic syntax, or want to learn about the advanced features of Python, " +
                "this course is for you! In this course we will teach you Python 3.\r\n\r\n" +
                "With over 100 lectures and more than 21 hours of video this comprehensive course leaves no stone unturned! " +
                "This course includes quizzes, tests, coding exercises and homework assignments as well as 3 major projects to create a " +
                "Python project portfolio!\r\n\r\nLearn how to use Python for real-world tasks, such as working with PDF Files, sending emails, " +
                "reading Excel files, Scraping websites for informations, working with image files, and much more!\r\n\r\n" +
                "This course will teach you Python in a practical manner, with every lecture comes a full coding " +
                "screencast and a corresponding code notebook! Learn in whatever manner is best for you!\r\n\r\n" +
                "We will start by helping you get Python installed on your computer, regardless of your operating system, " +
                "whether its Linux, MacOS, or Windows, we've got you covered.\r\n\r\nWe cover a wide variety of topics, including:\r\n\r\n" +
                "Command Line Basics\r\n\r\nInstalling Python\r\n\r\nRunning Python Code\r\n\r\n" +
                "Strings\r\n\r\nLists \r\n\r\nDictionaries\r\n\r\nTuples\r\n\r\nSets\r\n\r\nNumber Data Types\r\n\r\n" +
                "Print Formatting\r\n\r\nFunctions\r\n\r\nScope\r\n\r\nargs/kwargs\r\n\r\nBuilt-in Functions\r\n\r\n" +
                "Debugging and Error Handling\r\n\r\nModules\r\n\r\nExternal Modules\r\n\r\nObject Oriented Programming\r\n\r\n" +
                "Inheritance\r\n\r\nPolymorphism\r\n\r\nFile I/O\r\n\r\nAdvanced Methods\r\n\r\nUnit Tests\r\n\r\nand much more!\r\n\r\n" +
                "You will get lifetime access to over 100 lectures plus corresponding Notebooks for the lectures!\r\n\r\n" +
                "This course comes with a 30 day money back guarantee! If you are not satisfied in any way, you'll get your money back. " +
                "Plus you will keep access to the Notebooks as a thank you for trying out the course!\r\n\r\n" +
                "So what are you waiting for? Learn Python in a way that will advance your career and increase your knowledge, " +
                "all in a fun and practical way!",
                StartDate = faker.Date.Past(),
                EndDate = faker.Date.Future(),
                // Documents = null
            },
            new Course
            {
                Name = "ElasticSearch 101",
                Description = "Hi\r\n\r\nWelcome to my course ELASTICSEARCH 101, a beginners guide to elastic stack , " +
                "this is a hands on course with focus on deployment and management of elastics stack.\r\n\r\n\r\n\r\n" +
                "This course adopts a hands on approach towards learning and is focused towards\r\n\r\n" +
                "DevOps professionals who will be responsible to deploy and manage elasticsearch clusters\r\n\r\n" +
                "IT Support engineers that will be responsible for supporting elastic stack deployments\r\n\r\n" +
                "and it might benefit software developers if they need an understanding of how " +
                "Elasticsearch clusters are deployed and managed\r\n\r\n\r\n\r\n" +
                "In this course , we will start with some basics such as what are the different types of data that you will " +
                "encounter in an enterprise environment and then we will look at the difference between relational and " +
                "non-relational databases before moving on to have an understanding of JSON and Rest APIs.\r\n\r\n\r\n\r\n" +
                "After you have the pre-requisite knowledge which is essential if you want to work on elastic search we will " +
                "look at elastic search and elastic stack from a birds eye view and then it is time to get your hands dirty, " +
                "first we will setup our lab environment and then we will look at how you can provision an elastic search cluster in " +
                "different ways , we will follow this up by adding some data into our cluster through kibana integrations and have a " +
                "general feel of kibana. Next , we will look at Logstash an essential component of elastic stack if you want to transform " +
                "your data and ingest some csv data by using Logstash pipelines.\r\n\r\n\r\n\r\n" +
                "In the final section we will look at what's changed in elasticsearch version 8 and then deploy v8 cluster on a single node." +
                "\r\n\r\n\r\n\r\nFinally , we will install elastic agent which is a single agent needed to ship any type of " +
                "logs from your applications servers to Elasticsearch.",
                StartDate = faker.Date.Past(),
                EndDate = faker.Date.Future(),
                // Documents = null
            },
            new Course
            {
                Name = "MS SQL with C#",
                Description = "Hello there,\r\n\r\nWelcome to the Complete \"MS SQL with C#\" course\r\n" +
                "Learn C#, MS SQL Server, Ms SQL Backup & Recovery, how to connect C# to SQL, and more with this comprehensive SQL course\r\n\r\n" +
                "Are you looking for the right language to complete SQL depending on your interests, work demands, or career aspirations?\r\n" +
                "If you really want to develop real-world applications with C #, you need to support it with a database C# and other Net " +
                "languages can work together with most databases but most commonly pair MS SQL\r\n\r\n" +
                "In this course, you will learn the programming language and database The most important reason why we want to " +
                "prepare this course is that other courses only cover half of the event You can connect your C# application to data in a " +
                "SQL Server database using the NET Framework Data Provider for SQL Server The first step in a C# " +
                "application is to create an instance of the Server object and to establish its connection to an instance of Microsoft SQL Server" +
                "\r\n\r\nWhen I started this course I had no knowledge of SQL, but now I can claim I have all the basics to further succeed " +
                "The lectures were short so you do not lose focus and get bored and were easy to understand " +
                "The labs/assignments were designed perfectly to test all the knowledge which is gained in the lectures\r\n\r\n" +
                "Not only will you learn MS SQL, but you will also go beyond and master Backup and Recovery Thanks to this course, " +
                "you will be one step ahead in your professional life\r\n\r\nOne of the topics we will see in our course OOP, " +
                "Object-oriented programming, is the foundation of many current application developments approaches " +
                "Interfaces and principles of object-oriented programming are crucial It does not important whether you want to use " +
                "C# to build web apps, mobile apps, games, or understanding C# classes if you want to succeed with clean coding, agile, " +
                "and design patterns, you have to master OOP\r\n\r\nMicrosoft SQL Server is a relational database management " +
                "system developed by Microsoft which is the database we will be learning during the course As a database server, " +
                "it is a software product with the primary function of storing and retrieving data as requested by other " +
                "software applications SQL is the standard language for Relation Database Systems All relational " +
                "database management systems like SQL Server, MySQL, MS Access, Oracle, Sybase, and others use SQL as the " +
                "standard database language SQL is used to communicate with a database\r\n\r\nBackup and Recovery are " +
                "very important for those who want to progress in the database Because one of the most important roles of a " +
                "database administrator is to constantly protect the integrity of the databases and maintain the " +
                "ability to recover quickly in case of a failure In light of this, it’s critically important to have a " +
                "backup-and-recovery strategy in place in order to be ready for an emergency\r\n\r\n" +
                "In this course, you will make an excellent introduction to learning C# and SQL with MS Management " +
                "Studio which allows to manage database and retrieve data from the database with a graphical interface\r\n\r\n" +
                "We are going to start to learn from the basics and step by step we will be building our knowledge\r\n\r\n" +
                "In this course, we use interactive programming techniques; which means we will be building applications " +
                "together and furthermore there will have lots of home-works to be done, of course, " +
                "followed by answers There will be lots of tips and tricks regarding beautiful and efficient coding techniques\r\n\r\n" +
                "All my students will have a chance to learn not only the whats but also learn the whys and hows\r\n\r\n" +
                "What you will learn?\r\n\r\nC# Programming and Features of C#\r\n\r\nVisual Studio IDE\r\n\r\n" +
                "Console Application\r\n\r\nVariables\r\n\r\nPrimitive Types and Non-Primitive Types\r\n\r\n" +
                "Flow Control Expressions\r\n\r\nArrays and Lists\r\n\r\nError Handling and Debugging\r\n\r\nFunctions\r\n\r\n" +
                "Reading File\r\n\r\nWriting to File\r\n\r\nDateTime\r\n\r\nIntroduction to Object-Oriented Programming\r\n\r\n" +
                "Class Structure in Detail\r\n\r\nWindows Forms Applications\r\n\r\nSystem Input Output\r\n\r\nClass Hierarchies\r\n\r\n" +
                "Event-Driven Programming\r\n\r\nException Handling\r\n\r\nms sql\r\n\r\nmssql\r\n\r\nc#\r\n\r\nc# sql\r\n\r\nsql\r\n\r\n" +
                "ms sql server\r\n\r\nms sql server\r\n\r\nc# and sql\r\n\r\noak academy\r\n\r\nsql with c#\r\n\r\nc# sql server\r\n\r\n" +
                "microsoft sql\r\n\r\nssrs\r\n\r\nMicrosoft sql server\r\n\r\nTips and Tricks\r\n\r\nHow to install and setup these requirements" +
                "\r\n\r\nYou will learn the basics of SQL such as data, database, DBMS or SSMS, SQL, tables, and so on\r\n\r\n" +
                "Database normalization,\r\n\r\nManipulating data,\r\n\r\nRetrieving data from the database with different scenarios,\r\n\r\n" +
                "You will also learn SQL transactions and transaction commands,\r\n\r\nSchema and schema objects and\r\n\r\n" +
                "User privileges, permission commands, and roles\r\n\r\nWhat is the recovery model? What are the differences between the Full, " +
                "Bulked Logged, and Simple recover model?\r\n\r\nWhat is a full backup? Why does every backup have to start with a full backup?" +
                "\r\n\r\nWhat is a differential backup? what is it used for?\r\n\r\nWhat is a transaction log backup? what does it do\r\n\r\n" +
                "How do we reinstall if the database is completely deleted?\r\n\r\nHow do we return the database to a specific time?\r\n\r\n" +
                "ms sql mssql c# c# sql sql ms sql server ms sql with c# sql c# sql server c# and sql oak academy " +
                "c# sql server mssql server sql with c# ms-sql-with-c c# mssql ms sql with c c# ms sql sql and c# sql with c# ms sql " +
                "c# ms sql with c c# ms sql sql and c# c# with sql ms sql c# sql server c# database design sql server with c# c sharp " +
                "c# and sql server c# and ms sql mssql c# c# with mssql n tier architecture oak csharp microsoft sql server c# oop\r\n\r\n" +
                "If you’re doing well with your SQL learning or simply want to kill two birds with one stone, picking up another " +
                "programming language is a great idea A complementary skill will only increase your " +
                "programming and database abilities and your employability in the future\r\n\r\nDo not forget!\r\n\r\n" +
                "There are SO MANY types of database jobs you can do with SQL and C#alone, and another " +
                "programming language only adds more strings to your bow\r\n\r\nWhy would you want to take this course?\r\n\r\n" +
                "Our answer is simple: The quality of teaching\r\n\r\nWhen you enroll, you will feel our seasoned developers' expertise\r\n\r\n" +
                "No prior knowledge is needed!\r\n\r\nIt doesn't need any prior knowledge to learn it and it is easy to understand for beginners" +
                "\r\n\r\nThis course starts with the very basics First, you will learn how to install the tools, some " +
                "terminology Then the show will start and you will learn everything with hands-on practice I'll also " +
                "teach you the best practices and shortcuts\r\n\r\nStep-by-Step Way, Simple and Easy With Exercises\r\n\r\n" +
                "By the end of the course, you’ll have a firm understanding of the C# and " +
                "SQL You will have valuable insights on how things work under the hood and you'll also be very confident " +
                "in the basics of coding and game development, and hungry to learn more The good news is since the " +
                "Free and popular tools are used you don’t need to buy any tool or application\r\n\r\n\r\nWhat is C# ?\r\n" +
                "C# (pronounced see-sharp) is a general-purpose, object-oriented programming language It was designed as a " +
                "multi-paradigm programming language by Microsoft in around 2000 as part of its NET initiative The " +
                "NET framework and NET applications are multi-platform applications that you can use with " +
                "programming languages such as C++, C#, Visual Basic, and COBOL C# is open-source and was designed to be " +
                "simple yet modern, flexible yet powerful, and versatile yet easy to learn and program with Many programming " +
                "languages in the past were designed for specific purposes C# was developed with business and " +
                "enterprise needs in mind By providing functionality to support modern-day software development such as web " +
                "applications, mobile, and response app needs, C# supports many features of modern-day programming " +
                "languages That includes generics, var types\r\n\r\nWhat is SQL Server?\r\n\r\n" +
                "SQL Server is a relational database management system or RDBMS created and developed by Microsoft " +
                "Designed to store and retrieve data for other software applications using the client-server model-- these " +
                "applications connect to SQL Server through a network or the Internet with multiple applications using the same data One " +
                "SQL Server instance can also manage one or more relational databases Relational databases store data in " +
                "tables that can relate to each other For example, data for business customers get stored in one table " +
                "relating to another table that stores orders Applications that connect to Microsoft SQL Server retrieve, " +
                "store, and delete data using Structured Query Language (SQL) SQL Server is available for Windows and Linux\r\n\r\n" +
                "What is SQL Server Management Studio?\r\n\r\nSQL Server Management Studio is desktop software designed to connect to " +
                "Microsoft relational database management systems, including Microsoft SQL Server, Azure SQL Database, and Azure Synapse " +
                "Analytics SQL Server Management Studio is an integrated database development tool that database architects, " +
                "database developers, and software engineers can use to access, configure, manage, administer, and develop databases " +
                "on any one of these database systems It simplifies database management by providing graphical design tools and " +
                "rich script editors that allow database developers to visualize an entire database or multiple databases from a " +
                "single application SQL Server Management Studio only runs on the Microsoft Windows operating system but can " +
                "connect to database instances running on either Windows or Linux\r\n\r\nVideo and Audio Production Quality\r\n" +
                "All our videos are created/produced as high-quality video and audio to provide you the best learning experience\r\n\r\n" +
                "You will be,\r\n\r\nSeeing clearly\r\n\r\nHearing clearly\r\n\r\nMoving through the course without distractions\r\n\r\n" +
                "You'll also get:\r\n\r\nLifetime Access to The Course\r\n\r\nFast & Friendly Support in the Q&A section\r\n\r\nLexicon" +
                " Certificate of Completion Ready for Download\r\n\r\nDive in now\r\n\r\nWe offer full support, answering any questions" +
                "\r\n\r\nSee you in the MS SQL with C# course!",
                StartDate = faker.Date.Past(),
                EndDate = faker.Date.Future(),
                // Documents = null
            },
            new Course
            {
                Name = "Basic Dance Moves for Guys - Completely beginner lessons",
                Description = "Basic Dance Moves for Guys - Completely beginner lessons\" is a " +
                "step-by-step system that helps guys go from being awkward and out-of-place on the dance floor, " +
                "to becoming dance-floor-ready in 3 days time.\r\n\r\nThis system teaches you the dance style that's " +
                "best described as \"club dancing.\"\r\n\r\n" +
                "It's that casual, fun dancing people do at night-clubs, or parties! It's done to songs you hear on the " +
                "radio -- you know, house, pop, R&B, hip hop and other up-beat dance-jams.\r\n\r\nAnd the dance moves taught are JUST-FOR-GUYS." +
                "\r\n\r\nIn other words, this program does NOT teach you:\r\n\r\n" +
                "Ridiculous music video routines that you can never see yourself doing in public\r\n" +
                "Partner dancing stuff that's waaaay too formal. and not suitable for a casual night out\r\n" +
                "Goofy, silly dance moves that make you look like a clown\r\nSuper feminine dance moves that asks to go waaaay " +
                "too far beyond your comfort zone\r\n\r\n\r\nThis program takes you step-by-step, through the process of:\r\n\r\n" +
                "gaining rhythm \r\nmoving to the beat\r\nloosening up your body to stop looking stiff and tensed up\r\n" +
                "learning a set of the most versatile, best looking dance moves\r\nconnecting these moves together\r\n" +
                "practicing them the right way so you master them within the week\r\n\r\n\r\nHere's the bottom line:\r\n\r\n" +
                "Watch -- and follow -- 1 set of video a day -- for 3 Days. Each set of videos runs for around 42 minutes. " +
                "By the end of day 3, you'll possess the new ability to walk out onto any dance floor - start moving, " +
                "stepping, grooving to the music, and join in on the fun.\r\n\r\n" +
                "Follow exactly as I do, and start seeing results! It's that simple!\r\n\r\n" +
                "3 Bonuses:\r\n\r\nFootwork starter kit: 3 additional footwork-based dance moves to help you look AWESOMEl!\r\n" +
                "CLUB DANCE Best Practices: Answers to the most commonly asked club-dance questions\r\n" +
                "NEW Dancer's FAQ: Answers to most commonly asked questions by new dancers.",
                StartDate = faker.Date.Past(),
                EndDate = faker.Date.Future(),
                // Documents = null
            },
            new Course
            {
                Name = "LINQ Tutorial: Master the Key C# Library",
                Description = "Welcome to the \"LINQ Tutorial: Master the Key C# Library\", " +
                "the only course you need to become fluent in using library essential for every " +
                "C# programmer.\r\n\r\nNo matter what application you work on or ever will work on - " +
                "you will always need to manipulate collections. Whether it's ordering, filtering, or " +
                "transforming collections, or anything else - LINQ is the tool for the job. As a core part of " +
                ".NET, it's a library that every C# programmer must know. LINQ provides the ability to not only " +
                "query the collections stored in application memory, but also remote data sources, like databases or XML files.\r\n\r\n" +
                "Check out the free video about one of the most commonly used methods from LINQ - the Where method.\r\n\r\n\r\n\r\n" +
                "This course is practice-oriented. After each lecture, you will solve two coding exercises and one " +
                "refactoring challenge, where you will transform an awkward code into an elegant, short, and clean code using LINQ. " +
                "Even if you won't be able to solve the exercise right off the bat, don't worry - an article with a " +
                "detailed explanation of the solution is provided for each exercise.\r\n\r\n" +
                "This course is for everyone with basic knowledge of C#. Every topic will be explained in detail. " +
                "In the introduction section, you will learn all you need to continue, like what lambda expressions are or what IEnumerable is." +
                "\r\n\r\nIf you already know some LINQ, this course will help you start using it like a pro, as well as " +
                "understand how it works under the hood.\r\n\r\nBy solving 64 coding exercises and 32 " +
                "refactoring challenges, you will gain practical knowledge of LINQ and become a better programmer. " +
                "You will start \"thinking in LINQ\" and using this amazing library will become a natural thing to you." +
                "\r\n\r\nDon't forget that LINQ knowledge is expected by basically every employer, and it's extremely often " +
                "verified during interviews. After taking this course, you will gain confidence in discussing the topic of LINQ." +
                "\r\n\r\nLINQ has two ways of writing queries - the method syntax and the query syntax. We will learn both of them." +
                "\r\n\r\nYou'll be able to access all examples from videos, as well as all coding exercises and refactoring challenges, " +
                "including solutions, in the Git repository included in this course.\r\n\r\n\r\n\r\nT" +
                "his course comes with a 30-day money-back guarantee. If you are not satisfied, you can return it and get all your money back" +
                ", no questions asked. In other words, you don't risk anything by purchasing this course. " +
                "You have nothing to lose, and the knowledge you will gain may take your career to the next level.\r\n\r\n" +
                "So, why hesitate? Join me in this course and start using LINQ like a pro.",
                StartDate = faker.Date.Past(),
                EndDate = faker.Date.Future(),
                // Documents = null
            }
        };
        foreach (var course in courses)
        {
            var usersToAdd = Math.Min(10, availableUsers.Count);
            var selectedUsers = faker.PickRandom(availableUsers, usersToAdd).ToList();
            
            course.Users = selectedUsers;
            unitOfWork.Courses.Add(course);

            // Remove selected users from the available pool
            availableUsers.RemoveAll(u => selectedUsers.Contains(u));
        }

        await unitOfWork.SaveAsync();
    }

    private async Task GenerateUsersAsync()
    {
        // Create generic teacher and student for testing/demo purposes
        unitOfWork.Users.Add(new User
        {
            FirstName = "Sven",
            LastName = "Lärarsson",
            Email = "teacher@example.com",
            Password = "teacher",
            Role = UserRole.Teacher,
        });
        unitOfWork.Users.Add(new User
        {
            FirstName = "Anders",
            LastName = "Studentsson",
            Email = "student@example.com",
            Password = "student",
            Role = UserRole.Student,
        });

        // Create 2 random teachers
        for (var i = 0; i < 2; i++)
        {
            var user = new Faker<User>("sv")
                .RuleFor(u => u.FirstName, f => f.Person.FirstName)
                .RuleFor(u => u.LastName, f => f.Person.LastName)
                .RuleFor(u => u.Email, f => f.Person.Email)
                .RuleFor(u => u.Password, f => f.Internet.Password())
                .RuleFor(u => u.Role, _ => UserRole.Teacher)
                .Generate();

            unitOfWork.Users.Add(user);
        }

        // Create 20 random students
        for (var i = 0; i < 20; i++)
        {
            var user = new Faker<User>(locale: "sv")
                .RuleFor(u => u.FirstName, f => f.Person.FirstName)
                .RuleFor(u => u.LastName, f => f.Person.LastName)
                .RuleFor(u => u.Email, f => f.Person.Email)
                .RuleFor(u => u.Password, f => f.Internet.Password())
                .RuleFor(u => u.Role, _ => UserRole.Student)
                .Generate();

            unitOfWork.Users.Add(user);
        }

        await unitOfWork.SaveAsync();
    }

    private async Task AddExampleUsersToCoursesAsync()
    {
        var exampleTeacher = await unitOfWork.Users.GetAsync(1);
        var exampleStudent = await unitOfWork.Users.GetAsync(2);
        var courses = await unitOfWork.Courses.GetAllAsync();
        foreach (var course in courses)
        {
            if (course.Users.All(u => u.UserId != exampleTeacher.UserId))
            {
                course.Users.Add(exampleTeacher);
            }
        }

        if (exampleStudent.Courses.IsNullOrEmpty())
        {
            unitOfWork.Courses.AddUserToCourse(1, exampleStudent);
        }

        await unitOfWork.SaveAsync();
    }
}