# Hiptobesquare : Frontend

## Initiala konceptuella funderingar:

### Ramverk/verktyg:

- För den här skalan av projekt så gillar jag HTMX, vi har inga behov av massiva karuseller eller komplexa SPA:er. HTMX är enkelt att använda och ger oss möjlighet att skala till oändlighet.
- Men å andra sidan så vill vi jobba med reaktiva element som renderas och cachas i frontend, så jag tänker att vi kan använda React för att hantera detta. Väljer Vite för att snabba upp utvecklingen.
- Tailwind för CSS. Jag brukar jobba manuellt i CSS för småskaliga projekt men tänker att det kan vara kul att prova på en ny teknik för denna uppgift! Det är lagom i ett mindre projekt som detta, utan överdriven komplexitet (CSS-mässigt).
- IDE? Jag har byggt backend i Jetbrains Rider så tänker att jag fortsätter där, även om jag har Visual Studio Code installerat. Rider har bra stöd för React och jag är bekant med verktyget.

### Initialt fokus:

- Backend-frontend-kommunikation.
- Grundläggande grafisk struktur och API-anrop.
- Cacha och rendera kvadrater från API:et.
- CSS och positionering av kvadrater.

## Teknisk specifikation:

- Frontend: React (med Vite)
- Styling/CSS: Tailwind
- State-hantering: React Hooks? React State?
- API-hantering: Axios? Fetch?
- Rendering: Kvadrater cachas i frontend och återanvänds för att minimera API-anrop.

---

## Summering av tankar kring ovanstående, och beslut:

#### Jag tänker att vi har vissa strukturer att följa:
- Använd React för frontend och Tailwind för CSS/styling.
- Fokus på att få frontend och backend att prata ihop, och att få en grundläggande struktur på plats.
- Kvadraterna måste byggas utifrån en punkt i mitten av sidan och sprida sig utåt.
- Jag är obekant med dessa verktyg så vissa beslut kommer oundvikligen att tas/ändras under resans gång, då är det ännu viktigare att jobba efter en grundstruktur och modulärt!

---

## Disclaimer och not om mjukvara, hjälpmedel/LLM-verktyg:

- Se docs_backend.md för utveckling av denna punkt.
- Vill poängtera två saker: jag har scriptat lite i JS och har skrivit CSS manuellt i småskaliga hobbyprojekt, så att jobba med React och Tailwind är helt nytt för mig. Jag kommer att behöva förlita mig ganska tungt på officell dokumentation och LLMer, mitt fokus kommer bli strukturen och att försöka att knyta ihop front- och backend.

---

## Då börjar vi bygga frontend! Men först:

### Ett kort intro, för jag har ju aldrig arbetat i React, Vite eller TailwindCSS:

1. Node.js är redan förinstallerat, installerar en versionshanterare (fnm) för att hantera och uppdatera Node.
2. Kör npm create vite@latest i rotmappen för att skapa ett nytt Vite-projekt. Uppdaterar till senaste versionen.
3. Installerar och konfigurerar TailwindCSS enligt deras dokumentation på en gång.
4. Nu har vi en utvecklingsmiljö för React. Kör npm run dev för att starta utvecklingsservern.

### Då kan vi köra vidare. Val som görs under resans gång:

1. Jag installerar React innan jag testat endpointsen. Gör om och gör rätt!

2. Nu testar jag att endpointsen fungerar som de skall, så vi kan bygga frontend mot dessa.  
- Jag har kört två konsoller öppna, den ena simulerar backend med dotnet run och den andra skickar in kommandon via localhost:5000/squares. Hade viss problematik, den finns dokumenterad i docs_problems.md.

3. Tailwind eller inte: Jag gav mig på att försöka få till installationen med hjälp av https://tailwindcss.com/docs/installation/using-vite så nu är det installerat. Undrade lite över vad Tailwind egentligen ger, jag har alltid jobbat i mindre projekt och CSS har aldrig känts ohanterbart. Läste på om syntaxen och att Tailwind hjälper till att hålla CSS städat och strukturerat så kan förstå vinsten, i synnerhet i större projekt, men då är det en vinst i sig att lära sig det. Även om det är överkurs i detta projekt så kör vi på det!

4. Väljer att rensa index.css och App.css för att hålla dem till ett minimum. Jag vill ha en så ren struktur som möjligt. Eftersom vi har så få element i denna app kan vi lägga global design i App.css och styla element direkt i App.jsx.

5. Design choice: Jag skapar containers för att presentera sidan istället för en mono-färg-bakgrund, det ställer till det när vi skall kunna skala kvadrater i oändlighet. Men jag vill kopia min hemsidas design för att göra detta mer personligt. Löser detta genom att sätta overflow-x: auto och white-space:nowrap för att låta kvadraterna expandera bortom containern och att inte wrappa dem.

6. Nu när jag har back- och frontend som pratar med varandra och kan addera kvadrater på vår sida, då är det dags att tänka igenom en lösning för hur kvadraterna får sin position. Initialt skapade jag X och Y-värden i kvadratens klass i backend, men det gör det mer krävande att beräkna nu, och framför allt: vi skulle kunna skala oändligt, om vi kör på värdena så sätts det en begränsning i storleken på variabeln..
- Lösningen jag tar till är att jag kör en positioneringslogik som beräknas i React, då kan jag även ta bort X- och Y-värdena från kvadraterna (behåller ID för det gör dem unika för React).