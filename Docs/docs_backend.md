# Hiptobesquare : Backend

## Initiala konceptuella funderingar:

### JSON och lagring:

- Vi skall kunna skala till oändlighet, vilket innebär att vi måste hantera en stor mängd data.
- Data-arkitekturen: paginerad data, chunkad data?
- Hantera parsning?
- Detta skall göras med JSON-filer, dock kan det vara värt att ha med i arkitekturen att vi skall kunna byta ut JSON mot en databas i framtiden. Det slår an på skalbarhet och underhållbarhet.

### API-anrop och trafik:

- Skalbarhet: hur hanterar vi trafik?
- Spam: hur undviker vi att spammas av API-anrop? Debouncing på tangenttryckningar?
- Begränsat antal API-anrop per sekund? rate limiting?
- Serverless-lösningar: Azure Functions
- Azure Storage? Blob Storage för JSON-filer? Fördel med Azure är att vi kan använda Logic Apps för att automatisera arbetsflöden.

### Säkerhet och JWT:

- Implementera JWT-autentisering för att endast autentiserade användare kan interagera med API:et?  
  Sidonot på den: Det är en vettig lösning om vi har användarspecifik inlogg, men överflödigt här? Vi kan istället använda en API-nyckel för att säkra API-anropen.
- Felloggar: logga undantag och fel för felsökning, hantera loggfiler.
- Try-catch, exception handling, verktyg (NuGet) och tänk på UX för att hantera felmeddelanden.

### Infrastruktur och kostnader:

- Azure-gratistjänster: Azure Functions, Azure Storage (5gb), Logic Apps (2000 actions).
- CI/CD: Github Actions för att automatisera byggen och deployment? Bygga API och distribuera till Azure Functions?
- Kubernetes, Terraform, Ansible: overkill för denna skala + onödig komplexitet.

### API:

- GET-, POST-, DELETE- endpoints för att hantera data.
- Logik för att läsa och skriva till JSON-fil.
- Autentisering med JWT
- Testning av API med Postman el dylikt, kan det vara värt att bygga en test-svit?

### Kostnadsaspekter och skalbarhet:

- En billig VPS kan kosta 5-10 USD per månad men då når vi inte målen för skalbarhet och får mer underhåll och konfiguration att tänka på.
- En statisk server har begränsad skalbarhet, vi kan inte skala till oändlighet och då fallerar projektet. Dyrare insteg.
- Molntjänster är skalbara och kräver mindre underhåll, Azure har gratisnivåer som vi kan använda för att bygga en prototyp och även om vi skulle hantera säg 100 000 API-anrop per månad så är det fortfarande inte dyrare än en instegs-VPS. Se: https://azure.microsoft.com/en-us/pricing/calculator/  
  
## Summering av tankar kring ovanstående, och beslut:

#### Jag tänker att vi har vissa strukturer att följa:
- Enkelhet, skalbarhet och underhållbarhet.  
- Skala till oändlighet men vi är begränsade till JSON-filer. Då vill vi ha en maxstorlek på JSON-filen, när gränsen nås skapas en ny fil. Lagra i en /data/ mapp och ha en index.json som pekar på alla JSON-filer.
- Vi använder Azure Storage (Blob Storage för JSON-lagring), Logic App för automatiserade arbetsflöden, och Azure Functions för serverless-lösningar. Gratisnivån ger oss 5 gb lagring, 2000 actions och 1 million anrop per månad.
- JWT är troligen önskvärdigt för autentisering, men vi har inte användarspecifik data så detta tillför en onödig komplexitet. Vi kan istället säkra API-anropen med en API-nyckel.
- GitHub Actions för CI/CD, vi bygger API:et och distribuerar till Azure Functions.

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

---

## Då börjar vi bygga ett API! (val under resans gång):

1. Väljer att bygga ett minimal-API över Controller-patternet. Controllern är mer modulärt, men det passar bättre i större OOP-sammanhang och även om detta projekt skall vara skalbart så skall det även vara minimalt. Vi får mindre komplex kod och färre abstraktioner att tänka på, nu.  
Använder: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/apis?view=aspnetcore-9.0  
  
2. Väljer asynkrona metoder på GET, POST och DELETE, främst för skalbarhet och det känns som bra practice. Väljer däremot en enklare bekräftelse på att en operation har lyckats än att bygga in RESTful-egenskaper (overkill).  

3. Nästa val kommer i DataManager-klassen: jag vill ha en skalbar lösning där JSON-filen har en max-storlek, så jag väljer att skapa en squares_1.json där vi lagrar fyrkanter tills filen är 10 Mb, då skapar vi squares_2.json och så vidare. För att veta var vi är så har vi en index.json som pekar på alla JSON-filer.

- Allmänt: Jag har gjort ett fåtal APIer tidigare, så här bollar jag frågor om olika struktur mellan Microsoft Learn, ChatGPT och CoPilot aktivt, det går sömlöst in i varandra. Till exempel ville CoPilot köra async på alla metoder men ChatGPT var mer återhållsam. Jag valde i detta fall async för skalbarhet och säkerhet. I minimal-API vs Controller-fallet går jag på MS Learns template, och när det gällde IEnumerable (ChatGPT) vs List (CoPilot) så valde jag IEnum eftersom vi främst skall läsa data, inte manipulera så mycket.
  
- Det som guidar mig är att jag vill ha en enkel, skalbar och underhållbar lösning. Jag ställer mig hela tiden frågan: "Vad är enklast och mest effektivt för att uppnå målet?", och tar säkerhet och skalbarhet i beaktande.  

4. Jag väljer att skapa kvadraterna i backend: tanken är att datans integritet säkerställs som unika med rätt värden, färg, position. APIet kan med fördel då returnera en samling av kvadrater som klienten renderar, det ger en bättre UX-upplevelse och bättre prestanda. React kan då cacha kvadrater och minimera API-anrop.  

## Testers och linters vara eller icke vara..

5. Å ena sidan är tester rätt väg att gå i en skalbar applikation. Å andra sidan är detta program väldigt litet och kommer inte växa. Men, jag tycker tester av kritiska komponenter är viktiga och vill ändå visa på att jag tycker det är en del av strukturen så - det blev ingen TDD, men jag övervägde Unit-tester för logiken (SquareService, DataManager) samt Integrationstester för API-endpoints. Det hade gått utmärkt att bygga ett interface för att inte behöva mocka DataManager, men här valde jag minimalist-approachen genom att ärva och använda DataManager direkt.

- Valde till slut att kompromissa med grundläggande tester för SquareService. Minimalt med kod, förbättrar skalbarhet och visar på att jag värderar/hanterar tekniken. Väljer att hoppa över integrationstesterna. Eftersom jag kör Rider och kodanalys är inbyggd så kör jag ingen external linter.

6. ![testresultat.png](img.png)

- Ville bara testa kritiska moment, som att vi kan hantera över 10mb av JSON-fil.. Testerna går igenom.

## Rate Limiting, Loggning och API-nyckel-autentisering

7. Vid det här laget har vi ett bra ramverk för ett API, jag vill bara lägga till tre moment. Första steget blir Rate Limiting. Lägger den i en separat klass och sätter 30 API-anrop per 10 sekunder per IP-adress som initial config.

8. Implementerar loggning och exception handling via Serilog, även det i en separat klass.

---