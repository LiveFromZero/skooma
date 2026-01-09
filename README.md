# Skooma

Dieses Projekt besteht aus einem **Frontend** und einem **Backend**.

---

## Projekt einrichten

### Frontend

1. Den `frontend`-Ordner mit Visual Studio Code öffnen.

2. Abhängigkeiten installieren:

   ```bash
   npm install
   ```

3. Entwicklungsserver starten:

   ```bash
   npm run dev
   ```

4. Build erstellen:

   ```bash
   npm run build
   ```

---

### Backend

1. Die Solution im `backend`-Ordner öffnen (Doppelklick auf `.sln` oder über Visual Studio).
2. Starten: Beim Start öffnet sich der Backend-Server **und** der Frontend-Service.
3. Swagger nutzen, um Backend-APIs einfach zu testen:

   * URL im Browser öffnen, während der Backend-Service läuft:
     [https://localhost:7101/swagger/index.html](https://localhost:7101/swagger/index.html)

---

## Frontend und Backend verbinden / Build

1. Im `frontend`-Ordner:

   ```bash
   npm run build
   ```

2. Die Dateien aus dem `dist`-Ordner **kopieren** (nicht den Ordner selbst) und im Backend-Ordner `wwwroot` einfügen, bestehende Dateien überschreiben.

3. In Visual Studio das Backend ausführen und builden.
