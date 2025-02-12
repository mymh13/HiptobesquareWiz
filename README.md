## Välkommen till hiptobesquare!

### Det här är ett projekt som baseras på uppgiften som hittas här:  
https://github.com/Wizardworks-AB/programmeringsuppgift  
  
### Detta specifika projekt har skapats av https://github.com/mymh13  våren 2025.
Repository för detta projekt finns här: https://github.com/mymh13/HiptobesquareWiz

Vill försöka hålla detta så enkelt och koncist som möjligt, så, **besök docs om det är dokumentation du söker**!  
Lånar du någonting av detta projekt, så lämna gärna en kommentar i koden eller i dokumentationen med en referens hit, tack!  

---

## Mapp- och filstruktur

Hiptobesquare/  
├── Docs/  
│   ├── docs_backend.md             # Dokumentation för backend  
│   ├── docs_frontend.md            # Dokumentation för frontend  
│   ├── docs_problems.md            # Dokumentation av problem under utvecklingsprocessen  
│   └── docs_summary.md             # Sammanfattning av projektet  
│  
├── Hiptobesquare/  
│   ├── Controllers/  
│   │   └── SquareController.cs     # API-Controller för kvadrater  
│   │  
│   ├── Data/  
│   │   ├── squares_1.json          # JSON-fil för kvadrater  
│   │   └── index.json              # Index-fil för JSON-lagring  
│   │    
│   ├── Logs/                       # Loggfiler för API och anslutningar  
│   │  
│   ├── Services/  
│   │   ├── DataManager.cs          # Hanterar JSON-filer  
│   │   ├── LoggingService.cs       # Hanterar loggning  
│   │   ├── RateLimitingService.cs  # Implementerar Rate Limiting  
│   │   ├── MiddlewareExtensions.cs # Logging och Exception Middleware  
│   │   └── SquareService.cs        # Logik för att hantera kvadrater  
│   │  
│   ├── Program.cs                  # Minimal API-hantering   
│   ├── Square.cs                   # Modell för en kvadrat  
│   ├── SquareDto.cs                # DTO för inkommande data  
│   ├── .env.development            # Backend-miljövariabler för lokal utveckling  
│   └── .env.production             # Backend-miljövariabler för produktion  
│  
├── Hiptobesquare.Tests/  
│   ├── Services/  
│   │   ├── MSTestSettings.cs       # Inställningar för MSTest  
│   │   └── SquareServiceTests.cs   # Test av SquareService  
│   │  
│   └── Logs/                       # Loggfiler  
│   
├── hiptobesquare-frontend/  
│   ├── tailwindcss4/  
│   │   ├── src/  
│   │   │   ├── App.jsx             # React huvudkomponent  
│   │   │   ├── index.css           # Global CSS   
│   │   │   └── main.jsx            # React entry-point  
│   │   ├── .env.development        # Frontend-miljövariabler för lokal utveckling  
│   │   └── .env.production         # Frontend-miljövariabler för produktion  
│  
├── LICENSE                         # Projektlicens  
└── README.md                       # Dokumentation och projektinfo  

---