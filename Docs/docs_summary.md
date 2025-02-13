# Hiptobesquare : Summering

## Summering av projektet:

- **Lärorik och rolig process:** Projektet har varit otroligt givande, både i lärande och problemlösning. Att arbeta med React utan tidigare erfarenhet var en utmaning, men jag lyckades hitta lösningar och anpassa mig snabbt.
- **Tydlig strategi och genomförande:** Jag har alltid haft en plan för projektet, även om lösningarna ibland har behövt utforskas under vägen. Genom att följa den övergripande arkitekturen (och följa mina två ledstjärnor: minimalism och "skala oändligt") kunde jag ta genomtänkta beslut.
- **Nöjd med resultatet:** Projektet är komplett och väldokumenterat. Jag har lärt mig mycket och haft kul. Designen hade kunnat vara mer visuellt tilltalande, men prioriteringen låg på funktionalitet och arkitektur.

---

## Mjukvara, hjälpmedel och LLM-verktyg:

### Verktyg och resurser
1. **Utvecklingsmiljö:** Kodade .NET och markdown i JetBrains Rider.
2. **Dokumentation:** Huvudsakligen Microsoft Learn, React och Tailwinds officiella dokumentation. En YT-video.
3. **GitHub Copilot:** Användes som stöd för att generera kodsnuttar och dokumentation.
4. **Externa LLM-verktyg:** ChatGPT för att bolla idéer och få vägledning, särskilt inom React där jag saknade erfarenhet.

### Balans mellan egna kunskaper och LLM-verktyg
- **Arkitektur och planering:** 95%+ egna lösningar.
- **Backend:** 90% egna lösningar, 5% dokumentation, 5% LLM-verktyg.
- **Testning:** 70% egna lösningar, 30% LLM-verktyg – min arkitektur, CoPilot fick fylla ut metoderna.
- **Frontend:** 50% egna lösningar, 45% LLM-verktyg, 5% dokumentation. Installation och initial kodstruktur krävde mer stöd, men jag tog över mer under projektets gång. När jag hade syntaxen så kunde jag justera innehållet.

### Hur LLM-verktyg användes
- **Koden skrevs och justerades manuellt, med LLM som ett stöd för att effektivisera arbetsflödet.**
- **Dokumentationen gjorde jag on-the-fly medan jag arbetade.** Ex: strukturella val under resans gång, eller problem som jag fastnade i, då noterade jag problematiken/valen och lösningen innan jag fortsatte. Bad sedan ChatGPT om hjälp att komprimera underlaget. Därefter läste jag igenom allting och la sista handen innan jag lade till det i dokumentationen.

---

## Slutsats

Projektet har gett mig insikter i React, Tailwind, API-integration och bra träning på problemlösning/design. Genom en kombination av självständig problemlösning, officiell dokumentation och LLM-verktyg har jag lyckats bygga en fungerande och optimerad applikation.

#### Det har varit oerhört givande på många sätt
- Jag har haft kul, först och främst!
- Känner en förståelse men också att mina egenskaper passar bra till just detta, felsökningen har fungerat och jag har kunnat hantera helt nya tekniker.
- Har fått nya kunskaper om tekniska lösningar, och en förståelse för när det kan vara fördelaktigt att använda sig av verktyg som React och Tailwind.

---

## Uppdatering några dagar senare

Jag provade att hosta backend på Azure App Services (Web App) och använde Azure CLI för deployment:
- dotnet publish för att bygga .NET
- az webapp deploy för att deploya till Azure
- frontend ligger separat på en Apache-baserad service som inte tar .NET
- Azure Web App är Free tier (F1) och fungerar bra för detta projekt