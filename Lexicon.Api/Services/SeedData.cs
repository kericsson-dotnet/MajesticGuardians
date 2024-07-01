using Lexicon.Api.Entities;

namespace Lexicon.Api.Services;

public static class SeedData
{
    public static List<(string Name, string Description)> GetSeedModules()
    {
        var modules = new[]
        {
            // C# .net 2024 Course
            (
                Name: "C# Lexicon LMS",
                Description:
                "Ett Learning Management System (LMS) är en mjukvaruplattform som används för administration, dokumentation, spårning, rapportering, automatisering och leverans av utbildningskurser, utbildningsprogram eller inlärnings- och utvecklingsprogram. Ett LMS gör det möjligt för utbildningsinstitutioner, företag och organisationer att effektivt hantera och distribuera utbildningsinnehåll till sina användare."
            ),
            (
                Name: "Blazor",
                Description:
                "Ett maskinparkshanteringssystem är en mjukvaruplattform som används för att övervaka och hantera en flotta av maskiner och fordon. Systemet gör det möjligt för företag att effektivt spåra, underhålla och optimera användningen av sina maskiner och fordon, vilket resulterar i förbättrad driftseffektivitet och minskade driftkostnader."
            ),
            (
                Name: "Grundläggande LINQ i C#",
                Description:
                "Blazor och LINQ (Language Integrated Query) är två kraftfulla teknologier i C# som tillsammans kan användas för att bygga moderna och effektiva webbapplikationer. Blazor är ett ramverk för att skapa interaktiva webbgränssnitt med C#, medan LINQ är en uppsättning metoder och operatorer för att utföra datamanipulation och frågor på enhetliga sätt över olika datakällor."
            ),

            (
                Name: "Objektorienterad Programmering i Python",
                Description:
                "Objektorienterad programmering (OOP) är ett programmeringsparadigm som organiserar programvara genom att kombinera data och funktionalitet i enheter som kallas \"objekt\". Python är ett kraftfullt programmeringsspråk som stöder OOP, vilket gör det möjligt för utvecklare att skapa strukturerad och återanvändbar kod. OOP i Python underlättar design och utveckling av komplexa system genom att modulera problem i hanterbara och logiska enheter."
            ),
            (
                Name: "Fel och Undantag i Python",
                Description:
                "Fel och undantag är en viktig del av programmering som hjälper utvecklare att hantera oförutsedda situationer och buggar som kan uppstå under körning av ett program. I Python finns det ett robust system för att fånga och hantera undantag, vilket förbättrar programmets tillförlitlighet och användarupplevelse."
            ),
            (
                Name: "Fel och Undantag i Python 2 och Python Selenium",
                Description:
                "Fel och undantag är viktiga koncept inom programmering, som hjälper utvecklare att hantera oväntade situationer och buggar som kan uppstå under körning av ett program. Python 2 har ett system för att fånga och hantera undantag, vilket förbättrar programmets tillförlitlighet och användarupplevelse.\n" +
                "Selenium är ett kraftfullt verktyg för web automatisering. Det används för att automatisera webbläsare för att simulera en användares interaktioner med webbapplikationer. Selenium kan integreras med Python för att skriva testskript och automatiseringsskript."
            )
        };
        return modules.ToList();
    }

