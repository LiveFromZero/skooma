import { useState, type JSX } from "react";

function SummaryText(): JSX.Element {
  const [chosenYear, setChosenYear] = useState<string>("2024");
  const [allAveragePercent, setAllAveragePercent] = useState<number>(75.6);

  return (
    <div>
      <p>
        Im Jahr {chosenYear} waren durchschnittlich {allAveragePercent}% aller
        Rakenestarts erfolgreich.
      </p>
    </div>
  );
}

export default SummaryText;
