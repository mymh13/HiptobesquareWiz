# Hiptobesquare: Problem och lösningar

### 1. Varningsmeddelande i `Square.cs`
**Problem:**  
Varning i `Square.cs`: *"Positional property 'Hiptobesquare.Square.Id/PositionX/PositionY' is never accessed (except in implicit Equals/ToString implementations)"*.

**Lösning:**  
La till en metod som returnerar fälten för att eliminera varningen, även om den inte påverkar funktionaliteten.

---

### 2. Namngivning och struktur
**Problem:**  
Inkonsekvent namngivning – frontend använde `lower_case`, men projektet startades i backend, i C#, där PascalCase/camelCase är standard.

**Lösning:**
- Arkiverade det ursprungliga GitHub-repot och skapade en ny version med **konsekvent PascalCase**.
- Justerade kod och filstruktur.
- Anpassade Rider för att hantera **top-level statements** korrekt.

---

### 3. Problem med TailwindCSS-installation
**Problem:**  
Shell/Bash misslyckades med att köra Tailwind. Varken ChatGPT eller Tailwinds officiella dokumentation hjälpte.

**Lösning:**  
En installationsvideo visade att vissa steg saknades:  
[YouTube: Install Tailwind i Vite](https://youtu.be/sHnG8tIYMB4?si=9fp8mlDs71_Y8hDa)

Efter att ha följt dessa steg funderade det som det skulle, även om filstrukturen inte blev optimal.

---

### 4. Backend tar inte emot API-anrop
**Problem:**  
GET, POST och DELETE-anrop fungerade inte från frontend.

**Lösning:**
- Åtkomstdirektivet i `DataManager`-konstruktorn var **protected** istället för **public**.
- Efter att ha justerat det fungerade API-kommunikationen.

---

### 5. JSON-fil skapas inte vid POST-anrop
**Problem:**  
Backend accepterade POST-anrop men skapade ingen `squares_1.json`.

**Lösning:**
- **ASP.NET Core kan inte binda records till en POST-request**. `SquareDto` var en **record**, men behövde vara en **klass**.
- När `SquareDto` konverterades till en **klass** fungerade det.
- Efter den ändringen behövde även **DataManager** refaktoreras för att injiceras korrekt.

---

### 6. TailwindCSS overridar designen
**Problem:**  
Vissa CSS-regler fungerade inte som förväntat. ChatGPT var inte behjälplig heller, den föreslog åtgärder som var **föråldrade eller irrelevanta** p.g.a. Tailwind 4.

**Lösning:**
1. Rensade `index.css` och `App.css` helt och hållet (kommenterade ut kod och la till sektion för sektion).
2. Identifierade att **Tailwinds preflight** overridade Reacts styling.
3. Löste det genom att **använda Tailwind-klasser direkt i JSX** och låta `index.css` hantera globala inställningar.

--- 

### 7. Kvadrater renderas fel vid dynamisk inladdning
**Problem:**  
Vid första sidladdning såg kvadraterna rätt ut, men när de lades till genom knapp-tryck hamnade de fel.

**Lösning:**
- Felet berodde på att **Tailwinds `flex-wrap` påverkade layouten på ett oförutsägbart sätt**.
- **Lösning:** Tog bort `flex-wrap` och justerade kvadrat-positioneringen i **React state** istället.

---

### 8. .NET skriver JSON-filer i fel katalog
**Problem:**  
Efter övergången till `.env`-variabler istället för hårdkodade värden, började `.NET` skapa JSON-filer i `bin/Debug/net9.0/` istället för `/Data/`.

**Lösning:**
- `.NET` hanterar relativa paths annorlunda vid runtime.
- Lösningen var att justera `BaseDirectory` i `DataManager.cs`:

```csharp
private static readonly string BaseDirectory = Path.GetFullPath(
    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Data"));
```

---

### Summering och reflektion

- Att jag skulle få problem med React och Tailwind vad väntat, har aldrig jobbat med dem förut, men om det var logiskt att förstå att Tailwind skriver över CSS-regler så var det inte alls lika uppenbart att det skulle påverka layouten. Att det var en så enkel lösning som att ta bort `flex-wrap` var inte heller något jag hade förväntat mig. Det här var det enskilt största problemet under hela projektet.

- Alla andra problem var relativt små och/eller sådant jag sett förut, men lärde mig en ny sak: Records i C# kan inte bindas till POST-anrop. Jag skrev Square och SquareDto som klasser först men refaktorerade om de till records för att följa minimalist-principen. Att det skulle ställa till problem och kräva refaktorering i DataManager var oväntat.