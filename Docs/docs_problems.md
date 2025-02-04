## Problem och lösningar

1. **Problem:** Jag får en varning i Square.cs om att "Positional property 'Hiptobesquare.Square.Id/PositionX/PositionY' is never accessed (except in implicit Equals/ToString implementations)"
   - **Lösning:** Jag la till en metod som returnerar fälten bara för att slippa varningen. Det är inte nödvändigt att använda fälten i det här fallet.
   
2. **Problem:** Namngivning och struktur var inte konsekvent. Jag startade projekted med namngivning i Linux-stil, lowercase och underscore_, men eftersom detta primärt är ett C#-projekt så är det mer enhetligt med PascalCase och camelCase.
   - **Lösning:** Jag arkiverade det ursprungliga GitHub-repot och skapade en ny med PascalCase-namngivning. Jag ändrade även namngivningen i koden och i filstrukturen, samt la till så Rider använde top-level statements så vi kan hålla koll på namespaces och filer.
   
3. Fick problem med TailwindCSS, bash och shell lyckas inte exekvera Tailwind. Vill poängtera att jag aldrig installerat detta tidigare och varken ChatGPT eller den officiella dokumentationen kunde hjälpa mig.
   - **Lösning:** En installationsvideo på Youtube: https://youtu.be/sHnG8tIYMB4?si=9fp8mlDs71_Y8hDa) visar ett antal steg som saknas i den officiella dokumentaitonen.

4. **Problem:** Jag får problem när jag testar endpoints: Backend kör igång som det skall men tar inte emot anrop (GET, POST, DELETE) från frontend.
   - **Lösning:** Det var åtkomstdirektivet i konstruktorn i DataManager som jag hade satt till protected, när jag justerade den till public så fungerade det.

5. **Problem:** Jag får problem med att skapa en ny JSON-fil.
   - **Lösning:** För att skydda square hade jag byggt SquareDto som en record, men ASP.NET Core kan inte binda en record till en POST-request. Jag bytte till en klass och då fungerade det. Då blev det följdproblem med att DataManager inte injicerades rätt, så det blev lite refaktorering där också.

6. **Problem:** Jag noterar att mina CSS-inställningar blir overridade och färg och form blir felaktiga. ChatGPT är inte behjälplig i detta, den föreslår konsekvent att köra a) ominstallation (med felaktiga kommandon, se punkt #3 ovan), b) att lägga till filer som "saknas" (de är en del av en gammal filstruktur som inte förekommer längre i Tailwind 4), c) lägga till importer (components, samt andra är mergad in i utilities) som är föråldrade (ersatt med preflight)/flyttade i Tailwind 4, eller d) att felsöka genom att lägga till mängder med kod för att se vad som overridar vad. 
    - **Lösning:** Jag rensar CSS till ett minimum och gör en enkel struktur utan design och bygger därifrån. Det visar sig att index.css overridar App.css, men eftersom Tailwind kör denna struktur och importerar i index-filen så tänker jag att det finns två lösningar:
    - 1. Jag kan hacka grundstrukturen i index.css på klassiskt vis, det är en väldigt enkel design.
    - 2. Rensa index.css till ett minimum och bygga på det i App.css. Jag väljer det senare alternativet. 