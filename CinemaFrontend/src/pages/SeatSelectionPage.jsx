import { useEffect, useState } from "react";
import { useSearchParams, useNavigate } from "react-router-dom";
import { getSeats } from "../api/api";
import Seat from "../components/Seat";
import Loader from "../components/Loader";

export default function SeatSelectionPage() {
  const [searchParams] = useSearchParams();
  const navigate = useNavigate();
  const showtimeId = searchParams.get("showtimeId");

  const [seats, setSeats] = useState([]);
  const [selected, setSelected] = useState([]);

  useEffect(() => {
    getSeats(showtimeId).then(res => setSeats(res.data));
  }, [showtimeId]);

  const toggleSeat = (seat) => {
    if (selected.find(s => s.id === seat.id)) {
      setSelected(selected.filter(s => s.id !== seat.id));
    } else {
      setSelected([...selected, seat]);
    }
  };

  const goCheckout = () => {
    navigate("/checkout", { state: { showtimeId, seats: selected } });
  };

  if (!seats.length) return <Loader />;

  return (
    <div className="max-w-xl mx-auto bg-white shadow p-6 rounded">
      <h2 className="text-xl font-bold mb-4">Selecciona tus asientos</h2>
      <div className="grid grid-cols-8 gap-2">
        {seats.map(seat => (
          <Seat key={seat.id} seat={seat} onSelect={toggleSeat} />
        ))}
      </div>
      <button
        disabled={selected.length === 0}
        onClick={goCheckout}
        className="mt-6 bg-green-600 text-white px-4 py-2 rounded disabled:bg-gray-400"
      >
        Continuar ({selected.length})
      </button>
    </div>
  );
}