    public static List<(string Name, ActivityType Type, string Description)> GetSeedActivities()
    {
        var activities = new[]
        {
            (
                Name: "Läxa: C# Lexicon LMS",
                Type: ActivityType.Assignment,
                Description: "Lexicon LMS\r\n" +
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
                             "Detaljerad information om redovisningsmomentet kommer senare\r\nLycka till!"
            ),
            (
                Name: "E-learning",
                Type: ActivityType.ELearning,
                Description: "PluralSight"
            ),
            (
                Name: "Quiz",
                Type: ActivityType.Quiz,
                Description: "Quiz om det du har lärt dig"
            ),

            (
                Name: "Läxa: Industriell maskinpark –\r\nBlazor",
                Type: ActivityType.Assignment,
                Description: "Övning 16 – Industriell maskinpark –\r\nBlazor\r\n" +
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
                             " Uppdatera data i maskinen.\r\n Lägga till och ta bort maskiner."
            ),

            (
                Name: "E-learning",
                Type: ActivityType.ELearning,
                Description: "PluralSight"
            ),
            (
                Name: "Quiz",
                Type: ActivityType.Quiz,
                Description: "Quiz om det du har lärt dig"
            ),
            (
                Name: "Lecture",
                Type: ActivityType.Lecture,
                Description: "Blazor"
            ),
            (
                Name: "Läxa: Grundläggande LINQ i C#",
                Type: ActivityType.Assignment,
                Description:
                "#### Uppgift 1: Skapa en Lista\r\n1. Skapa en ny C#-konsolapplikation i Visual Studio.\r\n2. Skapa en klass `Person` med följande egenskaper:\r\n   - `Name` (string)\r\n   - `Age` (int)\r\n\r\n3. Skapa en lista av `Person`-objekt med minst fem olika personer med olika namn och åldrar.\r\n\r\n#### Uppgift 2: Använd LINQ för att Hämta Data\r\n1. Använd LINQ för att hitta alla personer som är äldre än 25 år. Skriv ut deras namn och åldrar till konsolen.\r\n2. Använd LINQ för att hitta personen med det längsta namnet. Skriv ut namnet och åldern till konsolen.\r\n\r\n#### Uppgift 3: Sortera och Gruppera Data\r\n1. Använd LINQ för att sortera listan av personer efter ålder i stigande ordning. Skriv ut den sorterade listan till konsolen.\r\n2. Använd LINQ för att gruppera personerna efter deras ålder. Skriv ut varje grupp och personerna i varje grupp till konsolen.\r\n\r\n#### Uppgift 4: Använd LINQ för att Transformera Data\r\n1. Använd LINQ för att skapa en ny lista av strängar som innehåller personernas namn i versaler. Skriv ut denna lista till konsolen.\r\n2. Använd LINQ för att skapa en ny lista av anonyma objekt som innehåller `Name` och en boolsk egenskap `IsAdult` (true om `Age` är 18 eller äldre, annars false). Skriv ut denna lista till konsolen.\r\n\r\n#### Bonusuppgift: Avancerad Fråga\r\n1. Använd LINQ för att hitta den genomsnittliga åldern av alla personer i listan. Skriv ut det genomsnittliga värdet till konsolen.\r\n2. Använd LINQ för att hitta alla personer vars namn börjar med bokstaven 'A'. Skriv ut deras namn och åldrar till konsolen.\r\n\r\n---\r\n\r\nLycka till med läxan!"
            ),

            (
                Name: "Quiz",
                Type: ActivityType.Quiz,
                Description: "Quiz om det du har lärt dig"
            ),

            // Python 2024

            (
                Name: "Läxa: Objektorienterad Programmering i Python",
                Type: ActivityType.Assignment,
                Description:
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
                "\r\n\r\n---\r\n\r\nLycka till med läxan!"
            ),

            (
                Name: "E-learning",
                Type: ActivityType.ELearning,
                Description: "PluralSight"
            ),

            (
                Name: "Quiz",
                Type: ActivityType.Quiz,
                Description: "Quiz om det du har lärt dig"
            ),

            (
                Name: "Läxa: Fel och Undantag i Python",
                Type: ActivityType.Assignment,
                Description:
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
                "Lycka till med läxan!"
            ),

            (
                Name: "E-learning",
                Type: ActivityType.ELearning,
                Description: "PluralSight"
            ),

            (
                Name: "Quiz",
                Type: ActivityType.Quiz,
                Description: "Quiz om det du har lärt dig"
            ),
            (
                Name: "Lecture",
                Type: ActivityType.Lecture,
                Description: "Python Selenium"
            ),
            (
                Name: "Quiz",
                Type: ActivityType.Quiz,
                Description: "Quiz om det du har lärt dig"
            ),

            (
                Name: "Lecture",
                Type: ActivityType.Lecture,
                Description: "Python object oriented"
            )
        };
        return activities.ToList();
    }
}