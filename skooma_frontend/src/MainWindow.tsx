import type { JSX } from "react";
import Filter from "./Filter";
import Graphic from "./Graphic";
import SummaryText from "./SummaryText";

function MainWindow(): JSX.Element {
  return (
    <div>
      <Filter />
      <Graphic />
      <SummaryText />
    </div>
  );
}

export default MainWindow;
