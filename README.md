## Välkommen till hiptobesquare!

### Det här är ett projekt som baseras på uppgiften som hittas här:  
https://github.com/Wizardworks-AB/programmeringsuppgift  
  
### Detta specifika projekt har skapats av https://github.com/mymh13  våren 2025.
Repository för detta projekt finns här: https://github.com/mymh13/HiptobesquareWiz

Vill försöka hålla detta så enkelt och koncist som möjligt, så, besök docs om det är dokumentation du söker!
Tack för att du läste, lycka till med projektet om du bestämmer dig för att använda det!  

---

## Mapp- och filstruktur

Hiptobesquare/  
├── Docs/  
│   ├── docs_backend.md             # Dokumentation för backend  
│   ├── docs_frontend.md            # Dokumentation för frontend  
│   └── docs_problems.md            # Dokumentation av problem under utvecklingsprocessen  
│  
├── Hiptobesquare/  
│   ├── Data/  
│   │   └── squares_1.json          # Lagring av JSON-data  
│   ├── Logs/                       # Loggfiler för API och anslutningar  
│   ├── Services/  
│   │   ├── DataManager.cs          # Hanterar JSON-filer  
│   │   ├── LoggingService.cs       # Hanterar loggning  
│   │   ├── RateLimitingService.cs  # Implementerar Rate Limiting  
│   │   ├── MiddlewareExtensions.cs # Logging och Exception Middleware  
│   │   └── SquareService.cs        # Logik för att hantera kvadrater  
│   │  
│   ├── Program.cs                  # Minimal API-hantering  
│   ├── Startup.cs                  # Registrerar API-endpoints  
│   ├── Square.cs                   # Modell för en kvadrat  
│   └── SquareDto.cs                # DTO för inkommande data  
│  
├── Hiptobesquare.Tests/  
│   ├── Services/  
│   │   ├── MSTestSettings.cs       # Inställningar för MSTest  
│   │   └── SquareServiceTests.cs   # Test av SquareService  
│   │  
│   └── Logs/                       # Loggfiler från tester   
│   
├── LICENSE                         # Projektlicens  
└── README.md                       # Dokumentation och projektinfo  