import type { JSX } from "react";
import "./App.css";
import MainWindow from "./MainWindow";

function App(): JSX.Element {
  return (
    <div>
      <h1>Skooma</h1>
      <MainWindow />
    </div>
  );
}

export default App;
