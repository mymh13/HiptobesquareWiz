# Hiptobesquare : Frontend

## Initiala konceptuella funderingar:

### Ramverk och teknologier:
- **React + Vite:** Snabb utveckling, komponentbaserad arkitektur.
- **Tailwind CSS:** Minimal styling via utility-klasser.
- **State-hantering:** React Hooks används för att hantera tillstånd.
- **API-hantering:** Fetch används för att kommunicera med backend.

### Design och funktionalitet:
- **Dynamisk rendering:** Kvadrater genereras i frontend baserat på API-data.
- **Cache och optimering:** Kvadrater lagras i frontend för att minimera API-anrop.
- **Responsiv layout:** UI anpassar sig efter antalet kvadrater.

---

## API och frontend-integration

### Dataflöde:
1. Vid sidladdning hämtas sparade kvadrater från backend.
2. När en ny kvadrat läggs till genereras dess position och färg i frontend.
3. Kvadraten skickas till backend för lagring.
4. Vid siduppdatering hämtas hela kvadrat-layouten från API:et.
5. Vid rensning skickas ett DELETE-anrop till backend.

### Optimeringar:
- **Positioneringslogik i frontend:** Undviker onödiga beräkningar i backend och blir mer skalbart.
- **API-anrop endast vid förändringar:** Förbättrar prestanda.
- **Styling via Tailwind direkt i JSX:** Minimalt behov av CSS-filer.

---

## Då börjar vi bygga frontend! Men först:

### Ett kort intro, för jag har ju aldrig arbetat i React, Vite eller TailwindCSS:

Detta beskriver jag mer i docs_problems.md, men jag fick problem vid installation av Tailwind4. Jag har aldrig arbetat med dessa verktyg tidigare så jag installerade React + Vite först, sedan TailwindCSS. På grund av förändringar i Tailwind4 så stämde varken officiella dokumentationen eller info jag fick från LLMer. Jag löste installationen men då hamnade Tailwind i en undermapp och React-filerna duplicerades, så jag tömde React-mappen och jobbade direkt i Tailwind-mappen. Detta var inte en optimal struktur, men jag prioriterade funktion över design här.  

Har fått förlita mig tungt på https://tailwindcss.com/docs/installation/using-vite , övrig dokumentation samt LLMer för att kunna jobba framförallt med React. Tailwind var lättare att förstå med min tidigare kunskap från CSS.

---

### Byggprocess och beslut under resans gång

### 1. Initial setup:
- **Vite-projekt skapades** med `npm create vite@latest`.
- **Tailwind CSS installerades** och konfigurerades enligt dokumentation.
- **Backend testades separat** för att säkerställa API-kommunikation.

### 2. Strukturering av kod:
- **CSS-filer reducerades** till ett minimum, all styling i Tailwind.
- **State-hantering implementerades** för att hantera kvadraternas tillstånd.
- **API-anrop optimerades** för att minimera onödig trafik.

### 3. Positioneringslogik:
- Kvadrater placeras i en **spiralformation** där varje ny kvadrat adderas på en ny rad eller kolumn.
- **Beräkning sker i frontend** för maximal flexibilitet och bättre prestanda.

---

## Förklaring av kodstrukturen i `App.jsx`

### **State och API-hantering**
- **`useState`** - Hanterar kvadraternas tillstånd i frontend.
- **`useEffect`** - Används för att hämta sparade kvadrater vid sidladdning.

### **Funktioner**
#### `generateRandomColour(prevColour)`
Genererar en slumpmässig färg som **inte är samma som den senaste kvadraten**.
- Använder hexadecimala färger (`#RRGGBB`).
- Säkerställer att samma färg inte upprepas två gånger i rad.

#### `calculateNextPosition(squares)`
Beräknar var nästa kvadrat ska placeras baserat på tidigare placeringar:
1. **Räknar antalet kvadrater** för att bestämma nuvarande grid-nivå.
2. **Fyller först höger sida vertikalt** innan den börjar fylla botten horisontellt.
3. **Returnerar x/y-koordinater** för den nya kvadraten.

#### `addSquare()`
- Genererar en ny kvadrat med unik **ID, färg och position**.
- Skickar den till backend via ett **POST-anrop**.
- Uppdaterar frontendens tillstånd genom att **lägga till den nya kvadraten i React State**.

#### `clearSquares()`
- Skickar ett **DELETE-anrop** till API:et för att rensa alla kvadrater.
- Återställer frontendens tillstånd.

---

## Refaktorering och optimeringar

### Deployment-förberedelser:
- **.env-filer implementerades** för att hantera API-URL.
- **API-anrop justerades** för att fungera både lokalt och i produktion.
- **Styling renodlades** genom att flytta Tailwind-klasser direkt till JSX (och App.css togs bort då vi hade så få app-specifika stilar, de sköttes av tailwind och de globala körs i index.css).

### Optimeringar:
- **Knapparnas stil förenklades** genom en gemensam `button`-klass.
- **Kvadrat-rendering optimerades** genom att använda `absolute` positionering i CSS.
- **Kod för API-hantering strömlinjeformades**.

---

## Slutsats

- **React + Vite + Tailwind som verktyg**.
- **Kvadrater renderas och positioneras i frontend**, vilket **minimerar API-belastning**.
- **Dynamisk layout och caching** förbättrar prestanda.
- **Backend och frontend kommunicerar sömlöst** via API-anrop.

Projektet har nu en frontend som renderar kvadrater dynamiskt, integrerar med backend och är optimerad för vidare utveckling och deployment.