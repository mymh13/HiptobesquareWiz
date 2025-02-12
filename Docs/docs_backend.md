# Hiptobesquare : Backend

## Initiala konceptuella funderingar:

### JSON och lagring:
- **Skalbarhet:** JSON-filer delas vid 10 MB för att möjliggöra obegränsad tillväxt.
- **Flexibilitet:** Arkitekturen gör det möjligt att byta ut JSON mot en databas i framtiden.
- **Indexering:** En `index.json` håller reda på alla datafiler.

### API-anrop och prestanda:
- **Endpunkter:** GET, POST och DELETE hanterar kvadraterna.
- **Rate limiting:** Begränsar API-anrop för att förhindra spam och överbelastning.
- **Cache:** Kvadraterna lagras i backend och cachas i frontend för bättre prestanda.

### Säkerhet och felhantering:
- **Autentisering:** JWT och API-nyckel övervägdes men ansågs onödiga i detta projekt.
- **Loggning:** Serilog används för att hantera undantag och skapa loggfiler.
- **Try-catch:** Hanteras primärt i middleware för minimal kod och bättre hantering av fel.

### Infrastruktur och deployment (tänk på kostnader):
- **Azure:** Gratisnivån ger 5 GB lagring, 2000 Logic Apps-actions och 1 miljon anrop/månad.
- **CI/CD:** Github Actions för att automatisera byggen och deployment?
- **Serverless:** Kubernetes, Terraform, Ansible: overkill för denna skala + onödig komplexitet. Azure Functions?

---

## Summering av arkitektur och beslut
- **Minimalism:** Enkelhet, skalbarhet och underhållbarhet är en ledstjärna genom hela projektet.
- **JSON-filer:** Skalbarhet hanteras genom att skapa nya filer när storleken överskrider 10 MB.
- **Backend-genererade kvadrater:** Data-integritet säkras genom backend medan frontend renderar kvadraterna.

---

## API:

### 1. API-struktur och val
- Minimal API ger **enklare kod och färre abstraktioner** än MVC.
- **Asynkrona metoder** används för GET, POST och DELETE för att hantera skalbarhet.
- **Lagring i JSON** hanteras via en `DataManager`-klass.

### 2. Hantering av JSON-data
- **Kvadrater lagras i en JSON-fil** tills den når 10 MB, därefter skapas en ny fil.
- **`index.json` håller koll på filerna** och används för att snabbt hitta sparade data.

### 3. Prestanda och optimering
- **Rate limiting:** Max 30 API-anrop per 10 sekunder per IP.
- **Loggning med Serilog:** Sparar felloggar i en separat mapp.
- **Middleware:** Hanterar undantag och loggning centralt.

---

### Kostnadsaspekter och skalbarhet:

- En billig VPS kan kosta 5-10 USD per månad men då når vi inte målen för skalbarhet och får mer underhåll och konfiguration att tänka på.
- En statisk server har begränsad skalbarhet, vi kan inte skala till oändlighet och då fallerar projektet. Dyrare insteg.
- Molntjänster är skalbara och kräver mindre underhåll, Azure har gratisnivåer som vi kan använda för att bygga en prototyp och även om vi skulle hantera säg 100 000 API-anrop per månad så är det fortfarande inte dyrare än en instegs-VPS. Se: https://azure.microsoft.com/en-us/pricing/calculator/

---

## Byggprocess och beslut under resans gång

### 1. Minimal API över Controller-mönster
- **Minimal API** valdes framför Controllers för att hålla koden enkel och lätt att underhålla.
- Mindre komplexitet och färre abstraktioner.

### 2. Asynkron hantering
- GET, POST och DELETE implementerades som **asynkrona metoder** för bättre prestanda.
- Enklare responsstruktur istället för full RESTful-implementering (overkill i detta fall).

### 3. Hantering av JSON-filer
- **Kvadrater sparas i JSON-filer** tills de når 10 MB, sedan skapas en ny fil.
- **`index.json`** används för att hålla reda på alla datafiler.

### 4. Backend-genererade kvadrater
- **Integritetskontroll:** Unika ID:n, färger och positioner säkerställs i backend.
- **Effektivare prestanda:** Kvadraterna skapas och valideras i backend men cachas i frontend.

### 5. API-skydd och prestandaförbättringar
- **Rate limiting:** 30 API-anrop per 10 sekunder per IP.
- **Serilog:** Hanterar loggning och undantag i separata loggfiler.
- **Middleware:** Ansvarar för att hantera fel globalt.

---

## Refaktorering och förbättringar

### Deployment-förberedelser
- **.env-filer:** Används för att hantera miljövariabler.
- **Uppdaterad `.gitignore`:** Ignorerar produktionsfiler men inkluderar `development`-filer.
- **Dynamiska konfigurationsvärden:** Hårdkodade värden ersattes med variabler i `Program.cs`.

### Optimeringar
- **Kompakt kod i `SquareDto.cs`:** Lambda-baserad konstruktor för färre kodrader.
- **Minimalistisk `DataManager.cs`:** Undantag hanteras via middleware.
- **Rensad `SquareService.cs`:** Tog bort onödiga `try-catch` då middleware hanterar fel.
- **Effektivare `SquareController.cs`:** Tog bort onödig bekräftelseloggning.

---

## Slutsats

- **Minimal API + JSON-lagring** ger en **lättviktig och flexibel lösning**.
- **Frontend renderar kvadraterna, backend hanterar data** → bättre prestanda och UX.
- **Initialt hanteras deployment lokalt**, men deployment till Azure förbereds.
- **Rate limiting och loggning implementerat**, ingen JWT/API-nyckel behövs.

Projektet har nu en färdigställd, minimalistisk och skalbar backend redo för vidare utveckling och deployment.