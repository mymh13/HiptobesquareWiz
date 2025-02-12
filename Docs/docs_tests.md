# Hiptobesquare : Testning

## Testers och linters vara eller icke vara..

1. Å ena sidan är tester rätt väg att gå i en skalbar applikation. Å andra sidan är detta program väldigt litet och kommer inte växa. Men, jag tycker tester av kritiska komponenter är viktiga och vill ändå visa på att jag tycker det är en del av strukturen så - det blev ingen TDD, men jag övervägde Unit-tester för logiken (SquareService, DataManager) samt Integrationstester för API-endpoints. Det hade gått utmärkt att bygga ett interface för att inte behöva mocka DataManager, men här valde jag minimalist-approachen genom att ärva och använda DataManager direkt.

- Valde till slut att kompromissa med grundläggande tester för SquareService. Minimalt med kod, förbättrar skalbarhet och visar på att jag värderar/hanterar tekniken. Väljer att hoppa över integrationstesterna. Eftersom jag kör Rider och kodanalys är inbyggd så kör jag ingen external linter.

2. ![testresultat.png](img.png)

- Ville bara testa kritiska moment, som att vi kan hantera över 10mb av JSON-fil.. Testerna går igenom.