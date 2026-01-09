import { useEffect, useState, type JSX } from "react";
import { dummyAllAveragePercentage } from "../mock";

function SummaryTextForGivenYear({
  selectedYear,
}: {
  selectedYear: string;
}): JSX.Element {
  const [allAveragePercent, setAllAveragePercent] = useState<number>();

  useEffect(() => {
    fetchDataFromBackend(selectedYear);
  }, []);

  function fetchDataFromBackend(selectedYear: string): void {
    if (selectedYear) {
      // fetch data from backend based on selected year
      var fetchedData: number = dummyAllAveragePercentage; // TODO Replace with fetch data
      setAllAveragePercent(fetchedData);
    }
  }

  return (
    <div>
      <p>
        Im Jahr {selectedYear} waren durchschnittlich {allAveragePercent}% aller
        Rakenestarts erfolgreich.
      </p>
    </div>
  );
}

export default SummaryTextForGivenYear;
