# Hiptobesquare : Summering

## Summering av projektet:

-  

## Justeringar för att bli redo för deployment: 

1. La till två .env-filer för att hantera miljövariabler i frontend, och två .env-filer i backend.
2. Justerade .gitignore för att ignorera production-filerna, men inkludera development-filerna i repot.
3. Justerade Program.cs och App.jsx för att hantera miljövariabler instället för hårdkodade värden.
- Nu bör vi kunna köra projektet i utvecklingsmiljö och i produktion, dynamiskt.

---

## Disclaimer och not om mjukvara, hjälpmedel/LLM-verktyg:

Jag kommer att använda hjälpmedel i form av LLMer och officiell dokumentation (primärt troligtvis Microsoft Learn och Reacts officiella dokumentation).

1. **Mjukvara:** Kodar .NET och dokumenterar markdown i Jetbrains Rider.
2. **Dokumentation:** Primärt Microsoft Learn och Reacts officiella dokumentation.
3. **CoPilot:** Jag har GitHubs CoPilot installerat som hjälpmedel för att generera kodsnuttar och dokumentation.
4. **Extern LLM:** När jag har begränsad kunskap, vilket bland annat är fallet primärt med React, kommer jag att använda mig av externa LLMer för att framförallt bolla frågeställningar ("om jag vill göra X för att få resultat Y med kodstrukturen jag presenterar här `presenterar kod-arkitektur`, vilka verktyg finns i React och hur kan jag lägga upp det?"). Vill notera två saker där:
    - Primärt när jag arbetar med LLMer så brukar jag skriva metoder och klasser själv och be om hjälp att se vilka verktyg som finns tillgängliga, sedan brukar jag skriva koden själv (med hjälp av CoPilots autocomplete för att snabba på processen).
    - Jag ber generellt om kortare segment som jag sedan går igenom och modifierar själv, för att förstå koden, jag copy-pastar inte stora kodblock och bara förutsätter att de fungerar.
    - Om jag använder mig av LLM i ett segment så kommer jag notera det i dokumentationen.