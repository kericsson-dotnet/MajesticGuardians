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
            (Name: "Blazor",
                Description:
                "Ett maskinparkshanteringssystem är en mjukvaruplattform som används för att övervaka och hantera en flotta av maskiner och fordon. Systemet gör det möjligt för företag att effektivt spåra, underhålla och optimera användningen av sina maskiner och fordon, vilket resulterar i förbättrad driftseffektivitet och minskade driftkostnader."
 
            ),
            (Name: "Grundläggande LINQ i C#",
                Description:
                "Blazor och LINQ (Language Integrated Query) är två kraftfulla teknologier i C# som tillsammans kan användas för att bygga moderna och effektiva webbapplikationer. Blazor är ett ramverk för att skapa interaktiva webbgränssnitt med C#, medan LINQ är en uppsättning metoder och operatorer för att utföra datamanipulation och frågor på enhetliga sätt över olika datakällor."
 
            ),

            (Name: "Objektorienterad Programmering i Python",
                Description:
                "Objektorienterad programmering (OOP) är ett programmeringsparadigm som organiserar programvara genom att kombinera data och funktionalitet i enheter som kallas \"objekt\". Python är ett kraftfullt programmeringsspråk som stöder OOP, vilket gör det möjligt för utvecklare att skapa strukturerad och återanvändbar kod. OOP i Python underlättar design och utveckling av komplexa system genom att modulera problem i hanterbara och logiska enheter."
            ),
            (Name: "Fel och Undantag i Python",
                Description:
                "Fel och undantag är en viktig del av programmering som hjälper utvecklare att hantera oförutsedda situationer och buggar som kan uppstå under körning av ett program. I Python finns det ett robust system för att fånga och hantera undantag, vilket förbättrar programmets tillförlitlighet och användarupplevelse."
            ),
            (Name: "Fel och Undantag i Python 2 och Python Selenium",
                Description:
                "Fel och undantag är viktiga koncept inom programmering, som hjälper utvecklare att hantera oväntade situationer och buggar som kan uppstå under körning av ett program. Python 2 har ett system för att fånga och hantera undantag, vilket förbättrar programmets tillförlitlighet och användarupplevelse.\n" +
                "Selenium är ett kraftfullt verktyg för web automatisering. Det används för att automatisera webbläsare för att simulera en användares interaktioner med webbapplikationer. Selenium kan integreras med Python för att skriva testskript och automatiseringsskript."
            )
        };
        return modules.ToList();
    }
}