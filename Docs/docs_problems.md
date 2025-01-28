## Problem och lösningar

1. **Problem:** Jag får en varning i Square.cs om att "Positional property 'Hiptobesquare.Square.Id/PositionX/PositionY' is never accessed (except in implicit Equals/ToString implementations)"
   - **Lösning:** Jag la till en metod som returnerar fälten bara för att slippa varningen. Det är inte nödvändigt att använda fälten i det här fallet.
   
2. **Problem:** Namngivning och struktur var inte konsekvent. Jag startade projekted med namngivning i Linux-stil, lowercase och underscore_, men eftersom detta primärt är ett C#-projekt så är det mer enhetligt med PascalCase och camelCase.
   - **Lösning:** Jag arkiverade det ursprungliga GitHub-repot och skapade en ny med PascalCase-namngivning. Jag ändrade även namngivningen i koden och i filstrukturen, samt la till så Rider använde top-level statements så vi kan hålla koll på namespaces och filer.
   
3. 