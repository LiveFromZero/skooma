import type { JSX } from "react";
import MainWindow from "./MainWindow";

const divStyle: React.CSSProperties = {
  padding: 30,
  display: "flex",
  flexDirection: "column",
  alignItems: "center",
};

function App(): JSX.Element {
  return (
    <div style={divStyle}>
      <h1>Skooma</h1>
      <MainWindow />
    </div>
  );
}

export default App;
