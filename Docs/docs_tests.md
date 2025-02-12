# Hiptobesquare : Testning

## 1. Teststrategi och beslut

### Varför testa?
- Även om projektet är **litet och inte kommer att växa**, är det viktigt att verifiera kritiska funktioner.
- Jag valde att **inte köra fullständig TDD** men ville ändå ha tester för nyckelfunktionalitet.

### Vilka tester implementerades?
- **Unit-tester för SquareService** (affärslogik).
- `MockDataManager` - en mockad / simulerad version av `DataManager`.
- **Ingen integrationstestning** av API-endpoints (minimalism var prioriterat).

### Varför inte fler tester?
- **Minimal kod, maximal funktionalitet** – testar det som påverkar skalbarhet och datalagring (kritiska funktioner).
- **CI/CD skulle kunna inkludera tester**, men valde att fokusera på funktionalitet först.

---

## 2. Testspecifikation

### **Testade scenarier**
1. **Lägga till en kvadrat** (`AddSquare_ShouldAddSquareToDataManager`)
2. **Hämta alla kvadrater** (`GetAllSquares_ShouldReturnAllSquares`)
3. **Rensa alla kvadrater** (`ClearSquares_ShouldRemoveAllSquares`)
4. **Hantera JSON-filens maxstorlek** (`ShouldCreateNewJsonFile_WhenMaxFileSizeExceeded`)

### **MockDataManager – Simulerad datahantering**
- Undviker hantera filer i testerna.
- Hanterar JSON-data i minnet.
- Simulerar maxstorlek (10 MB) för att skapa nya JSON-filer vid behov.

---

## 3. Refaktorering efter backend-justeringar

Efter att vi **implementerade NullLogger** i `MockDataManager`, uppdaterades testklassen:

- **Tillägg:**
  ```csharp
  using Microsoft.Extensions.Logging.Abstractions;
  ```
- **Justering i MockDataManager:**
  ```csharp
  public MockDataManager() : base(NullLogger<DataManager>.Instance) {}
  ```

**Varför?**  
- Undviker behovet av en riktig `ILogger<DataManager>` i testerna.  
- Inget behov av att mocka loggern, då hade vi behövt hantera loggningen i testerna också.

---

## 4. Testresultat

![testresultat.png](img.png)

- **Test av kritiska moment**: JSON-filskapande vid 10MB och radering av kvadrater.
- **Alla tester gick igenom** efter senaste backend-uppdateringen.

---

## 5. Slutsats och vidare testning

- **Grundläggande tester för logik och JSON-hantering fungerar**.
- **Fil-input/output mockas i testerna** för att undvika beroenden på filsystemet.
- **Inga integrationstester ännu** – det kan läggas till om vi behöver API-validering.

---

## 6. Möjliga förbättringar

Om vi skulle vidareutveckla testningen, skulle vi kunna:
1. **Lägga till integrationstester** för API-endpoints.
2. **Mocka API-anrop i frontend** för att verifiera API-kommunikation.
3. **Köra tester automatiskt i en CI/CD-pipeline**.

---

### Slutsats
Testerna täcker kärnfunktionaliteten, och projektet har nu en fungerande testsvit av kritiska moment.