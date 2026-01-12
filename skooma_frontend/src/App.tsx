import type { JSX } from "react";
import Filter from "./Filter";
const API_URL = import.meta.env.VITE_API_URL_BACKEND;

const divStyle: React.CSSProperties = {
  padding: 30,
  display: "flex",
  flexDirection: "column",
  alignItems: "center",
};

async function updateDatabase(): Promise<void> {
  const response = await fetch(`${API_URL}/rocket-starts/updateBackend`, {});
  console.log("Response status:", response.status);
  const data = await response.json();
  console.log("Fetched data:", data);
  alert("Datenbank wurde aktualisiert!");
}

function App(): JSX.Element {
  return (
    <div style={divStyle}>
      <h1>Skooma</h1>
      <button onClick={() => updateDatabase()}>Update Database</button>
      <Filter />
    </div>
  );
}

export default App;
