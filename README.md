# Skooma

Datenbankenprojekt.

## Überblick

**Technologien:**

- Frontend: React mit Vite
- Backend: C# (Visual Studio)
- Datenbank: SQLite
- ORM: Entity Framework

---

## Frontend

### Setup

1. Den Ordner `frontend` mit **Visual Studio Code** öffnen.
2. Abhängigkeiten installieren:
   - npm install
3. Entwicklungsserver starten:
   - npm run dev

### Builds

4. Build für die Backend-Entwicklerumgebung:
   - npm run build-backend-dev
5. Build für die Produktionsumgebung:
   - npm run build-backend-prod

---

## Backend

1. Die Solution im Ordner `backend` öffnen  
   (Doppelklick auf die `.sln` oder direkt über Visual Studio).

2. Beim Starten der **Dev-Umgebung**:

   - Der Backend-Server **und**
   - der Frontend-Service  
     werden automatisch gestartet.

3. **Swagger** zum Testen der Backend-APIs verwenden:
   - Während der Backend-Service läuft, im Browser öffnen:
     - http://localhost:5212/swagger/index.html

---

## Frontend und Backend verbinden / Build

### Entwicklung (Dev-Umgebung)

1. Im Ordner `frontend`:

   - npm run build-backend-dev

2. Die Dateien aus dem Ordner `dist` **kopieren**  
   (nicht den Ordner selbst) und in den Backend-Ordner `wwwroot` einfügen.  
   Bestehende Dateien überschreiben  
   (idealerweise vorher alte Dateien löschen).

3. In Visual Studio in der `Program.cs` die Frontend-Policy setzen auf:

   - http://localhost:5212

4. Projekt in Visual Studio normal im **Dev-Modus** starten.  
   Das neu gebaute Frontend startet automatisch mit und kommuniziert mit dem Dev-Backend.

---

### Produktion (Prod)

1. Im Ordner `frontend`:

   - npm run build-backend-prod

2. Die Dateien aus dem Ordner `dist` **kopieren**  
   (nicht den Ordner selbst) und in den Backend-Ordner `wwwroot` einfügen.  
   Bestehende Dateien überschreiben  
   (am besten vorher löschen).

3. In Visual Studio in der `Program.cs` die Frontend-Policy ändern auf:

   - http://localhost:5000

   **Achtung:**  
   Die Dev-Umgebung kann jetzt nicht mehr über APIs mit dem Frontend kommunizieren.

4. Veröffentlichung in einen Ordner mit folgenden Einstellungen:

   - Konfiguration: Release
   - Zielframework: net10
   - Bereitstellungsmodus: Eigenständig (self-contained)
   - Ziellaufzeit: win-x64
   - Dateiveröffentlichungsoptionen:
     - Einzelne Datei erstellen → Haken muss gesetzt sein

5. In den Release-Ordner wechseln und ausführen:

   - skooma-backend.exe

   Dadurch starten **Backend und Frontend** gemeinsam.

6. Um wieder in die dev-Umgebung zu wechseln und das frontend wieder mit dem backend kommunizieren zu lassen,wieder den Schritten aus Build für die dev-Umgebung folgen

### Vite mit dev-backend kommunizieren lassen

1. In Visual Studio in der `Program.cs` die Frontend-Policy ändern auf:

   - http://localhost:5173

2. Den Backend-client in der dev-Umgebung starten (optional alles aus wwwroot vorher löschen) und den startenden frontend-server ignorieren oder wegklicken

3. Zum Frontend wechseln und dort `npm run dev` ausführen
