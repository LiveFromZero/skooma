import type { JSX } from "react";
import MainWindow from "./MainWindow";

function App(): JSX.Element {
  return (
    <div
      style={{
        padding: 30,
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
      }}
    >
      <h1>Skooma</h1>
      <MainWindow />
    </div>
  );
}

export default App;
