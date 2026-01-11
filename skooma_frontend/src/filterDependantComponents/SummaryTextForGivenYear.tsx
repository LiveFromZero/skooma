import { useEffect, useState, type JSX } from "react";
const API_URL = import.meta.env.VITE_API_URL_BACKEND;

function SummaryTextForGivenYear({
  selectedYear,
}: {
  selectedYear: string;
}): JSX.Element {
  const [allAveragePercent, setAllAveragePercent] = useState<number>(0);

  useEffect(() => {
    fetchDataFromBackend(selectedYear);
  }, [selectedYear]);

  async function fetchDataFromBackend(selectedYear: string): Promise<void> {
    const response = await fetch(
      `${API_URL}/rocket-starts?year=` + selectedYear
    );
    console.log("Response status:", response.status);
    const data = await response.json();
    console.log("Fetched data:", data);
    var fetchedData: number = data;
    setAllAveragePercent(fetchedData);
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
