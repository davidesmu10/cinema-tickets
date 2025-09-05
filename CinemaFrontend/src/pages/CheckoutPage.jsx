import { useLocation, useNavigate } from "react-router-dom";
import { reserveSeats } from "../api/api";

export default function CheckoutPage() {
  const { state } = useLocation();
  const navigate = useNavigate();

  const handleReserve = () => {
    const payload = {
      showtimeId: state.showtimeId,
      seatIds: state.seats.map(s => s.id),
      customerName: "Cliente Demo",
      customerEmail: "demo@email.com"
    };

    reserveSeats(payload).then(res => {
      navigate(`/confirmation/${res.data.reservationId}`);
    });
  };

  return (
    <div className="max-w-md mx-auto bg-white shadow p-6 rounded">
      <h2 className="text-xl font-bold">Confirmar compra</h2>
      <p>Asientos: {state.seats.map(s => s.label).join(", ")}</p>
      <button
        onClick={handleReserve}
        className="mt-4 bg-blue-600 text-white px-4 py-2 rounded"
      >
        Pagar & Reservar
      </button>
    </div>
  );
}
